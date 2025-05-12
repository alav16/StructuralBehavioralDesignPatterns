/*
You're developing a pricing engine for an online store that sells various product types
— Books, Electronics, and Clothing.
Each product must accept a Visitor that can apply different seasonal discounts
(e.g., Holiday sale, Black Friday, etc.), but the discount logic is different per product type.
🎯 Objective
Use the Visitor Design Pattern to separate discount calculation logic from the product classes.
🏗️ Structure
Interfaces:
interface IProduct
{
    void Accept(IDiscountVisitor visitor);
    decimal Price { get; }
    string Name { get; }
}
interface IDiscountVisitor
{
    void Visit(Book product);
    void Visit(Electronics product);
    void Visit(Clothing product);
}
Concrete Products:
class Book : IProduct { ... }
class Electronics : IProduct { ... }
class Clothing : IProduct { ... }
Each implements Accept(visitor) by calling the visitor’s corresponding method:
 visitor.Visit(this);
Concrete Visitors:
class HolidayDiscountVisitor : IDiscountVisitor
{
    // e.g., 10% off books, 5% off electronics, 15% off clothing
}

class BlackFridayVisitor : IDiscountVisitor
{
    // e.g., 50% off electronics, 30% off clothing, no book discounts
}
*/

using System;
using System.Collections.Generic;


public interface IProduct
{
    void Accept(IDiscountVisitor visitor);
    decimal Price { get; }
    string Name { get; }
}


public interface IDiscountVisitor
{
    void Visit(Book product);
    void Visit(Electronics product);
    void Visit(Clothing product);
}


public class Book : IProduct
{
    public string Name { get; }
    public decimal Price { get; }

    public Book(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public void Accept(IDiscountVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class Electronics : IProduct
{
    public string Name { get; }
    public decimal Price { get; }

    public Electronics(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public void Accept(IDiscountVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class Clothing : IProduct
{
    public string Name { get; }
    public decimal Price { get; }

    public Clothing(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public void Accept(IDiscountVisitor visitor)
    {
        visitor.Visit(this);
    }
}


public class HolidayDiscountVisitor : IDiscountVisitor
{
    public void Visit(Book product)
    {
        decimal discount = product.Price * 0.10m;
        Console.WriteLine($"Book '{product.Name}': Original {product.Price:C} → Discounted {product.Price - discount:C}");
    }

    public void Visit(Electronics product)
    {
        decimal discount = product.Price * 0.05m;
        Console.WriteLine($"Electronics '{product.Name}': Original {product.Price:C} → Discounted {product.Price - discount:C}");
    }

    public void Visit(Clothing product)
    {
        decimal discount = product.Price * 0.15m;
        Console.WriteLine($"Clothing '{product.Name}': Original {product.Price:C} → Discounted {product.Price - discount:C}");
    }
}

public class BlackFridayVisitor : IDiscountVisitor
{
    public void Visit(Book product)
    {
       
        Console.WriteLine($"Book '{product.Name}': Original {product.Price:C} → No discount");
    }

    public void Visit(Electronics product)
    {
        decimal discount = product.Price * 0.50m;
        Console.WriteLine($"Electronics '{product.Name}': Original {product.Price:C} → Discounted {product.Price - discount:C}");
    }

    public void Visit(Clothing product)
    {
        decimal discount = product.Price * 0.30m;
        Console.WriteLine($"Clothing '{product.Name}': Original {product.Price:C} → Discounted {product.Price - discount:C}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<IProduct> cart = new List<IProduct>()
        {
            new Book("Design Patterns", 40),
            new Electronics("Headphones", 120),
            new Clothing("Jacket", 80)
        };

        Console.WriteLine("Holiday Sale Discounts:");
        IDiscountVisitor holidaySale = new HolidayDiscountVisitor();
        foreach (var item in cart)
        {
            item.Accept(holidaySale);
        }

        Console.WriteLine("Black Friday Discounts:");
        IDiscountVisitor blackFriday = new BlackFridayVisitor();
        foreach (var item in cart)
        {
            item.Accept(blackFriday);
        }
    }
}
