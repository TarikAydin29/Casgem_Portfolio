using Casgem_Portfolio.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Casgem_Portfolio.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        CasgemPortfolioEntities db = new CasgemPortfolioEntities();

        // GET: Project
        public ActionResult Index()
        {
            var projects = db.TblProject.ToList();
            return View(projects);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(TblProject p, HttpPostedFileBase image)
        {
            ResimKontrolleri(image);
            p.ImageUrl = ResimYukle(image);
            db.TblProject.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            TblProject project = db.TblProject.Find(id);
            db.TblProject.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            TblProject p = db.TblProject.FirstOrDefault(x => x.ProjectID == id);
            if (p.ImageUrl != null)
            {
                Session["imageUrl"] = p.ImageUrl;
            }

            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(TblProject p, HttpPostedFileBase image)
        {
            if (image != null)
            {
                ResimKontrolleri(image);
                p.ImageUrl = ResimYukle(image);
            }
            else
            {
                p.ImageUrl = Session["imageurl"].ToString();
            }
            TblProject project = db.TblProject.FirstOrDefault(x => x.ProjectID == p.ProjectID);
          
                project.Name = p.Name;           
                project.Summary = p.Summary;          
                project.Description = p.Description;

            db.SaveChanges();
            return RedirectToAction("Index");
        }




        private string ResimYukle(HttpPostedFileBase image)
        {
            string fileName = Path.GetFileName(image.FileName);
            string fileExtension = Path.GetExtension(fileName);
            string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
            string imagePath = Server.MapPath("/Images/" + uniqueFileName);
            image.SaveAs(imagePath);
            return uniqueFileName;
        }

        private void ResimKontrolleri(HttpPostedFileBase image)
        {
            string[] izinVerilenler = { ".jpg", ".png", ".jpeg" };
            if (image != null)
            {
                string ext = Path.GetExtension(image.FileName);
                if (!izinVerilenler.Contains(ext))
                {
                    ModelState.AddModelError("image", "İzin verilen dosya uzantıları jpeg,jpg,png");
                }
            }
            else
            {
                ModelState.AddModelError("image", "Resim Zorunlu");
            }
        }

    }
}