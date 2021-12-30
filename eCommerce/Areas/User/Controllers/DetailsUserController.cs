using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCommerce.Models;
using eCommerce.Areas.User.Models;
using eCommerce.Extensions;
using PagedList;
using System.Data.Entity;

using System.IO;

namespace eCommerce.Areas.User.Controllers
{
    public class DetailsUserController : Controller
    {
        DauGiaEntities db = new DauGiaEntities();
        // GET: User/DetailsUser
        
        public ActionResult Index()
        {
            if(Session["MaNguoiDung"]==null)
            {
                return Redirect("/");

            }
            else
            {
                int id = int.Parse(Session["MaNguoiDung"].ToString());
                NguoiDung nd = new NguoiDung();
                NguoiDungViewModel viewmodel = new NguoiDungViewModel();
                nd = db.NguoiDungs.Where(m => m.MaNguoiDung == id).SingleOrDefault();
                viewmodel = nd;
                return View(viewmodel);
            }
            
        }
        public List<ThanhPho> GetDSThanhPho()
        {
            DauGiaEntities db = new DauGiaEntities();
            List<ThanhPho> thanhPhos = db.ThanhPhoes.ToList();
            return thanhPhos;
        }
        public ActionResult Edit()
        {
            if(Session["MaNguoiDung"]!=null)
            {
                int id = int.Parse(Session["MaNguoiDung"].ToString());
                NguoiDung nds = new NguoiDung();
                nds = db.NguoiDungs.Where(m => m.MaNguoiDung == id).SingleOrDefault();
                ViewBag.DSThanhPho = new SelectList(GetDSThanhPho(), "MaTP", "TenTP");
                ViewBag.ListQuan = new SelectList(db.Quans.Where(s => s.MaTP == nds.MaTP).ToList(), "MaQuan", "TenQuan");
                ViewBag.ListPhuong = new SelectList(db.Phuongs.Where(s => s.MaQuan == nds.MaQuan).ToList(), "MaPhuong", "TenPhuong");
                NguoiDungViewModel nd = new NguoiDungViewModel();
                nd = nds;
                return View(nd);
            }  else
            { return Redirect("/"); }    
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NguoiDungViewModel thongTinAdmin, string submit)
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

            int id = int.Parse(Session["MaNguoiDung"].ToString());

            NguoiDung nguoi = db.NguoiDungs.Where(m=>m.MaNguoiDung==id).SingleOrDefault();
            nguoi.HoTen=thongTinAdmin.HoTen;
/*            

            nguoi.HinhAnh = nd.HinhAnh;
            nguoi.NgaySinh = nd.NgaySinh;
            nguoi.SoCMND = nd.SoCMND;
            nguoi.Email = nd.Email;
            nguoi.DiaChi = nd.DiaChi;
            nguoi.MaTP = nd.MaTP;
            nguoi.MaQuan = nd.MaQuan;
            nguoi.MaPhuong = nd.MaPhuong;
            nguoi.SDT = nd.SDT;
            nguoi.HinhAnh = nd.HinhAnh;*/
/*            db.Entry(nguoi).State = System.Data.Entity.EntityState.Modified;
*/            db.SaveChanges();

            return Redirect("Index");

        }
        [HttpPost, ActionName("Request")]
        public ActionResult Request()
        {
            int id = int.Parse(Session["MaNguoiDung"].ToString());
            NguoiDung nd = new NguoiDung();
            nd = db.NguoiDungs.Where(m => m.MaNguoiDung == id).SingleOrDefault();
            nd.IsRequesting = true;
            nd.TimeRequesting = DateTime.Now;
            db.Entry(nd).State = EntityState.Modified;
            db.SaveChanges();
            return Redirect("Index");

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
                return RedirectToAction("Index", "DetailsUser");

            if (!ModelState.IsValid)
                return View(model);

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
    }
}