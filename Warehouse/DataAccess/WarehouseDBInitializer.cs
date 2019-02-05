using ModelLibrary;
using System.Collections.Generic;
using System.Data.Entity;

namespace DataAccess
{
    class WarehouseDBInitializer : DropCreateDatabaseIfModelChanges<WarehouseContext>
    {
        protected override void Seed(WarehouseContext context)
        {
            //Creates and adds Categories to the ContextTable.
            var furniture = context.Categories.Add(new Category() { categoryId = 2, name = "Furniture" });
            var hotdog = context.Categories.Add(new Category() { categoryId = 3, name = "Hotdog"});
            var textiles = context.Categories.Add(new Category() {categoryId = 1, name = "Textiles"});

            //Creates and adds Products to the ContextTable
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
                description = "Red Carpet",
                price = 80,
                itemNumber = 1,
                color = "Red",
                inStock = 25,
                sold = 13
            });

            //Creates and adds Discounts to the ContextTable
            var discount1 = context.Discounts.Add(new Discount() { categoryId = furniture.categoryId, percentage = 0.25});
            var discount2 = context.Discounts.Add(new Discount() { categoryId = hotdog.categoryId, percentage = 0.15 });
            var discount3 = context.Discounts.Add(new Discount() { categoryId = textiles.categoryId, percentage = 0.30 });

            //Creates and adds Customers to the ContextTable
            var johnSnowden = context.Customers.Add(new Customer()
            {
                address = "Hakkebakkeskogen 52",
                areaCode = 47,
                discounts = new List<Discount>(){discount1, discount2},
                firstName = "John",
                lastName = "Snowden",
                isMemeber = true,
                phoneNumber = 12345678,
                zipCode = 1337,
            });

            var OleOlsen = context.Customers.Add(new Customer()
            {
                address = "Normansvei 3b",
                areaCode = 47,
                firstName = "Ole",
                lastName = "Olsen",
                isMemeber = false,
                phoneNumber = 87654321,
                zipCode = 1334,
            });

            var johnnyBravo = context.Customers.Add(new Customer()
            {
                address = "Melandsvei 7",
                areaCode = 47,
                discounts = new List<Discount>() { discount3, discount2, discount1 },
                firstName = "Johnny",
                lastName = "Bravo",
                phoneNumber = 12343214,
                isMemeber = false,
                zipCode = 1717,
            });

            //Creates orders and add them to the ContextTable
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

            //Creates Products to Orders and adds them to the ContextTable. Also adds them to they'r respective Order.
            var prodToOrder1 = context.ProdToOrders.Add(new ProductToOrder() { product = table, prodDescription = table.description, quantity = 5, order = orderOne, orderId = orderOne.orderId, categoryId = table.categoryId, price = table.price});
            var prodToOrder4 = context.ProdToOrders.Add(new ProductToOrder() { product = wool, prodDescription = wool.description, quantity = 19, order = orderOne, orderId = orderOne.orderId, categoryId = wool.categoryId, price = wool.price });
            var prodToOrder2 = context.ProdToOrders.Add(new ProductToOrder() { product = wool, prodDescription = wool.description, quantity = 1, order = orderTwo, orderId = orderTwo.orderId, categoryId = wool.categoryId, price = wool.price });
            var prodToOrder3 = context.ProdToOrders.Add(new ProductToOrder() { product = hDog, prodDescription = hDog.description, quantity = 3, order = orderThree, orderId = orderThree.orderId, categoryId = hDog.categoryId, price = hDog.price });
            var prodToOrder5 = context.ProdToOrders.Add(new ProductToOrder() { product = table, prodDescription = table.description, quantity = 5, order = orderThree, orderId = orderThree.orderId, categoryId = table.categoryId, price = table.price });

            //Adds everything 
            base.Seed(context);
        }
    }
}
