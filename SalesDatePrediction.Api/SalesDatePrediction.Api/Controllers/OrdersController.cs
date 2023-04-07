using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Api.DTOs;
using SalesDatePrediction.Api.Model;
using SalesDatePrediction.Api.Orders;
using SalesDatePrediction.Api.Task.Filter;

namespace SalesDatePrediction.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly DBContext context;
        private readonly IConfiguration _ConnectionString;
        private OrdersTask ordersTask;

        public OrdersController(DBContext context, IConfiguration Configuration)
        {
            this.context = context;
            _ConnectionString = Configuration;
        }

        public OrdersController(OrdersTask ordersTask)
        {
            this.ordersTask = ordersTask;
        }

        // GET: Listar ordenes por cliente

        [HttpGet("GetOrdersByIdClient/{custid}")]
        public async Task<ActionResult<List<ClientOrderDTO>>> GetOrdersByIdClient(int custid)
        {
            try
            {
                List<ClientOrderDTO> result = new();
                OrdersTask task = new(_ConnectionString.GetConnectionString("DefaultConnection"));
                result = await task.GetOrdersByIdClient(custid);

                return result;

            }
            catch (Exception ex)
            {
                var err = new ErrorDetails()
                {
                    StatusCode = 500,
                    Message = "Error:  " + ex.Message.ToString(),
                    StackTrace = ex.StackTrace?.ToString()
                };
                    
                return new JsonResult(err);
            }
        }

        // POST: Crear una orden nueva con un producto

        [HttpPost("AddNewOrder")]
        public async Task<ActionResult<List<ClientOrderDTO>>> AddNewOrder([FromBody] OrderDto order)
        {
            try
            {
                List<ClientOrderDTO> result = new();
                OrdersTask task = new(_ConnectionString.GetConnectionString("DefaultConnection"));
                result = await task.AddNewOrder(order);

                return result;

            }
            catch (Exception ex)
            {
                var err = new ErrorDetails()
                {
                    StatusCode = 500,
                    Message = "Error:  " + ex.Message.ToString(),
                    StackTrace = ex.StackTrace?.ToString()
                };

                return new JsonResult(err);
            }
        }
    }
}
