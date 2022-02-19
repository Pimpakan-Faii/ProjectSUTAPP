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
    public class SafesAPIController : ApiController
    {
        private MyModel db = new MyModel();

        // GET: api/SafesAPI
        public IQueryable<Safe> GetSafe()
        {
            Uri host = new Uri(Request.RequestUri.ToString());
            string url = host.GetLeftPart(UriPartial.Authority);
            var protect = db.Safe.ToList();

            foreach (var item in protect)
            {
                item.Safe_img = url + "/Content/images/" + item.Safe_img;
            }
            return db.Safe;
        }

        // GET: api/SafesAPI/5
        [ResponseType(typeof(Safe))]
        public IHttpActionResult GetSafe(int id)
        {

            Uri host = new Uri(Request.RequestUri.ToString());
            string url = host.GetLeftPart(UriPartial.Authority);
  
            Safe safe = db.Safe.Find(id);
            if (safe == null)
            {
                return NotFound();
            }
            safe.Safe_img = url + "/Content/images/" + safe.Safe_img;

            return Ok(safe);
        }

        // PUT: api/SafesAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSafe(int id, Safe safe)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != safe.Safe_id)
            {
                return BadRequest();
            }

            db.Entry(safe).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SafeExists(id))
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

        // POST: api/SafesAPI
        [ResponseType(typeof(Safe))]
        public IHttpActionResult PostSafe(Safe safe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Safe.Add(safe);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = safe.Safe_id }, safe);
        }

        // DELETE: api/SafesAPI/5
        [ResponseType(typeof(Safe))]
        public IHttpActionResult DeleteSafe(int id)
        {
            Safe safe = db.Safe.Find(id);
            if (safe == null)
            {
                return NotFound();
            }

            db.Safe.Remove(safe);
            db.SaveChanges();

            return Ok(safe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SafeExists(int id)
        {
            return db.Safe.Count(e => e.Safe_id == id) > 0;
        }
    }
}