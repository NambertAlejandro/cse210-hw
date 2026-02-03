using System.Collections.Generic;
using System.Text;

namespace OnlineOrdering
{
    public class Order
    {
        private List<Product> _products;
        private Customer _customer;

        public Order(Customer customer)
        {
            _customer = customer;
            _products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public double GetTotalCost()
        {
            double total = 0;
            foreach (Product product in _products)
            {
                total += product.GetTotalCost();
            }

            double shippingCost = _customer.LivesInUsa() ? 5.0 : 35.0;
            return total + shippingCost;
        }

        public string GetPackingLabel()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Packing Label");
            foreach (Product product in _products)
            {
                sb.AppendLine(product.GetPackingLabelLine());
            }
            return sb.ToString().TrimEnd();
        }

        public string GetShippingLabel()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Shipping Label");
            sb.AppendLine(_customer.GetName());
            sb.AppendLine(_customer.GetShippingAddress());
            return sb.ToString().TrimEnd();
        }
    }
}
