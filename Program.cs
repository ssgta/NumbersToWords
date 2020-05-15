using System;

namespace NumbersToWords
{
    class Program
    {
        static void Main()
        {
            bool repeat = true;

            while (repeat)
            {
                Console.Write("Input a number: ");
                var inputValue = Console.ReadLine();
                decimal input = Convert.ToDecimal(inputValue); // Changed from decimal.Parse

                Console.WriteLine(NumberToWords(input));
                Console.ReadLine();
            }
        }

        public static string NumberToWords(decimal input) 
        {
            bool decimalNumber = CheckForDecimal(input);
            var integralPart = GetIntegralPart(input);
            var decimalPart = GetDecimalPart(input);

            string result = decimalNumber ? string.Format("{0} Pounds and {1} Pence", integralPart, decimalPart) : string.Format("{0}", integralPart);

            return result;
        }

        private static string GetIntegralPart(decimal input)
        {
            long integralPart = Convert.ToInt64(Math.Floor(input));

            return NumberToWords(integralPart);
        }

        private static string GetDecimalPart(decimal input)
        {
            decimal decimalPart = input % 1 * 100;
            decimalPart = Math.Floor(decimalPart * 100) / 100;
            long decimalAsInt = decimal.ToInt64(decimalPart);

            return NumberToWords(decimalAsInt);
        }

        private static bool CheckForDecimal(decimal input)
        {
            if ((input % 1) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static string NumberToWords(long input)
        {
            string words = "";

            if ((input / 1000000000) > 0) 
            {
                words += NumberToWords((decimal)(input / 1000000000)) + " Billion ";
                input %= 1000000000;
            }

            if ((input / 1000000) > 0)
            {
                words += NumberToWords((decimal)(input / 1000000)) + " Million ";
                input %= 1000000;
            }

            if ((input / 1000) > 0)
            {
                words += NumberToWords((decimal)(input / 1000)) + " Thousand ";
                input %= 1000;
            }

            if ((input / 100) > 0)
            {
                words += NumberToWords((decimal)(input / 100)) + " Hundred ";
                input %= 100;
            }
            
            if (input > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (input < 20)
                    words += unitsMap[input];
                else
                {
                    words += tensMap[input / 10];
                    if ((input % 10) > 0)
                        words += "-" + unitsMap[input % 10];
                }
            }
            return words;
        }
    }
}