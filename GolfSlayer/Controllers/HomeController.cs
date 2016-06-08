using Repositories;
using Repositories.Database;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GolfSlayer.Models;
using GolfSlayer.AppCode;

namespace GolfSlayer.Controllers
{
    public class HomeController : BaseController
    {       

        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult SignIn()
        {
            if (CurrentTeam != null)
            {
                return RedirectToAction("Hub");
            }
            return View();
        }

        // POST: /Account/Manage
        [HttpPost]        
        public ActionResult SignIn(SignIn model)
        {           
            if (TryLogin(model.PIN))
            {
                return RedirectToAction("Hub");
            }
            else
            {
                ModelState.AddModelError("PIN", "Invalid Pin");
                return View();
            }                  
        }
        
        public ActionResult SignOut()
        {
            Logout();

            return RedirectToAction("Index");
        }


        [AuthorizeTeam]
        public ActionResult Hub()
        {           
            return View();
        }
    }
}