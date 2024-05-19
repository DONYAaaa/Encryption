using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Encryption.Model
{
    internal static class ChekingInput
    {
        public static int ProcessInput(string input)
        {
            bool isBinary = IsBinary(input);

            if (isBinary)
            {
                return Convert.ToInt32(input, 2);
            }
            else
            {
                bool isValidNumber = int.TryParse(input, out int number);

                if (isValidNumber)
                {
                    return number;
                }
                else
                {
                    MessageBox.Show("Введено инвалидное значение");
                    return 0;
                }
            }
        }

        private static bool IsBinary(string input)
        {
            if (input.Length > 0)
            {
                foreach (char c in input)
                {
                    if (c != '0' && c != '1')
                    {
                        return false;
                    }
                }
                return true;
            }
            else { return false; }
        }
    }
}
