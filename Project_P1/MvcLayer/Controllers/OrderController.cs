using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using ModelsLibrary;

namespace MvcLayer.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;

        private readonly IOrderLogic _orderLogic;
        //create a constructor into which i'll inject the business layer.
        public OrderController(IOrderLogic orderLogic, ILogger<OrderController> logger)
        {
            this._orderLogic = orderLogic;
            this._logger = logger;
        }
        // GET: OrderController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetOrders()
        {
            //ViewData.Add("CustomerId", id);
            return RedirectToAction("Orders");
        }
        public async Task<ActionResult> Orders(int id)
        {
            List<Order> orders = await _orderLogic.OrderListAsync(id);
            //ViewData.Add("CustomerId", id);
            if(orders.Count()==0)
            {
                return RedirectToAction("Locations", "Shop");
            }
            return View(orders);
        }
        public async Task<ActionResult> StoreHistory(int id)
        {
            List<Order> orders = await _orderLogic.StoreOrderListAsync(id);
            //ViewData.Add("CustomerId", id);
            return View(orders);
        }
        // GET: OrderController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            List<OrderItem> orderItems = await _orderLogic.OrderItemListAsync(id);
            return View(orderItems);
        }

        
    }
}
