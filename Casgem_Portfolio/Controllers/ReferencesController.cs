using Casgem_Portfolio.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Casgem_Portfolio.Controllers
{
    [Authorize]
    public class ReferencesController : Controller
    {
        CasgemPortfolioEntities db = new CasgemPortfolioEntities();
        // GET: References
        public ActionResult Index()
        {
            var values = db.TblReference.ToList();
            return View(values);
        }
    }
}