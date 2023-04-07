﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SalesDatePrediction.Api.Model
{
    [Table("Shippers", Schema = "Sales")]
    public partial class Shipper
    {
        public Shipper()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        [Column("shipperid")]
        public int Shipperid { get; set; }
        [Column("companyname")]
        [StringLength(40)]
        public string Companyname { get; set; } = null!;
        [Column("phone")]
        [StringLength(24)]
        public string Phone { get; set; } = null!;

        [InverseProperty("Shipper")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
