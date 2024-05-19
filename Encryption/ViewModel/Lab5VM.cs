using Encryption.Infastructure;
using Encryption.Model;
using Encryption.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public string startMessage
        {
            get { return _startMessage; }
            set { Set(ref _startMessage, value); }
        }

        private int _N = 32;
        public int N
        {
            get { return _N; }
            set
            {
                if (value >= 0 && value <= 50)
                {
                    _N = value;
                    OnPropertyChanged(nameof(N));
                }
            }
        }

        #endregion

        #region комманды

        #region Расшифровать

        public ICommand GetDecryptCommand { get; }
        private bool CanDecryptCommand(object p)
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

        }

        #endregion

        #endregion


        public Lab5VM()
        {
            GetDecryptCommand = new LambdaCommand(OnDecryptCommand, CanDecryptCommand);
        }
    }
}
