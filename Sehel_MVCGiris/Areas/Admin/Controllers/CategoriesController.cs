using Sehel_MVCGiris.Data;
using Sehel_MVCGiris.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sehel_MVCGiris.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Admin/Categories
        public ActionResult Index()
        {
            using (var db = new ApplicationDbContext())
            {

                var categories= db.Categories.ToList();
                return View(categories);
            }
        }


        public ActionResult Create()
        {
            var category = new Category();
            return View(category);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {

                using (var db = new ApplicationDbContext())
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                


                }
            }

            return View(category);
        }
        public ActionResult Edit(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var category = db.Categories.Where(x => x.Id == id).FirstOrDefault();

                if (category != null)
                {
                    return View(category);

                }
                else
                {
                    return HttpNotFound();
                }

            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    var oldProject = db.Categories.Where(x => x.Id == category.Id).FirstOrDefault();
                    if (oldProject != null)
                    {
                        oldProject.Name = category.Name;
                        oldProject.Description = category.Description;
            
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                }
            }
            return View(category);
        }


        public ActionResult Delete(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var category = db.Categories.Where(x => x.Id == id).FirstOrDefault();

                var projects = db.Projects.Where(x=>x.CategoryId==id).ToList();


                foreach (var item in projects)
                {
                    item.CategoryId = null;
                }

                db.SaveChanges();
                db.Categories.Remove(category);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
        }


    }
}