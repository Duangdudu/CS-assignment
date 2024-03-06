// See https://aka.ms/new-console-template for more information
using System;
namespace CS_1
{
    class Program
    {   

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

        
        static void Main(string[] args)
        {  
            //“埃氏筛法”
            Console.WriteLine("\n2~100 以内的素数：");
            List<int> primes = SieveOfEratosthenes(100);
            foreach (int prime in primes)
                Console.Write(prime + " ");
            
        }
    }
}
