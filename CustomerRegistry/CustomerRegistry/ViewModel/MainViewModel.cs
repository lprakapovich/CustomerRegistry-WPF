using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
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
            Customers = new ObservableCollection<Customer>(CustomerService.Customers);
            Customers.CollectionChanged += Customers_CollectionChanges;
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
                _selectedCustomer = value; 
                CustomerDetailsViewModel = new CustomerDetailsViewModel(SelectedCustomer); 
                OnPropertyChanged(nameof(SelectedCustomer));
                OnPropertyChanged(nameof(IsCustomerSelected));
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
            return new CustomerEditorViewModel(SelectedCustomer?.DeepCopy() ?? new Customer())
            { 
                SaveCustomerDetailsEvent = OnCustomerDetailsSaved
            };
        }

        public bool IsCustomerSelected
        {
            get => SelectedCustomer != null;
            set => OnPropertyChanged(nameof(IsCustomerSelected));
        }

        #endregion

        #region Events

        public event EventHandler AddCustomerEvent;

        public event EventHandler EditCustomerEvent;

        #endregion

        #region Commands

        private RelayCommand _addCustomerCommand;
        public RelayCommand AddCustomerCommand => 
            _addCustomerCommand ??
                (_addCustomerCommand = new RelayCommand(ex => AddCustomerEvent?.Invoke(this, new EventArgs())));


        private RelayCommand _editCustomerCommand;
        public RelayCommand EditCustomerCommand => 
            _editCustomerCommand ??
            (_editCustomerCommand = new RelayCommand(ex => EditCustomerEvent?.Invoke(this, new EventArgs()),
                canEx => SelectedCustomer != null));


        private RelayCommand _deleteCustomerCommand;
        public RelayCommand DeleteCustomerCommand =>
            _deleteCustomerCommand ??
                (_deleteCustomerCommand = new RelayCommand(
                    ex => Customers.Remove(SelectedCustomer), canEx => IsCustomerSelected));
        #endregion

        #region Private

        private void Customers_CollectionChanges(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    int newIndex = e.NewStartingIndex;
                    SelectedCustomer = Customers[newIndex];
                    CustomerService.AddCustomer(Customers[newIndex]);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    List<Customer> tempListOfRemovedItems = e.OldItems.OfType<Customer>().ToList();
                    CustomerService.DeleteCustomer(tempListOfRemovedItems[0].Id);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    List<Customer> tempListOfItems = e.NewItems.OfType<Customer>().ToList();
                    SelectedCustomer = tempListOfItems[0];
                    CustomerService.UpdateCustomer(tempListOfItems[0]);
                    break;
            }
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

        #endregion
    }
}
