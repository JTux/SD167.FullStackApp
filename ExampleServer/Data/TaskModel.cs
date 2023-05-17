// TaskModel is our POCO (plain old C# object)
// It is going to represent the data object

namespace ExampleServer.Data;

// Task or ToDo is something we want to get done
// A TaskModel instance represents a single task
// Identifier, Title, Description, a completion status
public class TaskModel
{
    public static int TotalTasks = 0;

    // Constructor
    public TaskModel(string title, string description)
    {
        TotalTasks++;
        Id = TotalTasks;

        Title = title;
        Description = description;
    }

    // Properties
    public int Id { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsComplete { get; set; }

    public void WriteTotalTasks()
    {
        Console.WriteLine($"Task {Id}/{TotalTasks}");
    }
}
