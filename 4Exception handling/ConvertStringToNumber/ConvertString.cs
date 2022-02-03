using System;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace ConvertStringToNumber
{
    public static class ConvertString
    {
        public static int ToInt(string wordConvert)
        {
            int a;
            int[] arrayInt = new int[wordConvert.Length];
            
            for(int i = 0 ; i< wordConvert.Length; i++)
            {
                char characterFromString = wordConvert[i];
                a = characterFromString - '0';
                if (a <= 9)
                {
                    arrayInt[i] = a;
                }
                else
                {
                    return 0;
                }
            }

            int number = 0;

            for (int i = 0; i < arrayInt.Length; i++)
            {
                number += arrayInt[i] * (int)Math.Pow(10, arrayInt.Length - 1 - i);
            }

            return number;

            

        }
    }
}
