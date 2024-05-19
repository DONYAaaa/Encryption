using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption.Model
{
    internal class TremiusTable
    {
        public char[,] TrithemiusTable { get; private set; }

        public TremiusTable() 
        {
            GenerateTrithemiusTable();
        }

        private void GenerateTrithemiusTable()
        {
            char[,] table = new char[32, 32]; // Размерность таблицы для русского алфавита
            char currentChar = 'а';

            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    table[i, j] = currentChar;
                    currentChar = (char)((currentChar - 'а' + 1) % 32 + 'а'); // 33 буквы в русском алфавите
                }
                currentChar = (char)((table[i, 0] - 'а' + 1) % 32 + 'а');
            }

            TrithemiusTable =  table;
        }

        public string Encrypt(string input)
        {
            StringBuilder encryptedText = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];
                if (currentChar >= 'а' && currentChar <= 'я')
                {
                    int row = currentChar - 'а';
                    int col = i % 32; // Индекс по формуле

                    // Добавляем зашифрованный символ к результату
                    encryptedText.Append(TrithemiusTable[col, row]);
                }
                else
                {
                    // Пропускаем символы, не являющиеся русскими буквами
                    encryptedText.Append(currentChar);
                }
            }

            return encryptedText.ToString();
        }

        public string Decrypt(string input)
        {
            StringBuilder encryptedText = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];
                if (currentChar >= 'а' && currentChar <= 'я')
                {
                    int row = (32 -(i % 32))%32; // Индекс по формуле
                    int col = currentChar - 'а';

                    // Добавляем зашифрованный символ к результату
                    encryptedText.Append(TrithemiusTable[row, col]);
                }
                else
                {
                    // Пропускаем символы, не являющиеся русскими буквами
                    encryptedText.Append(currentChar);
                }
            }

            return encryptedText.ToString();
        }

    }
}
