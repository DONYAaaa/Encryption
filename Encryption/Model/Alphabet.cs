using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Encryption.Model
{
    internal class Alphabet
    {
        private Dictionary<int, char> _alphabet = new Dictionary<int, char>();
        public Dictionary<int, char> Alphabetic
        {
            get { return _alphabet; }
            set { _alphabet = value; }
        }

        private Dictionary<char, double> _alphabetProbobility = new Dictionary<char, double>();
        public Dictionary<char, double> AlphabeticProbobility
        {
            get { return _alphabetProbobility; }
            set { _alphabetProbobility = value; }
        }

        private Dictionary<char, double> _alphabetRussianProbobility = new Dictionary<char, double>();
        public Dictionary<char, double> AlphabeticRussianProbobility
        {
            get { return _alphabetRussianProbobility; }
            set { _alphabetRussianProbobility = value; }
        }

        private Dictionary<int, char> _encryptedAlphabet = new Dictionary<int, char>();
        public Dictionary<int, char> encryptedAlphabetic
        {
            get { return _encryptedAlphabet; }
            set { _encryptedAlphabet = value; }
        }

        public Alphabet(int k)
        {
            FillDictionary();
            FillEncryptedDictionary(k);
            FillDictionaryRussianProbobility();
        }

        public Alphabet()
        {
            FillDictionary();
        }

        private void FillDictionary()
        {
            for (int i = 0; i < 32; i++)
            {
                char letter = (char)('а' + i);
                _alphabet.Add(i, letter);
            }
        }

        public double ChiSquaredCalculateForSymbol(Char symbol)
        {
           double b = Math.Pow(_alphabetProbobility[symbol] - _alphabetRussianProbobility[symbol], 2) / _alphabetRussianProbobility[symbol];
           return b;
        }

        public void FillDictionaryProbability(string message)
        {
            string cleanedMessage = new string(message.Where(c => !char.IsPunctuation(c) && !char.IsWhiteSpace(c)).ToArray());
            int count = cleanedMessage.Length;

            foreach (char symbol in cleanedMessage)
            {
                if (_alphabetProbobility.ContainsKey(symbol))
                {
                    _alphabetProbobility[symbol]++;
                }
                else
                {
                    _alphabetProbobility.Add(symbol, 1);
                }
            }

        }

        private void FillDictionaryRussianProbobility()
        {
            _alphabetRussianProbobility = new Dictionary<char, double>()
                 {
            {'а', 8.04},
            {'б', 1.59},
            {'в', 4.54},
            {'г', 1.70},
            {'д', 2.98},
            {'е', 8.45},
            {'ё', 0.03},
            {'ж', 1.01},
            {'з', 1.63},
            {'и', 7.33},
            {'й', 1.74},
            {'к', 3.48},
            {'л', 4.32},
            {'м', 3.20},
            {'н', 6.70},
            {'о', 10.97},
            {'п', 2.81},
            {'р', 5.53},
            {'с', 5.45},
            {'т', 6.26},
            {'у', 2.72},
            {'ф', 0.26},
            {'х', 1.01},
            {'ц', 0.48},
            {'ч', 1.44},
            {'ш', 0.72},
            {'щ', 0.36},
            {'ъ', 0.04},
            {'ы', 1.90},
            {'ь', 1.74},
            {'э', 0.42},
            {'ю', 0.64},
            {'я', 2.01}
        };
        }

        private void FillEncryptedDictionary(int k)
        {
            for (int i = 0; i < 32; i++)
            {
                char letter = _alphabet[(i + k)%32];
                _encryptedAlphabet.Add(i, letter);
            }
        }

        public string EncrypteMessage(string message)
        {
            string encryptedMessage = "";
            if (message.Length > 0 && message != null)
            {
                for (int i = 0; i < message.Length; i++)
                {
                    foreach (var item in _alphabet)
                    {
                        char symbol = item.Value;
                        char probel = ' ';

                        if (message[i] == symbol)
                        {
                            encryptedMessage += _encryptedAlphabet[item.Key].ToString();
                            break;
                        }
                        else if (message[i] == probel)
                        {
                            encryptedMessage += " ";
                            break;
                        }
                    }
                }
            }
            return encryptedMessage;
        }



    }
}
