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
    public class ShippersController : ControllerBase
    {
        private readonly DBContext context;
        private readonly IConfiguration _ConnectionString;

        public ShippersController(DBContext context, IConfiguration Configuration)
        {
            this.context = context;
            _ConnectionString = Configuration;
        }

        // GET: Listar todos los transportistas (Shippers)

        [HttpGet("GetShippers")]
        public async Task<ActionResult<List<ShippersDto>>> GetShippers()
        {
            try
            {
                List<ShippersDto> result = new();
                ShippersTask task = new(_ConnectionString.GetConnectionString("DefaultConnection"));
                result = await task.GetShippers();

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
