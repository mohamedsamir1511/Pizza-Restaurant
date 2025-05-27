using Microsoft.EntityFrameworkCore;
using Pizaa_Restaurant.Context;
using Pizaa_Restaurant.Entities;
using Pizaa_Restaurant.Seed;
using System;
using System.Linq;

namespace Pizaa_Restaurant
{
    class Program
    {
        static void Main(string[] args)
        {
            using var dbContext = new AppDbContext();
            var seed = new DbSeeder(dbContext);
            seed.Seed(); // إضافة البيانات الأولية لو مش موجودة

            var employee = Login(dbContext);

            if (employee == null)
            {
                Console.WriteLine("Exiting the system...");
                return;
            }

            Console.WriteLine("\nWelcome to Pizaa Restaurant!");

            while (true)
            {
                Console.WriteLine("\n1. Show All Pizzas");
                Console.WriteLine("2. Create New Order");
                Console.WriteLine("3. Show All Orders");
                Console.WriteLine("4. Add New Pizza");
                Console.WriteLine("5. Update Pizza");
                Console.WriteLine("6. Search Pizza by Name");
                Console.WriteLine("7. Delete Pizza");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ShowAllPizzas(dbContext);
                            break;
                        case 2:
                            CreateNewOrder(dbContext);
                            break;
                        case 3:
                            ShowAllOrders(dbContext);
                            break;
                        case 4:
                            AddNewPizza(dbContext);
                            break;
                        case 5:
                            UpdatePizza(dbContext);
                            break;
                        case 6:
                            SearchPizza(dbContext);
                            break;
                        case 7:
                            DeletePizza(dbContext);
                            break;
                        case 0:
                            Console.WriteLine("Exiting the system...");
                            return;
                        default:
                            Console.WriteLine("Invalid option!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a valid number.");
                }
            }
        }

        private static Employee Login(AppDbContext dbContext)
        {
            int attempts = 0;
            const int maxAttempts = 3;

            while (attempts < maxAttempts)
            {
                Console.WriteLine("=== Employee Login ===");
                Console.Write("Enter Username: ");
                string username = Console.ReadLine()?.Trim();
                Console.Write("Enter Password: ");
                string password = Console.ReadLine()?.Trim();

                var employee = dbContext.Employees
                    .FirstOrDefault(e => e.Username == username && e.Password == password);

                if (employee != null)
                {
                    Console.WriteLine($"Welcome {employee.Name}!\n");
                    return employee;
                }
                else
                {
                    attempts++;
                    Console.WriteLine("Invalid username or password. Please try again.\n");
                }
            }

            Console.WriteLine("Too many failed login attempts. Exiting...");
            Environment.Exit(0); // يقفل البرنامج
            return null; // لن يصل إلى هنا بس لازم في السي شارب
        }

        public static void ShowAllPizzas(AppDbContext dbContext)
        {
            var pizzas = dbContext.Pizzas.ToList();
            Console.WriteLine("\nAvailable Pizzas:");
            foreach (var pizza in pizzas)
            {
                Console.WriteLine($"- {pizza.Name}: {pizza.Description} - ${pizza.Price}");
            }
        }

        public static void CreateNewOrder(AppDbContext dbContext)
        {
            Console.Write("\nEnter Customer ID (1 for Mohamed Samir, 2 for Mohamed Gammal): ");
            if (int.TryParse(Console.ReadLine(), out int customerId))
            {
                var customer = dbContext.Customers.FirstOrDefault(c => c.CustomerId == customerId);

                if (customer == null)
                {
                    Console.WriteLine("Customer not found!");
                    return;
                }

                Console.Write("Select Pizza ID (1 for Margherita, 2 for Pepperoni, 3 for Vegetarian): ");
                if (int.TryParse(Console.ReadLine(), out int pizzaId))
                {
                    var pizza = dbContext.Pizzas.FirstOrDefault(p => p.PizzaId == pizzaId);

                    if (pizza == null)
                    {
                        Console.WriteLine("Pizza not found!");
                        return;
                    }

                    Console.Write("Enter Quantity: ");
                    if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                    {
                        var order = new Order
                        {
                            CustomerId = customer.CustomerId,
                            OrderDate = DateTime.Now,
                            OrderItems = new[]
                            {
                                new OrderItem
                                {
                                    PizzaId = pizza.PizzaId,
                                    Quantity = quantity
                                }
                            }
                        };

                        dbContext.Orders.Add(order);
                        dbContext.SaveChanges();
                        Console.WriteLine("Order Created Successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid quantity! Please enter a valid number.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Pizza ID!");
                }
            }
            else
            {
                Console.WriteLine("Invalid Customer ID!");
            }
        }

        public static void ShowAllOrders(AppDbContext dbContext)
        {
            var orders = dbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Pizza)
                .ToList();

            if (!orders.Any())
            {
                Console.WriteLine("No orders found.");
                return;
            }

            foreach (var order in orders)
            {
                Console.WriteLine($"\nOrder ID: {order.OrderId}");
                Console.WriteLine($"Customer: {order.Customer.Name}");
                Console.WriteLine($"Order Date: {order.OrderDate}");

                foreach (var item in order.OrderItems)
                {
                    Console.WriteLine($" - Pizza: {item.Pizza.Name}, Quantity: {item.Quantity}");
                }
                Console.WriteLine("--------------------------------");
            }
        }

        public static void AddNewPizza(AppDbContext dbContext)
        {
            Console.Write("\nEnter Pizza Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Pizza Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter Pizza Price: ");
            decimal price;
            while (!decimal.TryParse(Console.ReadLine(), out price))
            {
                Console.WriteLine("Invalid price. Please enter a valid number:");
            }

            var pizza = new Pizza
            {
                Name = name,
                Description = description,
                Price = price
            };

            dbContext.Pizzas.Add(pizza);
            dbContext.SaveChanges();

            Console.WriteLine("New Pizza Added Successfully!");
        }

        public static void UpdatePizza(AppDbContext dbContext)
        {
            Console.Write("\nEnter the Pizza ID you want to update: ");
            if (int.TryParse(Console.ReadLine(), out int pizzaId))
            {
                var pizza = dbContext.Pizzas.FirstOrDefault(p => p.PizzaId == pizzaId);

                if (pizza == null)
                {
                    Console.WriteLine("Pizza not found!");
                    return;
                }

                Console.WriteLine($"Current Name: {pizza.Name}");
                Console.Write("Enter New Name (or press Enter to keep the same): ");
                string newName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newName))
                {
                    pizza.Name = newName;
                }

                Console.WriteLine($"Current Description: {pizza.Description}");
                Console.Write("Enter New Description (or press Enter to keep the same): ");
                string newDescription = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newDescription))
                {
                    pizza.Description = newDescription;
                }

                Console.WriteLine($"Current Price: {pizza.Price}");
                Console.Write("Enter New Price (or press Enter to keep the same): ");
                string priceInput = Console.ReadLine();
                if (decimal.TryParse(priceInput, out decimal newPrice))
                {
                    pizza.Price = newPrice;
                }

                dbContext.SaveChanges();
                Console.WriteLine("Pizza Updated Successfully!");
            }
            else
            {
                Console.WriteLine("Invalid Pizza ID!");
            }
        }

        public static void SearchPizza(AppDbContext dbContext)
        {
            Console.Write("\nEnter Pizza Name to Search: ");
            string name = Console.ReadLine().ToLower();

            var pizzas = dbContext.Pizzas
                                  .Where(p => p.Name.ToLower().Contains(name))
                                  .ToList();

            if (!pizzas.Any())
            {
                Console.WriteLine("No pizzas found with that name.");
                return;
            }

            Console.WriteLine("\nFound Pizzas:");
            foreach (var pizza in pizzas)
            {
                Console.WriteLine($"- {pizza.Name}: {pizza.Description} - ${pizza.Price}");
            }
        }

        public static void DeletePizza(AppDbContext dbContext)
        {
            Console.Write("\nEnter the Pizza ID you want to delete: ");
            if (int.TryParse(Console.ReadLine(), out int pizzaId))
            {
                var pizza = dbContext.Pizzas.FirstOrDefault(p => p.PizzaId == pizzaId);

                if (pizza == null)
                {
                    Console.WriteLine("Pizza not found!");
                    return;
                }

                dbContext.Pizzas.Remove(pizza);
                dbContext.SaveChanges();

                Console.WriteLine("Pizza Deleted Successfully!");
            }
            else
            {
                Console.WriteLine("Invalid Pizza ID!");
            }
        }
    }
}
