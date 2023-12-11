using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class System62Dec
    {
        public static string DecToSys62(ulong n)
        {
            string finalResult = string.Empty;

            // char array to store
            // hexadecimal number
            char[] sys62DeciNum = new char[100];

            // counter for hexadecimal number array
            int i = 0;
            while (n != 0)
            {
                // temporary variable to
                // store remainder
                ulong temp = 0;

                // storing remainder in temp
                // variable.
                temp = n % 62;

                // check if temp < 10
                if (temp < 10)
                {
                    sys62DeciNum[i] = (char)(temp + 48);
                    i++;
                }
                if (temp >= 10 && temp < 36)
                {
                    sys62DeciNum[i] = (char)(temp + 87);
                    i++;
                }
                if (temp >= 36 && temp < 62)
                {
                    sys62DeciNum[i] = (char)(temp + 29);
                    i++;
                }

                n = n / 62;
            }

            // printing hexadecimal number
            // array in reverse order
            for (int j = i - 1; j >= 0; j--)
                finalResult += sys62DeciNum[j];

            return finalResult;
        }

        public static ulong Sys62ToDec(string sys62Val)
        {
            int len = sys62Val.Length;

            // Initializing base1 value
            // to 1, i.e 16^0
            ulong base1 = 1;

            ulong dec_val = 0;

            // Extracting characters as
            // digits from last character
            for (int i = len - 1; i >= 0; i--)
            {
                // if character lies in '0'-'9',
                // converting it to integral 0-9
                // by subtracting 48 from ASCII value
                if (sys62Val[i] >= '0' && sys62Val[i] <= '9')
                {
                    dec_val += (ulong)(sys62Val[i] - 48) * base1;

                    // incrementing base1 by power
                    base1 = base1 * 62;
                }

                // if character lies in 'a'-'z' ,
                // converting it to integral
                // 10 - 35 by subtracting 87
                // from ASCII value
                if (sys62Val[i] >= 'a' && sys62Val[i] <= 'z')
                {
                    dec_val += (ulong)(sys62Val[i] - 87) * base1;

                    // incrementing base1 by power
                    base1 = base1 * 62;
                }

                // if character lies in 'A'-'Z' ,
                // converting it to integral
                // 36 - 61 by subtracting 29
                // from ASCII value
                if (sys62Val[i] >= 'A' && sys62Val[i] <= 'Z')
                {
                    dec_val += (ulong)(sys62Val[i] - 29) * base1;

                    // incrementing base1 by power
                    base1 = base1 * 62;
                }
            }
            return dec_val;
        }
    }
}
