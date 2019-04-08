using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrashCollectorApplication.Models;

namespace TrashCollectorApplication.Controllers
{
    public class AdministratorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrators.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        public ActionResult CreateAccount()
        {
            ViewBag.CreateAccount = new SelectList(db.Roles.Where(u => !u.Name.Contains("Admin")).ToList(), "Name", "Name");
            return View();
        }

        public ActionResult CreateEmployee(string correctId)
        {
            Employee employeeToAdd = new Employee();
            employeeToAdd.ApplicationUserId = correctId;
            return View(employeeToAdd);
        }

        [HttpPost]
        public ActionResult CreateEmployee(Employee employee)
        {
            string currentUserId = User.Identity.GetUserId();
            employee.ApplicationUserId = currentUserId;
            db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EmployeeDetails(int? id)
        {
            Employee employeeToFind = db.Employees.Find(id);
            return View(employeeToFind);
        }

        public ActionResult ClientDetails(int? id)
        {
            Client clientToFind = db.Clients.Find(id);
            return View(clientToFind);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult AllEmployees()
        {
            var allEmployees = db.Employees.ToList();
            return View(allEmployees);
        }

        public ActionResult AllClients()
        {
            var allClients = db.Clients.ToList();
            return View(allClients);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,UserName,Password")] Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                db.Administrators.Add(administrator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(administrator);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrators.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        public ActionResult EmployeeEdit(int? id)
        {
            Employee employee = db.Employees.Find(id);
            return View(employee);
        }

        public ActionResult ClientEdit(int? id)
        {
            Client client = db.Clients.Find(id);
            return View(client);
        }

        [HttpPost]
        public ActionResult EmployeeEdit(Employee employee)
        {
            Employee editedEmployee = db.Employees.Where(e => e.id == employee.id).FirstOrDefault();
            editedEmployee.ZipCode = employee.ZipCode;
            editedEmployee.FirstName = employee.FirstName;
            editedEmployee.LastName = employee.LastName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ClientEdit(Client client)
        {
            Client editedClient = db.Clients.Where(e => e.id == client.id).FirstOrDefault();
            editedClient.ZipCode = client.ZipCode;
            editedClient.FirstName = client.FirstName;
            editedClient.LastName = client.LastName;
            editedClient.State = client.State;
            editedClient.Address = client.Address;
            editedClient.City = client.City;
            editedClient.SuspensionEndDate = client.SuspensionEndDate;
            editedClient.SuspensionStartDate = client.SuspensionStartDate;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(administrator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(administrator);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrators.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Administrator administrator = db.Administrators.Find(id);
            db.Administrators.Remove(administrator);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteEmployee(int id)
        {
            Employee employeeToDelete = db.Employees.Find(id);
            db.Employees.Remove(employeeToDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteClient(int id)
        {
            Client clientToDelete = db.Clients.Find(id);
            db.Clients.Remove(clientToDelete);
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
