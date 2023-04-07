using Newtonsoft.Json;

namespace SalesDatePrediction.Api.DTOs
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }

        public override string ToString()
        {
            StatusCode = 400;
            return JsonConvert.SerializeObject(this);
        }
    }
}
