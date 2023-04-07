namespace SalesDatePrediction.Api.DTOs
{
    public class OrderDto
    {
        public int? CustId { get; set; }
        public int? EmpId { get; set; }
        public int? ShipperId { get; set; }
        public string? ShipName { get; set;}
        public string? ShipAddress { get; set; }
        public string? ShipCity { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? Freight { get; set;}
        public string? ShipCountry { get; set; }

        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public double? UnitPrice { get; set; }
        public int? Qty { get; set; }
        public double? Discount { get; set; }

    }
}
