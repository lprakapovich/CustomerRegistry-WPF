using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerRegistry.Model;

namespace CustomerRegistry.ViewModel
{
    public class CustomerDetailsViewModel
    {
        private Customer _customer; 
        public CustomerDetailsViewModel(Customer customer)
        {
            Customer = customer;
        }

        public Customer Customer
        {
            get => _customer;
            set => _customer = value;
        }
    }
}