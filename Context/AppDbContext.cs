using Microsoft.EntityFrameworkCore;
using Pizaa_Restaurant.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizaa_Restaurant.Context
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {optionsBuilder.UseSqlServer("Server=.;database=PizaaRestaurDb;Trusted_Connection=True;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);


          
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.PizzaId }); //Composite Key 

            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.PizzaId }); // مفتاح مركب


            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Pizza)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.PizzaId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);
        }
    }
}