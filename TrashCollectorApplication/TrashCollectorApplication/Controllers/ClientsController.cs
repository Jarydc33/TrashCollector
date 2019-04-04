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
            Client client = db.Clients.Include(c => c.ApplicationUser).SingleOrDefault(c => c.id == id);

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

        public ActionResult RequestPickup()
        {
            return View();
        }

        public ActionResult SuspendPickups(int id)
        {
            Client client = db.Clients.Include(c => c.ApplicationUser).SingleOrDefault(c => c.id == id);
            return View(client);
        }

        [HttpPost]
        public ActionResult SuspendPickups(Client client)
        {
            var clientToSuspend = db.Clients.Single(c => c.id == client.id);
            clientToSuspend.PickupDayId = null;
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
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Client client = db.Clients.Include(c => c.ApplicationUser).SingleOrDefault(c => c.id == id);

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
        public ActionResult Delete(int? id)
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
