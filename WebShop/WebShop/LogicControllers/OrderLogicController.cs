﻿using Microsoft.AspNetCore.Identity;
﻿using WebShop.Models;
using Newtonsoft.Json;
using WebShop.ServiceLayer;

namespace WebShop.LogicControllers
{
    public class OrderLogicController : IOrderLogicController
    {
        private DBSaleOrderAccess DBSaleOrderAccess = new();
        private OrderAddress orderAddress = new OrderAddress();
        public SaleOrder CreateSaleOrder(SaleOrder mov, IdentityUser user)
        {
            SaleOrder saleOrder = new SaleOrder();
            orderAddress = CreateOrderAddress(mov);
            saleOrder.OrderAddress = orderAddress;
            saleOrder.orderLines = mov.orderLines;
            saleOrder.customer = CustomerLogicController.CreateCustomerFromModelOrderView(mov, user);
            //saleOrder.customer.CustomerNo = 2;
            return saleOrder;
        }


        public void AddSaleOrderToDB(SaleOrder mov, IdentityUser user)
        {
            DBSaleOrderAccess.addSaleOrder(ConvertSaleOrderToJson(CreateSaleOrder(mov, user)),user);

        }

        public string ConvertSaleOrderToJson(SaleOrder saleOrder)
        {
            string jsonString;

            jsonString = JsonConvert.SerializeObject(saleOrder);

            return jsonString;
        }

        private OrderAddress CreateOrderAddress(ModelOrderView mov)
        {
            orderAddress.Street = mov.customer.Street;
            orderAddress.streetNo = mov.customer.StreetNo;
            orderAddress.zipcode = mov.customer.ZipCode;
            orderAddress.city = mov.customer.City;
            return orderAddress;
        }
    }
}
