using Encryption.Infastructure;
using Encryption.Model;
using Encryption.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Encryption.ViewModel
{
    internal class Lab5VM : BaseVM
    {
        #region поля и свойства
        public ObservableCollection<StrochkaFSCR> Strochechki = new ObservableCollection<StrochkaFSCR>();

        private string _startMessage;
        public int startMessage
        {
            get { if (_startMessage != null) return ChekingInput.ProcessInput(_startMessage); else return 0; }
            set { Set(ref _startMessage, value.ToString()); }
        }

        private int _N = 32;
        public int N
        {
            get { return _N; }
            set
            {
                if (value >= 3 && value <= 50)
                {
                    _N = value;
                    OnPropertyChanged(nameof(N));
                }
            }
        }

        #endregion

        #region комманды

        #region Расшифровать

        public ICommand GetCalculateNODCommand { get; }
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
            Strochechki.Clear();
            FSCR fSCR = new FSCR(startMessage, N);
            foreach (var item in fSCR._strochechki)
            {
                Strochechki.Add(item);
            }
            OnPropertyChanged(nameof(Strochechki));
        }

        #endregion



        #endregion


        public Lab5VM()
        {
            GetCalculateNODCommand = new LambdaCommand(OnDecryptCommand, CanODecryptCommand);
        }
    }
}
