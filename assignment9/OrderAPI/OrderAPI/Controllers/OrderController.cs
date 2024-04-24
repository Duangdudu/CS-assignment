using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderAPI.Models;

// 声明命名空间
namespace OrderAPI.Controllers
{
    // 声明控制器类，并指定路由
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService orderService;

        // 控制器类的构造函数，接受一个 OrderService 实例
        public OrderController(OrderService orderService)
        {
            this.orderService = orderService;
        }

        // 获取所有订单的方法
        [HttpGet]
        public ActionResult<List<Order>> GetOrders()
        {
            return orderService.GetAllOrders();
        }

        // 根据订单 ID 获取单个订单的方法
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(string id)
        {
            var order = orderService.GetOrder(id);
            return (order == null) ? NotFound() : order;
        }

        // 添加订单的方法
        [HttpPost]
        public ActionResult<Order> AddOrder(Order order)
        {
            try
            {
                order.OrderId = Guid.NewGuid().ToString(); // 生成订单 ID
                orderService.AddOrder(order); // 调用 OrderService 中的添加订单方法
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message); // 返回错误信息
            }

            return order; // 返回添加的订单
        }

        // 更新订单的方法
        [HttpPut("{id}")]
        public ActionResult<Order> UpdateOrder(string id, Order order)
        {
            if (id != order.OrderId) // 检查传入的订单 ID 是否与 URL 中的 ID 匹配
            {
                return BadRequest(); // 不匹配则返回错误
            }
            try
            {
                orderService.UpdateOrder(order); // 调用 OrderService 中的更新订单方法
            }
            catch (Exception e)
            {
                string error = e.Message;
                if (e.InnerException != null) error = e.InnerException.Message;
                return BadRequest(error); // 返回错误信息
            }
            return NoContent(); // 成功更新后返回无内容
        }

        // 删除订单的方法
        [HttpDelete("{id}")]
        public ActionResult<Order> DeleteOrder(string id)
        {
            try
            {
                orderService.RemoveOrder(id); // 调用 OrderService 中的删除订单方法
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message); // 返回错误信息
            }
            return NoContent(); // 成功删除后返回无内容
        }

    }
}
