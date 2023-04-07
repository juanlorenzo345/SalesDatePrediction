using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SalesDatePrediction.Api.Model
{
    [Table("Products", Schema = "Production")]
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        [Column("productid")]
        public int Productid { get; set; }
        [Column("productname")]
        [StringLength(40)]
        public string Productname { get; set; } = null!;
        [Column("supplierid")]
        public int Supplierid { get; set; }
        [Column("categoryid")]
        public int Categoryid { get; set; }
        [Column("unitprice", TypeName = "money")]
        public decimal Unitprice { get; set; }
        [Column("discontinued")]
        public bool Discontinued { get; set; }

        [ForeignKey("Categoryid")]
        [InverseProperty("Products")]
        public virtual Category Category { get; set; } = null!;
        [ForeignKey("Supplierid")]
        [InverseProperty("Products")]
        public virtual Supplier Supplier { get; set; } = null!;
        [InverseProperty("Product")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
