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
                return View(clientsByDay);
            }
            
        }

        public ActionResult Confirm(int id)
        {
            Client client = db.Clients.Find(id);
            client.AmountOwed += 35;
            db.SaveChanges();
            return RedirectToAction("FilterByZip");
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

        public ActionResult InitializeMap(int? id)
        {
            Client client = db.Clients.Find(id);
            List<float> allLatLongs = new List<float>();
            GetLatLong(client);
            allLatLongs.Add(ViewBag.Lat);
            allLatLongs.Add(ViewBag.Long);
            return View(allLatLongs);
        }

        public ActionResult ViewAllClients()
        {
            var allClients = db.Clients.ToList();
            List<float> allLatLongs = new List<float>();
            foreach (var client in allClients)
            {
                if(client.Address != null)
                {
                    GetLatLong(client);
                    allLatLongs.Add(ViewBag.Lat);
                    allLatLongs.Add(ViewBag.Long);
                }
            }
            return View("InitializeMap",allLatLongs);
        }

        public void GetLatLong(Client client)
        {
            GoogleMap myMap = new GoogleMap();
            string strurltest = "https://maps.googleapis.com/maps/api/geocode/json?address=" + client.Address+ ",+"+client.City+",+"+client.State+ "&key="; 
            WebRequest requestObject = WebRequest.Create(strurltest);
            requestObject.Method = "GET";
            HttpWebResponse responseObject = null;
            responseObject = (HttpWebResponse)requestObject.GetResponse();

            string strresulttest = null;
            using (Stream stream = responseObject.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                strresulttest = sr.ReadToEnd();
                sr.Close();
            }

            float lat;
            float longitutde;

            myMap = JsonConvert.DeserializeObject<GoogleMap>(strresulttest);
            try
            {
                lat = myMap.results[0].geometry.location.lat;
                longitutde = myMap.results[0].geometry.location.lng;
            }
            catch
            {
                return;
            }
            ViewBag.Lat = lat;
            ViewBag.Long = longitutde;

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

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }

    public class GoogleMap
    {
        public Result[] results { get; set; }
        public string status { get; set; }
    }

    public class Result
    {
        public Geometry geometry { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
        public string location_type { get; set; }
    }

    public class Location
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }
}
