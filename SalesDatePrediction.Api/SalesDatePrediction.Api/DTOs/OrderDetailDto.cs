namespace SalesDatePrediction.Api.DTOs
{
    public class OrderDetailDto
    {
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public double? UnitPrice { get; set; }
        public int? Qty { get; set; }
        public double? Discount { get; set; }
    }
}
