using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TrashCollectorApplication.Models;

namespace TrashCollectorApplication.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(List<Client> filteredClient, string dayOfWeek)
        {
            ViewBag.DayList = new SelectList(db.PickupDays.ToList(), "id", "GarbagePickupDay");

            if (dayOfWeek == null)
            {
                string currentUserId = User.Identity.GetUserId();
                Employee user = db.Employees.Where(c => c.ApplicationUserId == currentUserId).FirstOrDefault();
                var clientsByZip = db.Clients.Where(c => c.ZipCode == user.ZipCode).ToList();
                foreach (var client in clientsByZip)
                {
                    client.PickupDays = db.PickupDays.ToList();
                }
                TempData["myModel"] = clientsByZip;
                return View(clientsByZip);
            }
            else
            {
                int? dayId = null;
                try
                {
                    dayId = int.Parse(dayOfWeek);
                }
                catch
                {
                    dayId = null;
                }
                
                string currentUserId = User.Identity.GetUserId();
                Employee user = db.Employees.Where(c => c.ApplicationUserId == currentUserId).FirstOrDefault();
                var clientsByDay = db.Clients.Where(c => c.ZipCode == user.ZipCode && c.PickupDayId == dayId).ToList();
                foreach (var clients in clientsByDay)
                {
                    clients.PickupDays = db.PickupDays.ToList();
                }
                TempData["myModel"] = clientsByDay;
                return View(clientsByDay);
            }
            
        }

        public ActionResult Confirm(int id)
        {
            Client client = db.Clients.Find(id);
            client.AmountOwed += 35;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Details(int? id)
        {
            Client client = db.Clients.Find(id);

            client.ApplicationUser = db.Users.Where(c => c.Id == client.ApplicationUserId).FirstOrDefault();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(client);
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
        public ActionResult Edit(int? id)
        {
            Client client = db.Clients.Find(id);
            client.PickupDays = db.PickupDays.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Client client)
        {
            var clientToEdit = db.Clients.Single(c => c.id == client.id);
            clientToEdit.FirstName = client.FirstName;
            clientToEdit.LastName = client.LastName;
            clientToEdit.ZipCode = client.ZipCode;
            clientToEdit.State = client.State;
            clientToEdit.City = client.City;
            if(clientToEdit.Address != client.Address)
            {
                clientToEdit.Address = client.Address;
                float[] coords = ClientsController.GetLatLong(client);
                clientToEdit.Latitude = coords[0];
                clientToEdit.Longitutde = coords[1];
            }
            clientToEdit.PickupDayId = client.PickupDayId;
            client.PickupDays = db.PickupDays.ToList();
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult InitializeMap(int? id)
        {            
            if (id != null)
            {
                List<Client> allClients = new List<Client>();
                Client client = db.Clients.Find(id);
                allClients.Add(client);
                List<float> latLongs = new List<float>();
                latLongs.Add(client.Latitude);
                latLongs.Add(client.Longitutde);
                ViewBag.LatLongs = latLongs;
                
            }
            return View();
        }

        public ActionResult ViewAllClients(List<Client> clients)
        {
            List<Client> model = TempData["myModel"] as List<Client>;
            List<float> latLongs = new List<float>();
            //string currentUserId = User.Identity.GetUserId();
            //Employee currentEmployee = db.Employees.Where(e => e.ApplicationUserId == currentUserId).FirstOrDefault();
            //var allClients = db.Clients.Where(c => c.ZipCode == currentEmployee.ZipCode).ToList();
            //var allClients = model;
            foreach(var client in model)
            {
                latLongs.Add(client.Latitude);
                latLongs.Add(client.Longitutde);
            }
            ViewBag.LatLongs = latLongs;
            return View("InitializeMap",model);
        }

        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            ApplicationUser user = db.Users.Where(u => u.Id == client.ApplicationUserId).FirstOrDefault();
            db.Clients.Remove(client);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
