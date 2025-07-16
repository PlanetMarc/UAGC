using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace UAGC_Lambda;
public class EventData
{
    public string? studentId { get; set; }
    public string? studentUPN { get; set; }
    public string? studentFirstName { get; set; }
    public string? studentLastName { get; set; }
    public string? studentStatus { get; set; }
}

public class NewStudentEvent
{
    public string? eventType { get; set; }
    public string? eventId { get; set; }
    public string? requestedBy { get; set; }
    public string? source { get; set; }
    public string? timestamp { get; set; }
    public EventData? eventData { get; set; }
}

public class CreateActiveDirectoryAcct
{
    
    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public string FunctionHandler(string input, ILambdaContext context)
    {
        // Deserialize the SNS message into NewStudentEvent
        var newStudentEvent = System.Text.Json.JsonSerializer.Deserialize<NewStudentEvent>(input);

        // Call placeholder for further processing
        if (newStudentEvent != null)
        {
            ProcessNewStudentEvent(newStudentEvent);
        }

        // For now, return a confirmation with student info
        return $"Received event for student: {newStudentEvent?.eventData?.studentFirstName} {newStudentEvent?.eventData?.studentLastName}, ID: {newStudentEvent?.eventData?.studentId}";
    }
    // Placeholder for further processing logic
    private void ProcessNewStudentEvent(NewStudentEvent evt)
    {
        // TODO: Implement event processing logic here
    }
}
