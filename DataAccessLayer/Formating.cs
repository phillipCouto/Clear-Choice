using System;
using System.Text.RegularExpressions;

namespace Stemstudios.DataAccessLayer
{
    /// <summary>
    /// This class houses functions for performing common regular expression checks
    /// </summary>
    public class Formating
    {
        /// <summary>
        /// Checks if password is between 6-16 characters and if there is one lower case, one upper case and digit.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean PasswordCheck(String value)
        {
            Regex exp = new Regex("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,16}$");
            return exp.IsMatch(value);
        }
        /// <summary>
        /// Checks if the Value is only alphabetic Characters.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean NameCheck(String value)
        {
            Regex exp = new Regex("[^a-zA-Z]");
            if (exp.IsMatch(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Checks if the Value is only alphanumaric Characters.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean ItemIDCheck(String value)
        {
            Regex exp = new Regex("[^a-zA-Z0-9\\.\\-/\\s\\+]");
            if (exp.IsMatch(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Checks to see if the text fits a Title format which contains basic puncutation characters and alphanumeric characters
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean TitleCheck(String value)
        {
            Regex exp = new Regex("[';]");
            return !exp.IsMatch(value);
        }
       
        /// <summary>
        /// Checks to see if the Value meets the (555) 555-5555 format
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean PhoneNumberCheck(String value)
        {
            foreach (Char c in value.ToCharArray())
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
       
        /// <summary>
        /// Checks to see if String matches A0A 0A0 postal code format
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean PostalCodeCheck(String value)
        {
            Regex exp = new Regex("\\w\\d\\w\\s\\d\\w\\d");
            return exp.IsMatch(value);
        }

        /// <summary>
        /// Checks to see if email address fits valid form of an email address
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean EmailAddressCheck(String value)
        {
            Regex exp = new Regex("\\b[A-Z0-9._%+-]+@(?:[A-Z0-9-]+\\.)+[A-Z]{2,4}\\b");
            return exp.IsMatch(value.ToUpper());
        }
    }
}
