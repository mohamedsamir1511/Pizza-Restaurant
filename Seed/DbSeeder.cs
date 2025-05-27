using Microsoft.EntityFrameworkCore;
using Pizaa_Restaurant.Entities;
using Pizaa_Restaurant.Context;
using System;
using System.Linq;

namespace Pizaa_Restaurant.Seed
{
    internal class DbSeeder
    {
        private readonly AppDbContext _dbContext;

        public DbSeeder(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            // Clear old Employees if needed (be cautious in real apps)
            _dbContext.Employees.RemoveRange(_dbContext.Employees);
            _dbContext.SaveChanges();

            // Seed Pizzas
            if (!_dbContext.Pizzas.Any())
            {
                _dbContext.Pizzas.AddRange(
                    new Pizza
                    {
                        Name = "Margherita",
                        Description = "Classic Margherita pizza with fresh tomatoes and mozzarella",
                        Price = 10.99m
                    },
                    new Pizza
                    {
                        Name = "Pepperoni",
                        Description = "Delicious pizza with spicy pepperoni",
                        Price = 12.99m
                    },
                    new Pizza
                    {
                        Name = "Vegetarian",
                        Description = "Veggie pizza loaded with fresh vegetables",
                        Price = 11.99m
                    }
                );
            }

            // Seed Employees
            if (!_dbContext.Employees.Any())
            {
                _dbContext.Employees.AddRange(
                    new Employee
                    {
                        Name = "Mohamed Sami",
                        Role = "Chef",
                        Username = "Mohamed",
                        Password = "1234"
                    },
                    new Employee
                    {
                        Name = "Momen Mostafa",
                        Role = "Chef",
                        Username = "Momen",
                        Password = "1234"
                    },
                    new Employee
                    {
                        Name = "Ibrahim Mohamed",
                        Role = "Chef",
                        Username = "Ibrahim",
                        Password = "1234"
                    },
                    new Employee
                    {
                        Name = "Maher Ibrahim",
                        Role = "Delivery",
                        Username = "Maher",
                        Password = "1234"
                    }
                );
            }

            // Seed Customers
            if (!_dbContext.Customers.Any())
            {
                _dbContext.Customers.AddRange(
                    new Customer
                    {
                        Name = "Mohamed Samir",
                        Address = "6 October City, Giza",
                        Phone = "010048524"
                    },
                    new Customer
                    {
                        Name = "Mohamed Gammal",
                        Address = "Nasr City",
                        Phone = "011146845"
                    }
                );
            }

            _dbContext.SaveChanges();
        }
    }
}
