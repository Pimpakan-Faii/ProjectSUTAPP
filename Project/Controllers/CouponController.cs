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
    public class CouponController : Controller
    {
        private MyModel db = new MyModel();

        // GET: Coupon
        public ActionResult Index()
        {
            return View(db.Coupon.ToList());
        }
    
    }
}
