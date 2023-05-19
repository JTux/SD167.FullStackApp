using ExampleServer.Data;
using ExampleServer.Server;

TaskModel.TotalTasks = 0;
Console.WriteLine(TaskModel.TotalTasks);

// Instance of our class
TaskModel task1 = new TaskModel("Adopt a dog", "We don't have a dog. Let's get a dog.");
task1.WriteTotalTasks();

TaskModel task2 = new TaskModel("Feed the cats", "The cats are going to be upset if we don't feed them soon.");
task1.WriteTotalTasks();
task2.WriteTotalTasks();

Console.WriteLine(task2.Id);

// Implicit Types
// var assumes the type from the righthand side of the expression
var task3 = new TaskModel("Wash the dog", "The dog definitely needs a bath soon.");

// Target-typed new
// Implicit new will assume the type from the lefthand side
TaskModel task4 = new("Add a task to the list", "Let's make a new task and add it to our task list.");

// TaskRepository repo = new();
// var repo = new TaskRepository();
// TaskRepository repo = new TaskRepository();

task4.IsComplete = true;
task1.IsComplete = true;

TaskRepository repo = new();

if (repo.GetTasks().Count == 0)
{
    repo.AddTask(task4);
    repo.AddTask(task1);
    repo.AddTask(task2);
    repo.AddTask(task3);

    repo.AddTask(new("Create a README", "No one knows how to use our app. Let's make a README.md file."));
    repo.AddTask(new("Get a job", "The landlord is asking how I can afford to feed all these animals but not pay my rent..."));
}

// repo.DeleteTaskById(3);

// tasks gets its type from the GetTasks() return type
// var tasks = repo.GetTasks();
var tasks = repo.GetTasksByStatus(false);
foreach (var task in tasks)
{
    Console.WriteLine(task.Description);
}

// Initializing a WebServer instance
// Passing in a repository instance, and a URL to listen on
WebServer server = new WebServer(repo, "http://localhost:8000/");
server.Run(); // Public method in the class WebServer
