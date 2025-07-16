# UAGC Identity Management Integration – AWS Lambda Functions

This repository contains AWS Lambda functions and supporting code for the University of Arizona Global Campus (UAGC) identity management integration project. The solution is implemented in C# (.NET 8) and is designed to automate and orchestrate user account provisioning, group membership, and record management across Active Directory, Ashford systems, and Canvas LMS.

## Project Structure

```
.
├── AddUserToADGroup.cs
├── CreateActiveDirectoryAcct.cs
├── CreateAshfordUserRecord.cs
├── CreateCanvasAcct.cs
├── aws-lambda-tools-defaults.json
├── UAGC-Lambda.csproj
├── UAGC-Lambda.sln
├── .gitignore
├── bin/
├── obj/
└── Properties/
```

## Source Code Files

### [`AddUserToADGroup.cs`](AddUserToADGroup.cs:1)
**Purpose:**  
Implements an AWS Lambda function to add a user to an Active Directory group via LDAP.  
- Validates input parameters for LDAP connection and distinguished names.
- Connects to the specified LDAP server using provided credentials.
- Adds the specified user DN to the group's `member` attribute.
- Handles errors and returns status codes for success or failure.

**Key Classes:**
- `Function`: Lambda handler for group membership addition.
- `GroupAddEvent`: Input model for LDAP connection and DN details.

---

### [`CreateActiveDirectoryAcct.cs`](CreateActiveDirectoryAcct.cs:1)
**Purpose:**  
Handles the creation of new Active Directory accounts for students.  
- Receives event data (typically from SNS or another event source).
- Deserializes the event into a strongly-typed object.
- Placeholder for further processing logic (e.g., actual AD account creation).
- Returns a confirmation string with student information.

**Key Classes:**
- `EventData`, `NewStudentEvent`: Models for event structure.
- `CreateActiveDirectoryAcct`: Lambda handler for processing new student events.

---

### [`CreateAshfordUserRecord.cs`](CreateAshfordUserRecord.cs:1)
**Purpose:**  
Creates or updates user records in the Ashford system database.  
- Defines a data model for Ashford user records, including system and user fields.
- Lambda handler connects to a SQL database (connection string from environment variable).
- Inserts user data into the `users` table using Dapper ORM.
- Returns a boolean indicating success.

**Key Classes:**
- `CreateAshfordUserRecord`: Data model for user records.
- `Function`: Lambda handler for database insertion.

---

### [`CreateCanvasAcct.cs`](CreateCanvasAcct.cs:1)
**Purpose:**  
Automates the creation of user accounts in Canvas LMS via its REST API.  
- Accepts input parameters for Canvas API, account, and user details.
- Constructs and sends an HTTP POST request to Canvas to create a new user.
- Handles authentication using a bearer token.
- Returns the HTTP status code and response body from Canvas.

**Key Classes:**
- `CanvasCreateAccountInput`: Input model for Canvas account creation.
- `Function`: Lambda handler for Canvas user creation.

---

### [`aws-lambda-tools-defaults.json`](aws-lambda-tools-defaults.json:1)
**Purpose:**  
Configuration file for AWS Lambda deployment using the .NET Core CLI and Visual Studio.  
- Specifies deployment region, runtime, memory, timeout, and handler.
- Used by AWS tooling to package and deploy Lambda functions.

---

### [`UAGC-Lambda.csproj`](UAGC-Lambda.csproj:1)
**Purpose:**  
.NET project file defining dependencies, target framework, and AWS Lambda settings.  
- Targets .NET 8.0.
- References packages for AWS Lambda, Dapper, SQL Client, and Directory Services.
- Configures build and publish options for Lambda deployment.

---

### [`UAGC-Lambda.sln`](UAGC-Lambda.sln:1)
**Purpose:**  
Visual Studio solution file for the project.  
- Organizes the project for development in Visual Studio.
- Supports multiple build configurations (Debug/Release).

---

## Supporting Directories

- `bin/` and `obj/`: Build output and intermediate files (excluded from git).
- `Properties/`: Contains project properties and launch settings.

---

## .gitignore

A tailored `.gitignore` is included to exclude build artifacts, user-specific files, deployment packages, and IDE settings, ensuring only source and configuration files are tracked.

---

## AWS Lambda & .NET Integration

- All Lambda functions are implemented in C# targeting .NET 8.
- Functions are designed for integration with AWS Lambda, using the Amazon.Lambda.Core library.
- Deployment is managed via the AWS Lambda .NET CLI and Visual Studio tooling.

---

## University of Arizona Global Campus (UAGC) Context

This project is part of UAGC's identity management integration, automating user provisioning and synchronization across multiple systems (Active Directory, Ashford, Canvas LMS) to streamline onboarding and access management.

---

## Getting Started

1. Clone the repository.
2. Restore NuGet packages:  
   `dotnet restore`
3. Build the solution:  
   `dotnet build`
4. Deploy Lambda functions using the AWS Lambda .NET CLI or Visual Studio.

---

## Contact

For questions or contributions, please contact the UAGC IT Identity Management team.
