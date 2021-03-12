using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerRegistry.Model;

namespace CustomerRegistry.Utils
{
    public class CustomerService
    {
        private List<Customer> _customers;

        public CustomerService()
        {
            Customers = new List<Customer>();
        }

        public List<Customer> Customers
        {
            get => _customers;
            set => _customers = value;
        }

        public void AddCustomer(Customer customer)
        {
            if (customer != null)
            {
                Customers.Add(customer);
            }
        }

        public void DeleteCustomer(string id)
        { 
            var customerToDelete = Customers.FirstOrDefault(customer => customer.Id.Equals(id));
            Customers.Remove(customerToDelete);
        }

        public void UpdateCustomer(Customer updated)
        {
           var customerToUpdate = Customers.FirstOrDefault(customer => customer.Id.Equals(updated.Id));

           if (customerToUpdate != null)
           {
               UpdateData(customerToUpdate, updated);
           }
        }

        private void UpdateData(Customer customerToUpdate, Customer updated)
        {
            if (!customerToUpdate.FirstName.Equals(updated.FirstName))
            {
                customerToUpdate.FirstName = updated.FirstName;
            }

            if (!customerToUpdate.LastName.Equals(updated.LastName))
            {
                customerToUpdate.LastName = updated.LastName;
            }

            if (!customerToUpdate.ContactData.Equals(updated.ContactData))
            {
                customerToUpdate.ContactData = updated.ContactData;
            }
        }
    }
}
