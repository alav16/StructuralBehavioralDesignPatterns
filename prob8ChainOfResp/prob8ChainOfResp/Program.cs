/*
Suppose you are building a customer support system where incoming tickets
must be routed to the correct department based on their issue type.
Some issues are clearly related to billing, others are technical, and some are general or unknown.
You decide to use the Chain of Responsibility pattern
so that each handler checks whether it can process the ticket
and either handles it or passes it to the next in line.
This promotes clean separation of responsibilities and flexibility in routing logic.
🧱 Structure Overview
1. Entities
SupportTicket
 Contains:
string IssueType
string Description
2. Abstract Handler:
SupportHandler
Method: SetNext(SupportHandler next)
Method: Handle(SupportTicket ticket)
Abstract: CanHandle(SupportTicket ticket)
Abstract: Process(SupportTicket ticket)
3. Concrete Handlers:
BillingSupport: handles tickets with issue type "billing"
TechnicalSupport: handles tickets with issue type "technical"
GeneralSupport: handles all other issues (default catch-all)
*/

using System;

public class SupportTicket
{
    public string IssueType { get; }
    public string Description { get; }

    public SupportTicket(string issueType, string description)
    {
        IssueType = issueType;
        Description = description;
    }
}

public abstract class SupportHandler
{
    private SupportHandler _next;

    public SupportHandler SetNext(SupportHandler next)
    {
        _next = next;
        return next;
    }

    public void Handle(SupportTicket ticket)
    {
        if (CanHandle(ticket))
        {
            Process(ticket);
        }
        else if (_next != null)
        {
            _next.Handle(ticket);
        }
    }

    protected abstract bool CanHandle(SupportTicket ticket);
    protected abstract void Process(SupportTicket ticket);
}

public class BillingSupport : SupportHandler
{
    protected override bool CanHandle(SupportTicket ticket)
    {
        return ticket.IssueType == "billing";
    }

    protected override void Process(SupportTicket ticket)
    {
        Console.WriteLine($" Billing Support handled the issue: {ticket.Description}");
    }
}

public class TechnicalSupport : SupportHandler
{
    protected override bool CanHandle(SupportTicket ticket)
    {
        return ticket.IssueType == "technical";
    }

    protected override void Process(SupportTicket ticket)
    {
        Console.WriteLine($" Technical Support handled the issue: {ticket.Description}");
    }
}

public class GeneralSupport : SupportHandler
{
    protected override bool CanHandle(SupportTicket ticket)
    {
        return true;
    }

    protected override void Process(SupportTicket ticket)
    {
        Console.WriteLine($" General Support handled the issue: {ticket.Description}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var billing = new BillingSupport();
        var tech = new TechnicalSupport();
        var general = new GeneralSupport();

       
        billing.SetNext(tech).SetNext(general);

        
        var ticket1 = new SupportTicket("billing", "Refund for last month's invoice.");
        var ticket2 = new SupportTicket("technical", "App crashes when I login.");
        var ticket3 = new SupportTicket("shipping", "My package hasn't arrived.");
        var ticket4 = new SupportTicket("unknown", "I need help but don't know who to ask.");

        
        billing.Handle(ticket1);
        billing.Handle(ticket2);
        billing.Handle(ticket3);
        billing.Handle(ticket4);
    }
}