using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollectorApplication.Models;

namespace TrashCollectorApplication.Controllers
{
    public class ClientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();
            Client user = db.Clients.Where(c => c.ApplicationUserId == currentUserId).FirstOrDefault();

            return View(user);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Client client = db.Clients.SingleOrDefault(c => c.id == id);
            client.PickupDays = db.PickupDays.ToList();
            client.ApplicationUser = db.Users.Where(c => c.Id == client.ApplicationUserId).FirstOrDefault();

            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        public ActionResult Create()
        {
            var days = db.PickupDays.ToList();
            Client clientToAdd = new Client()
            {
                PickupDays = days
            };
            return View(clientToAdd);
        }

        public ActionResult RequestPickup(int id)
        {
            var days = db.PickupDays.ToList();
            Client client = new Client()
            {
                PickupDays = days
            };
            return View(client);
        }

        [HttpPost]
        public ActionResult RequestPickup(Client client)
        {
            Client clientRequesting = db.Clients.Single(c => c.id == client.id);
            clientRequesting.OneTimePickupDate = client.OneTimePickupDate;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SuspendPickups(int id)
        {
            Client client = new Client();
            return View(client);
        }

        [HttpPost]
        public ActionResult SuspendPickups(Client client)
        {
            var suspendClient = db.Clients.Single(c => c.id == client.id);
            suspendClient.SuspensionStartDate = client.SuspensionStartDate;
            suspendClient.SuspensionEndDate = client.SuspensionEndDate;
            client.AccountSuspended = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                client.ApplicationUserId = currentUserId;
                if(client.FirstName == null)
                {
                    client.FirstName = "Unknown";
                }
                client.OneTimePickupDate = null;
                client.SuspensionStartDate = null;
                client.SuspensionEndDate = null;
                if(client.Address != null)
                {
                    float[] coords = GetLatLong(client);
                    client.Latitude = coords[0];
                    client.Longitutde = coords[1];
                }
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Client client = db.Clients.SingleOrDefault(c => c.id == id);
            client.PickupDays = db.PickupDays.ToList();

            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }
        
        [HttpPost]
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
                float[] coords = GetLatLong(client);
                clientToEdit.Latitude = coords[0];
                clientToEdit.Longitutde = coords[1];
            }
            clientToEdit.PickupDayId = client.PickupDayId;
            clientToEdit.OneTimePickupDate = client.OneTimePickupDate;
            client.PickupDays = db.PickupDays.ToList();
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult MakePayment(int? id)
        {
            Client client = db.Clients.Find(id);
            var stripePublishKey = "";
            ViewBag.StripePublishKey = stripePublishKey;
            return View(client);
        }
        [HttpPost]
        public ActionResult MakePayment(int id)
        {
            Client client = db.Clients.Find(id);
            client.AmountOwed = 0;
            db.SaveChanges();
            return View(client);
        }

        public static float[] GetLatLong(Client client)
        {
            GoogleMap myMap = new GoogleMap();
            string url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + client.Address + ",+" + client.City + ",+" + client.State + "&key=";
            WebRequest requestObject = WebRequest.Create(url);
            requestObject.Method = "GET";
            HttpWebResponse responseObject = null;
            responseObject = (HttpWebResponse)requestObject.GetResponse();

            string urlResult = null;
            using (Stream stream = responseObject.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                urlResult = sr.ReadToEnd();
                sr.Close();
            }

            float lat;
            float longitutde;

            myMap = JsonConvert.DeserializeObject<GoogleMap>(urlResult);
            try
            {
                lat = myMap.results[0].geometry.location.lat;
                longitutde = myMap.results[0].geometry.location.lng;
            }
            catch
            {
                return null;
            }
            float[] coords = new float[2];
            coords[0] = lat;
            coords[1] = longitutde;
            return coords;

        }
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
