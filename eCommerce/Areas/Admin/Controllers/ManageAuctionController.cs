using eCommerce.Areas.Admin.Models;
using eCommerce.Extensions;
using eCommerce.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Areas.Admin.Controllers
{
    public class ManageAuctionController : Controller
    {
        // GET: Admin/ManageAuction
        public ActionResult DanhSachDauGia(int? page, string loaiTimKiem, string tenTimKiem, string submit)
        {
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
            TempData.Keep();
            DauGiaEntities db = new DauGiaEntities();
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            DauGia dauGia = db.DauGias.Find(id);
            if (dauGia == null)
                return HttpNotFound();

            DauGiaViewModel dauGiaViewModel = dauGia;
            return View(dauGiaViewModel);
        }
    }
}