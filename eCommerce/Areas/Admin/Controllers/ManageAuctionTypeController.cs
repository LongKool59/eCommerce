using eCommerce.Extensions;
using eCommerce.Models;
using eCommerce.Models.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Areas.Admin.Controllers
{
    public class ManageAuctionTypeController : Controller
    {
        // GET: Admin/ManageAuctionType
        public ActionResult DanhSachLoaiDauGia(int? page, string loaiTimKiem, string tenTimKiem, string submit)
        {
            DauGiaEntities db = new DauGiaEntities();
            int pageNumber = page ?? 1;
            int pageSize = 10;
            IQueryable<Loai> loais = db.Loais.OrderBy(x => x.TenLoai);
            List<LoaiDauGiaViewModel> loaiViewModels; ;
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
                    case "MaLoaiDauGia":
                        {
                            if (tenTimKiem == "" || tenTimKiem == null)
                                this.AddNotification("Vui lòng nhập từ khóa để tìm kiếm theo mã người dùng!", NotificationType.WARNING);
                            loais = db.Loais.Where(s => s.MaLoai.ToString().Contains(tenTimKiem)).OrderBy(s => s.TenLoai);
                            break;
                        }
                    case "TenLoaiDauGia":
                        {
                            if (tenTimKiem == "" || tenTimKiem == null)
                                this.AddNotification("Vui lòng nhập từ khóa để tìm kiếm theo tên người dùng!", NotificationType.WARNING);
                            loais = db.Loais.Where(s => s.TenLoai.Contains(tenTimKiem)).OrderBy(s => s.TenLoai);
                            break;
                        }
                }
            }
            catch
            {
                this.AddNotification("Có lỗi xảy ra. Vui lòng thực hiện tìm kiếm lại!", NotificationType.ERROR);
            }
            loaiViewModels = loais.ToList().ConvertAll<LoaiDauGiaViewModel>(s => s);
            return View("DanhSachLoaiDauGia", loaiViewModels.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleDelete(List<LoaiDauGiaViewModel> loaiDauGiaViewModels)
        {
            DauGiaEntities db = new DauGiaEntities();
            db.Configuration.ValidateOnSaveEnabled = false;

            //kiểm tra danh sách những người dùng được chọn
            var checkIsChecked = loaiDauGiaViewModels.Where(x => x.IsChecked == true).ToList();
            if (checkIsChecked.Count == 0)
            {
                this.AddNotification("Vui lòng chọn loại đấu giá để thực hiện.!", NotificationType.WARNING);
                return RedirectToAction("DanhSachLoaiDauGia", new { page = TempData["page"], loaiTimKiem = TempData["loaiTimKiem"], tenTimKiem = TempData["tenTimKiem"] });
            }
            foreach (var loaiDauGia in checkIsChecked)
            {
                int maLoai = loaiDauGia.MaLoai;
                Loai loai = db.Loais.Where(s => s.MaLoai == maLoai).FirstOrDefault();
                if (loai != null)
                    loai.TrangThai = false;
            }

            db.SaveChanges();
            this.AddNotification("Vô hiệu hóa loại đấu giá thành công.", NotificationType.SUCCESS);
            return RedirectToAction("DanhSachLoaiDauGia", new { page = TempData["page"], loaiTimKiem = TempData["loaiTimKiem"], tenTimKiem = TempData["tenTimKiem"] });
        }
    }
}