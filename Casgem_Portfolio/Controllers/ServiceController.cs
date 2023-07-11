using Casgem_Portfolio.Models.Entities;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;

namespace Casgem_Portfolio.Controllers
{
    public class ServiceController : Controller
    {

        CasgemPortfolioEntities db = new CasgemPortfolioEntities();

        // GET: Service
        public ActionResult Index()
        {
            var values = db.TblService.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddService()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddService(TblService tblService)
        {
            db.TblService.Add(tblService);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteService(int id)
        {
            TblService tblService = db.TblService.FirstOrDefault(x => x.ServiceID == id);
            db.TblService.Remove(tblService);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UpdateService(int id)
        {
            TblService tblService = db.TblService.FirstOrDefault(x => x.ServiceID == id);
            return View(tblService);
        }
        [HttpPost]
        public ActionResult UpdateService(TblService tblService)
        {
            TblService value = db.TblService.FirstOrDefault(x => x.ServiceID == tblService.ServiceID);
            value.ServiceTitle = tblService.ServiceTitle;
            value.ServiceIcon = tblService.ServiceIcon;
            value.ServiceNumber = tblService.ServiceNumber;
            value.ServiceContent = tblService.ServiceContent;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}