using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistry.Model
{
    public class Phone
    {
        private string _phoneNumber;

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(typeof(Phone) == obj.GetType()))
            {
                return false;
            }

            Phone phone = (Phone) obj;

            return phone.PhoneNumber.Equals(_phoneNumber);
        }
    }

}
