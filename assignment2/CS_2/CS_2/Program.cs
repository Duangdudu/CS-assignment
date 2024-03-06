// See https://aka.ms/new-console-template for more information
using System;
namespace CS_1
{
    class Program
    {   
        //编程求一个整数数组的最大值、最小值、平均值和所有数组元素的和。

        static void MinMaxAverage(int[] a, int num)
        {
            int min = a[0];
            int max = a[0];
            int sum = 0;
            double averge = 0;

            for (int i = 0; i < num; i++)
            {
                sum += a[i];
                if (a[i] > max)
                {
                    max = a[i];
                }

                if (a[i] < min)
                {
                    min = a[i];
                }
            }

            averge = (double)sum / num;
            Console.WriteLine("max:" + max);
            Console.WriteLine("min:" + min);
            Console.WriteLine("sum:" + sum);
            Console.WriteLine("average:" + averge);
        }

        
        static void Main(string[] args)
        {   
            //编程求一个整数数组的最大值、最小值、平均值和所有数组元素的和。
            int[] a = new int[10000];
            Console.WriteLine("\n数组中有多少个数字？");
            int num = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < num; i++)
            {
                a[i] = Convert.ToInt32(Console.ReadLine());
            }
            MinMaxAverage(a, num);
            

        }
    }
}

