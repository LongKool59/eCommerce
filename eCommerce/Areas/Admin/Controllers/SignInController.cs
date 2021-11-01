using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Areas.Admin.Controllers
{
    public class SignInController : Controller
    {
        // GET: Admin/SignIn
        public ActionResult SignIn()
        {
            return View();
        }
    }
}