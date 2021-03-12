using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CustomerRegistry.Common;
using CustomerRegistry.Model;
using CustomerRegistry.Utils;

namespace CustomerRegistry.ViewModel
{
    public class CustomerEditorViewModel: BindableBase
    {
        #region Private Fields

        private Customer _customer;

        #endregion

        #region Setup
        public CustomerEditorViewModel(Customer customer)
        {
            Customer = customer;
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
            get => Customer.FirstName;
            set
            {
                if (value != null)
                {
                    Customer.FirstName = value;
                    ValidateProperty(nameof(FirstName), FirstName);
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }

        public string LastName
        {
            get => Customer.LastName;
            set
            {
                if (value != null)
                {
                    Customer.LastName = value;
                    ValidateProperty(nameof(LastName), LastName);
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }

        public string HomePhone
        {
            get => Customer.ContactData.Phone.HomeNumber;
            set
            {
                if (value != null)
                {
                    Customer.ContactData.Phone.HomeNumber = value;
                    ValidateProperty(nameof(HomePhone), HomePhone);
                    OnPropertyChanged(nameof(HomePhone));
                }
            }
        }

        public string CellPhone
        {
            get => Customer.ContactData.Phone.CellNumber;
            set
            {
                if (value != null)
                {
                    Customer.ContactData.Phone.CellNumber = value;
                    ValidateProperty(nameof(CellPhone), CellPhone);
                    OnPropertyChanged(nameof(CellPhone));
                }
            }
        }

        public string WorkingEmail 
        {
            get => Customer.ContactData.Email.WorkingEmail;
            set
            {
                if (value != null)
                {
                    Customer.ContactData.Email.WorkingEmail = value;
                    ValidateProperty(nameof(WorkingEmail), WorkingEmail);
                    OnPropertyChanged(nameof(WorkingEmail));
                }
            }
        }

        public string PrivateEmail
        {
            get => Customer.ContactData.Email.PrivateEmail;
            set
            {
                if (value != null)
                {
                    Customer.ContactData.Email.PrivateEmail = value;
                    ValidateProperty(nameof(PrivateEmail), PrivateEmail);
                    OnPropertyChanged(nameof(PrivateEmail));
                }
            }
        }

        public string Street
        {
            get => Customer.ContactData.Address.Street;
            set
            {
                if (value != null)
                {
                    Customer.ContactData.Address.Street = value;
                    OnPropertyChanged(nameof(Street));
                }
            }
        }

        public string City
        {
            get => Customer.ContactData.Address.City;
            set
            {
                if (value != null)
                {
                    Customer.ContactData.Address.City = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }

        public string PostalCode
        {
            get => Customer.ContactData.Address.PostalCode;
            set
            {
                if (value != null)
                {
                    Customer.ContactData.Address.PostalCode = value;
                    OnPropertyChanged(nameof(PostalCode));
                }
            }
        }

        public Country Country 
        {
            get => Customer.ContactData.Address.Country;
            set
            {
                Customer.ContactData.Address.Country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public Action<string> MessageBoxDelegate { get; set; }

        #endregion

        #region Commands

        private RelayCommand _saveCustomerCommand;
        public RelayCommand SaveCustomerCommand
        {
            get =>
                _saveCustomerCommand ?? 
                (_saveCustomerCommand = new RelayCommand(e =>
                    {
                        if (MissingRequiredFields)
                        {
                            MessageBoxDelegate("Please, fill all the required fields");
                        } 
                        else if (!IsViewModelValid)
                        {
                            MessageBoxDelegate("Some fields are invalid");
                        }
                        else
                        {
                            SaveCustomerDetailsEvent?.Invoke(Customer);
                            CloseWindow?.Invoke(this, new EventArgs());
                        }
                    }
                ));
        }

        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand =>
            _cancelCommand ??
            (_cancelCommand = new RelayCommand(e => CloseWindow?.Invoke(this, new EventArgs())));

        #endregion

        #region Validation

        private void ValidateProperty(string property, string value)
        {
            bool isValid = ValidationService.IsValid(property, value, out ICollection<string> validationErrors);

            if (!isValid)
            {
                _validationErrors[property] = validationErrors;
                RaiseErrorsChanged(property); 
                OnPropertyChanged(nameof(IsViewModelValid));
            }

            else if (_validationErrors.ContainsKey(property))
            {
                _validationErrors.Remove(property);
                RaiseErrorsChanged(property);
                OnPropertyChanged(nameof(IsViewModelValid));
            }
        }

        private bool IsViewModelValid => !_validationErrors.Any();

        private bool MissingRequiredFields => string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(City);
        
        #endregion
    }
}
