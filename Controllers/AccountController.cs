using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MasteringCRUD.Models;
using System.Data.Entity;

namespace MasteringCRUD.Controllers
{
    public class AccountController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            using(MasteringCRUDEntities db = new MasteringCRUDEntities())
            {
                var user = db.Users.ToList();
                return View(user);
            }
            
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            using(MasteringCRUDEntities db = new MasteringCRUDEntities())
            {
                var login = db.Users.FirstOrDefault(m => m.Email == email && m.Password == password);
                if(login == null)
                {
                    ViewBag.Message = "Incorrect Email or Password, Try again";
                    return View();
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            using(MasteringCRUDEntities db = new MasteringCRUDEntities())
            {
                db.Users.Add(user);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.Message = "Saved succesfully";
                return View();
            }
            
        }
        public ActionResult Edit(int? id)
        {
            using(MasteringCRUDEntities db = new MasteringCRUDEntities())
            {
                var edit = db.Users.Find(id);
                return View(edit);
            }
            
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            using(MasteringCRUDEntities db = new MasteringCRUDEntities())
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Details(int? id)
        {
            using(MasteringCRUDEntities db = new MasteringCRUDEntities())
            {
                var details = db.Users.Find(id);
                return View(details);
            }
        }
        public ActionResult Delete(int? id)
        {
            using(MasteringCRUDEntities db = new MasteringCRUDEntities())
            {
                var delete = db.Users.Find(id);
                db.Users.Remove(delete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}