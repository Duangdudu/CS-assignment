using System;
namespace CS_1
{
    class Program1
    {   //编写程序输出用户指定数据的所有素数因子。
        static List<int> PrimeFactors(int n)
        {
            List<int> factors = new List<int>();
            for (int i = 2; i <= n; i++)
            {
                while (n % i == 0)
                {
                    factors.Add(i);
                    n /= i;
                }
            }
            return factors;
        }

        //编程求一个整数数组的最大值、最小值、平均值和所有数组元素的和。

        static void MinMaxAverage(int[] a,int num)
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

        // 使用埃氏筛法求解 2~100 以内的素数
        static List<int> SieveOfEratosthenes(int n)
        {
            bool[] primes = new bool[n + 1];
            for (int i = 0; i <= n; i++)
                primes[i] = true;

            for (int p = 2; p * p <= n; p++)
            {
                if (primes[p] == true)
                {
                    for (int i = p * p; i <= n; i += p)
                        primes[i] = false;
                }
            }

            List<int> primeNumbers = new List<int>();
            for (int i = 2; i <= n; i++)
            {
                if (primes[i] == true)
                    primeNumbers.Add(i);
            }

            return primeNumbers;
        }

        //托普利茨矩阵
        static bool isMatrix(int[,] matrix) 
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = 0; j < cols - 1; j++)
                {
                    if (matrix[i, j] != matrix[i + 1, j + 1])
                        return false;
                }
            }
            return true;

        }

        static void Main(string[] args)
        {   //编写程序输出用户指定数据的所有素数因子。
            Console.WriteLine("Please input number:");
            bool[] visited = new bool[10000];
            List<int> primeFactor = PrimeFactors(Convert.ToInt32(Console.ReadLine()));
            foreach(int factor in primeFactor)
            {
                if (visited[factor] == false)
                {
                    Console.Write(factor + " ");
                    visited[factor] = true;
                }
                
            }
            //编程求一个整数数组的最大值、最小值、平均值和所有数组元素的和。
            int[] a = new int [10000];
            Console.WriteLine("\n数组中有多少个数字？");
            int num = Convert.ToInt32(Console.ReadLine());
            for(int i = 0; i < num; i++)
            {
                a[i]=Convert.ToInt32(Console.ReadLine());
            }
            MinMaxAverage(a,num);
            //“埃氏筛法”
            Console.WriteLine("\n2~100 以内的素数：");
            List<int> primes = SieveOfEratosthenes(100);
            foreach (int prime in primes)
                Console.Write(prime + " ");
            //托普利茨矩阵
            int[,] matrix = {
            {1, 2, 3},
            {4, 1, 2},
            {5, 4, 1}
        };
            Console.WriteLine($"\n\n判断矩阵是否为托普利茨矩阵：{isMatrix(matrix)}");

        }
    }
}
