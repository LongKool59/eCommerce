using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class GuestController : Controller
    {
        // GET: Guest
        public ActionResult Home()
        {
            return RedirectToAction("Home", "User/Home");
        }
    }
}