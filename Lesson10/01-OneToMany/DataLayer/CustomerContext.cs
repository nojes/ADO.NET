using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_OneToMany.DataLayer
{
    public class Initializer : DropCreateDatabaseAlways<CustomerContext>
    {
        protected override void Seed(CustomerContext context)
        {
            Customer[] customers = new Customer[] {
                new Customer() {
                    FirstName = "Petro",
                    LastName = "Vasichkin",
                    Birthday = new DateTime(1995, 01, 24),
                    Email = "petro@test.com"
                },
                new Customer() {
                    FirstName = "Svetlana",
                    LastName = "Chizhik",
                    Birthday = new DateTime(1990, 05, 14),
                    Email = "svetlana@test.com"
                },
                new Customer() {
                    FirstName = "Test",
                    LastName = "Testovich",
                    Birthday = DateTime.Now,
                    Email = "testovich@test.com"
                }
            };

            Order[] orders = new Order[] {
                new Order() {
                    ProductName = "Tea",
                    PurchaseDate = DateTime.Now,
                    Quantity = 2,
                    Customer = customers[0]
                },
                new Order() {
                    ProductName = "Pepsi",
                    PurchaseDate = DateTime.Now,
                    Quantity = 1,
                    Customer = customers[1]
                },
                new Order() {
                    ProductName = "Lays",
                    PurchaseDate = DateTime.Now,
                    Quantity = 3,
                    Customer = customers[1]
                }
            };

            foreach (Customer customer in customers) {
                context.Customers.Add(customer);
            }
            foreach (Order order in orders) {
                context.Orders.Add(order);
            }

            base.Seed(context);
        }
    }


    public class CustomerContext : DbContext
    {
        public CustomerContext() : base("name=Customer")
        {
            //Database.SetInitializer<CustomerContext>(new DropCreateDatabaseAlways<CustomerContext>());
            if (!Database.Exists()) {
                Database.SetInitializer<CustomerContext>(new Initializer());
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
