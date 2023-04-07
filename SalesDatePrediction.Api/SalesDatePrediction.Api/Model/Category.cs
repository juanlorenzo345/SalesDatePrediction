using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SalesDatePrediction.Api.Model
{
    [Table("Categories", Schema = "Production")]

    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        [Column("categoryid")]
        public int Categoryid { get; set; }
        [Column("categoryname")]
        [StringLength(15)]
        public string Categoryname { get; set; } = null!;
        [Column("description")]
        [StringLength(200)]
        public string Description { get; set; } = null!;

        [InverseProperty("Category")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
