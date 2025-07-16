// File: CreateAshfordUserLambda.cs
using Amazon.Lambda.Core;
using Microsoft.Data.SqlClient;
using Dapper;

namespace UAGC_Lambda;
public class CreateAshfordUserRecord
{
    // System fields
    public DateTime? Changed { get; set; } // _changed
    public int? Deleted { get; set; } // _deleted
    public int? Enabled { get; set; } // _enabled
    public int? Notified { get; set; } // _notified
    public string? NotifyType { get; set; } // _notifytype
    public int? ModifiedBy { get; set; } // _modifiedby
    public string? UserType { get; set; } // _usertype

    // User info
    public string? FName { get; set; } // fname
    public string? LName { get; set; } // lname
    public string? Email { get; set; } // email
    public string? HomePhone { get; set; } // homephone
    public string? PW { get; set; } // pw
    public string? Pin { get; set; } // pin
    public string? RegQuestion { get; set; } // regquestion
    public string? RegAnswer { get; set; } // reganswer
    public int? AccountType { get; set; } // accounttype
    public int? Processed { get; set; } // processed
    public int? LdapCreated { get; set; } // _ldap_created
    public string? BBId { get; set; } // _bbid

    // Security
    public string? SecurityQuestion { get; set; } // security_question
    public string? SecurityAnswer { get; set; } // security_answer
}

public class Function
{
    public async Task<bool> FunctionHandler(CreateAshfordUserRecord input, ILambdaContext context)
    {
        // Retrieve connection string from AWS Secrets Manager or environment variable
        var connectionString = Environment.GetEnvironmentVariable("ASHFORD_DB_CONNECTION");
        using var connection = new SqlConnection(connectionString);

        // Example insert (Schema may not be accurate, adjust as necessary))
        var sql = @"
            INSERT INTO users (
                _changed, _deleted, _enabled, _notified, _notifytype, _modifiedby, _usertype,
                fname, lname, email, homephone, pw, pin, regquestion, reganswer,
                accounttype, processed, _ldap_created, _bbid, security_question, security_answer
            ) VALUES (
                @_changed, @_deleted, @_enabled, @_notified, @_notifytype, @_modifiedby, @_usertype,
                @fname, @lname, @email, @homephone, @pw, @pin, @regquestion, @reganswer,
                @accounttype, @processed, @_ldap_created, @_bbid, @security_question, @security_answer
            )";

           var parameters = new
           {
               _changed = input.Changed,
               _deleted = input.Deleted,
               _enabled = input.Enabled,
               _notified = input.Notified,
               _notifytype = input.NotifyType,
               _modifiedby = input.ModifiedBy,
               _usertype = input.UserType,
               fname = input.FName,
               lname = input.LName,
               email = input.Email,
               homephone = input.HomePhone,
               pw = input.PW,
               pin = input.Pin,
               regquestion = input.RegQuestion,
               reganswer = input.RegAnswer,
               accounttype = input.AccountType,
               processed = input.Processed,
               _ldap_created = input.LdapCreated,
               _bbid = input.BBId,
               security_question = input.SecurityQuestion,
               security_answer = input.SecurityAnswer
           };

           var result = await connection.ExecuteAsync(sql, parameters);

           return result > 0;
       }
   } // end class Function