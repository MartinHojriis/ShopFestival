﻿using ModelLayer;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace WebShop.LogicControllers
{
    public class OrderLineLogicController : IOrderLineLogicController
    {
        private List<OrderLine> OrderLines;
        public string CreateNewOrderlines(OrderLine orderLine)
        {
            OrderLines = new List<OrderLine>();
            OrderLines.Add(orderLine);
            //OrderLines.Add(new OrderLine() { Product=  new Product() { Price= 22, Title = "et eller andet", id= 2}, Quantity = 22 }); Dette er tilføjer så der kunne testes
            string JsonOrderLines = JsonConvert.SerializeObject(OrderLines);
            return JsonOrderLines;
        }


        public string AddToExcistingOrderLines(string JsonOrderLines, OrderLine orderLine)
        {
            OrderLines = JsonConvert.DeserializeObject<List<OrderLine>>(JsonOrderLines);
            bool QuantityWasUpdatede= false;
            foreach (var item in OrderLines)
            {
                if (orderLine.Product.id == item.Product.id)
                {
                    item.Quantity += orderLine.Quantity;
                    QuantityWasUpdatede= true;

                }
            }

            if (QuantityWasUpdatede == false)
            {
                OrderLines.Add(orderLine);
            }

            string JsonOrderLinesToReturn = JsonConvert.SerializeObject(OrderLines);
            return JsonOrderLinesToReturn;
        }

        public string CheckExistingOrderLine(HttpContext http, OrderLine orderLine)
        {
            string json;
            if (http.Session.GetString("OrderLines") == null)
            {
                json = CreateNewOrderlines(orderLine);
            }
            else
            {
                string JsonOrderlines = http.Session.GetString("OrderLines");
                json = AddToExcistingOrderLines(JsonOrderlines, orderLine);

            }
            http.Session.SetString("OrderLines", json);
            return json;
        }
    }
}
