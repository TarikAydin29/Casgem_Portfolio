using Casgem_Portfolio.Models.Entities;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace Casgem_Portfolio.Controllers
{
    public class StatisticsController : Controller
    {
        CasgemPortfolioEntities db = new CasgemPortfolioEntities();

        // GET: Statistics
        public ActionResult Index()
        {
            var salary = db.TblEmployee.Max(x => x.EmployeeSalary);
            ViewBag.employeeCount = db.TblEmployee.Count();

            ViewBag.maxSalaryEmployee = db.TblEmployee.Where(x => x.EmployeeSalary == salary).Select(x =>
                x.EmployeeName + " " + x.EmployeeSurname
            ).FirstOrDefault();

            ViewBag.totalCityCount = db.TblEmployee.Select(x=>x.EmployeeCity).Distinct().Count();

            ViewBag.avgEmployeeSalary = db.TblEmployee.Average(x => x.EmployeeSalary);
           
            ViewBag.countSoftwareDeparment = db.TblEmployee.Where(x => x.EmployeeDepartment == db.TblDepartment.Where(y=>y.DepartmentName=="Muhasebe").Select(z=>z.DepartmentID).FirstOrDefault()).Count();

            //Şehri ankara veya adana olanların toplam maaşı
            ViewBag.totalAnkaraAdanaSalary = db.TblEmployee.Where(x => x.EmployeeCity == "Ankara" || x.EmployeeCity == "Adana").Sum(y => y.EmployeeSalary);


            ViewBag.ankaraMuhasebeToplamMaaş = db.TblEmployee.Where(x => x.EmployeeCity == "Ankara" && x.EmployeeDepartment == db.TblDepartment.Where(y => y.DepartmentName == "Muhasebe").Select(z=>z.DepartmentID).FirstOrDefault()).Sum(a=>a.EmployeeSalary);
            
            return View();
        }
    }
}
