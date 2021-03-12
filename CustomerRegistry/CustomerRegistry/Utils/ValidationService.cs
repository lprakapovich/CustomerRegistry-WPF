using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CustomerRegistry.Utils
{
    public class ValidationService 
    {
        public static bool IsValid(string property, string value, out ICollection<string> validationErrors)
        {
            validationErrors = new List<string>();

            Action<string, string, ICollection<string>> validation = null;

            switch (property)
            {
                case "FirstName":
                    validation = ValidateString;
                    break;

                case "LastName":
                    validation = ValidateString;
                    break;

                case "HomePhone":
                    validation = ValidateNumber; 
                    break;

                case "CellPhone":
                    validation = ValidateNumber;
                    break;

                case "WorkingEmail":
                    validation = ValidateEmail;
                    break;

                case "PrivateEmail":
                    validation = ValidateEmail;
                    break;

                case "Street":
                    validation = ValidateStringOrNumber;
                    break;

                case "City":
                    validation = ValidateString; 
                    break;

                case "PostalCode":
                    validation = ValidateStringOrNumber;
                    break;
            }

            validation?.Invoke(property, value, validationErrors);

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

        private static void ValidateStringOrNumber(string property, string value, ICollection<string> errors)
        {
            if (!ContainsCharsAndNumbersOnly(value))
            {
                errors.Add($"{property} should contain letters and digits only.");
            }
        }
             
        private static bool ContainsCharsOnly(string sequence)
        {
            return GetChars(sequence).All(char.IsLetter);
        }

        private static bool ContainsNumbersOnly(string sequence)
        {
            return GetChars(sequence).All(char.IsDigit);
        }

        private static bool ContainsCharsAndNumbersOnly(string sequence)
        {
            return GetChars(sequence).All(char.IsLetterOrDigit);
        }

        private static char[] GetChars(string sequence)
        {
            return sequence.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray();
        }
        private static void ValidateEmail(string property, string value, ICollection<string> errors)
        {
            bool isEmail = Regex.IsMatch(value,
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", 
                RegexOptions.IgnoreCase);

            if (!isEmail)
            {
                errors.Add($"{property} doesn't match email regex.");
            }
        }
    }
}
