using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Project
{
    public class HomeController : Controller
    {
        public MyModel db = new MyModel();

    
        public ActionResult Index(string searchString)
        {
            dynamic dynamic = new ExpandoObject();

            dynamic.storelist = GetStores(searchString);
            dynamic.commentlist = GetComments();
            return View(dynamic);
        }

        public List<Store> GetStores(string searchString)
        {
            MyModel db = new MyModel();
            List<Store> Lstore ;
            //var store = from p in db.Store select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                Lstore = db.Store.Where(p => p.name.ToUpper().Contains(searchString.ToUpper())).ToList();

            }else
            {
                Lstore = db.Store.ToList();
            }

            //List<Store> Lstore = db.Store.ToList();
            return Lstore;
        }


        public List<Comment> GetComments()
        {
            MyModel db = new MyModel();
            List<Comment> Lcomment = db.Comment.ToList();
            return Lcomment;
        }

        /*[HttpGet]
        public async Task<ActionResult> Index(string searchString) 
        {
            ViewData["search"] = searchString;
            var store = from p in db.Store select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                store = store.Where(p => p.name.ToUpper().Contains(searchString.ToUpper()));

            }
            return View(await store.AsNoTracking().ToListAsync());
        }*/



        public ViewResult search(string searchString)
        {

            var store = from p in db.Store select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                store = store.Where(p => p.name.ToUpper().Contains(searchString.ToUpper()));

            }

            return View(store);
        }

        public ActionResult Select_Store(int? id)
        {
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store user = db.Store.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }*/
            dynamic dynamic = new ExpandoObject();
            dynamic.storelist = db.Store.ToList().Where(comment => comment.product_id == id);
            dynamic.commentlist = db.Comment.ToList().Where(comment => comment.Store_Id == id);
            return View(dynamic);
        }

        //Comments
        [HttpPost]
        public ActionResult Comments(string Commentstxt, int? comment_id)
        {
            if(Request.IsAuthenticated)
            {
                Comment comment = new Comment();

                comment.Store_Id = comment_id;
                comment.Text = Commentstxt;
                comment.UserName = User.Identity.GetUserName();
                db.Comment.Add(comment);
                db.SaveChanges();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            return RedirectToAction("Index");
        }

        // GET: Home/Details/5
        /*public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }*/

        // GET: Home/Create
        /*public ActionResult Create()
        {
            return View();
        }*/

        // POST: Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /* [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Create([Bind(Include = "product_id,url,name,time,Review")] Store store)
         {
             if (ModelState.IsValid)
             {
                 db.Stores.Add(store);
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }

             return View(store);
         }*/

        // GET: Home/Edit/5
        /*public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }*/

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,url,name,time,Review")] Store store)
        {
            if (ModelState.IsValid)
            {
                db.Entry(store).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(store);
        }*/

        // GET: Home/Delete/5
        /*public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }*/

        // POST: Home/Delete/5
        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Store store = db.Stores.Find(id);
            db.Stores.Remove(store);
            db.SaveChanges();
            return RedirectToAction("Index");
        }*/

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
