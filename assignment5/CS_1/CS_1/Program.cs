using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderManagementSystem
{
    //订单明细
    public class OrderDetail
    {
        public string ProductName { get; set; }//货物名称
        public int Quantity { get; set; }//货物数量
        public double OnePrice { get; set; }//单价
        public override string ToString()
        {
            return $"Product:{ProductName},Quantity:{Quantity},OnePrice:{OnePrice}";
        }
    }

    //订单类
    public class Order
    {
        public int ID { get; set; }
        public string Customer { get; set; }
        public List<OrderDetail> Details { get; set; }//一个订单有多个细节（一次购买多种类别的东西）
        public double TotalPrice=>Details.Sum(detail=>detail.Quantity*detail.OnePrice);//Linq查询语句Sum方法
        public override string ToString()
        {
            return $"Order ID:{ID},Customer:{Customer},TotalPrice:{TotalPrice}";
        }
        public override bool Equals(object? obj)
        {
            if(obj is Order other)
            {
                return this.ID == other.ID;
            }
            return false;
        }



    }


    //订单服务类
    public class OrderService
    {
        private List<Order> orders=new List<Order>();
        //添加订单
        public void AddOrder(Order order)
        {
            if (orders.Contains(order))
            {
                throw new InvalidOperationException("Order already exists!");
            }
            orders.Add(order);//List<T> 类的方法，将元素添加到末尾
        }

        //删除订单
        public void DeleteOrder(int orderid)
        {
            Order order=orders.FirstOrDefault(o => o.ID == orderid);
            if(order != null)
            {
                orders.Remove(order);
            }
            else
            {
                throw new InvalidOperationException("Order not found!");
            }
        }

        //修改订单
        public void ModifyOrder(Order modiorder)
        {
            Order order=orders.FirstOrDefault(o=>o.ID == modiorder.ID);
            if(order != null)
            {
                orders.Remove(order);
                orders.Add(modiorder);//相当于传入一个完整的order，替换掉ID相同的原来的order
            }
            else
            {
                throw new InvalidOperationException("Order not found!");
            }
        }

        //按订单号查询订单
        public Order SearchOrderByID(int orderid)
        {
            return orders.FirstOrDefault(o => o.ID == orderid);
        }

        //按商品名称查询订单
        public List<Order> SearchOrderByProductName(string productName)
        {
            return orders.Where(o=>o.Details.Any(d=>d.ProductName==productName)).OrderBy(o=>o.TotalPrice).ToList();
        }

        //按客户查询订单
        public List<Order> SearchOrderByCustomer(string customerName)
        {
            return orders.Where(o=>o.Customer==customerName).OrderBy(o=>o.TotalPrice).ToList();
        }

        //按订单金额查询订单
        public List<Order> SearchOrderByPrice(double price)
        {
            return orders.Where(o=>o.TotalPrice==price).ToList();
        }

        //对订单进行排序
        public void SortOrders(Func<Order, object> keySelector)
        {
            orders=orders.OrderBy(keySelector).ToList();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
        }
    }

}