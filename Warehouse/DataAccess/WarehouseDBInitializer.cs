using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;

namespace DataAccess
{
    class WarehouseDBInitializer : DropCreateDatabaseIfModelChanges<WarehouseContext>
    {
        protected override void Seed(WarehouseContext context)
        {
            var furniture = context.Categories.Add(new Category() { categoryId = 2, name = "Furniture" });
            var hotdog = context.Categories.Add(new Category() { categoryId = 3, name = "Hotdog"});
            var textiles = context.Categories.Add(new Category() {categoryId = 1, name = "Textiles"});

            var table = context.Products.Add(new Furniture()
            {
                categoryId = furniture.categoryId,
                description = "Black Table",
                price = 1299,
                itemNumber = 1,
                weight = 13.22,
                inStock = 20,
                sold = 4
            });

            var hDog = context.Products.Add(new HotDog()
            {
                categoryId = hotdog.categoryId,
                description = "Bacon hotdog",
                price = 29.99,
                flavor = "Bacon",
                inStock = 300,
                sold = 37
            });

            var wool = context.Products.Add(new Textile()
            {
                categoryId = textiles.categoryId,
                description = "Red wool",
                price = 80,
                itemNumber = 1,
                color = "Red",
                inStock = 25,
                sold = 13
            });

            var discount1 = context.Discounts.Add(new Discount() { categoryId = furniture.categoryId, percentage = 0.25});
            var discount2 = context.Discounts.Add(new Discount() { categoryId = hotdog.categoryId, percentage = 0.15 });
            var discount3 = context.Discounts.Add(new Discount() { categoryId = textiles.categoryId, percentage = 0.30 });

            var johnSnowden = context.Customers.Add(new Customer()
            {
                address = "Russland 52",
                areaCode = 47,
                discounts = new List<Discount>(){discount1, discount2},
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
                discounts = new List<Discount>() { discount3, discount2 },
                firstName = "Johnny",
                lastName = "Bravo",
                phoneNumber = 12345678,
                isMemeber = false,
                zipCode = 1337,
            });

            var orderOne = context.Orders.Add(new Order()
            {
                Customers = new List<Customer>() { johnnyBravo },
            });
            var orderTwo = context.Orders.Add(new Order()
            {
                Customers = new List<Customer>() { johnnyBravo },
            });
            var orderThree = context.Orders.Add(new Order()
            {
                Customers = new List<Customer>() { johnSnowden },
            });

            var prodToOrder1 = context.ProdToOrders.Add(new ProductToOrder() { product = table, prodDescription = table.description, quantity = 5, order = orderOne, orderId = orderOne.orderId});
            var prodToOrder4 = context.ProdToOrders.Add(new ProductToOrder() { product = wool, prodDescription = wool.description, quantity = 19, order = orderOne, orderId = orderOne.orderId });
            var prodToOrder2 = context.ProdToOrders.Add(new ProductToOrder() { product = wool, prodDescription = wool.description, quantity = 1, order = orderTwo, orderId = orderTwo.orderId });
            var prodToOrder3 = context.ProdToOrders.Add(new ProductToOrder() { product = hDog, prodDescription = hDog.description, quantity = 3, order = orderThree, orderId = orderThree.orderId });
            var prodToOrder5 = context.ProdToOrders.Add(new ProductToOrder() { product = table, prodDescription = table.description, quantity = 5, order = orderThree, orderId = orderThree.orderId });


            base.Seed(context);
        }
    }
}
