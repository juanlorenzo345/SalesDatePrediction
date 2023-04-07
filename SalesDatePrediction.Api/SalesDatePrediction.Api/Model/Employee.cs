﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SalesDatePrediction.Api.Model
{
    [Table("Employees", Schema = "HR")]

    public partial class Employee
    {
        public Employee()
        {
            InverseMgr = new HashSet<Employee>();
            Orders = new HashSet<Order>();
        }

        [Key]
        [Column("empid")]
        public int Empid { get; set; }
        [Column("lastname")]
        [StringLength(20)]
        public string Lastname { get; set; } = null!;
        [Column("firstname")]
        [StringLength(10)]
        public string Firstname { get; set; } = null!;
        [Column("title")]
        [StringLength(30)]
        public string Title { get; set; } = null!;
        [Column("titleofcourtesy")]
        [StringLength(25)]
        public string Titleofcourtesy { get; set; } = null!;
        [Column("birthdate", TypeName = "datetime")]
        public DateTime Birthdate { get; set; }
        [Column("hiredate", TypeName = "datetime")]
        public DateTime Hiredate { get; set; }
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
        [Column("mgrid")]
        public int? Mgrid { get; set; }

        [ForeignKey("Mgrid")]
        [InverseProperty("InverseMgr")]
        public virtual Employee? Mgr { get; set; }
        [InverseProperty("Mgr")]
        public virtual ICollection<Employee> InverseMgr { get; set; }
        [InverseProperty("Emp")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
