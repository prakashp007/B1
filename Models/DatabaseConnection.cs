using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Expense_Tracker.Models
{
    public class DatabaseConnection : DbContext
    {
        public DatabaseConnection() : base("connection"){ }

        public DbSet<categories> c_obj { get; set; }
        public DbSet<expenses> e_obj { get; set; }
        public DbSet<TotalLimit> t_obj { get; set; }

    }

}