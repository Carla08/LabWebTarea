using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SAPapp.Models
{
    public class Context: DbContext
    {
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}