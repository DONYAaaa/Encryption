using Encryption.Infastructure;
using Encryption.ViewModel.Base;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.IO;
using System.Diagnostics;
using System.Windows.Documents;
using System.IO.Packaging;
using Encryption.Model.Lab1;

namespace Encryption.ViewModel
{
    internal class MainVM : BaseVM
    {
        private string _pathReadingFile;
        private Dictionary<int, double> _cryptoAnal;
        private Alphabet _alphabet;

        private int _k;
        public int K 
        { 
            get { return _k; } 
            set {
                if (value >= 0 && value <= 50)
                {
                    _k = value;
                    OnPropertyChanged(nameof(K));
                }
            }
        }

        private string _message;

        private string _encryptedMessage;
        public string encryptedMessage
        {
            get { return _encryptedMessage; }
            set { Set(ref _encryptedMessage, value); }
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




        public ICommand GetOpenTextCommand { get; }
        public ICommand GetDecryptCommand { get; }
        public ICommand GetSaveCommand { get; }


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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"Encryption\Data\";
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                // Получаем имя выбранного файла
                string selectedFileName = openFileDialog.FileName;
                _pathReadingFile = selectedFileName;
            }

            ReadFile();

            _startMessage = _message;
            OnPropertyChanged(nameof(startMessage));

            _cryptoMessage = "";
            OnPropertyChanged(nameof(cryptoMessage));

            _alphabet = new Alphabet(32-_k);
            encryptedMessage = _alphabet.EncrypteMessage(_message);
            _alphabet.FillDictionaryProbability(_message);

            
        }


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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"Encryption\Data\";
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                // Получаем имя выбранного файла
                string selectedFileName = openFileDialog.FileName;
                _pathReadingFile = selectedFileName;
            }

            ReadFile();

            _startMessage = _message;
            OnPropertyChanged(nameof(startMessage));

            _alphabet = new Alphabet(_k);
            encryptedMessage = _alphabet.EncrypteMessage(_message);
            _alphabet.FillDictionaryProbability(_message);
            
            _cryptoAnal = new Dictionary<int, double>();
            cryptoAnalisys();
            
            MessageBuild();
            OnPropertyChanged(nameof(cryptoMessage));

            SaveFile();
        }

        private void cryptoAnalisys()
        {
            for (int i = 0; i < 32; i++)
            {
                Alphabet alphabet = new Alphabet(i);

                double summ = 0;
                string cleanedMessage = new string(alphabet.EncrypteMessage(_encryptedMessage).Where(c => !char.IsPunctuation(c) && !char.IsWhiteSpace(c)).ToArray());
                cleanedMessage = new string(cleanedMessage.Distinct().ToArray());
                
                alphabet.FillDictionaryProbability(cleanedMessage);

                foreach (char symbol in cleanedMessage)
                {
                    summ += alphabet.ChiSquaredCalculateForSymbol(symbol); 
                }

                _cryptoAnal.Add(i, summ);
            }
        }

        private void MessageBuild()
        {
            string info = "";

            info += $"   K                              Значение\n";

            foreach (var kvp in _cryptoAnal)
            {
                info += $"{kvp.Key}                                {Math.Round(kvp.Value,2)}\n"; 
            }

            double MaxValue = double.MinValue;
            int KeyOfMaxValue = -1;

            foreach (var kvp in _cryptoAnal)
            {
                if (kvp.Value > MaxValue)
                {
                    MaxValue = kvp.Value;
                    KeyOfMaxValue = kvp.Key;
                }
            }

            info += $"\n";
            info += $"Максимальное значение                                 Его K\n";
            info += $"     {MaxValue}                                        {31 - KeyOfMaxValue}\n";


            _cryptoMessage = info;
        }



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

        private void ReadFile()
        {
            try
            {
                _message = File.ReadAllText(_pathReadingFile);
            }
            catch (Exception e)
            {
                // В случае ошибки выводим сообщение об ошибке
                Console.WriteLine($"Ошибка: {e.Message}");
            }
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

        public MainVM()
        {
            GetOpenTextCommand = new LambdaCommand(OnOpenTextCommand, CanOpenTextCommand);
            GetDecryptCommand = new LambdaCommand(OnDecryptCommand, CanODecryptCommand);
            GetSaveCommand = new LambdaCommand(OnSaveFileCommand, CanSaveFileCommand);
        }
    }
}
