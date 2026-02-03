using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineOrdering
{
    public static class Program
    {
        public static void Main()
        {
            var app = new OnlineStoreApp();
            app.Run();
        }
    }

    public class OnlineStoreApp
    {
        private readonly ShoppingCart _cart = new();
        private readonly ProductCatalog _catalog = new();
        private readonly ShippingCalculator _shipping = new();
        private readonly PaymentProcessor _payment = new();

        public OnlineStoreApp()
        {
            _catalog.SeedDefault();
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Online Ordering");
                Console.WriteLine("1 - View products");
                Console.WriteLine("2 - Add to cart");
                Console.WriteLine("3 - View cart");
                Console.WriteLine("4 - Apply discount");
                Console.WriteLine("5 - Checkout");
                Console.WriteLine("0 - Exit");

                int choice = ReadInt(0, 5);

                switch (choice)
                {
                    case 1: ViewProducts(); break;
                    case 2: AddToCart(); break;
                    case 3: ViewCart(); break;
                    case 4: ApplyDiscount(); break;
                    case 5: Checkout(); break;
                    case 0: return;
                }
            }
        }

        private void ViewProducts()
        {
            foreach (var p in _catalog.GetAll())
                Console.WriteLine($"{p.Sku} - {p.Name} - ${p.Price}");
            Console.ReadKey();
        }

        private void AddToCart()
        {
            Console.Write("SKU: ");
            var sku = Console.ReadLine() ?? "";
            Console.Write("Quantity: ");
            int qty = int.Parse(Console.ReadLine() ?? "1");

            var product = _catalog.FindBySku(sku);
            if (product != null)
                _cart.AddProduct(product, qty);

            Console.ReadKey();
        }

        private void ViewCart()
        {
            foreach (var item in _cart.GetItems())
                Console.WriteLine($"{item.Product.Name} x{item.Quantity} = ${item.GetSubtotal()}");
            Console.WriteLine($"Total: ${_cart.GetTotal()}");
            Console.ReadKey();
        }

        private void ApplyDiscount()
        {
            Console.Write("Percent off: ");
            decimal percent = decimal.Parse(Console.ReadLine() ?? "0");
            _cart.ApplyDiscount(new PercentOffDiscount(percent));
            Console.ReadKey();
        }

        private void Checkout()
        {
            Console.Write("ZIP: ");
            string zip = Console.ReadLine() ?? "";
            decimal shipping = _shipping.Calculate(zip, _cart.GetTotal());

            var order = new Order(_cart.GetItems().ToList(), _cart.GetTotal() + shipping);
            _payment.Process(order, "card");

            Console.WriteLine(order.GetSummary());
            Console.ReadKey();
        }

        private static int ReadInt(int min, int max)
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int n) && n >= min && n <= max)
                    return n;
            }
        }
    }

    public class ProductCatalog
    {
        private readonly List<Product> _products = new();

        public void SeedDefault()
        {
            _products.Add(new Product("p1", "Keyboard", 50));
            _products.Add(new Product("p2", "Mouse", 30));
            _products.Add(new Product("p3", "Monitor", 200));
        }

        public List<Product> GetAll() => _products;

        public Product? FindBySku(string sku)
            => _products.FirstOrDefault(p => p.Sku == sku);
    }

    public class Product
    {
        public string Sku { get; }
        public string Name { get; }
        public decimal Price { get; }

        public Product(string sku, string name, decimal price)
        {
            Sku = sku;
            Name = name;
            Price = price;
        }
    }

    public class CartItem
    {
        public Product Product { get; }
        public int Quantity { get; private set; }

        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public void SetQuantity(int qty) => Quantity = qty;

        public decimal GetSubtotal() => Product.Price * Quantity;
    }

    public class ShoppingCart
    {
        private readonly List<CartItem> _items = new();
        private DiscountPolicy? _discount;

        public void AddProduct(Product product, int qty)
        {
            var item = _items.FirstOrDefault(i => i.Product.Sku == product.Sku);
            if (item == null)
                _items.Add(new CartItem(product, qty));
            else
                item.SetQuantity(item.Quantity + qty);
        }

        public void ApplyDiscount(DiscountPolicy discount) => _discount = discount;

        public IReadOnlyList<CartItem> GetItems() => _items;

        public decimal GetSubtotal() => _items.Sum(i => i.GetSubtotal());

        public decimal GetTotal()
        {
            decimal subtotal = GetSubtotal();
            decimal discount = _discount?.Calculate(subtotal) ?? 0;
            return subtotal - discount;
        }
    }

    public abstract class DiscountPolicy
    {
        public abstract decimal Calculate(decimal subtotal);
    }

    public class PercentOffDiscount : DiscountPolicy
    {
        private readonly decimal _percent;

        public PercentOffDiscount(decimal percent) => _percent = percent;

        public override decimal Calculate(decimal subtotal)
            => subtotal * (_percent / 100);
    }

    public class ShippingCalculator
    {
        public decimal Calculate(string zip, decimal total) => total > 100 ? 0 : 15;
    }

    public class Order
    {
        private readonly List<CartItem> _items;
        private readonly decimal _total;
        private string _status = "Created";

        public Order(List<CartItem> items, decimal total)
        {
            _items = items;
            _total = total;
        }

        public void MarkPaid() => _status = "Paid";

        public string GetSummary()
            => $"Order status: {_status} | Total: ${_total}";
    }

    public class PaymentProcessor
    {
        public bool Process(Order order, string method)
        {
            order.MarkPaid();
            return true;
        }
    }
}
