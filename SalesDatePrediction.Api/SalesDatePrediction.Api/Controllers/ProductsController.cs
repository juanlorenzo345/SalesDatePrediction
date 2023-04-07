using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Api.DTOs;
using SalesDatePrediction.Api.Model;
using SalesDatePrediction.Api.Orders;
using SalesDatePrediction.Api.Prediction;
using static NuGet.Packaging.PackagingConstants;

namespace SalesDatePrediction.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly DBContext context;
        private readonly IConfiguration _ConnectionString;

        public ProductsController(DBContext context, IConfiguration Configuration)
        {
            this.context = context;
            _ConnectionString = Configuration;
        }

        // GET: Listar todos los productos

        [HttpGet("GetProducts")]
        public async Task<ActionResult<List<ProductsDto>>> GetProducts()
        {
            try
            {
                List<ProductsDto> result = new();
                ProductsTask task = new(_ConnectionString.GetConnectionString("DefaultConnection"));
                result = await task.GetProducts();

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
