using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OrderAPI.Models
{
    /**
     * 订单类
     **/
    public class Order : IComparable<Order>
    {
        // 订单ID
        public string OrderId { get; set; }

        // 客户ID
        public string CustomerId { get; set; }

        // 外键关联客户对象
        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }

        // 客户名称，根据关联的客户对象获取
        public string? CustomerName { get => (Customer != null) ? Customer.Name : ""; }

        // 订单创建时间
        public DateTime CreateTime { get; set; }

        // 订单详情列表
        public List<OrderItem> Details { get; set; }

        // 构造函数，初始化订单ID、详情列表和创建时间
        public Order()
        {
            OrderId = Guid.NewGuid().ToString();
            Details = new List<OrderItem>();
            CreateTime = DateTime.Now;
        }

        // 带参数的构造函数，用于初始化订单ID、客户对象和详情列表
        public Order(string orderId, Customer customer, List<OrderItem> items) : this()
        {
            this.OrderId = orderId;
            this.Customer = customer;
            this.Details = items;
        }

        // 计算订单总价
        public double TotalPrice
        {
            get => Details.Sum(item => item.TotalPrice);
        }

        // 添加订单详情
        public void AddDetail(OrderItem orderItem)
        {
            if (Details.Contains(orderItem))
                throw new ApplicationException($"添加错误：订单项{orderItem.GoodsName} 已经存在!");
            Details.Add(orderItem);
        }

        // 移除订单详情
        public void RemoveDetail(OrderItem orderItem)
        {
            Details.Remove(orderItem);
        }

        // 重写 ToString 方法，用于返回订单的字符串表示形式
        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append($"Id:{OrderId}, customer:{Customer},orderTime:{CreateTime},totalPrice：{TotalPrice}");
            Details.ForEach(od => strBuilder.Append("\n\t" + od));
            return strBuilder.ToString();
        }

        // 重写 Equals 方法，用于比较订单对象是否相等
        public override bool Equals(object obj)
        {
            var order = obj as Order;
            return order != null &&
                   OrderId == order.OrderId;
        }

        // 重写 GetHashCode 方法，用于获取订单对象的哈希码
        public override int GetHashCode()
        {
            var hashCode = -531220479;
            hashCode = hashCode * -1521134295 + OrderId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CustomerName);
            hashCode = hashCode * -1521134295 + CreateTime.GetHashCode();
            return hashCode;
        }

        // 实现 IComparable<Order> 接口，用于订单对象的比较
        public int CompareTo(Order other)
        {
            if (other == null) return 1;
            return this.OrderId.CompareTo(other.OrderId);
        }
    }
}
