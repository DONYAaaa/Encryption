using Encryption.Infastructure;
using Encryption.Model;
using Encryption.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Encryption.ViewModel
{
    internal class Lab3VM : BaseVM
    {
        #region поля и свойства
        public ObservableCollection<Strochechka> Strochechki = new ObservableCollection<Strochechka>();
        private Dictionary<char, string> symbols = new Dictionary<char, string>();

        private string _startMessage;
        public string startMessage
        {
            get { return _startMessage; }
            set { Set(ref _startMessage, value); }
        }


        private string _cryptoMessage;
        public string cryptoMessage
        {
            get { return _cryptoMessage; }
            set { Set(ref _cryptoMessage, value); }
        }

        #endregion

        #region комманды

        #region Расшифровать

        public ICommand GetDecryptCommand { get; }
        private bool CanODecryptCommand(object p)
        {
            if (true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void OnDecryptCommand(object p)
        {
            symbols = GetUniqueChars(_startMessage);
            RunMainCreateDe();
            RunEncript();
        }

        private void RunMainCreateDe()
        {
            Strochechki.Clear();
            foreach (var item in symbols)
            {
                string kod2 = Convert.ToString(item.Key, 2).PadLeft(16, '0');
                int left = item.Key >> 8;
                int right = item.Key & 0xff;
                int xor = left ^ right;
                Strochechki.Add(new Strochechka(item.Key.ToString(), item.Key, kod2, left, right, xor));
                symbols[item.Key] = ((char)((xor << 8) | left)).ToString();
            }
        }

        #endregion


        #region  зашифровать

        public ICommand GetOpenTextCommand { get; }
        private bool CanOpenTextCommand(object p)
        {
            if (true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void OnOpenTextCommand(object p)
        {
            symbols = GetUniqueChars(_startMessage);
            RunMainCreate();
            RunEncript();
        }

        private void RunMainCreate()
        {
            Strochechki.Clear();
            foreach (var item in symbols)
            {
                string kod2 = Convert.ToString(item.Key, 2).PadLeft(16, '0');
                int left = item.Key >> 8;
                int right = item.Key & 0xff;
                int xor = left ^ right;
                Strochechki.Add(new Strochechka(item.Key.ToString(), item.Key, kod2, left, right, xor));
                symbols[item.Key] = ((char)((right << 8) | xor)).ToString();
            }
        }

        private void RunEncript()
        {
            string message = startMessage;
            _cryptoMessage = "";

            foreach (var item in message)
            {
                _cryptoMessage += symbols[item].ToString();
            }

            OnPropertyChanged(nameof(cryptoMessage));
        }

        #endregion

        #endregion

        static Dictionary<char, string> GetUniqueChars(string input)
        {
            Dictionary<char, string> uniqueChars = new Dictionary<char, string>();

            foreach (char c in input)
            {
                if (!uniqueChars.ContainsKey(c))
                {
                    uniqueChars.Add(c, "");
                }
            }

            return uniqueChars;
        }


        public Lab3VM()
        {
            GetOpenTextCommand = new LambdaCommand(OnOpenTextCommand, CanOpenTextCommand);
            GetDecryptCommand = new LambdaCommand(OnDecryptCommand, CanODecryptCommand);
        }
    }
}
