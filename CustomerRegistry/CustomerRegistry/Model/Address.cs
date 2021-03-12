
using System;

namespace CustomerRegistry.Model
{
    [Serializable]
    public class Address
    {
        private Country _country;
        private string _city;
        private string _street;
        private string _postalCode;

        public Address() : this(Country.Afghanistan, string.Empty, string.Empty) { }

        public Address(Country country, string city, string street) : this(country, city, street, string.Empty) { }

        public Address(Country country, string city, string street, string postalCode)
        {
            _country = country;
            _city = city;
            _street = street;
            _postalCode = postalCode;
        }

        public Country Country
        {
            get => _country;
            set => _country = value;
        }

        public string City
        {
            get => _city;
            set => _city = value;
        }

        public string Street
        {
            get => _street;
            set => _street = value;
        }

        public string PostalCode
        {
            get => _postalCode;
            set => _postalCode = value;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null ) || !(typeof(Address) == obj.GetType()))
            {
                return false;
            }

            Address address = (Address) obj;

            return address.Country.Equals(_country) && address.City.Equals(_city) && address.Street.Equals(_street) &&
                   address.PostalCode.Equals(_postalCode);
        }
    } 
}
