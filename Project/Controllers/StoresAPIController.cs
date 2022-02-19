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
using Project;

namespace Project.Controllers
{
    public class StoresAPIController : ApiController
    {
        private MyModel db = new MyModel();

        // GET: api/StoresAPI
        public IQueryable<Store> GetStore()
        {
            Uri host = new Uri(Request.RequestUri.ToString());
            string url = host.GetLeftPart(UriPartial.Authority);

            var product = db.Store.ToList();

            foreach(var item in product)
            {
                item.url = url + "/Content/images/" + item.url;
            }

            return db.Store;
        }


        // GET: api/StoresAPI/5
        [ResponseType(typeof(Store))]
        public IHttpActionResult GetStore(int id)
        {
            Uri host = new Uri(Request.RequestUri.ToString());
            string url = host.GetLeftPart(UriPartial.Authority);

            Store store = db.Store.Find(id);
            if (store == null)
            {
                return NotFound();
            }
            store.url = url + "/Content/images/" + store.url;

            return Ok(store);
        }

        // PUT: api/StoresAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStore(int id, Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != store.product_id)
            {
                return BadRequest();
            }

            db.Entry(store).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(id))
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

        // POST: api/StoresAPI
        [ResponseType(typeof(Store))]
        public IHttpActionResult PostStore(Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Store.Add(store);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = store.product_id }, store);
        }

        // DELETE: api/StoresAPI/5
        [ResponseType(typeof(Store))]
        public IHttpActionResult DeleteStore(int id)
        {
            Store store = db.Store.Find(id);
            if (store == null)
            {
                return NotFound();
            }

            db.Store.Remove(store);
            db.SaveChanges();

            return Ok(store);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StoreExists(int id)
        {
            return db.Store.Count(e => e.product_id == id) > 0;
        }
    }
}