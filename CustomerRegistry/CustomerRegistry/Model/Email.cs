using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistry.Model
{
    public class Email
    {
        private string _personalEmail;
        private string _workingEmail;
        
        public Email()
        {
        }

        public Email(string workingEmail) : this(workingEmail, string.Empty)
        {
        }

        public Email(Email other)
        {
            this._personalEmail = other._personalEmail;
            this._workingEmail = other._workingEmail;
        }

        public Email(string workingEmail, string personalEmail)
        {
            _workingEmail = workingEmail;
            this._personalEmail = personalEmail; 
        }

        public string PersonalEmail
        {
            get => _personalEmail;

            set => _personalEmail = value;
        }

        public string WorkingEmail
        {
            get => _workingEmail;

            set => _workingEmail = value;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(typeof(Email) == obj.GetType()))
            {
                return false;
            }

            Email email = (Email) obj;

            return email.PersonalEmail.Equals(_personalEmail) && email.WorkingEmail.Equals(_workingEmail);
        }
    }
}
