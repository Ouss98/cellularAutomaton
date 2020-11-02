using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace _0Assignment1
{
    class NumberToArr
    {
        // Converts decimal values (byte) to char array of size 'size' to display in binary form
        public char [] NumberToCharArr(byte number, int size)
        {
            char[] outputArray = new char[size];
            int i = 0;
            uint mask = 1, res = 0;
            mask = mask << size - 1;
            for (i = size - 1; i >= 0; i--)
            {
                res = number & mask;
                if (res == 0)
                {
                    outputArray[i] = '0';
                }
                else
                {
                    outputArray[i] = '1';
                }
                mask = mask >> 1;
            }
            return outputArray;
        }

        // Converts uint value to uint array of size 'size'
        public uint [] NumberToUintArr(uint number, int size)
        {
            uint[] outputArray = new uint[size];
            int i = 0;
            uint mask = 1, res = 0;
            mask = mask << size - 1;
            for (i = size - 1; i >= 0; i--)
            {
                res = number & mask;
                if (res == 0)
                {
                    outputArray[i] = 0;
                }
                else
                {
                    outputArray[i] = 1;
                }
                mask = mask >> 1;
            }
            return outputArray;
        }
    }
}
