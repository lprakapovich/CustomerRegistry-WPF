using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistry.Utils
{
    public class ValidationService 
    {
        public static bool IsValid(string property, string value, out ICollection<string> validationErrors)
        {
            validationErrors = new List<string>();
            int count = 0;

            switch (property)
            {
                case "FirstName":
                    ValidateString(property, value, validationErrors); 
                    break;

                case "LastName":
                    ValidateString(property, value, validationErrors);
                    break;

                case "HomePhone":
                    ValidateNumber(property, value, validationErrors);
                    break;

                case "CellPhone":
                    break;

                case "WorkingEmail":
                    break;

                case "PrivateEmail":
                    break;

                case "Street":
                    break;

                case "City":
                    break;

                case "PostalCode":
                    break;
            }

            return validationErrors.Count == 0;
        }

        private static void ValidateString(string property, string value, ICollection<string> errors)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                errors.Add($"{property} cannot be blank!");
            }
            else if (!ContainsCharsOnly(value))
            {
                errors.Add($"{property} must contain only characters.");
            }
        }

        private static void ValidateNumber(string property, string value, ICollection<string> errors)
        {
            if (!ContainsNumbersOnly(value))
            {
                errors.Add($"{property} can contain digits [0...9] only!");
            }
        }
             
        private static bool ContainsCharsOnly(string sequence)
        {
            return sequence.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray().All(Char.IsLetter);
        }

        private static bool ContainsNumbersOnly(string sequence)
        {
            return sequence.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray().All(Char.IsDigit);
        }
    }
}
