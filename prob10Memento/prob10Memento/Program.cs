/*
You are building an HR system that manages employee profiles.
Every time an employee is promoted or their salary is changed,
the HR system can optionally create a Memento, which is a snapshot of the profile at that exact point in time.
Unlike undo stacks or named checkpoints, here you save a specific version as a Memento object
and can restore the employee profile to that precise state later by directly calling Restore(memento).
This approach mimics real-world versioning systems where you bookmark a version
and later revert back to that exact version — without altering any other history.
🧱 Structure Overview
🔹 Originator
EmployeeProfile
Fields: Name, Position, Salary
Methods:
Promote(string newPosition, int newSalary)
CreateMemento(): returns a snapshot of current state
Restore(Memento m): restores state from memento
ToString() to print current state
🔹 Memento
Stores Name, Position, Salary
Immutable, private fields
Only accessible to EmployeeProfile
The Memento class should be private or internal to prevent external tampering
*/

using System;


public class Memento
{
    public string Name { get; }
    public string Position { get; }
    public int Salary { get; }

    public Memento(string name, string position, int salary)
    {
        Name = name;
        Position = position;
        Salary = salary;
    }
}


public class EmployeeProfile
{
    public string Name { get; private set; }
    public string Position { get; private set; }
    public int Salary { get; private set; }

    public EmployeeProfile(string name, string position, int salary)
    {
        Name = name;
        Position = position;
        Salary = salary;
    }

    public void Promote(string newPosition, int newSalary)
    {
        Position = newPosition;
        Salary = newSalary;
    }

    public Memento CreateMemento()
    {
        return new Memento(Name, Position, Salary);
    }

    public void Restore(Memento memento)
    {
        Name = memento.Name;
        Position = memento.Position;
        Salary = memento.Salary;
    }

    public override string ToString()
    {
        return $"Employee: {Name}, Position: {Position}, Salary: {Salary}";
    }
}


class Program
{
    static void Main(string[] args)
    {
        var profile = new EmployeeProfile("Anna", "Junior Developer", 40000);

       
        var v1 = profile.CreateMemento();

        profile.Promote("Mid Developer", 60000);
        var v2 = profile.CreateMemento();

        profile.Promote("Senior Developer", 80000);
        Console.WriteLine("Current: " + profile);

        
        profile.Restore(v2);
        Console.WriteLine("Restored to Mid: " + profile);

        
        profile.Restore(v1);
        Console.WriteLine("Restored to Junior: " + profile);
    }
}