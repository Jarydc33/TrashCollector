using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollectorApplication.Models;

namespace TrashCollectorApplication.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated) //Potentially, add the authentication from this pages index to every page frmo the other controllers
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Admin";
                    return RedirectToAction("Index","Administrators");
                }
                else if (isEmployeeUser())
                {
                    ViewBag.displayMenu = "Employee";
                    return RedirectToAction("Index", "Employees");
                }
                else if (isClientUser())
                {
                    ViewBag.displayMenu = "Client";
                    return RedirectToAction("Index", "Clients");
                }
            }
            return View();
        }

        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                var getRole = UserManager.GetRoles(user.GetUserId());
                if(getRole[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public Boolean isEmployeeUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                var getRole = UserManager.GetRoles(user.GetUserId());
                if (getRole[0].ToString() == "Employee")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public Boolean isClientUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                var getRole = UserManager.GetRoles(user.GetUserId());
                if (getRole[0].ToString() == "Client")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}