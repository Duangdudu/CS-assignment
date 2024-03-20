// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

//链表节点
public class Node<T>
{
    public T Data { get; set; }
    public Node<T> Next { get; set; }

    public Node(T t)
    {
        Data = t;
        Next = null;
    }
}

// 泛型链表类
public class GenericList<T>
{
    private Node<T> head;

    // 添加元素到链表尾部
    public void Add(T data)
    {
        Node<T> newNode = new Node<T>(data);
        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node<T> current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
    }

    // ForEach方法实现
    public void ForEach(Action<T> action)
    {
        Node<T> current = head;
        while (current != null)
        {
            action(current.Data);
            current = current.Next;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        GenericList<int> myList = new GenericList<int>();
        myList.Add(1);
        myList.Add(2);
        myList.Add(3);
        myList.Add(4);
        myList.Add(5);

        // 打印链表元素
        Console.WriteLine("Printing LinkedList elements:");
        myList.ForEach(data => Console.WriteLine(data));

        // 求和
        int sum = 0;
        myList.ForEach(data => sum += data);
        Console.WriteLine("Sum: " + sum);

        // 求最大值
        int max = int.MinValue;
        myList.ForEach(data => max = Math.Max(max, data));
        Console.WriteLine("Max: " + max);

        // 求最小值
        int min = int.MaxValue;
        myList.ForEach(data => min = Math.Min(min, data));
        Console.WriteLine("Min: " + min);
    }
}
