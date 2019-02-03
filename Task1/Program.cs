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
        static string[,] CreateArray(int rows, int columns, string nums)
        {
            var table = new string[rows, columns];
            int chrIndx = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (chrIndx < nums.Length) table[i, j] = nums[chrIndx++].ToString();
                    else table[i, j] = "*";
                }
            }
            return table;
        }
        static bool IsConditionTrue(string first, string second)
        {
            if (first == "*" || second == "*") return false;
            return (first == second || Int32.Parse(first) + Int32.Parse(second) == 10);
        }
        //возвращает false если не было найдено ни одного соседа
        static bool ArrayWalk(string[,] array)
        {
            bool flag = false;           
            for (int i=0; i<array.GetLength(0); i++)
            {
                for(int j=0; j<array.GetLength(1); j++)
                {
                    if(array[i, j] != "*")
                    {
                         if(FindNeighbourInRowAndReplace(array, i, j)) flag=true;
                    }
                }
            }
            return flag;           
        }
        
        static bool GoToTheRight(string [,] array, int row, int curElemColumn)
        {         
            if ((curElemColumn+1)<array.GetLength(1) && array[row, curElemColumn + 1] != "*")
            {
                if (IsConditionTrue(array[row, curElemColumn], array[row, curElemColumn + 1]))
                {
                    array[row, curElemColumn] = "*";
                    array[row, curElemColumn + 1] = "*";
                    return true;
                }
            }
            else
            {
                int k = 2;
                while (curElemColumn + k < array.GetLength(1)-1 && array[row, curElemColumn + k] == "*")
                {
                    k++;
                }
                if (curElemColumn+k < array.GetLength(1) && IsConditionTrue(array[row, curElemColumn], array[row, curElemColumn + k]))
                {
                    array[row, curElemColumn] = "*";
                    array[row, curElemColumn + k] = "*";
                    return true;
                }
            }
            return false;
        }
        static bool GoToTheDown(string [,] array, int row, int column)
        {
            if ((row + 1) < array.GetLength(0) && array[row+1, column] != "*")
            {
                if (IsConditionTrue(array[row, column], array[row+1, column]))
                {
                    array[row, column] = "*";
                    array[row+1, column] = "*";
                    return true;
                }
            }
            else
            {
                int k = 2;
                while (row + k < array.GetLength(0) - 1 && array[row+k, column] == "*")
                {
                    k++;
                }
                if (row + k < array.GetLength(0) && IsConditionTrue(array[row, column], array[row+k, column]))
                {
                    array[row, column] = "*";
                    array[row+k, column] = "*";
                    return true;
                }
            }
            return false;
        }
        static bool FindNeighbourInRowAndReplace(string [,] array, int row, int curElemColumn)
        {             
            return GoToTheRight(array, row, curElemColumn) || GoToTheDown(array, row, curElemColumn);
        }
        //возвращает количество изменений в массиве
        static int OneIteration(string [,] arr)
        {
            bool flag = true;
            int count = 0;
            while (flag)
            {
                //если не будет сделано ни одной замены за весь обход, то цикл прекратится
                flag = ArrayWalk(arr);
                if(flag)
                {
                    //если изменений не было, то не печатаем массив
                    for (int i = 0; i < arr.GetLength(0); i++)
                    {
                        for (int j = 0; j < arr.GetLength(1); j++)
                        {
                            Console.Write(arr[i, j] + " ");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                    //счетчик изменений
                    count++;
                }              
            }
            return count;
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
            
            var builder = new StringBuilder();
            if (OneIteration(arr) > 0)
            {
                do
                {
                    builder.Clear();
                    for (int i = 0; i < arr.GetLength(0); i++)
                    {
                        for (int j = 0; j < arr.GetLength(1); j++)
                        {
                            if (arr[i, j] != "*") builder.Append(arr[i, j]);
                        }
                    }
                    arr = CreateArray(CalcAmountOfRows(builder.ToString().Length, a), a, builder.ToString());

                } while (OneIteration(arr) > 0);
            }

            Console.WriteLine("\nРезультат:");            
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
