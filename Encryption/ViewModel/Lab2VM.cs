using Encryption.Infastructure;
using Encryption.Model;
using Encryption.ViewModel.Base;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Encryption.ViewModel
{
    internal class Lab2VM : BaseVM
    {
        #region поля и свойства

        private string _pathReadingFile;
        private Alphabet _alphabet;


        private double _entropy;

        public double Entropya
        {
            get { return _entropy; }
            set { Set(ref _entropy, value); }
        }


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


        private string _encryptedMessage;
        public string encryptedMessage
        {
            get { return _encryptedMessage; }
            set { Set(ref _encryptedMessage, value); }
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
            TremiusTable tremiusTable = new TremiusTable();
            _encryptedMessage = tremiusTable.Decrypt(_startMessage);
            OnPropertyChanged(nameof(encryptedMessage));
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
            TremiusTable tremiusTable = new TremiusTable();
            _encryptedMessage = tremiusTable.Encrypt(_startMessage);
            OnPropertyChanged(nameof(encryptedMessage));

            Entropya = Entropy.CalculateEntropy(_encryptedMessage);
            OnPropertyChanged(nameof(Entropya));
        } 

        #endregion


        #region Сохранить

        public ICommand GetSaveCommand { get; }
        private bool CanSaveFileCommand(object p)
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
        private void OnSaveFileCommand(object p)
        {
            SaveFile();
        }
        private void SaveFile()
        {
            string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = "output.txt";
            string filePath = Path.Combine(directoryPath, fileName);

            try
            {
                // Записываем текст в файл
                File.WriteAllText(filePath, _encryptedMessage);
            }
            catch (Exception e)
            {
                throw new Exception("Не удалось сохранить файл");
            }

            try
            {
                // Проверяем, существует ли файл
                if (File.Exists(filePath))
                {
                    // Открываем файл с помощью программы, ассоциированной с его типом
                    Process.Start(filePath);
                }
                else
                {
                    Console.WriteLine($"Файл не найден: {filePath}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка при открытии файла: {e.Message}");
            }
        } 

        #endregion

        #endregion

        public Lab2VM() 
        {
            GetOpenTextCommand = new LambdaCommand(OnOpenTextCommand, CanOpenTextCommand);
            GetDecryptCommand = new LambdaCommand(OnDecryptCommand, CanODecryptCommand);
            GetSaveCommand = new LambdaCommand(OnSaveFileCommand, CanSaveFileCommand);
        }
    }
}
