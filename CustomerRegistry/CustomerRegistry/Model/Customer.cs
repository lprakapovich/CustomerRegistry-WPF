using System;
using CustomerRegistry.Common;

namespace CustomerRegistry.Model
{
    public class Customer : BindableBase
    {
        private string _id;
        private string _firstName;
        private string _lastName;
        private ContactData _contactData;

        public Customer() : this(string.Empty, string.Empty, new ContactData()) { }

        public Customer(string firstName, string lastName, ContactData contactData) : this(Guid.NewGuid().ToString())
        {
            _firstName = firstName;
            _lastName = lastName;
            _contactData = contactData;
        }

        public Customer(string id)
        {
            _id = id;
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public ContactData ContactData
        {
            get => _contactData;
            set => _contactData = value;
        }

        public string Id
        {
            get => _id;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName + ", " + Id;
        }
    }
}
