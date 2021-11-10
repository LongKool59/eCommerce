using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class SignInController : Controller
    {
        // GET: SignIn
        public ActionResult SignIn()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}