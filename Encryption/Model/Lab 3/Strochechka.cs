using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption.Model
{
    internal class Strochechka
    {
        private string _symbols;
        public string Symbol { get => _symbols; set => _symbols = value; }

        private int _kodTen;
        public int KodTen { get => _kodTen; set => _kodTen = value; }

        private string _kodTwo;
        public string KodTwo { get => _kodTwo; set => _kodTwo = value; }

        private int _leftByte = new int();
        public int LeftByte { get => _leftByte; set => _leftByte = value; }

        private int _rightByte = new int();
        public int RightByte { get => _rightByte; set => _rightByte = value; }

        private int _XORByte = new int();
        public int XORByte { get => _XORByte; set => _XORByte = value; }

        public Strochechka(string symbol, int kod10, string kod2, int left, int rifht, int xor) 
        {
            _symbols = symbol;
            _kodTen = kod10;
            _kodTwo = kod2;
            _leftByte = left;
            _rightByte = rifht;
            _XORByte = xor;
        }
    }
}
