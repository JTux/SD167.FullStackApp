// TaskRepository is responsible for storing and manipulating
// our collection of data, in this case TaskModels

using System.Reflection;
using System.Text;
using System.Text.Json;

namespace ExampleServer.Data;

public class TaskRepository
{
    // Data storage (All of our Tasks)
    private readonly List<TaskModel> _taskList = new List<TaskModel>();

    public TaskRepository()
    {
        CreateJsonFile();
        TaskModel[] tasks = GetTasksFromFile();
        _taskList = new List<TaskModel>(tasks);
    }

    private string FilePath
    {
        get
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
            return Path.Combine(directory, "tasks.json");
        }
    }

    // Create method
    // Not creating an instance, we're creating an entry in the list
    public void AddTask(TaskModel task)
    {
        _taskList.Add(task);
        // _taskList.Contains(task);
        SaveTasksToFile();
    }

    // Read method
    public List<TaskModel> GetTasks()
    {
        // return _taskList;
        return new List<TaskModel>(_taskList);
    }

    public List<TaskModel> GetTasksByStatus(bool isComplete)
    {
        // Start a new list
        List<TaskModel> tasks = new List<TaskModel>();

        // Iterate through all tasks and check the status
        foreach (TaskModel task in _taskList)
        {
            // Add a task to the new list if its status matches the parameter
            if (task.IsComplete == isComplete)
            {
                tasks.Add(task);
            }
        }

        // Return the new list
        return tasks;
    }

    // Update method
    public bool MarkTaskAsComplete(int taskId)
    {
        // Returns the first element of the sequence that satisfies
        // a condition or a default value if no such element is found.
        TaskModel? task = _taskList.FirstOrDefault(t => t.Id == taskId);

        // Check if the task doesn't exist OR
        // check if the task is already complete
        if (task == null || task.IsComplete)
        {
            // Return false to indicate it didn't change anything
            return false;
        }

        task.IsComplete = true;

        SaveTasksToFile();

        return true;
    }

    // Delete method
    public bool DeleteTaskById(int id)
    {
        // Loop through each task
        foreach (var task in _taskList)
        {
            // Check the task Id against our parameter
            if (task.Id == id)
            {
                // If we find it remove the task and return true/false
                bool deleteResult = _taskList.Remove(task);
                SaveTasksToFile();
                return deleteResult;
            }
        }

        // Return false if we don't find the Id in the loop
        return false;
    }

    private TaskModel[] GetTasksFromFile()
    {
        var x = File.ReadAllText(FilePath);
        var y = JsonSerializer.Deserialize<TaskModel[]>(x);
        return y ?? new TaskModel[0];
    }

    private void SaveTasksToFile()
    {
        var y = JsonSerializer.Serialize(_taskList);
        File.WriteAllText(FilePath, y);
    }

    private void CreateJsonFile()
    {
        if (File.Exists(FilePath) != true)
        {
            StreamWriter sw = new StreamWriter(FilePath, true, Encoding.ASCII);
            sw.Write("[]");
            sw.Close();
        }
    }
}