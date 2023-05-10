using System;
using System.Linq;

namespace Homework_10.logic
{
    public struct PhoneNumber
    {
        public bool Plus { get; }
        public uint CountryCode { get; }
        public uint OperatorCode { get; }
        public uint BaseNumber { get; }

        public string Number 
        { 
            get 
            { 
                return ToString(); 
            }
            set
            {
                this = new PhoneNumber(value);
            }
        }

        public PhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length == 0)
                throw new ArgumentException("Empty phone number.");

            phoneNumber = phoneNumber.Trim();

            Plus = phoneNumber[0] == '+';

            if (!IsOnlyDigit(phoneNumber))
                throw new ArgumentException("Wrong phone number format.");

            phoneNumber = GetOnlyDigit(phoneNumber);

            if (phoneNumber.Length < 11 || phoneNumber.Length > 13)
                throw new ArgumentException("Wrong phone number format.");

            string countryCode = phoneNumber.Substring(0, phoneNumber.Length - 10);
            string operatorCode = phoneNumber.Substring(phoneNumber.Length - 10, 3);
            string baseNumber = phoneNumber.Substring(phoneNumber.Length - 7);

            CountryCode = Convert.ToUInt32(countryCode);
            OperatorCode = Convert.ToUInt32(operatorCode);
            BaseNumber = Convert.ToUInt32(baseNumber);
        }

        public static bool IsPhoneNumber(string number)
        {
            try
            {
                new PhoneNumber(number);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsOnlyDigit(string phoneNumber)
        {
            foreach (char symbol in phoneNumber)
            {
                if (char.IsLetter(symbol)) return false;
            }

            return true;
        }

        private static string GetOnlyDigit(string phoneNumber)
        {
            string result = "";

            foreach (char symbol in phoneNumber)
            {
                if (char.IsDigit(symbol)) result += symbol;
            }

            return result;
        }

        public static PhoneNumber Empty 
        {
            get
            {
                return new PhoneNumber("0(000)000-00-00");
            }
        }

        public override string ToString()
        {
            App app = App.Current as App;

            if (app.CurrentUser.Accesses.Contains(Access.ReadPhone) ||
                app.CurrentUser.Accesses.Contains(Access.ChangePhone))
            {
                string baseNumber = BaseNumber.ToString().PadLeft(7, '0');

                return $"{(Plus ? "+" : "")}" +
                       $"{CountryCode}" +
                       $" ({OperatorCode.ToString().PadLeft(3, '0')}) " +
                       $"{baseNumber.Substring(0, 3)}-" +
                       $"{baseNumber.ToString().Substring(3, 2)}-" +
                       $"{baseNumber.ToString().Substring(5)}";
            }

            return "* (***) ***-**-**";       
        }

        public static bool operator ==(PhoneNumber number1, PhoneNumber number2)
        {
            return number1.Number == number2.Number;
        }

        public static bool operator !=(PhoneNumber number1, PhoneNumber number2)
        {
            return !(number1 == number2);
        }

    }
}
