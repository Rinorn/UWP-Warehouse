using DataAccess;
using ModelLibrary;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Warehouse.Data.Api.Controllers
{
    public class ProductToOrdersController : ApiController
    {
        private WarehouseContext db = new WarehouseContext();

        // GET: api/ProductToOrders
        public IQueryable<ProductToOrder> GetProdToOrders()
        {
            return db.ProdToOrders;
        }

        // GET: api/ProductToOrders/5
        [ResponseType(typeof(ProductToOrder))]
        public async Task<IHttpActionResult> GetProductToOrder(int id)
        {
            ProductToOrder productToOrder = await db.ProdToOrders.FindAsync(id);
            if (productToOrder == null)
            {
                return NotFound();
            }

            return Ok(productToOrder);
        }

        // PUT: api/ProductToOrders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProductToOrder(int id, ProductToOrder productToOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productToOrder.prodToOrderId)
            {
                return BadRequest();
            }

            db.Entry(productToOrder).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductToOrderExists(id))
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

        // POST: api/ProductToOrders
        [ResponseType(typeof(ProductToOrder))]
        public async Task<IHttpActionResult> PostProductToOrder(ProductToOrder productToOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProdToOrders.Add(productToOrder);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = productToOrder.prodToOrderId }, productToOrder);
        }

        // DELETE: api/ProductToOrders/5
        [ResponseType(typeof(ProductToOrder))]
        public async Task<IHttpActionResult> DeleteProductToOrder(int id)
        {
            ProductToOrder productToOrder = await db.ProdToOrders.FindAsync(id);
            if (productToOrder == null)
            {
                return NotFound();
            }

            db.ProdToOrders.Remove(productToOrder);
            await db.SaveChangesAsync();

            return Ok(productToOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductToOrderExists(int id)
        {
            return db.ProdToOrders.Count(e => e.prodToOrderId == id) > 0;
        }
    }
}