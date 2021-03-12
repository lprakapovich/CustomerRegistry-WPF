using System;

namespace CustomerRegistry.Model
{
    [Serializable]
    public class Email
    {
        private string _privateEmail;
        private string _workingEmail;

        public Email() : this(string.Empty, string.Empty) { }

        public Email(string workingEmail) : this(workingEmail, string.Empty) { }

        public Email(Email other)
        {
            this._privateEmail = other._privateEmail;
            this._workingEmail = other._workingEmail;
        }

        public Email(string workingEmail, string privateEmail)
        {
            _workingEmail = workingEmail;
            this._privateEmail = privateEmail; 
        }

        public string PrivateEmail
        {
            get => _privateEmail;

            set => _privateEmail = value;
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

            return email.PrivateEmail.Equals(_privateEmail) && email.WorkingEmail.Equals(_workingEmail);
        }
    }
}
