using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using eCommerce.Models;
using eCommerce.Models.ViewModels;
using eCommerce.Extensions;
using System.Net;

namespace eCommerce.Areas.Admin.Controllers
{
    public class ManageUserController : Controller
    {
        // GET: Admin/ManageUser
        public ActionResult DanhSachNguoiDung(int? page, string loaiTimKiem, string tenTimKiem, string submit)
        {
            DauGiaEntities db = new DauGiaEntities();
            int pageNumber = page ?? 1;
            int pageSize = 10;
            IQueryable<NguoiDung> nguoiDungs = db.NguoiDungs.OrderBy(x => x.HoTen);
            List<NguoiDungViewModel> nguoiDungViewModels; ;
            TempData["loaiTimKiem"] = loaiTimKiem;
            TempData["tenTimKiem"] = tenTimKiem;
            TempData["page"] = page;
            try
            {
                if (submit != null && loaiTimKiem == null)
                {
                    if (tenTimKiem != "")
                        this.AddNotification("Vui lòng chọn loại tìm kiếm!", NotificationType.WARNING);
                    else
                        this.AddNotification("Vui lòng chọn loại tìm kiếm và nhập từ khóa tìm kiếm!", NotificationType.WARNING);
                }

                switch (loaiTimKiem)
                {
                    case "MaNguoiDung":
                        {
                            if (tenTimKiem == "" || tenTimKiem == null)
                                this.AddNotification("Vui lòng nhập từ khóa để tìm kiếm theo mã người dùng!", NotificationType.WARNING);
                            nguoiDungs = db.NguoiDungs.Where(s => s.MaNguoiDung.ToString().Contains(tenTimKiem)).OrderBy(s => s.HoTen);
                            break;
                        }
                    case "TenNguoiDung":
                        {
                            if (tenTimKiem == "" || tenTimKiem == null)
                                this.AddNotification("Vui lòng nhập từ khóa để tìm kiếm theo tên người dùng!", NotificationType.WARNING);
                            nguoiDungs = db.NguoiDungs.Where(s => s.HoTen.Contains(tenTimKiem)).OrderBy(s => s.HoTen);
                            break;
                        }
                }
            }
            catch
            {
                this.AddNotification("Có lỗi xảy ra. Vui lòng thực hiện tìm kiếm lại!", NotificationType.ERROR);
            }
            nguoiDungViewModels = nguoiDungs.ToList().ConvertAll<NguoiDungViewModel>(s => s);
            return View("DanhSachNguoiDung", nguoiDungViewModels.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ChiTietNguoiDung(int? id)
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

        public ActionResult XoaNguoiDung(int? id)
        {
            TempData.Keep();
            DauGiaEntities db = new DauGiaEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            nguoiDung.TrangThai = false;
            db.SaveChanges();
            return RedirectToAction("DanhSachNguoiDung", new { page = TempData["page"], loaiTimKiem = TempData["loaiTimKiem"], tenTimKiem = TempData["tenTimKiem"] });
        }

        public ActionResult KichHoatNguoiDung(int? id)
        {
            TempData.Keep();
            DauGiaEntities db = new DauGiaEntities();
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            nguoiDung.TrangThai = true;
            db.SaveChanges();
            return RedirectToAction("DanhSachNguoiDung", new
            {
                page = TempData["page"],
                loaiTimKiem = TempData["loaiTimKiem"],
                tenTimKiem = TempData["tenTimKiem"]
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleDelete(List<NguoiDungViewModel> nguoiDungViewModels, string submit)
        {
            DauGiaEntities db = new DauGiaEntities();
            db.Configuration.ValidateOnSaveEnabled = false;
            var checkIsChecked = nguoiDungViewModels.Where(x => x.IsChecked == true).ToList();
            if (checkIsChecked.Count == 0)
            {
                this.AddNotification("Vui lòng chọn người dùng để thực hiện.!", NotificationType.WARNING);
                return RedirectToAction("DanhSachNguoiDung", new { page = TempData["page"], loaiTimKiem = TempData["loaiTimKiem"], tenTimKiem = TempData["tenTimKiem"] });
            }
            if (submit == "XoaNguoiDung")
            {
                //lọc danh sách người dùng có trạng thái là true
                var listTrangThai = checkIsChecked.Where(s => s.TrangThai == true).ToList();
                if (listTrangThai.Count == 0)
                {
                    this.AddNotification("Vui lòng chọn người dùng có trạng thái là đang hoạt động.!", NotificationType.WARNING);
                    return RedirectToAction("DanhSachNguoiDung", new { page = TempData["page"], loaiTimKiem = TempData["loaiTimKiem"], tenTimKiem = TempData["tenTimKiem"] });
                }
                foreach (var nguoiDung in listTrangThai)
                {
                    int maNguoiDung = nguoiDung.MaNguoiDung;
                    NguoiDung nguoi = db.NguoiDungs.Where(s => s.MaNguoiDung == maNguoiDung).FirstOrDefault();
                    if (nguoi != null)
                        if (!nguoi.IsAdmin)
                            nguoi.TrangThai = false;
                }
                db.SaveChanges();
                this.AddNotification("Vô hiệu hóa người dùng thành công.", NotificationType.SUCCESS);
                return RedirectToAction("DanhSachNguoiDung", new { page = TempData["page"], loaiTimKiem = TempData["loaiTimKiem"], tenTimKiem = TempData["tenTimKiem"] });
            }
            if (submit == "XoaQuyenDangDauGia")
            {
                //lọc danh sách người dùng có quyền đăng đấu giá là true
                var listApproved = checkIsChecked.Where(s => s.TrangThai == true).ToList();
                if (listApproved.Count == 0)
                {
                    this.AddNotification("Vui lòng chọn người dùng đang có quyền đăng đấu giá.!", NotificationType.WARNING);
                    return RedirectToAction("DanhSachNguoiDung", new { page = TempData["page"], loaiTimKiem = TempData["loaiTimKiem"], tenTimKiem = TempData["tenTimKiem"] });
                }
                foreach (var nguoiDung in listApproved)
                {
                    int maNguoiDung = nguoiDung.MaNguoiDung;
                    NguoiDung nguoi = db.NguoiDungs.Where(s => s.MaNguoiDung == maNguoiDung).FirstOrDefault();
                    if (nguoi != null)
                        nguoi.IsApproved = false;
                }
                db.SaveChanges();
                this.AddNotification("Xóa quyền đăng đấu giá thành công.", NotificationType.SUCCESS);
                return RedirectToAction("DanhSachNguoiDung", new { page = TempData["page"], loaiTimKiem = TempData["loaiTimKiem"], tenTimKiem = TempData["tenTimKiem"] });
            }

            foreach (var nguoiDung in checkIsChecked)
            {
                int maNguoiDung = nguoiDung.MaNguoiDung;
                NguoiDung nguoi = db.NguoiDungs.Where(s => s.MaNguoiDung == maNguoiDung).FirstOrDefault();
                if (nguoi != null)
                    nguoi.Password = nguoi.NgaySinh.Day.ToString() + nguoi.NgaySinh.Month.ToString() + nguoi.NgaySinh.Year.ToString();
            }
            db.SaveChanges();
            this.AddNotification("Khôi phục mật khẩu thành công.", NotificationType.SUCCESS);
            return RedirectToAction("DanhSachNguoiDung", new { page = TempData["page"], loaiTimKiem = TempData["loaiTimKiem"], tenTimKiem = TempData["tenTimKiem"] });
        }

        public ActionResult DanhSachCapQuyen(int? page, string loaiTimKiem, string tenTimKiem)
        {
            DauGiaEntities db = new DauGiaEntities();
            int pageNumber = page ?? 1;
            int pageSize = 10;
            IQueryable<NguoiDung> nguoiDungs = db.NguoiDungs.Where(s => s.IsRequesting == true).OrderBy(x => x.HoTen);
            List<NguoiDungViewModel> nguoiDungViewModels; ;
            TempData["loaiTimKiem"] = loaiTimKiem;
            TempData["tenTimKiem"] = tenTimKiem;
            TempData["page"] = page;
            try
            {
                switch (loaiTimKiem)
                {
                    case "MaNguoiDung":
                        {
                            if (tenTimKiem == "" || tenTimKiem == null)
                                this.AddNotification("Vui lòng nhập từ khóa để tìm kiếm theo mã người dùng!", NotificationType.WARNING);
                            nguoiDungs = db.NguoiDungs.Where(s => s.MaNguoiDung.ToString().Contains(tenTimKiem) && s.IsRequesting == true).OrderBy(s => s.HoTen);
                            break;
                        }
                    case "TenNguoiDung":
                        {
                            if (tenTimKiem == "" || tenTimKiem == null)
                                this.AddNotification("Vui lòng nhập từ khóa để tìm kiếm theo tên người dùng!", NotificationType.WARNING);
                            nguoiDungs = db.NguoiDungs.Where(s => s.HoTen.Contains(tenTimKiem) && s.IsRequesting == true).OrderBy(s => s.HoTen);
                            break;
                        }
                }
            }
            catch
            {
                this.AddNotification("Có lỗi xảy ra. Vui lòng thực hiện tìm kiếm lại!", NotificationType.ERROR);
            }
            nguoiDungViewModels = nguoiDungs.ToList().ConvertAll<NguoiDungViewModel>(s => s);
            return View("DanhSachNguoiDung", nguoiDungViewModels.ToPagedList(pageNumber, pageSize));
        }
    }
}