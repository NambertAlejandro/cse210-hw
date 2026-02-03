namespace OnlineOrdering
{
    public class Address
    {
        private string _street;
        private string _city;
        private string _stateProvince;
        private string _country;

        public Address(string street, string city, string stateProvince, string country)
        {
            _street = street;
            _city = city;
            _stateProvince = stateProvince;
            _country = country;
        }

        public bool IsInUsa()
        {
            string c = _country.Trim().ToLower();
            return c == "usa" || c == "united states" || c == "united states of america" || c == "us";
        }

        public string GetFullAddress()
        {
            return $"{_street}\n{_city}, {_stateProvince}\n{_country}";
        }
    }
}
