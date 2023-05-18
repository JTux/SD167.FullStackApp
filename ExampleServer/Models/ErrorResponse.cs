using System.Text.Json.Serialization;

namespace ExampleServer.Models;

public class ErrorResponse
{
    // Constructor
    // No return type
    // The name is the same as the class
    public ErrorResponse(string message)
    {
        Message = message;
    }

    // Property
    [JsonPropertyName("message")]
    public string Message { get; }
}
