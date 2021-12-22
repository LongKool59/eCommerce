using eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ThongBaoDSNguoiDung()
        {
            NotificationComponents NC = new NotificationComponents();
            var list = NC.GetNguoiDungs(true);
            //DauGiaEntities db = new DauGiaEntities();
            //var list = db.NguoiDungs.Where(s => s.IsRequesting == true).OrderByDescending(s => s.TimeRequesting).ToList();
            //update session here for get only new added contacts (notification)
            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}