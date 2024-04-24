using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;

namespace OrderAPI.Models
{
    /**
     * 订单管理服务类
     **/
    public class OrderService
    {
        OrderContext dbContext;

        // 构造函数，接受 OrderContext 实例
        public OrderService(OrderContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // 获取所有订单
        public List<Order> GetAllOrders()
        {
            return dbContext.Orders
                .Include(o => o.Details)
                .ThenInclude(d => d.GoodsItem)
                .Include(o => o.Customer)
                .ToList<Order>();
        }

        // 根据订单ID获取订单
        public Order GetOrder(string id)
        {
            return dbContext.Orders
                .Include(o => o.Details)
                .ThenInclude(d => d.GoodsItem)
                .Include(o => o.Customer)
                .SingleOrDefault(o => o.OrderId == id);
        }

        // 添加订单
        public void AddOrder(Order order)
        {
            FixOrder(order); // 修复订单对象，避免级联添加或修改Customer和Goods
            dbContext.Entry(order).State = EntityState.Added; // 将订单对象标记为添加状态
            dbContext.SaveChanges(); // 保存更改
        }

        // 删除订单
        public void RemoveOrder(string orderId)
        {
            var order = dbContext.Orders
                .Include(o => o.Details)
                .SingleOrDefault(o => o.OrderId == orderId);
            if (order == null) return;
            dbContext.OrderDetails.RemoveRange(order.Details); // 移除订单项
            dbContext.Orders.Remove(order); // 移除订单
            dbContext.SaveChanges(); // 保存更改
        }

        // 根据商品名称查询订单
        public List<Order> QueryOrdersByGoodsName(string goodsName)
        {
            var query = dbContext.Orders
                .Include(o => o.Details)
                .ThenInclude(d => d.GoodsItem)
                .Include(o => o.Customer)
                .Where(order => order.Details.Any(item => item.GoodsItem.Name == goodsName));
            return query.ToList();
        }

        // 根据客户名称查询订单
        public List<Order> QueryOrdersByCustomerName(string customerName)
        {
            return dbContext.Orders
                .Include(o => o.Details)
                .ThenInclude(d => d.GoodsItem)
                .Include("Customer")
                .Where(order => order.Customer.Name == customerName)
                .ToList();
        }

        // 更新订单
        public void UpdateOrder(Order newOrder)
        {
            RemoveOrder(newOrder.OrderId); // 先移除原订单
            AddOrder(newOrder); // 添加新订单
        }

        // 修复订单对象，避免级联添加或修改Customer和Goods
        private static void FixOrder(Order newOrder)
        {
            if (newOrder.Customer != null)
            {
                newOrder.CustomerId = newOrder.Customer.Id;
            }
            newOrder.Customer = null;
            newOrder.Details.ForEach(d =>
            {
                if (d.GoodsItem != null)
                {
                    d.GoodsId = d.GoodsItem.Id;
                }
                d.GoodsItem = null;
            });
        }

        // 导出订单数据到 XML 文件
        public void Export(String fileName)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                xs.Serialize(fs, GetAllOrders());
            }
        }

        // 从 XML 文件导入订单数据
        public void Import(string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                List<Order> temp = (List<Order>)xs.Deserialize(fs);
                temp.ForEach(order =>
                {
                    if (dbContext.Orders.SingleOrDefault(o => o.OrderId == order.OrderId) == null)
                    {
                        FixOrder(order);
                        dbContext.Orders.Add(order);
                    }
                });
                dbContext.SaveChanges();
            }
        }

        // 根据订单总金额查询订单
        public object QueryByTotalAmount(float amount)
        {
            return dbContext.Orders.Include(o => o.Details).ThenInclude(d => d.GoodsItem).Include("Customer")
                .Where(order => order.Details.Sum(d => d.Quantity * d.GoodsItem.Price) > amount)
                .ToList();
        }
    }
}
