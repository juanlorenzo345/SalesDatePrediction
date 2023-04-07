using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Api.DTOs;
using SalesDatePrediction.Api.Model;
using SalesDatePrediction.Api.Prediction;
using static NuGet.Packaging.PackagingConstants;

namespace SalesDatePrediction.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly DBContext context;
        private readonly IConfiguration _ConnectionString;

        public CustomersController(DBContext context, IConfiguration Configuration)
        {
            this.context = context;
            _ConnectionString = Configuration;
        }

        // GET: Listar clientes con fecha de ultima orden y fecha de posible orden

        [HttpGet("GetCustomerOrders")]
        public async Task<ActionResult<List<CustomerOrderDto>>> GetCustomerOrders()
        {
            try
            {
                List<CustomerOrderDto> result = new();
                SalesPredictionTask task = new(_ConnectionString.GetConnectionString("DefaultConnection"));
                result = await task.GetCustomerOrders();

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
