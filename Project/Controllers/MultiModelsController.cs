using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;
using Project.Models;
using Microsoft.AspNet.Identity;
using System.Net;

namespace Project.Controllers
{
    public class MultiModelsController : Controller
    {
        // GET: MultiModels
        public ActionResult Index()
        {
            dynamic dynamic = new ExpandoObject();
            dynamic.storelist = GetStores();
            dynamic.commentlist = GetComments();
            return View(dynamic);
        }

        public List<Store> GetStores()
        {
            MyModel db = new MyModel();
            List<Store> Lstore = db.Store.ToList();
            return Lstore;
        }

        public List<Comment> GetComments()
        {
            MyModel db = new MyModel();
            List<Comment> Lcomment = db.Comment.ToList();
            return Lcomment;
        }

        public ActionResult Select_Store(int? id)
        {
            MyModel db = new MyModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store user = db.Store.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //Comments
        [HttpPost]
        public ActionResult Comments(string Commentstxt)
        {
            MyModel db = new MyModel();
            if (Request.IsAuthenticated)
            {
                //var comment = from p in db.Comment select p;
                //comment.Text
                Comment comment = new Comment();
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


    }
}