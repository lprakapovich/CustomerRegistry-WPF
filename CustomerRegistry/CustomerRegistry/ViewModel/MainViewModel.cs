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

            Customers.Add(new Customer("Liza", "Pr", new ContactData())); 
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
                IsSelected = true;
                CustomerDetailsViewModel = new CustomerDetailsViewModel(SelectedCustomer); 
                OnPropertyChanged(nameof(SelectedCustomer));
                
            }
        }

        public bool IsSelected
        {
            get => SelectedCustomer != null;
            set { OnPropertyChanged(nameof(IsSelected)); }
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
            return new CustomerEditorViewModel(SelectedCustomer ?? new Customer())
            { 
                SaveCustomerDetailsEvent = OnCustomerDetailsSaved
            };
        }


        #endregion

        #region Commands

        private RelayCommand _deleteCustomerCommand;
        public RelayCommand DeleteCustomerCommand =>
            _deleteCustomerCommand ??
                (_deleteCustomerCommand = new RelayCommand(
                    ex => Customers.Remove(SelectedCustomer),
                    canEx => SelectedCustomer != null));
        #endregion

        #region Private

        private void Customers_CollectionChanges(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    int newIndex = e.NewStartingIndex;
                    CustomerService.AddCustomer(Customers[newIndex]);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    List<Customer> tempListOfRemovedItems = e.OldItems.OfType<Customer>().ToList();
                    CustomerService.DeleteCustomer(tempListOfRemovedItems[0].Id);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    List<Customer> tempListOfItems = e.NewItems.OfType<Customer>().ToList();
                    CustomerService.UpdateCustomer(tempListOfItems[0]);
                    SelectedCustomer = null;
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
