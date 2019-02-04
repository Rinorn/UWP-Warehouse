using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.Mvc;
using DataAccess;
using ModelLibrary;
using ModelLibrary.Annotations;
using Newtonsoft.Json;

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
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> GetOrder(int id)
        {
            Order order = new Order();
            List<int> discountCategory = new List<int>();
            List<double> discountPercentage = new List<double>();
            order = await db.Orders.FindAsync(id);
            using (SqlConnection conn = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                int customerId;

                //Get the customerId for the spesified order
                SqlCommand cmdCust = new SqlCommand("Select customerId from OrderCustomer where orderId=(@id)", conn);
                cmdCust.Parameters.AddWithValue("@id", id);
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
                conn.Close();
                
                //Get the orderInformation for the spesified order
                SqlCommand cmd = new SqlCommand("Select * from ProdToOrder where orderId=(@id)", conn);
                cmd.Parameters.AddWithValue("@id", id);
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
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders.Add(order);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = order.orderId }, order);
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