using DataAccess;
using ModelLibrary;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;

namespace Warehouse.Data.Api.Controllers
{
    public class OrdersController : ApiController
    {
        private WarehouseContext db = new WarehouseContext();

        // GET: api/Orders
        public IQueryable<Order> GetOrders()
        {
            return db.Orders;
        }

        // GET: api/Orders/5
        //This should be split into functions for: Getting customerId of the spesified order, Get the customers discount information, Get the order information
        [System.Web.Http.HttpGet()]
        [System.Web.Http.Route("api/Orders/{orderId}")]
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> GetOrder(int orderId)
        {
            Order order = new Order();
            List<int> discountCategory = new List<int>();
            List<double> discountPercentage = new List<double>();
            order = await db.Orders.FindAsync(orderId);
            using (SqlConnection conn = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                int customerId;

                //Get the customerId for the spesified order
                SqlCommand cmdCust = new SqlCommand("Select customerId from OrderCustomer where orderId=(@id)", conn);
                cmdCust.Parameters.AddWithValue("@id", orderId);
                conn.Open();
                using (SqlDataReader reader = cmdCust.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);
                    List<DataRow> row = dataTable.AsEnumerable().ToList();
                    DataRow iDrow = row[0];
                    customerId = (int)iDrow[0];
                }

                //Get the customers discounts
                CustomersController ctrl = new CustomersController();
                var result = await ctrl.GetDiscount(customerId);
                if (result is OkNegotiatedContentResult<List<Discount>> returnobjects)
                {
                    foreach (var cont in returnobjects.Content)
                    {
                        discountCategory.Add(cont.categoryId);
                        discountPercentage.Add(cont.percentage);
                    }
                }
                Customer cust = new Customer();
                result = await ctrl.GetCustomer(customerId);
                if (result is OkNegotiatedContentResult<Customer> CustObject)
                {
                   cust = CustObject.Content;
                }
                
                //Adds the customer to order
                if (order != null)
                {
                    order.Customers = new List<Customer>();
                    order.Customers.Add(cust); 
                }
                conn.Close();

                //Get the orderInformation for the spesified order
                SqlCommand cmd = new SqlCommand("Select * from ProdToOrder where orderId=(@id)", conn);
                cmd.Parameters.AddWithValue("@id", orderId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    order.ProductsToOrders = new List<ProductToOrder>();
                    var dataTable = new DataTable();
                    dataTable.Load(reader);
                    List<DataRow> row = dataTable.AsEnumerable().ToList();
                    foreach (var unit in row)
                    {
                        ProductToOrder prod = new ProductToOrder(){};
                        prod.prodToOrderId = ((int) unit[0]); 
                        prod.prodDescription = unit[1].ToString();
                        prod.categoryId = (int)unit[2];
                        prod.quantity = (int)unit[3];
                        prod.orderId = (long) unit[4];
                        prod.price = (double) unit[5];
                        for (int i = 0; i < discountCategory.Count; i++)
                        {
                            if (discountCategory[i] == prod.categoryId)
                            {
                                prod.discountPercentage = discountPercentage[i];
                            }
                        }
                        order.ProductsToOrders.Add(prod);
                    }
                }
                await cmd.ExecuteNonQueryAsync();
            }
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.orderId)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Orders
        //This should be split into the seperate functions
        [System.Web.Http.HttpPost()]
        [System.Web.Http.Route("api/Orders/{customerId}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostOrder(Order order, int customerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            using (SqlConnection conn = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                List<int> discountCategory = new List<int>();
                List<double> discountPercentage = new List<double>();

                //Get the customers discounts
                CustomersController ctrl = new CustomersController();
                var result = await ctrl.GetDiscount(customerId);
                if (result is OkNegotiatedContentResult<List<Discount>> returnobjects)
                {
                    foreach (var cont in returnobjects.Content)
                    {
                        discountCategory.Add(cont.categoryId);
                        discountPercentage.Add(cont.percentage);
                    }
                }

                //sets the prodToOrder products category in case its not entered.
                ProductsController prods = new ProductsController();
                IQueryable<Product> products = prods.GetProducts();
                foreach (var prod in products)
                {
                    foreach (var orderProd in order.ProductsToOrders)
                    {
                        if (prod.description.Equals(orderProd.prodDescription))
                        {
                            orderProd.categoryId = prod.categoryId;
                            orderProd.price = prod.price;
                        }
                    }
                }
                foreach (var prod in order.ProductsToOrders)
                {
                    for (int i = 0; i < discountCategory.Count; i++)
                    {
                        if (discountCategory[i] == prod.categoryId)
                        {
                            prod.discountPercentage = discountPercentage[i];
                        }
                    }
                }

                //Gets the number of orders in the DB and Sets the new OrderId to be 1+
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [Order]", conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);
                    List<DataRow> row = dataTable.AsEnumerable().ToList();
                    foreach (var unit in row)
                    {
                        order.orderId = (int)unit[0] + 1;
                    } 
                }
                conn.Close();

                db.Orders.Add(order);
                await db.SaveChangesAsync();

                //Adds the order-customer relationship to the OrderCustomer table.
                SqlCommand cmdCust = new SqlCommand("INSERT INTO OrderCustomer VALUES (@customerId, @orderId);", conn);
                cmdCust.Parameters.AddWithValue("@customerId", customerId);
                cmdCust.Parameters.AddWithValue("@orderId", order.orderId);
                conn.Open();
                await cmdCust.ExecuteNonQueryAsync();

                await db.SaveChangesAsync();
                return StatusCode(HttpStatusCode.Created);
            }
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> DeleteOrder(int id)
        {
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            await db.SaveChangesAsync();

            return Ok(order);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.orderId == id) > 0;
        }
    }
}