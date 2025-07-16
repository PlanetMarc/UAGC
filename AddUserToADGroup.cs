using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using System.DirectoryServices.Protocols;
using System.Net;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.

namespace LambdaADGroupMember
{
    public class Function
    {
        public async Task<object> FunctionHandler(GroupAddEvent request, ILambdaContext context)
        {
            try
            {
                // Validate input
                if (string.IsNullOrEmpty(request.LdapServer) ||
                    string.IsNullOrEmpty(request.LdapUsername) ||
                    string.IsNullOrEmpty(request.LdapPassword) ||
                    string.IsNullOrEmpty(request.UserDistinguishedName) ||
                    string.IsNullOrEmpty(request.GroupDistinguishedName))
                {
                    return new { statusCode = 400, error = "Missing required LDAP connection or DN parameters." };
                }

                // Connect to LDAP (Active Directory)
                var credential = new NetworkCredential(request.LdapUsername, request.LdapPassword);
                using (var connection = new LdapConnection(request.LdapServer))
                {
                    connection.Credential = credential;
                    connection.AuthType = AuthType.Negotiate;
                    connection.Bind();

                    // Prepare modification: add user DN to group's member attribute
                    var modification = new DirectoryAttributeModification
                    {
                        Name = "member",
                        Operation = DirectoryAttributeOperation.Add
                    };
                    modification.Add(request.UserDistinguishedName);

                    var modifyRequest = new ModifyRequest(
                        request.GroupDistinguishedName,
                        modification
                    );

                    connection.SendRequest(modifyRequest);
                }

                await Task.CompletedTask;
                return new { statusCode = 200, message = "Successfully added user to group" };
            }
            catch (DirectoryOperationException ex)
            {
                context.Logger.LogError($"Directory operation failed: {ex.Message}");
                return new { statusCode = 500, error = $"Directory operation failed: {ex.Message}" };
            }
            catch (Exception ex)
            {
                context.Logger.LogError($"Failed to add user: {ex.Message}");
                return new { statusCode = 500, error = ex.Message };
            }
        }
    }

    public class GroupAddEvent
    {
        // LDAP connection info
        public string? LdapServer { get; set; } // e.g., "ad.example.com"
        public string? LdapUsername { get; set; } // e.g., "serviceaccount@example.com"
        public string? LdapPassword { get; set; } // e.g., "password"

        // Distinguished Names
        public string? UserDistinguishedName { get; set; } // e.g., "CN=John Doe,OU=Users,DC=example,DC=com"
        public string? GroupDistinguishedName { get; set; } // e.g., "CN=MyGroup,OU=Groups,DC=example,DC=com"
    }
}
