using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace Encryption.Model
{
    internal class FSCR
    {
        public int Number;
        public ObservableCollection<StrochkaFSCR> _strochechki = new ObservableCollection<StrochkaFSCR>();
        int N;

        public FSCR(int number, int n) 
        { 
            this.Number = number;
            N = n;
            Run();
        }   

        private void Run()
        {
            Recurs(Number, 0);
        }

        private void Recurs(int number, int transferRegister)
        {
            StrochkaFSCR strocka = new StrochkaFSCR(FillWithZeros(number, N), transferRegister, 0); ;

            for (int i = 0; i < 100; i++)
            {
                int outputBit = number & 1;
                int chtoto = CalculateBitSumModulo(number, strocka.TransferRegister);
                number = CircularShiftAndReplace(number, chtoto, N);
                
                 strocka = new StrochkaFSCR(FillWithZeros(number, N), transferRegister, outputBit);
                _strochechki.Add(strocka);

                transferRegister = CalculateRegister(number, transferRegister);
            }
        }

        public int CalculateBitSumModulo(int number, int transferRegister)
        {
            // Инициализируем переменную для хранения суммы битов
            int bitSum = 0;

            // Проходимся по каждому биту, кроме старшего
            for (int i = 0; i < N - 1; i++)
            {
                // Извлекаем i-й бит и добавляем его к bitSum
                bitSum += (number >> i) & 1;
            }

            return (bitSum + transferRegister) % 2;
        }

        public int CalculateRegister(int number, int transferRegister)
        {
            // Инициализируем переменную для хранения суммы битов
            int bitSum = 0;

            // Проходимся по каждому биту, кроме старшего
            for (int i = 0; i < N - 1; i++)
            {
                // Извлекаем i-й бит и добавляем его к bitSum
                bitSum += (number >> i) & 1;
            }

            return (bitSum + transferRegister) / 2;
        }

        public int CircularShiftAndReplace(int A, int B, int bitSize)
        {
            // Циклический сдвиг вправо для A
            int shiftedA = (A >> 1) | ((A & 1) << (bitSize - 1));

            // Извлечение старшего бита из B
            int mostSignificantBitB = B;

            // Маска для обнуления старшего бита
            int mask = ~(1 << (bitSize - 1));

            // Обнуление старшего бита и установка его значением из B
            int result = (shiftedA & mask) ^  (mostSignificantBitB << (bitSize - 1));

            return result;
        }

        private string FillWithZeros(int number, int N)
        { 

            // Представляем результат в виде строки, содержащей бинарное представление числа
            string binaryString = Convert.ToString(number, 2);

            // Дополняем строку нулями до длины N
            return binaryString.PadLeft(N, '0');
        }
    }
}
