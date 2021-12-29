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
        public ActionResult Edit(NguoiDungViewModel nd)
        {
            /*if (submit == "QuayLai")
                return RedirectToAction("ThongTinCaNhan", new { id = Session["MaNguoiDung"] });*/


            ViewBag.DSThanhPho = new SelectList(GetDSThanhPho(), "MaTP", "TenTP");
            if (!ModelState.IsValid)
            {
                ViewBag.ListQuan = new SelectList(db.Quans.Where(s => s.MaTP == nd.MaTP).ToList(), "MaQuan", "TenQuan");
                ViewBag.ListPhuong = new SelectList(db.Phuongs.Where(s => s.MaQuan == nd.MaQuan).ToList(), "MaPhuong", "TenPhuong");
            }

            //nếu người dùng thay đổi hình ảnh
            if (nd.ImageFile != null)
            {
                //*/Xóa hình ảnh cũ trong folder
                /*string oldImgPath = nd.HinhAnh;
                FileInfo fi = new FileInfo(oldImgPath);
                if (fi != null)
                    System.IO.File.Delete(Server.MapPath(oldImgPath));

                //thêm hình ảnh vào thư mục UserImages và lưu đường dẫn vào database
                string extension = Path.GetExtension(thongTinAdmin.ImageFile.FileName);
                string fileName = DateTime.Now.ToString("ddMMyyyymmssfff") + extension;
                thongTinAdmin.HinhAnh = "~/UserImages/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/UserImages/"), fileName);
                thongTinAdmin.ImageFile.SaveAs(fileName);*/
            }

            //kiểm tra mail nếu có thay đổi thì có trùng không
            NguoiDung mail = db.NguoiDungs.Where(s => s.MaNguoiDung != nd.MaNguoiDung && s.Email == nd.Email).FirstOrDefault();
            if (mail != null)
            {
                this.AddNotification("Email này bị trùng. Vui lòng chọn email khác!", NotificationType.WARNING);
                return View(nd);
            }
            int id = int.Parse(Session["MaNguoiDung"].ToString());

            NguoiDung nguoi = db.NguoiDungs.Where(m=>m.MaNguoiDung==id).SingleOrDefault();
            nguoi.HoTen=nd.HoTen;
            
            nguoi.HinhAnh = nd.HinhAnh;
            nguoi.NgaySinh = nd.NgaySinh;
            nguoi.SoCMND = nd.SoCMND;
            nguoi.Email = nd.Email;
            nguoi.DiaChi = nd.DiaChi;
            nguoi.MaTP = nd.MaTP;
            nguoi.MaQuan = nd.MaQuan;
            nguoi.MaPhuong = nd.MaPhuong;
            nguoi.SDT = nd.SDT;
/*            db.Entry(nguoi).State = System.Data.Entity.EntityState.Modified;
*/            db.SaveChanges();

            return View(nd);
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
        public ActionResult ListRating(int? page)
        {
            /* int pageNumber = page ?? 1;
             int pageSize = 5;
             IQueryable<DauGia> DauGia;
             List<DauGiaViewModel> DGViewModel;
             var DauGia_full = db.DauGias.OrderBy(x => x.NgayDang);

             int ID = int.Parse(Session["MaNguoiDung"].ToString());
             DauGia = DauGia_full.Where(p => p.MaNguoiMua == ID).OrderBy(x => x.NgayKetThuc);

             DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);*/

            return View();
        }
    }
}