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
using Encryption.Model;
using System.IO;
using System.Diagnostics;

namespace Encryption.ViewModel
{
    internal class MainVM : BaseVM
    {
        private string _pathReadingFile;
        private Alphabet _alphabet;

        private int _k;
        public int K 
        { 
            get { return _k; } 
            set { 
                    if (value > 0) 
                    { 
                        Set(ref _k, value); 
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




        public ICommand GetOpenTextCommand { get; }
        public ICommand GetSaveCommand { get; }

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

            _alphabet = new Alphabet(_k);
            encryptedMessage = _alphabet.EncrypteMessage(_message);
            SaveFile();
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
                Console.WriteLine($"Ошибка при записи в файл: {e.Message}");
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
            GetSaveCommand = new LambdaCommand(OnSaveFileCommand, CanSaveFileCommand);
        }
    }
}
