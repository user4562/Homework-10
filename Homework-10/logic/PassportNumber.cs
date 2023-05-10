using System;
using System.Linq;

namespace Homework_10.logic
{
    public struct PassportNumber
    {
        public uint SeriesFirst { get; }
        public uint SeriesLast { get; }
        public uint SerialNumber { get; }

        public PassportNumber(string passportNumber)
        {
            if(passportNumber.Length == 0)
                throw new ArgumentException("Empty passport number.");

            passportNumber = passportNumber.Trim();

            if (!IsOnlyDigit(passportNumber))
                throw new ArgumentException("Wrong phone number format.");

            if (passportNumber.Where(c => c == ' ').Count() != 2)
                throw new ArgumentException("Wrong passport number formst.");

            string[] numbers = passportNumber.Split(' ');

            if (numbers[0].Length != 2 ||
                numbers[1].Length != 2 ||
                numbers[2].Length != 6)
                throw new ArgumentException("Wrong passport number formst.");

            SeriesFirst = Convert.ToUInt32(numbers[0]);
            SeriesLast = Convert.ToUInt32(numbers[1]);
            SerialNumber = Convert.ToUInt32(numbers[2]);
        }

        public PassportNumber(uint seriesFirst, uint seriesLast, uint serialNumber)
        {
            SeriesFirst = IsSeries(seriesFirst);
            SeriesLast = IsSeries(seriesLast);
            SerialNumber = IsSerial(serialNumber);
        }

        public static bool IsPassportNumber(string number)
        {
            try
            {
                new PassportNumber(number);
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

        public string Number { 
            get 
            { 
                return ToString(); 
            } 
            set
            {
                this = new PassportNumber(value);        
            }
        }

        public static PassportNumber Empty
        {
            get
            {
                return new PassportNumber("00 00 000000");
            }
        }


        public override string ToString()
        {
            App app = App.Current as App;

            if(app.CurrentUser.Accesses.Contains(Access.ReadPassport) ||
               app.CurrentUser.Accesses.Contains(Access.ChangePassport))
            {
                return $"{SeriesFirst.ToString().PadLeft(2, '0')} " +
                    $"{SeriesLast.ToString().PadLeft(2, '0')} " +
                    $"{SerialNumber.ToString().PadLeft(6, '0')}";
            }

            return "** ** ******";
        }

        private static uint IsSeries(uint series)
        {
            if (series < 100) return series;
            else throw new ArgumentException("Wrong series number.");
        }
        private static uint IsSerial(uint series)
        {
            if (series < 1_000_000) return series;
            else throw new ArgumentException("Wrong serial number.");
        }

        public static bool operator ==(PassportNumber number1, PassportNumber number2)
        {
            return  number1.SeriesFirst == number2.SeriesFirst &&
                    number1.SeriesLast == number2.SeriesLast &&
                    number1.SerialNumber == number2.SerialNumber;
        }

        public static bool operator !=(PassportNumber number1, PassportNumber number2)
        {
            return !(number1 == number2);
        }
    }
}
