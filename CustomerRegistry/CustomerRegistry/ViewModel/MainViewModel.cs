using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using CustomerRegistry.Common;
using CustomerRegistry.Model;
using CustomerRegistry.Utils;

namespace CustomerRegistry.ViewModel
{
    public class MainViewModel: BindableBase
    {
        #region Private fields

        private ObservableCollection<Customer> _customers;
        private Customer _selectedCustomer;

        private CustomerDetailsViewModel _customerDetailsViewModel;
        private CustomerEditorViewModel _customerEditorViewModel;

        #endregion

        #region Setup

        public MainViewModel()
        {
            CustomerService = new CustomerService();
            Customers = new ObservableCollection<Customer>();
            Customers.CollectionChanged += Customers_CollectionChanges;

            CustomerDetailsViewModel = new CustomerDetailsViewModel();
            Customers.Add(new Customer("Liza", "P", new ContactData(new Address(Country.Afghanistan, "v", "c", "c"), new Email(), new Phone("d", "c"))));
        }

        #endregion

        #region API

        public  CustomerService CustomerService { get; }

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set
            {
                if (value != null)
                {
                    _customers = value;
                    OnPropertyChanged(nameof(Customers));
                }
            }
        }

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                if (value != null)
                {
                    _selectedCustomer = value;
                    OnPropertyChanged(nameof(SelectedCustomer));
                }
            }
        }

        public CustomerDetailsViewModel CustomerDetailsViewModel
        {
            get => _customerDetailsViewModel;
            set
            {
                if (value != null)
                {
                    _customerDetailsViewModel = value;
                    OnPropertyChanged(nameof(CustomerDetailsViewModel));
                }
            }
        }

        public CustomerEditorViewModel CustomerEditorViewModel
        {
            get => _customerEditorViewModel;
            set
            {
                if (value != null)
                {
                    _customerEditorViewModel = value;
                    OnPropertyChanged(nameof(CustomerEditorViewModel));
                }
            }
        }

        public CustomerEditorViewModel GetCustomerEditorDataContext()
        {
            var customerEditorViewModel = new CustomerEditorViewModel(SelectedCustomer);
            customerEditorViewModel.SaveCustomerDetailsEvent += OnCustomerDetailsSaved;
            return customerEditorViewModel;
        }

        private void OnCustomerDetailsSaved(Customer customer)
        {
            var optionalCustomer = Customers.FirstOrDefault(c => c.Id == customer.Id);

            if (optionalCustomer == null)
            {
                Customers.Add(customer);
            }
            else
            {
                Customers[Customers.IndexOf(optionalCustomer)] = customer;
            }
        }

        public CustomerDetailsViewModel DetailsViewModel
        {
            get => _customerDetailsViewModel;
            set => _customerDetailsViewModel = value;
        }

        #endregion

        #region Commands

        private RelayCommand _deleteCustomer;
        public RelayCommand DeleteCustomer
        {
            get => _deleteCustomer;
            set
            {
                if (value != null)
                {
                    _deleteCustomer = value;
                }
            }
        }

      

        #endregion


        #region Private

        private void Customers_CollectionChanges(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    MessageBox.Show("Adding a customer");
                    int newIndex = e.NewStartingIndex;
                    CustomerService.AddCustomer(Customers[newIndex]);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    MessageBox.Show("Removing a customer"); 

                    List<Customer> tempListOfRemovedItems = e.OldItems.OfType<Customer>().ToList();
                    CustomerService.DeleteCustomer(tempListOfRemovedItems[0].Id);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    MessageBox.Show("Replacing a customer");
                    List<Customer> tempListOfItems = e.NewItems.OfType<Customer>().ToList();
                    CustomerService.UpdateCustomer(tempListOfItems[0]);
                    MessageBox.Show("Replaced a customer" + tempListOfItems[0].FirstName);

                    break;
            }
        }

        private void RemoveCustomerFromCollection()
        {

        }

        #endregion
    }
}
