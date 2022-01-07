using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Areas.User.Controllers
{
    public class GuestController : Controller
    {
        // GET: User/Guest
        public ActionResult GuestHome()
        {
            return View();
        }
    }
}