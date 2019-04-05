using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollectorApplication.Models;

namespace TrashCollectorApplication.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(List<Client> filteredClient)
        {
            //string currentUserId = User.Identity.GetUserId();
            //Employee user = db.Employees.Where(c => c.ApplicationUserId == currentUserId).FirstOrDefault();
            //var clientsByZip = db.Clients.Where(c => c.ZipCode == user.ZipCode).ToList();
            //foreach (var client in clientsByZip)
            //{
            //    client.PickupDays = db.PickupDays.ToList();
            //}
            return View("Index",filteredClient);
        }

        public ActionResult FilterByZip()
        {
            string currentUserId = User.Identity.GetUserId();
            Employee user = db.Employees.Where(c => c.ApplicationUserId == currentUserId).FirstOrDefault();
            var clientsByZip = db.Clients.Where(c => c.ZipCode == user.ZipCode).ToList();
            foreach (var client in clientsByZip)
            {
                client.PickupDays = db.PickupDays.ToList();
            }
            return Index(clientsByZip);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        public ActionResult Create()
        {
            Employee employeeToAdd = new Employee();
            return View(employeeToAdd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                employee.ApplicationUserId = currentUserId;
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        public ActionResult Filter()
        {
            var days = db.PickupDays.ToList();
            Client client = new Client()
            {
                PickupDays = days
            };
            return FilterByDay(client);
        }
        
        public ActionResult FilterByDay(Client filteredClient)
        {
            string currentUserId = User.Identity.GetUserId();
            Employee user = db.Employees.Where(c => c.ApplicationUserId == currentUserId).FirstOrDefault();
            var clientsByDay = db.Clients.Where(c => c.ZipCode == user.ZipCode && c.PickupDayId == filteredClient.PickupDayId).ToList();
            foreach (var client in clientsByDay)
            {
                client.PickupDays = db.PickupDays.ToList();
            }
            return Index(clientsByDay);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,UserName,Password,FirstName,LastName,EmployeeId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
