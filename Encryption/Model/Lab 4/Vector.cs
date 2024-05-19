using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption.Model
{
    internal class Vector
    {
        public int x1 { get; set; }
        public int x2 { get; set; }
        public int x3 { get; set; }

        public Vector (int x1, int x2, int x3)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.x3 = x3;
        }

        public Vector Multiplication(int q)
        {
            return new Vector(x1*q, x2*q, x3*q);
        }

        public static Vector Subtraction(Vector a, Vector b)
        {
            return new Vector(a.x1-b.x1, a.x2 - b.x2, a.x3 - b.x3);
        }
    }
}
