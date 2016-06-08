using Repositories;
using Repositories.Database;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GolfSlayer.Models;

namespace GolfSlayer.Controllers
{
    public class InformationController : BaseController
    {
        public ActionResult About()
        {
            return View();
        }

        public ActionResult AboutSH()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Rules()
        {
            return View();
        }

        public ActionResult Sponsors()
        {
            return View();
        }
    }
}