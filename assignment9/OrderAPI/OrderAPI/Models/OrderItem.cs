using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAPI.Models
{
    /**
     * 订单项类
     **/
    public class OrderItem
    {
        // 订单项ID
        public string Id { get; set; }

        // 序号
        public int Index { get; set; }

        // 商品ID
        public string? GoodsId { get; set; }

        // 外键关联商品对象
        [ForeignKey("GoodsId")]
        public Goods? GoodsItem { get; set; }

        // 商品名称，根据关联的商品对象获取
        public string? GoodsName { get => GoodsItem != null ? this.GoodsItem.Name : ""; }

        // 商品单价，根据关联的商品对象获取
        public double? UnitPrice { get => GoodsItem != null ? this.GoodsItem.Price : 0.0; }

        // 订单ID
        public string? OrderId { get; set; }

        // 商品数量
        public int Quantity { get; set; }

        // 构造函数，初始化订单项ID
        public OrderItem()
        {
            Id = Guid.NewGuid().ToString();
        }

        // 带参数的构造函数，用于初始化序号、商品对象和数量
        public OrderItem(int index, Goods goods, int quantity)
        {
            this.Index = index;
            this.GoodsItem = goods;
            this.Quantity = quantity;
        }

        // 计算订单项总价
        public double TotalPrice
        {
            get => GoodsItem == null ? 0.0 : GoodsItem.Price * Quantity;
        }

        // 重写 ToString 方法，用于返回订单项的字符串表示形式
        public override string ToString()
        {
            return $"[No.:{Index},goods:{GoodsName},quantity:{Quantity},totalPrice:{TotalPrice}]";
        }

        // 重写 Equals 方法，用于比较订单项对象是否相等
        public override bool Equals(object obj)
        {
            var item = obj as OrderItem;
            return item != null &&
                   GoodsName == item.GoodsName;
        }

        // 重写 GetHashCode 方法，用于获取订单项对象的哈希码
        public override int GetHashCode()
        {
            var hashCode = -2127770830;
            hashCode = hashCode * -1521134295 + Index.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(GoodsName);
            hashCode = hashCode * -1521134295 + Quantity.GetHashCode();
            return hashCode;
        }
    }
}
