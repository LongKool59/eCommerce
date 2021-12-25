using eCommerce.Extensions;
using eCommerce.Models;
using eCommerce.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult ThongTinCaNhan(int? id)
        {
            TempData.Keep();
            DauGiaEntities db = new DauGiaEntities();
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
                return HttpNotFound();

            NguoiDungViewModel nguoiDungViewModel = nguoiDung;
            return View(nguoiDungViewModel);
        }

        public ActionResult DoiMatKhau()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoiMatKhau(ChangePasswordModel model, string submit)
        {
            if (submit == "QuayLai")
                return RedirectToAction("ThongTinCaNhan", new { id = Session["MaNguoiDung"] });

            if (!ModelState.IsValid)
                return View(model);

            DauGiaEntities db = new DauGiaEntities();
            int MaNguoiDung = Convert.ToInt32(Session["MaNguoiDung"]);
            NguoiDung nguoiDung = db.NguoiDungs.Where(s => s.MaNguoiDung == MaNguoiDung).FirstOrDefault();
            if (nguoiDung == null)
                return View();

            if (nguoiDung.Password != model.MatKhauCu)
            {
                this.AddNotification("Sai mật khẩu cũ. Vui lòng nhập lại!", NotificationType.WARNING);
                return View(model);
            }
            nguoiDung.Password = model.MatKhauMoi;
            db.SaveChanges();
            this.AddNotification("Đổi mật khẩu thành công.", NotificationType.SUCCESS);
            return View();
        }
        public ActionResult SuaThongTinCaNhan(int? id)
        {
            TempData.Keep();
            DauGiaEntities db = new DauGiaEntities();
            NguoiDung nguoi = db.NguoiDungs.Find(id);
            ViewBag.DSThanhPho = new SelectList(GetDSThanhPho(), "MaTP", "TenTP");
            ViewBag.ListQuan = new SelectList(db.Quans.Where(s => s.MaTP == nguoi.MaTP).ToList(), "MaQuan", "TenQuan");
            ViewBag.ListPhuong = new SelectList(db.Phuongs.Where(s => s.MaQuan == nguoi.MaQuan).ToList(), "MaPhuong", "TenPhuong");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (nguoi == null)
            {
                return HttpNotFound();
            }
            ThongTinAdmin thongTinAdmin = nguoi;
            return View(thongTinAdmin);
        }

        public List<ThanhPho> GetDSThanhPho()
        {
            DauGiaEntities db = new DauGiaEntities();
            List<ThanhPho> thanhPhos = db.ThanhPhoes.ToList();
            return thanhPhos;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaThongTinCaNhan(ThongTinAdmin thongTinAdmin, string submit)
        {
            if (submit == "QuayLai")
                return RedirectToAction("ThongTinCaNhan", new { id = Session["MaNguoiDung"] });

            DauGiaEntities db = new DauGiaEntities();

            ViewBag.DSThanhPho = new SelectList(GetDSThanhPho(), "MaTP", "TenTP");
            if (!ModelState.IsValid)
            {
                ViewBag.ListQuan = new SelectList(db.Quans.Where(s => s.MaTP == thongTinAdmin.MaTP).ToList(), "MaQuan", "TenQuan");
                ViewBag.ListPhuong = new SelectList(db.Phuongs.Where(s => s.MaQuan == thongTinAdmin.MaQuan).ToList(), "MaPhuong", "TenPhuong");
                return View(thongTinAdmin);
            }

            //nếu người dùng thay đổi hình ảnh
            if (thongTinAdmin.ImageFile != null)
            {
                //Xóa hình ảnh cũ trong folder
                string oldImgPath = thongTinAdmin.HinhAnh;
                FileInfo fi = new FileInfo(oldImgPath);
                if (fi != null)
                    System.IO.File.Delete(Server.MapPath(oldImgPath));

                //thêm hình ảnh vào thư mục UserImages và lưu đường dẫn vào database
                string extension = Path.GetExtension(thongTinAdmin.ImageFile.FileName);
                string fileName = DateTime.Now.ToString("ddMMyyyymmssfff") + extension;
                thongTinAdmin.HinhAnh = "~/UserImages/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/UserImages/"), fileName);
                thongTinAdmin.ImageFile.SaveAs(fileName);
            }

            //kiểm tra mail nếu có thay đổi thì có trùng không
            NguoiDung mail = db.NguoiDungs.Where(s => s.MaNguoiDung != thongTinAdmin.MaNguoiDung && s.Email == thongTinAdmin.Email).FirstOrDefault();
            if (mail != null)
            {
                this.AddNotification("Email này bị trùng. Vui lòng chọn email khác!", NotificationType.WARNING);
                return View(thongTinAdmin);
            }

            NguoiDung nguoi = thongTinAdmin;
            db.Entry(nguoi).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ThongTinCaNhan", new { id = Session["MaNguoiDung"] });
        }
    }
}