using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using eCommerce.Models;
using eCommerce.Models.ViewModels;
using eCommerce.Extensions;

namespace eCommerce.Areas.Admin.Controllers
{
    public class ManageUserController : Controller
    {
        // GET: Admin/ManageUser
        public ActionResult DanhSachNguoiDung(int? page, string loaiTimKiem, string tenTimKiem)
        {
            DauGiaEntities db = new DauGiaEntities();
            int pageNumber = page ?? 1;
            int pageSize = 10;
            IQueryable<NguoiDung> nguoiDungs;
            List<NguoiDungViewModel> nguoiDungViewModels; ;
            TempData["loaiTimKiem"] = loaiTimKiem;
            TempData["tenTimKiem"] = tenTimKiem;

            try
            {
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
            nguoiDungs = db.NguoiDungs.OrderBy(x => x.HoTen);
            nguoiDungViewModels = nguoiDungs.ToList().ConvertAll<NguoiDungViewModel>(s => s);
            return View("DanhSachNguoiDung", nguoiDungViewModels.ToPagedList(pageNumber, pageSize));
        }
    }
}