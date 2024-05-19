using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption.Model
{
    internal class Evklid
    {
        Vector A;
        public Evklid() { }

        public int CalculateNOD(int m, int n) 
        {
            if (m%n == 0) { return n; } 
            else return CalculateNOD(n, m%n);
        }

        public int CalculateUVT(int m, int n)
        {
            Vector U = new Vector(0, 1, n);
            Vector V = new Vector(1, 0, m);

            return UVT(U, V);
        }

        private int UVT(Vector U, Vector V)
        {
            if (V.x3 == 0) return U.x3;
            else
            { 
                Vector T = Vector.Subtraction(U, V.Multiplication((int)(U.x3 / V.x3)));

                U = V; V = T;

                return UVT(U, V);
            }
        }

        public int CalculateA(int m, int n)
        {
            Vector U = new Vector(0, 1, m);
            Vector V = new Vector(1, 0, n);

            int k = 0;
            
            
            int d = AVT(U, V, k);
            if (d > 0)
                return (A.x1 + m) % m;

            else return 0;
        }

        private int AVT(Vector U, Vector V, int k)
        {
            if (U.x3 == 1 && k < 1000)
            {
                A = U;
                return U.x3;
            }

            else if (k > 1000) return 0;

            else
            {
                int q;
                if (V.x3 != 0) { q = U.x3 / V.x3; }
                else q = 0;
                Vector T = Vector.Subtraction(U, V.Multiplication(q));

                U = V; V = T;
                k++;

                return AVT(U, V, k);
            }
        }
    }
}
