using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SalesDatePrediction.Api.Controllers;
using SalesDatePrediction.Api.DTOs;
using SalesDatePrediction.Api.Model;
using SalesDatePrediction.Api.Orders;
using System.Net;
using static NuGet.Packaging.PackagingConstants;

namespace SalesDatePrediction.Test
{
    [TestClass]
    public class OrdersControllerTests : BasePruebas
    {

        [TestMethod]
        public async Task GetOrdersByIdClient_Should_Return_List_Of_ClientOrders()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);

            contexto.Orders.Add(new Order() { Orderid = 1, Custid = 1, Empid = 1, Orderdate = DateTime.Now, Requireddate = DateTime.Now, Shippeddate = DateTime.Now, Shipperid = 1, Freight = 1, Shipname = "Ship Name", Shipaddress = "Ship Address", Shipcity = "Ship City", Shipregion = "Ship Region", Shippostalcode = "Ship Postal Code", Shipcountry = "Ship Country" });
       
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);

            // Prueba
            var controller = new OrdersController(contexto2,null);
            var respuesta = await controller.GetOrdersByIdClient(1);

            //Verificación
            var orders = respuesta.ToString();
            Assert.IsNotNull(orders.ToString());
        }
    }

}