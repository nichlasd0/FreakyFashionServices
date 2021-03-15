using FreakyFashionServices.OrderProcessor.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreakyFashionServices.OrderProcessor.Data
{
    public class OrderContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,15799;Database=FreakyFashionServices;User=SA;Password=Testing1122");
        }

    }
}
