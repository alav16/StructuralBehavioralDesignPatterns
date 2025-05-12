/*
you are developing a flexible, event-driven news broadcasting platform
where users can subscribe to specific news categories such as Politics, Sports, or Tech.
Your system will use C#'s event and delegate mechanisms to notify subscribers, but with a twist:
Categories are defined using an enum
Each category acts as an independent event channel
Subscribers can subscribe/unsubscribe to any number of categories
Publishing a news headline in a category triggers only the relevant subscribers
This exercise simulates real-world streaming platforms and advanced pub-sub (publish-subscribe) architectures.
🧱 Structure Overview
🔹 Enum: NewsCategory
public enum NewsCategory
{
    Politics,
    Sports,
    Tech
}
🔹 NewsAgency (Publisher)
Maintains a dictionary: Dictionary<NewsCategory, event Action<string>>
Methods:
Subscribe(NewsCategory category, Action<string> handler)
Unsubscribe(NewsCategory category, Action<string> handler)
Publish(NewsCategory category, string headline)
🔹 Subscriber
Has a Name
Method: Receive(string headline) (will be adapted per category)
Subscribes/unsubscribes to specific NewsCategory events
*/

using System;
using System.Collections.Generic;

public enum NewsCategory
{
    Politics,
    Sports,
    Tech
}


public class NewsAgency
{
    private readonly Dictionary<NewsCategory, Action<string>> _subscribers = new Dictionary<NewsCategory, Action<string>>();

    public void Subscribe(NewsCategory category, Action<string> handler)
    {
        if (_subscribers.ContainsKey(category))
        {
            _subscribers[category] += handler;
        }
        else
        {
            _subscribers[category] = handler;
        }
    }

    public void Unsubscribe(NewsCategory category, Action<string> handler)
    {
        if (_subscribers.ContainsKey(category))
        {
            _subscribers[category] -= handler;
        }
    }

    public void Publish(NewsCategory category, string headline)
    {
        if (_subscribers.ContainsKey(category) && _subscribers[category] != null)
        {
            _subscribers[category]?.Invoke(headline);
        }
    }
}


public class Subscriber
{
    public string Name { get; }

    public Subscriber(string name)
    {
        Name = name;
    }

    public void ReceiveTech(string headline) => Console.WriteLine($"[{Name}] [Tech] {headline}");
    public void ReceiveSports(string headline) => Console.WriteLine($"[{Name}] [Sports] {headline}");
    public void ReceivePolitics(string headline) => Console.WriteLine($"[{Name}] [Politics] {headline}");
}

class Program
{
    static void Main(string[] args)
    {
        var agency = new NewsAgency();

        var alice = new Subscriber("Armen");
        var bob = new Subscriber("Mon");
        var chris = new Subscriber("Len");

      
        agency.Subscribe(NewsCategory.Tech, alice.ReceiveTech);
        agency.Subscribe(NewsCategory.Politics, alice.ReceivePolitics);
        agency.Subscribe(NewsCategory.Sports, bob.ReceiveSports);
        agency.Subscribe(NewsCategory.Tech, bob.ReceiveTech);
        agency.Subscribe(NewsCategory.Politics, chris.ReceivePolitics);

        
        agency.Publish(NewsCategory.Tech, "AI Beats Human in Chess Again");
        agency.Publish(NewsCategory.Politics, "New Election Date Announced");
        agency.Publish(NewsCategory.Sports, "Local Team Wins Championship");

      
        agency.Unsubscribe(NewsCategory.Politics, alice.ReceivePolitics);

        agency.Publish(NewsCategory.Politics, "Parliament Passes New Law");
    }
}
