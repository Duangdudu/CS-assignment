// See https://aka.ms/new-console-template for more information
using System;
namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("欢迎使用简单计算器！");
            Console.WriteLine("请输入第一个数字:");
            double num1 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("请输入第二个数字:");
            double num2 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("请选择要进行的操作 (+, -, *, /)：");
            char operation = Convert.ToChar(Console.ReadLine());

            double result = 0;

            switch (operation)
            {
                case '+':
                    result = num1 + num2;
                    break;
                case '-':
                    result = num1 - num2;
                    break;
                case '*':
                    result = num1 * num2;
                    break;
                case '/':
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    else
                    {
                        Console.WriteLine("错误：除数不能为零！");
                        return;
                    }
                    break;
                default:
                    Console.WriteLine("错误：无效的操作符！");
                    return;
            }

            Console.WriteLine("结果: " + result);
        }
    }
}