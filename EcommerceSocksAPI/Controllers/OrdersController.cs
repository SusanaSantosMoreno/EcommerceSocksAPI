using EcommerceSocksAPI.Models;
using EcommerceSocksAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceSocksAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase {

        Ecommerce_socksRepository repository;

        public OrdersController (Ecommerce_socksRepository repository) { this.repository = repository; }

        [HttpGet]
        [Route("[action]/{userId}")]
        public ActionResult<List<Orders>> GetOrders (int userId) {
            return this.repository.GetOrders(userId);
        }

        [HttpGet]
        [Route("[action]/{orderId}")]
        public ActionResult<Orders> GetOrderById (int orderId) {
            return this.repository.GetOrderById(orderId);
        }

        [HttpGet]
        [Route("[action]/{orderId}")]
        public ActionResult<List<Order_details>> GetOrder_Detail (int orderId) {
            return this.repository.GetOrder_Detail(orderId);
        }

        [HttpPost]
        [Route("[action]")]
        public void AddOrder (Orders order) {
            this.repository.AddOrder(order.Orders_user);
        }

        [HttpPost]
        [Route("[action]")]
        public void AddOrderDetails (Order_details details) {
            this.repository.AddOrderDetails(details.Order_id, details.Product_id, details.Size_id, details.Amount);
        }
    }
}
