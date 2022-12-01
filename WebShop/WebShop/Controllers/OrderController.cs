﻿using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using ModelLayer;
using Newtonsoft.Json;
using WebShop.ServiceLayer;
using WebShop.LogicControllers;

namespace WebShop.Controllers
{
    public class OrderController : Controller
    {
        private OrderLogicController _OrderLogicController = new();
        public IActionResult OrderView()
        {
            ModelOrderView mov = new ModelOrderView();
            mov.orderLines = JsonConvert.DeserializeObject<List<OrderLine>>(HttpContext.Session.GetString("OrderLines"));

            return View(mov);
        }

        [HttpPost]
        public IActionResult OrderView(ModelOrderView _MOV)
        {
            //TODO Add customer information from the textboxes from the checkout page to the object
            _MOV.orderLines = JsonConvert.DeserializeObject<List<OrderLine>>(HttpContext.Session.GetString("OrderLines"));
            _OrderLogicController.AddSaleOrderToDB(_MOV);

            return View(_MOV);
        }
    }
}
