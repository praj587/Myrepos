using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using POSDEMO.Models;

namespace POSDEMO.Controllers
{
    public class ProductGroupsController : ApiController
    {
        private ProductContainer db = new ProductContainer();

        // GET: api/ProductGroups
        public IQueryable<ProductGroup> GetProductGroups()
        {
            return db.ProductGroups;
        }

        // GET: api/ProductGroups/5
        [ResponseType(typeof(ProductGroup))]
        public IHttpActionResult GetProductGroup(int id)
        {
            ProductGroup productGroup = db.ProductGroups.Find(id);
            if (productGroup == null)
            {
                return NotFound();
            }

            return Ok(productGroup);
        }

        // PUT: api/ProductGroups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductGroup(int id, ProductGroup productGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productGroup.GroupId)
            {
                return BadRequest();
            }

            db.Entry(productGroup).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductGroupExists(id))
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

        // POST: api/ProductGroups
        [ResponseType(typeof(ProductGroup))]
        public IHttpActionResult PostProductGroup(ProductGroup productGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductGroups.Add(productGroup);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productGroup.GroupId }, productGroup);
        }

        // DELETE: api/ProductGroups/5
        [ResponseType(typeof(ProductGroup))]
        public IHttpActionResult DeleteProductGroup(int id)
        {
            ProductGroup productGroup = db.ProductGroups.Find(id);
            if (productGroup == null)
            {
                return NotFound();
            }

            db.ProductGroups.Remove(productGroup);
            db.SaveChanges();

            return Ok(productGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductGroupExists(int id)
        {
            return db.ProductGroups.Count(e => e.GroupId == id) > 0;
        }
    }
}