using eCommerce.Areas.Admin.Models;
using eCommerce.Areas.User.Models;
using eCommerce.Extensions;
using eCommerce.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DauGiaViewModel = eCommerce.Areas.Admin.Models.DauGiaViewModel;

namespace eCommerce.Areas.Admin.Controllers
{
    public class ManageAuctionController : Controller
    {
        // GET: Admin/ManageAuction
        public ActionResult DanhSachDauGia(int? page, string loaiTimKiem, string tenTimKiem, string submit)
        {
            if (Session["IsAdmin"] == null)
            {
                TempData["toastr-warning"] = "Vui lòng đăng nhập với quyền admin để tiếp tục!";
                return RedirectToAction("SignIn", "SignIn", new { area = "" });
            }
            DauGiaEntities db = new DauGiaEntities();
            int pageNumber = page ?? 1;
            int pageSize = 10;
            IQueryable<DauGia> dauGias = db.DauGias.OrderByDescending(x => x.NgayDang).ThenBy(s => s.NguoiDung.HoTen);
            List<DauGiaViewModel> dauGiaViewModels; ;
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
                    case "MaDauGia":
                        {
                            if (tenTimKiem == "" || tenTimKiem == null)
                                this.AddNotification("Vui lòng nhập từ khóa để tìm kiếm theo mã đấu giá!", NotificationType.WARNING);
                            dauGias = db.DauGias.Where(s => s.MaDauGia.ToString().Contains(tenTimKiem)).OrderByDescending(x => x.NgayDang).ThenBy(s => s.NguoiDung.HoTen);
                            break;
                        }
                    case "TenNguoiBan":
                        {
                            if (tenTimKiem == "" || tenTimKiem == null)
                                this.AddNotification("Vui lòng nhập từ khóa để tìm kiếm theo tên người bán!", NotificationType.WARNING);
                            dauGias = db.DauGias.Where(s => s.NguoiDung.HoTen.Contains(tenTimKiem)).OrderByDescending(x => x.NgayDang).ThenBy(s => s.NguoiDung.HoTen);
                            break;
                        }
                }
            }
            catch
            {
                this.AddNotification("Có lỗi xảy ra. Vui lòng thực hiện tìm kiếm lại!", NotificationType.ERROR);
            }
            dauGiaViewModels = dauGias.ToList().ConvertAll<DauGiaViewModel>(s => s);
            return View("DanhSachDauGia", dauGiaViewModels.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ChiTietDauGia(int? id)
        {
            if (Session["IsAdmin"] == null)
            {
                TempData["toastr-warning"] = "Vui lòng đăng nhập với quyền admin để tiếp tục!";
                return RedirectToAction("SignIn", "SignIn", new { area = "" });
            }
            TempData.Keep();
            DauGiaEntities db = new DauGiaEntities();
            ViewModel view = new ViewModel();
            var dg = db.DauGias.Where(m => m.MaDauGia == id).SingleOrDefault();
            var loai = from l in db.Loais
                       join ct in db.CT_LoaiDauGia on l.MaLoai equals ct.MaLoai
                       join b in db.DauGias on ct.MaDauGia equals b.MaDauGia
                       where b.MaDauGia == id
                       select l;
            var hinh = db.HinhAnhs.Where(m => m.MaDauGia == id).Select(m => m.LinkAnh).ToArray();
            view.ListLoaiSanPham = loai.ToList().ConvertAll<LoaiViewModel>(x => x);
            view.DauGia = dg;
            view.Hinh = hinh;
            view.YeuThich = false;
            if (Session["MaNguoiDung"] == null)
            {
                view.YeuThich = false;
            }
            else
            {
                int ID = int.Parse(Session["MaNguoiDung"].ToString());
                var yt = db.YeuThiches.Where(m => m.MaDauGia == id && m.MaNguoiDung == ID).SingleOrDefault();
                if (yt != null)
                {
                    view.YeuThich = true;
                }
            }
            NotificationComponents components = new NotificationComponents((int)id);
            components.RegisterLiveAuction((int)id);
            TempData["MaDauGia"] = id;
            return View(view);
        }

        public ActionResult VoHieuHoaDauGia(int? maDauGia)
        {
            if (Session["IsAdmin"] == null)
            {
                TempData["toastr-warning"] = "Vui lòng đăng nhập với quyền admin để tiếp tục!";
                return RedirectToAction("SignIn", "SignIn", new { area = "" });
            }
            if (maDauGia == null)
                return HttpNotFound();

            DauGiaEntities db = new DauGiaEntities();
            CT_TrangThai ct = new CT_TrangThai();
            ct.MaDauGia = (int)maDauGia;
            var tt = db.TrangThaiDauGias.Where(m => m.TenTrangThai == "UnActive").SingleOrDefault();
            ct.MaTrangThai = tt.MaTrangThai;
            ct.ThoiGian = DateTime.Now;
            db.CT_TrangThai.Add(ct);
            db.SaveChanges();
            TempData["toastr-success"] = $"Vô hiệu hóa buổi đấu giá (Mã {maDauGia}) thành công.";
            return RedirectToAction("DanhSachDauGia");
        }
    }
}