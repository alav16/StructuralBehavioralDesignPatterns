/*
 You are building a task management system.
 Each task can be:
A SimpleTask (basic task with a name)
A CompositeTask (a task made up of multiple sub-tasks)
✅ You must use the Composite Design Pattern to represent tasks and sub-tasks uniformly.
✅ Both Simple and Composite tasks must have a common interface.
🛠 Your Tasks:
Define a TaskItem interface (or abstract class) that declares:
getName(): string
display(indent: string): void
Implement SimpleTask class:
Stores a name.
Implements display() to print its name.
Implement CompositeTask class:
Stores a name.
Contains a list of TaskItem children.
Can add() sub-tasks.
Implements display() to print itself and recursively print its sub-tasks.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prob3Composite
{
    internal class Program
    {
        public interface ITaskItem
        {
            string GetName();
            void Display(string message);
            int GetTotalTaskCount();
        }

        public class SimpleTask : ITaskItem
        {
            private string _name;

            public SimpleTask(string name)
            {
                _name = name;
            }

            public string GetName() => _name;
            public void Display(string message)
            {
                Console.WriteLine($"{message} - {_name}");
            }

            public int GetTotalTaskCount() => 1;
        }

        public class CompositeTask : ITaskItem
        {
            private readonly string _name;
            private List<ITaskItem> taskItems = new List<ITaskItem>();

            public CompositeTask(string name)
            {
                _name = name;
            }

            public string GetName() => _name;

            public void Add(ITaskItem taskItem)
            {
                taskItems.Add(taskItem);
            }


            public void Display(string message)
            {
                Console.WriteLine($"{message}+ {_name}");

                foreach (var item in taskItems)
                {
                    item.Display(message + " ");
                }
            }

            public int GetTotalTaskCount()
            {
                int count = 0;
                foreach (var item in taskItems)
                {
                    count += item.GetTotalTaskCount();
                }
                return count;
            }
        }
        static void Main(string[] args)
        {
            var task1 = new SimpleTask("Buy groceries");
            var task2 = new SimpleTask("Call mom");
            var task3 = new SimpleTask("Pay bills");

            var personalTasks = new CompositeTask("Personal Tasks");
            personalTasks.Add(task1);
            personalTasks.Add(task2);
            personalTasks.Add(task3);

            var task4 = new SimpleTask("Fix login bug");
            var task5 = new SimpleTask("Deploy new release");

            var workTasks = new CompositeTask("Work Tasks");
            workTasks.Add(task4);
            workTasks.Add(task5);

            var allTasks = new CompositeTask("All Tasks");
            allTasks.Add(personalTasks);
            allTasks.Add(workTasks);
            allTasks.Display("");

        }
    }
}
