using System;
using CustomerRegistry.Common;
using CustomerRegistry.Model;

namespace CustomerRegistry.ViewModel
{
    public class CustomerEditorViewModel: BindableBase
    {
        #region Private Fields

        private Customer _customer;

        private string _firstName;
        private string _lastName;
        private string _homePhone;
        private string _cellPhone;
        private string _workingEmail;
        private string _privateEmail;
        private string _street;
        private string _city;
        private string _postalCode;
        private Country _country;

        #endregion

        #region Setup
        public CustomerEditorViewModel(Customer customer)
        {
            if (customer != null)
            {
                //InitializeControls(customer);
            }

            Customer = customer;
            SaveCustomerCommand = new RelayCommand(e => SaveCustomerDetailsEvent.Invoke(GetCustomer()));
            CancelCommand = new RelayCommand(e => CloseWindow(this, new EventArgs()));
        }

        private void InitializeControls(Customer customer)
        {
            Customer = customer;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            HomePhone = customer.ContactData.Phone.HomeNumber;
            CellPhone = customer.ContactData.Phone.CellNumber;
            PrivateEmail = customer.ContactData.Email.PrivateEmail;
            WorkingEmail = customer.ContactData.Email.WorkingEmail;
            PostalCode = customer.ContactData.Address.PostalCode;
            Country = customer.ContactData.Address.Country;
            Street = customer.ContactData.Address.Street;
            City = customer.ContactData.Address.City;
        }

        #endregion

        #region Event Handlers

        public delegate void CustomerEvent(Customer customer);

        public CustomerEvent SaveCustomerDetailsEvent;

        public event EventHandler CloseWindow; 

        #endregion

        #region API
        public Customer Customer
        {
            get => _customer;
            set
            {
                _customer = value;
                OnPropertyChanged(nameof(Customer));
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value != null)
                {
                    _firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (value != null)
                {
                    _lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }

        public string HomePhone
        {
            get => _homePhone;
            set
            {
                if (value != null)
                {
                    _homePhone = value;
                    OnPropertyChanged(nameof(HomePhone));
                }
            }
        }

        public string CellPhone
        {
            get => _cellPhone;
            set
            {
                if (value != null)
                {
                    _cellPhone = value;
                    OnPropertyChanged(nameof(CellPhone));
                }
            }
        }

        public string WorkingEmail 
        {
            get => _workingEmail;
            set
            {
                if (value != null)
                {
                    _workingEmail = value;
                    OnPropertyChanged(nameof(WorkingEmail));
                }
            }
        }

        public string PrivateEmail
        {
            get => _privateEmail;
            set
            {
                if (value != null)
                {
                    _privateEmail = value;
                    OnPropertyChanged(nameof(PrivateEmail));
                }
            }
        }

        public string Street
        {
            get => _street;
            set
            {
                if (value != null)
                {
                    _street = value;
                    OnPropertyChanged(nameof(Street));
                }
            }
        }

        public string City
        {
            get => _city;
            set
            {
                if (value != null)
                {
                    _city = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }

        public string PostalCode
        {
            get => _postalCode;
            set
            {
                if (value != null)
                {
                    _postalCode = value;
                    OnPropertyChanged(nameof(PostalCode));
                }
            }
        }

        public Country Country 
        {
            get => _country;
            set
            {
                _country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        #endregion

        #region Commands

        private RelayCommand _saveCustomerCommand;

        public RelayCommand SaveCustomerCommand
        {
            get => _saveCustomerCommand;
            set
            {
                if (value != null)
                {
                    _saveCustomerCommand = value;
                }
            }
        }

        private RelayCommand _cancelCommand;

        public RelayCommand CancelCommand
        {
            get => _cancelCommand;
            set
            {
                if (value != null)
                {
                    _cancelCommand = value;
                }
            }
        }
        #endregion

        #region Private methods

        private Customer GetCustomer()
        {
            return new Customer(
                FirstName,
                LastName,
                new ContactData(
                    new Address(Country.Algeria, City, Street, PostalCode), 
                    new Email(WorkingEmail, PrivateEmail),
                    new Phone(HomePhone, CellPhone)));
        }

        #endregion
    }
}
