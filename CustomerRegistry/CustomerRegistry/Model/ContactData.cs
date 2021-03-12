using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistry.Model
{
    public class ContactData
    {
        private Address _address;
        private Email _email;
        private Phone _phone;

        public ContactData(Address address, Email email, Phone phone)
        {
            _address = address;
            _email = email;
            _phone = phone;
        }

        public Address Address
        {
            get => _address;
            set => _address = value;
        }

        public Email Email
        {
            get => _email;
            set => _email = value;
        }

        public Phone Phone
        {
            get => _phone;
            set => _phone = value; 
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(typeof(ContactData) == obj.GetType()))
            {
                return false;
            }

            ContactData contactData = (ContactData) obj;

            return contactData.Address.Equals(_address) && contactData.Email.Equals(_email) && contactData.Phone.Equals(_phone);
        }
    }
}
