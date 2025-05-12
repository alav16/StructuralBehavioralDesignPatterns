/*
You receive product data in CSV line format, like:
"Chair,129.99"
But your system works internally with a Product object:
public class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
}
Your task:
Define an interface IProductProvider with a method:
Product GetProduct();
Create a class CsvProductAdapter that:
Takes a CSV line string in the constructor
Implements the IProductProvider interface
In GetProduct(), parses the CSV string and returns a correctly populated Product object.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prob1Adapter;

namespace prob1Adapter
{
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
   
    public interface IProductProvider
    {
        Product GetProduct();
    }

    public class CsvProductAdapter : IProductProvider
    {
        private readonly string _csvline;
        public CsvProductAdapter(string csvLine)
        {
            _csvline = csvLine;
        }

        public Product GetProduct()
        {
            string[] parts = _csvline.Split(',');

            string name = parts[0];
            double price = double.Parse(parts[1]);

            return new Product(name, price);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string csvLine = "Chair,129.99";
            IProductProvider adapter = new CsvProductAdapter(csvLine);
            Product product = adapter.GetProduct();
            Console.WriteLine($"Name: {product.Name}, Price: {product.Price}");

        }
    }
}
