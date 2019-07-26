using Sehel_MVCGiris.Data;
using Sehel_MVCGiris.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult Create(Project project,HttpPostedFileBase upload)
        {
            if(ModelState.IsValid)
            {
               
                using (var db = new ApplicationDbContext())
                {//dosyayı upload yapmayı dene
                    try
                    {
                        //yüklenen dosyanın addını entitdeki alana ata.
                        
                    project.Photo = UploadFile(upload);
                    }
                    catch(Exception ex)
                    {//upload sırasında bir hata oluşursa view de görüntülmej üzere hatayı değişkene ekle.
                        ViewBag.Error = ex.Message;
                        ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
                        //hata oluş. için proyei veritabanına eklemek yerine viewvı tekrar göster ve mettottan çık
                        return View(project);


                    }
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


        public string UploadFile(HttpPostedFileBase upload)
        {
            //yüklemek istenen dosya var mı?
            if (upload != null && upload.ContentLength > 0)
            {
              
                    //dosyanın uzantısını kontrol et
                    var extension = Path.GetExtension(upload.FileName).ToLower();
               if (extension==".jpg"||extension==".jpeg"||extension==".gif"||extension==".png")
               {
                    //uzantı doğruysa dosynaın yükleneceği uploads dizini var mı kontol et
                    if (Directory.Exists(Server.MapPath("~/Uploads")))//dosyannın halinin en uzununu alıyor cnin içinde onun içinde vs. diyor.
                    {
                        string fileName = upload.FileName.ToLower();//Türkçe karakter Sorununu aşmaya çallıyorum.dosya adındaki türkçe karakterleri düzelt.
                        fileName = fileName.Replace("İ", "i");
                        fileName = fileName.Replace("Ş", "s");
                        fileName = fileName.Replace("ı", "i");
                        fileName = fileName.Replace("(", "");
                        fileName = fileName.Replace(")", "");
                        fileName = fileName.Replace(" ", "-");
                        fileName = fileName.Replace(",", "");
                        fileName = fileName.Replace("ö", "o");
                        fileName = fileName.Replace("ü", "u");
                        fileName = fileName.Replace("`", "");
                        fileName = fileName.Replace("ğ", "g");
                        fileName = fileName.Replace("G", "g");
                        fileName = DateTime.Now.Ticks.ToString() + fileName;//Dosya adları hertürlü zaman pulu sayesinde farklı olacak.
                        upload.SaveAs(Path.Combine(Server.MapPath("~/Uploads"), fileName));//dosyayı upload dizinine yükle.combine ile mac de doysa arasında - var burada / linux da başka bu sorunu çözebilmek için! Platform bağımsız olan Conbine yapıuyoryz.
                        return fileName;
                        //project.Photo = fileName;
                    }
                    else
                    {
                        throw new Exception("Uploads dizini mevcut değildir.");
                    }

                }
                else
                {
                    throw new Exception("Dosya uzantısı .jpg .gif .png olmalıdır.");
                }

            }return null;
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
            public ActionResult Edit(Project project, HttpPostedFileBase upload, string foto)
            {


            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    try
                    {
                        //yüklenen dosyanın addını entitdeki alana ata.
                        project.Photo = UploadFile(upload);
                    }
                    catch (Exception ex)
                    {//upload sırasında bir hata oluşursa view de görüntülmej üzere hatayı değişkene ekle.
                        ViewBag.Error = ex.Message;
                        ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
                        //hata oluş. için proyei veritabanına eklemek yerine viewvı tekrar göster ve mettottan çık
                        return View(project);


                    }
                    //yüklenen dosyanın addını entitdeki alana ata.
                   

                    var oldProject = db.Projects.Where(x => x.Id == project.Id).FirstOrDefault();
                    if (oldProject != null)
                    {
                        oldProject.Title = project.Title;
                        oldProject.Description = project.Description;
                        oldProject.Body = project.Body;
                        if (!string.IsNullOrEmpty(foto))
                        {
                            oldProject.Photo = null;
                        }
                        if (!string.IsNullOrEmpty(project.Photo))
                        {
                            oldProject.Photo = project.Photo;
                        }

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