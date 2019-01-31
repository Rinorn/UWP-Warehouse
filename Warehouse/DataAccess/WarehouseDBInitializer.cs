using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;

namespace DataAccess
{
    class WarehouseDBInitializer : DropCreateDatabaseIfModelChanges<warehouseContext>
    {
        protected override void Seed(warehouseContext context)
        {
            var furniture = context.Categories.Add(new Category() { categoryId = 2, name = "Furniture" });
            var hotdog = context.Categories.Add(new Category() { categoryId = 3, name = "Hotdog"});
            var textiles = context.Categories.Add(new Category() {categoryId = 1, name = "Textiles"});

            var table = context.Products.Add(new Furniture()
            {
                category = furniture,
                description = "Black Table",
                price = 1299,
                itemNumber = 1,
                weight = 13.22
            });

            var hDog = context.Products.Add(new HotDog()
            {
                category = hotdog,
                description = "Bacon hotdog",
                price = 29.99,
                flavor = "Bacon"
            });
            

            var johnSnowden = context.Customers.Add(new Customer()
            {
                address = "Russland 52",
                areaCode = 47,
                discounts = new List<Category>(){furniture, hotdog},
                firstName = "John",
                lastName = "Snowden",
                isMemeber = true,
                phoneNumber = 12345678,
                zipCode = 1337,
            });


            var testTestet = context.Customers.Add(new Customer()
            {
                address = "Russland 52",
                areaCode = 47,
                discounts = new List<Category>() {textiles, hotdog, furniture },
                firstName = "Test",
                lastName = "Testet",
                isMemeber = true,
                phoneNumber = 12345678,
                zipCode = 1337,
            });

            var johnnyBravo = context.Customers.Add(new Customer()
            {
                address = "Russland 52",
                areaCode = 47,
                firstName = "Johnny",
                lastName = "Bravo",
                phoneNumber = 12345678,
                isMemeber = false,
                zipCode = 1337,
            });

            var orderOne = context.Orders.Add(new Order()
            {
                orderId = 1,
                customer = johnnyBravo,
                Products = new List<Product>(){table}
            });

            base.Seed(context);
        }
    }
}
