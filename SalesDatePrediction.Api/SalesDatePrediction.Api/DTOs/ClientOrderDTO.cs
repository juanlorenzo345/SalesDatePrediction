using SalesDatePrediction.Api.Model;
using System.Net;

namespace SalesDatePrediction.Api.DTOs
{
    public class ClientOrderDTO
    {
        public int? OrderId { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string? ShipName { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipCity { get; set; }
        public string? CompanyName { get; set; }
    }
}
