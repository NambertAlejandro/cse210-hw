namespace OnlineOrdering
{
    public class Customer
    {
        private string _name;
        private Address _address;

        public Customer(string name, Address address)
        {
            _name = name;
            _address = address;
        }

        public string GetName()
        {
            return _name;
        }

        public bool LivesInUsa()
        {
            return _address.IsInUsa();
        }

        public string GetShippingAddress()
        {
            return _address.GetFullAddress();
        }
    }
}
