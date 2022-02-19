using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project;

namespace Project.Controllers
{
    public class SafeController : Controller
    {
        private MyModel db = new MyModel();

        // GET: Safe
        public ActionResult Index()
        {
            return View(db.Safe.ToList());
        }

        // GET: Safe/Details/5
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
