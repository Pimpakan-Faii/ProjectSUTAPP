using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project.Helper;
using Project.Models;
namespace Project.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        private MyModel db = new MyModel();
        [AuthorizationFilter]
        public ActionResult Index()
        {
            //var netsalary = db.Stores.Where(x => x.product_id == data.ID && x.IsArchive == true).Select(x => x.NetSalary).LastOrDefault();

            //var last = db.Stores.OrderByDescending(p => p.product_id).LastOrDefault().product_id;

            /*var count = db.Stores.GroupBy(n => n.product_id)
                        .Select(g => new { Count = g.Count() }).ToList();*/


            /*var count = db.Stores.GroupBy(n => n.product_id)
                        .Select(g => new {Count = g.Count() }).ToList();*/

            /*List<Storecount> CountList = new List<Storecount>();

            for (int i = 0; i < count.ToList().Count; i++)
            {
                CountList.Add(new Storecount { Count = count[i].Count });
            }*/
            var data = db.Store.Select(n => n.product_id).Count();
            ViewData["Message"] = data;

            var safe = db.Safe.Select(n => n.Safe_id).Count();
            ViewData["Safe"] = safe;

            var unSafe = db.Store.Select(n => n.product_id).Count();
            ViewData["unSafe"] = unSafe - safe;

            var proMo = db.Coupon.Select(n => n.Coupon_Id).Count();
            ViewData["proMo"] = proMo;

            var users = db.AspNetUsers.Select(n => n.Id).Count();
            ViewData["Users"] = users;

            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        public JsonResult checkLogin(string username, string password) 
        {
            var dataItem = db.AdminUser.Where(x => x.Username == username && x.Password == password).SingleOrDefault();
            bool isLogged = true;
            if (dataItem != null) 
            {
                //FormsAuthentication.SetAuthCookie(dataItem.Username, false);
                Session["Username"] = dataItem.Username;
                isLogged = true;

            }
            else 
            {
                isLogged = false;
            }
            return Json(isLogged, JsonRequestBehavior.AllowGet);
        }


    }
}