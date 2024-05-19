using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption.Model
{
    internal class StrochkaFSCR
    {
        private string _shiftRegister;
        public string ShiftRegister { get => _shiftRegister; set => _shiftRegister = value; }

        private int _transferRegister;
        public int TransferRegister { get => _transferRegister; set => _transferRegister = value; }

        private int _outputBit;
        public int OutputBit { get => _outputBit; set => _outputBit = value; }

        public StrochkaFSCR (string shiftRegister, int transferRegister, int outputBit)
        {
            _shiftRegister = shiftRegister;
            _transferRegister = transferRegister;
            _outputBit = outputBit;
        }
    }
}
