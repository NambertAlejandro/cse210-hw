using System;
using System.Collections.Generic;

namespace OnlineOrdering
{
    public static class Program
    {
        public static void Main()
        {
            Address address1 = new Address("123 Main St", "Seattle", "WA", "USA");
            Customer customer1 = new Customer("John Carter", address1);
            Order order1 = new Order(customer1);
            order1.AddProduct(new Product("Keyboard", "P100", 49.99, 1));
            order1.AddProduct(new Product("Mouse", "P200", 25.50, 2));

            Address address2 = new Address("55 Queen St", "Toronto", "ON", "Canada");
            Customer customer2 = new Customer("Maria Silva", address2);
            Order order2 = new Order(customer2);
            order2.AddProduct(new Product("Monitor", "P300", 199.99, 1));
            order2.AddProduct(new Product("USB-C Cable", "P400", 9.99, 3));

            List<Order> orders = new List<Order> { order1, order2 };

            foreach (Order order in orders)
            {
                Console.WriteLine(order.GetPackingLabel());
                Console.WriteLine();
                Console.WriteLine(order.GetShippingLabel());
                Console.WriteLine();
                Console.WriteLine($"Total Price: ${order.GetTotalCost():0.00}");
                Console.WriteLine(new string('-', 30));
            }
        }
    }
}
