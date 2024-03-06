// See https://aka.ms/new-console-template for more information
using System;
namespace CS_1
{
    class Program
    {   
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
        {   
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

