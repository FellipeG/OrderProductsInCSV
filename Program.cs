using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using OrderCsv.Entities;


namespace OrderCsv
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<Product> productList = new List<Product>();
                Console.Write("Enter file full path: ");
                string path = @Console.ReadLine();
                using (StreamReader sr = File.OpenText(path))
                {
                    while(!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split(',');
                        string name = line[0];
                        double price = double.Parse(line[1], CultureInfo.InvariantCulture);
                        productList.Add(new Product(name, price));
                    }

                }

                double averagePrice = productList.Select(p => p.Price).DefaultIfEmpty(0.0).Average();
                var newListOfProducts = productList.Where(p => p.Price < averagePrice)
                    .Select(p => p.Name).OrderByDescending(p => p.ToUpper());

                Console.WriteLine("Average price: " + averagePrice.ToString("F2", CultureInfo.InvariantCulture));
                foreach(string product in newListOfProducts)
                {
                    Console.WriteLine(product);
                }

            }
            catch(IOException e)
            {
                Console.WriteLine("Input/output error: " + e.Message);
            }
        }
    }
}
