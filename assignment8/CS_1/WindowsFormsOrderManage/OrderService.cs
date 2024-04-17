using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WindowsFormsOrderManage
{
    public class OrderService
    {
        public List<Order> OrderList { get; set; }// 这里没有删掉，把它用来接收从数据库读到的数据

        public OrderService()
        {
            ImportFromDB();
        }

        public void AddOrder(Order order)
        {
            OrderList.Add(order);
            //添加订单
            using (var context = new OrderContext())
            {
                context.Orders.Add(order);
                context.SaveChanges();
            }
        }


        public void AddOrderItem(Order order, OrderItem orderItem)
        {
            try
            {
                order.AddOrderItem(orderItem);
                order.AddOrderItem(orderItem);
                long checkId = order.OrderID;
                using (var context = new OrderContext())
                {
                    var checkOrder = context.Orders.FirstOrDefault(p => p.OrderID == checkId);
                    checkOrder.AddOrderItem(orderItem);
                    context.SaveChanges();
                }
            }
            catch(ApplicationException e)
            {
                throw e;
            }
        }

        //根据订单号删除订单
        public void RemoveOrder(long orderID)
        {
            Order order = QueryByOrderID(orderID);

            if(order == null)
            {
                throw new ApplicationException("Error: 找不到订单");
            }
            else
            {
                RemoveOrder(order);
            }
        }

        //根据订单删除订单
        public void RemoveOrder(Order order)
        {
            OrderList.Remove(order);
            long checkId = order.OrderID;
            using (var context = new OrderContext())
            {
                var checkOrder = context.Orders.Include("OrderItems").FirstOrDefault(p => p.OrderID == checkId);
                if (checkOrder != null)
                {
                    context.Orders.Remove(checkOrder);
                    context.SaveChanges();
                }
            }
        }

        public void RemoveOrderItem(Order order, OrderItem orderItem)
        {
            try
            {
                order.RemoveOrderItem(orderItem);
                long checkId = order.OrderID;
                using (var context = new OrderContext())
                {
                    var checkOrder = context.Orders.FirstOrDefault(p => p.OrderID == checkId);
                    if (checkOrder != null)
                    {
                        checkOrder.RemoveOrderItem(orderItem);
                        context.SaveChanges();
                    }
                }
            }
            catch(ApplicationException e)
            {
                throw e;
            }
        }

        //根据订单号和类型修改订单
        //type-1 Modify Customer Name
        //type-2 Modify Address
        //modifiedContent 已修改的内容
        public void ModifyOrder(long orderID, int type, string modifiedContent)
        {
            //处理没有查询到订单的异常
            Order order = QueryByOrderID(orderID);

            if(order == null)
            {
                throw new ApplicationException("Error: No such order");
            }

            switch(type)
            {
                case 1:
                    order.CustomerName = modifiedContent;
                    break;
                case 2:
                    order.Address = modifiedContent;
                    break;
            }
        }

        //根据订单对象和类型修改订单
        //type-1 Modify Customer Name
        //type-2 Modify Address
        //modifiedContent 已修改的内容
        public void ModifyOrder(Order order, int type, string modifiedContent)
        {
            switch(type)
            {
                case 1:
                    order.CustomerName = modifiedContent;
                    break;
                case 2:
                    order.Address = modifiedContent;
                    break;
            }
        }

        //根据订单修改订单
        public void ModifyOrder(Order order, Order newOrder)
        {
            long checkId = order.OrderID;
            order = newOrder;
            using (var context = new OrderContext())
            {
                var checkOrder = context.Orders.Include("OrderItems").FirstOrDefault(p => p.OrderID == checkId);
                if (checkOrder != null)
                {
                    checkOrder.CustomerName = newOrder.CustomerName;
                    checkOrder.Address = newOrder.Address;
                    checkOrder.TotalPrice = newOrder.TotalPrice;

                    List<long> IdsToBeDelete = new List<long>();
                    foreach (OrderItem item in checkOrder.OrderItemList)
                    {
                        IdsToBeDelete.Add(item.OrderItemId);
                    }

                    foreach (int i in IdsToBeDelete)
                    {
                        var item = context.OrderItems.FirstOrDefault(p => p.OrderItemId == i);
                        context.OrderItems.Remove(item);
                    }

                    foreach (OrderItem item in newOrder.OrderItemList)
                    {
                        OrderItem newItem = new OrderItem();
                        newItem.ProductAmount = item.ProductAmount;
                        newItem.ProductName = item.ProductName;
                        newItem.ProductPrice = item.ProductPrice;
                        newItem.OrderItemId = checkId;
                        checkOrder.OrderItemList.Add(newItem);
                    }
                    context.SaveChanges();
                }
            }
        }

        //根据订单号查询订单
        public Order QueryByOrderID(long orderID)
        {

            using (var context = new OrderContext())
            {
                var query = context.Orders.Include("OrderItems").FirstOrDefault(x => x.OrderID == orderID);
                return query;
            }
        }

        //根据商品名称查询订单
        public List<Order> QueryByProductName(string queryContent)
        {
            using (var context = new OrderContext())
            {
                var query = context.Orders.Include("OrderItems").Where(x => x.OrderItemList.Any(y => y.ProductName.Contains(queryContent))).OrderBy(s => s.TotalPrice);
                return query.ToList();
            }
        }

        //根据客户查询订单
        public List<Order> QueryByCustomerName(string queryContent)
        {

            using (var context = new OrderContext())
            {
                var query = context.Orders.Include("OrderItems").Where(x => x.CustomerName.Contains(queryContent)).OrderBy(s => s.TotalPrice);
                return query.ToList();
            }
        }

        //默认排序方法，按照订单号排序
        public void SortOrderList()
        {
            OrderList.Sort((x, y) => (int)(x.OrderID - y.OrderID));
        }

        //使用Lambda表达式进行自定义排序
        //例如: SortOrderList((x,y)=> (int)(x.TotalPrice - y.TotalPrice));
        public void SortOrderList(Func<Order, Order, int> sortAction)
        {
            OrderList.Sort((x, y) => sortAction(x, y));
        }

        //将所有的订单序列化为XML文件
        public void Export(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
            using(FileStream fs = new FileStream(path, FileMode.Create))
            {
                xmlSerializer.Serialize(fs, OrderList);
            }
        }

        //从XML文件中载入订单
        public void Import(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));

            try
            {
                using(FileStream fs = new FileStream(path, FileMode.Open))
                {
                    OrderList = (List<Order>)xmlSerializer.Deserialize(fs);
                }
            }
            catch(FileNotFoundException e)
            {
                throw e;
            }
        }

        //从数据库中载入订单
        public void ImportFromDB()
        {
            using (var context = new OrderContext())
            {
                OrderList = context.Orders.Include("OrderItemList").ToList();
            }
        }

       
    }
}
