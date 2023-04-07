using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SalesDatePrediction.Api.Model
{
    [Table("Suppliers", Schema = "Production")]
    public partial class Supplier
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        [Column("supplierid")]
        public int Supplierid { get; set; }
        [Column("companyname")]
        [StringLength(40)]
        public string Companyname { get; set; } = null!;
        [Column("contactname")]
        [StringLength(30)]
        public string Contactname { get; set; } = null!;
        [Column("contacttitle")]
        [StringLength(30)]
        public string Contacttitle { get; set; } = null!;
        [Column("address")]
        [StringLength(60)]
        public string Address { get; set; } = null!;
        [Column("city")]
        [StringLength(15)]
        public string City { get; set; } = null!;
        [Column("region")]
        [StringLength(15)]
        public string? Region { get; set; }
        [Column("postalcode")]
        [StringLength(10)]
        public string? Postalcode { get; set; }
        [Column("country")]
        [StringLength(15)]
        public string Country { get; set; } = null!;
        [Column("phone")]
        [StringLength(24)]
        public string Phone { get; set; } = null!;
        [Column("fax")]
        [StringLength(24)]
        public string? Fax { get; set; }

        [InverseProperty("Supplier")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
