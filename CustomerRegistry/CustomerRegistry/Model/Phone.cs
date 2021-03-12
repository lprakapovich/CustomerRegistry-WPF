using System;

namespace CustomerRegistry.Model
{
    [Serializable]
    public class Phone
    {
        private string _homeNumber;
        private string _cellNumber;

        public Phone() : this(string.Empty, string.Empty)
        {
        }

        public Phone(string homeNumber, string cellNumber)
        {
            _homeNumber = homeNumber;
            _cellNumber = cellNumber;
        }

        public string HomeNumber
        {
            get => _homeNumber;
            set => _homeNumber = value;
        }

        public string CellNumber
        {
            get => _cellNumber;
            set => _cellNumber = value;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(typeof(Phone) == obj.GetType()))
            {
                return false;
            }

            Phone phone = (Phone) obj;

            return phone.HomeNumber.Equals(_homeNumber);
        }
    }

}
