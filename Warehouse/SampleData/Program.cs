using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using ModelLibrary;

namespace SampleData
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var furniture = new Category() {categoryId = 1, name = "Furniture"};
                var table = new Furniture()
                {
                    category = furniture,
                    description = "Black Table",
                    price = 1299,
                    furProdNumber = 1,
                    weight = 13.22
                };
                var johnSnowden = new Member()
                {
                    address = "Russland 52",
                    areaCode = 47,
                    customerId = 1,
                    discounts = new List<Category> {furniture},
                    firstName = "John",
                    lastName = "Snowden",
                    membershipId = 1,
                    phoneNumber = 12345678,
                    zipCode = 1337,
                };

                var johnnyBravo = new Customer()
                {
                    address = "Russland 52",
                    areaCode = 47,
                    customerId = 2,
                    firstName = "Johnny",
                    lastName = "Bravo",
                    phoneNumber = 12345678,
                    zipCode = 1337,
                };


                using (var db = new warehouseContext())
                {
                    db.Categories.Add(furniture);
                    db.Products.Add(table);
                    db.Customers.Add(johnSnowden);
                    db.Customers.Add(johnnyBravo);
                    db.SaveChanges();
                }
                Console.WriteLine("Items sucessfully added to database");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong");
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
        }
    }
}
