using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static string CreateString(int n)
        {
            var result = new StringBuilder();
            for (int i=1; i<=n; i++)
            {
                result.Append(i);
            }
            result.Replace("0", "");
            return result.ToString();
        }
        static int CalcAmountOfRows(int strCharCount, int a)
        {
            if (strCharCount <= a) return 1;
            else
            {
                if(strCharCount%a == 0) return strCharCount/a;
                else return strCharCount/a + 1;
            }
        }
        static char[,] CreateArray(int rows, int columns, string nums)
        {
            var table = new char[rows, columns];
            int chrIndx = 0;
            for(int i=0; i<rows; i++)
            {
                for(int j=0; j<columns; j++)
                {
                    if (chrIndx < nums.Length) table[i, j] = nums[chrIndx++];
                }
            }
            return table;
        }
        static void Main(string[] args)
        {
            // a - amount of columns, n - max value
            Console.Write("A = ");
            int a = Int32.Parse(Console.ReadLine());
            Console.Write("N = ");
            int n = Int32.Parse(Console.ReadLine());
            string str = CreateString(n);

            var arr = CreateArray(CalcAmountOfRows(str.Length, a), a, str);            
            for(int i =0; i< arr.GetLength(0); i++)
            {
               for(int j=0; j<arr.GetLength(1); j++)
               {
                    Console.Write(arr[i, j] + " ");
               }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
