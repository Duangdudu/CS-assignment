using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderAPI.Models;

// ���������ռ�
namespace OrderAPI.Controllers
{
    // �����������࣬��ָ��·��
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService orderService;

        // ��������Ĺ��캯��������һ�� OrderService ʵ��
        public OrderController(OrderService orderService)
        {
            this.orderService = orderService;
        }

        // ��ȡ���ж����ķ���
        [HttpGet]
        public ActionResult<List<Order>> GetOrders()
        {
            return orderService.GetAllOrders();
        }

        // ���ݶ��� ID ��ȡ���������ķ���
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(string id)
        {
            var order = orderService.GetOrder(id);
            return (order == null) ? NotFound() : order;
        }

        // ��Ӷ����ķ���
        [HttpPost]
        public ActionResult<Order> AddOrder(Order order)
        {
            try
            {
                order.OrderId = Guid.NewGuid().ToString(); // ���ɶ��� ID
                orderService.AddOrder(order); // ���� OrderService �е���Ӷ�������
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message); // ���ش�����Ϣ
            }

            return order; // ������ӵĶ���
        }

        // ���¶����ķ���
        [HttpPut("{id}")]
        public ActionResult<Order> UpdateOrder(string id, Order order)
        {
            if (id != order.OrderId) // ��鴫��Ķ��� ID �Ƿ��� URL �е� ID ƥ��
            {
                return BadRequest(); // ��ƥ���򷵻ش���
            }
            try
            {
                orderService.UpdateOrder(order); // ���� OrderService �еĸ��¶�������
            }
            catch (Exception e)
            {
                string error = e.Message;
                if (e.InnerException != null) error = e.InnerException.Message;
                return BadRequest(error); // ���ش�����Ϣ
            }
            return NoContent(); // �ɹ����º󷵻�������
        }

        // ɾ�������ķ���
        [HttpDelete("{id}")]
        public ActionResult<Order> DeleteOrder(string id)
        {
            try
            {
                orderService.RemoveOrder(id); // ���� OrderService �е�ɾ����������
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message); // ���ش�����Ϣ
            }
            return NoContent(); // �ɹ�ɾ���󷵻�������
        }

    }
}
