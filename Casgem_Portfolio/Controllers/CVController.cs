using Casgem_Portfolio.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Casgem_Portfolio.Controllers
{
    [Authorize]
    public class CVController : Controller
    {
        CasgemPortfolioEntities db = new CasgemPortfolioEntities();
        // GET: CV

        public ActionResult Index()
        {
            return View(db.TblResume.FirstOrDefault());
        }
        public ActionResult Edit(int id)
        {
            TblResume r = db.TblResume.Find(id);
            return View(r);
        }
        [HttpPost]
        public ActionResult Edit(TblResume r)
        {
            TblResume resume = db.TblResume.FirstOrDefault(x=>x.ResumeID==r.ResumeID);
            resume.Title1 = r.Title1;
            resume.Title2 = r.Title2;
            resume.Description = r.Description;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}