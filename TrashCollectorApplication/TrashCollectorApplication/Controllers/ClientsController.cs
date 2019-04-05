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
    public class ClientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clients
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();
            Client user = new Client();
            user = db.Clients.Where(c => c.ApplicationUserId == currentUserId).FirstOrDefault(); 

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
            clientRequesting.OneTimePickup = true;
            clientRequesting.OneTimePickupDayId = client.OneTimePickupDayId;
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
            suspendClient.SuspensionStart = client.SuspensionStart;
            suspendClient.SuspensionEnd = client.SuspensionEnd;
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
                client.PickupComplete = false;
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
            clientToEdit.Address = client.Address;
            clientToEdit.PickupDayId = client.PickupDayId;
            client.PickupDays = db.PickupDays.ToList();
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id) //Keep this?
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
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
