using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project;
using Project.Helper;

namespace Project.Controllers
{
    public class SafesController : Controller
    {
        private MyModel db = new MyModel();

        // GET: Safes
        [AuthorizationFilter]
        public ActionResult Index()
        {
            return View(db.Safe.ToList());
        }

        // GET: Safes/Details/5
        [AuthorizationFilter]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Safe safe = db.Safe.Find(id);
            if (safe == null)
            {
                return HttpNotFound();
            }
            return View(safe);
        }

        // GET: Safes/Create
        [AuthorizationFilter]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Safes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Safe_id,Safe_name,Safe_img")] Safe safe)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                    file.SaveAs(path);
                    safe.Safe_img = fileName;
                }

                db.Safe.Add(safe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(safe);
        }

        // GET: Safes/Edit/5
        [AuthorizationFilter]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Safe safe = db.Safe.Find(id);
            if (safe == null)
            {
                return HttpNotFound();
            }
            return View(safe);
        }

        // POST: Safes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Safe_id,Safe_name,Safe_img")] Safe safe)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                    file.SaveAs(path);
                    safe.Safe_img = fileName;
                }

                db.Entry(safe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(safe);
        }

        // GET: Safes/Delete/5
        [AuthorizationFilter]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Safe safe = db.Safe.Find(id);
            if (safe == null)
            {
                return HttpNotFound();
            }
            return View(safe);
        }

        // POST: Safes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Safe safe = db.Safe.Find(id);
            db.Safe.Remove(safe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
