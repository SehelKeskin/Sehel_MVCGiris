using Sehel_MVCGiris.Data;
using Sehel_MVCGiris.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sehel_MVCGiris.Areas.Admin.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: Admin/Projects
        public ActionResult Index()
        {
            using (var db = new ApplicationDbContext())
            {

                var projects = db.Projects.Include("Category").ToList();
                return View(projects);
            }
             
        }

        public ActionResult Create()
        {
            var project = new Project();//Boş enttity de varsayılan değer varsa formda dolu geliyor

            using (var db=new ApplicationDbContext())
            {
                ViewBag.Categories =new SelectList( db.Categories.ToList(),"Id","Name");
            }
            return View(project);

       }

        [HttpPost]
        [ValidateInput(false)]//Eksiona zararlı içerik gelmesi engelleniyordu bunu kaldırmış olduk.Bu actiona artık html/scpipt etiketleri göndrilebilir hale getirildi.
        public ActionResult Create(Project project)
        {
            if(ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    db.Projects.Add(project);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            using (var db = new ApplicationDbContext())
            {
                ViewBag.Categories = new SelectList(db.Categories.ToList(),"Id","Name");

            }
                return View(project);

        }

        public ActionResult Edit(int id)
        {
            using (var db=new ApplicationDbContext())
            {
                var project = db.Projects.Where(x=>x.Id==id).FirstOrDefault();

                if (project!=null)
                {
                    ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
                    return View(project);

                }
                else
                {
                    return HttpNotFound();
                }

            }
        }

        [HttpPost]
        [ValidateInput(false)]
            public ActionResult Edit(Project project)
            {


                if (ModelState.IsValid)
                {
                    using (var db = new ApplicationDbContext())
                    {
                        var oldProject = db.Projects.Where(x => x.Id == project.Id).FirstOrDefault();
                        if (oldProject!=null)
                        {
                            oldProject.Title = project.Title;
                            oldProject.Description = project.Description;
                            oldProject.Body = project.Body;
                            oldProject.Photo = project.Photo;
                        oldProject.CategoryId = project.CategoryId;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }

                    }
                }
                using (var db=new ApplicationDbContext())
                {
                    ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
                }
                    return View(project);   
            }


        public ActionResult Delete(int id)
        {
            using (var db=new ApplicationDbContext())
            {
                var project = db.Projects.Where(x => x.Id == id).FirstOrDefault();
                if (project!=null)
                {
                    db.Projects.Remove(project);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    return HttpNotFound();
                }



            }
        }
    }
}