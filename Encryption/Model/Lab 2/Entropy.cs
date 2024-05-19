using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption.Model
{
    internal static class Entropy
    {
        public static double CalculateEntropy(string message)
        {
            Dictionary<char, int> frequencies = new Dictionary<char, int>();

            // Подсчитываем частоту каждого символа в сообщении
            foreach (char c in message)
            {
                if (frequencies.ContainsKey(c))
                {
                    frequencies[c]++;
                }
                else
                {
                    frequencies[c] = 1;
                }
            }

            // Вычисляем вероятность появления каждого символа
            int totalCharacters = message.Length;
            Dictionary<char, double> probabilities = new Dictionary<char, double>();
            foreach (var kvp in frequencies)
            {
                probabilities[kvp.Key] = (double)kvp.Value / totalCharacters;
            }

            // Вычисляем энтропию                                                
            double entropy = 0;
            foreach (var kvp in probabilities)
            {
                entropy -= kvp.Value * Math.Log(kvp.Value, 2);
            }

            return entropy;
        }
    }
}
