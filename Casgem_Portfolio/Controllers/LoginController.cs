using Casgem_Portfolio.Models.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Casgem_Portfolio.Controllers
{
    public class LoginController : Controller
    {
        CasgemPortfolioEntities db = new CasgemPortfolioEntities();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(TblAdmins admin)
        {
            TblAdmins a = db.TblAdmins.FirstOrDefault(x => x.UserName == admin.UserName && x.Password == admin.Password);
            if (a != null)
            {
                FormsAuthentication.SetAuthCookie(a.UserName, false);
                return RedirectToAction("Index", "Feature");
            }
            else
            {
                return View();
            }
        }
    }
}