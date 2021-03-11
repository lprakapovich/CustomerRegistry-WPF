using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerRegistry.Common;
using CustomerRegistry.Model;

namespace CustomerRegistry.ViewModel
{
    public class CustomerEditorViewModel: BaseViewModel
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

        #endregion
    }
}
