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
using PagedList.Mvc;
using MoMo;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace eCommerce.Areas.User.Controllers
{
    public class HomeController : Controller
    {
        // GET: User/Home
        DauGiaEntities db = new DauGiaEntities();
        public ActionResult Home(int? page, string ten, string loai, string gia, string startdate)
        {
            List<Loai> listL = db.Loais.ToList();
            Loai l = new Loai();
            l.MaLoai = 0;
            l.TenLoai = "Tất cả";
            l.Ngay = DateTime.Now;
            l.MaNguoiDung = 1;
            listL.Add(l);
            ViewBag.ListLoai = new SelectList(listL, "MaLoai", "TenLoai", "0");
            int pageNumber = page ?? 1;
            int pageSize = 9;
            IQueryable<DauGia> DauGia;
            List<DauGiaViewModel> DGViewModel;
            var DauGia_full = db.DauGias.OrderBy(x => x.NgayDang);

            var tt = db.TrangThaiDauGias.Where(m => m.TenTrangThai == "UnActive").SingleOrDefault();
            if (ten == null || ten.Trim() == "")
            {
                if (loai == "0" || loai == null)
                {
                    if (gia == "" || gia == null)
                    {
                        if (startdate == null || startdate.Trim() == "")
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;


                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiMua == null).OrderByDescending(m => m.NgayDang);

                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;
                            DateTime ngay = DateTime.Parse(startdate);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiMua == null && p.NgayBatDau >= ngay).OrderByDescending(m => m.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                    }
                    else
                    {
                        if (startdate == null || startdate.Trim() == "")
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;
                            int price = int.Parse(gia);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiMua == null && p.GiaBanDau >= price).OrderByDescending(m => m.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;
                            DateTime ngay = DateTime.Parse(startdate);
                            int price = int.Parse(gia);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiMua == null && p.NgayBatDau >= ngay && p.GiaBanDau >= price).OrderByDescending(m => m.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                    }
                }
                else
                {
                    if (gia.Trim() == "" || gia == null)
                    {
                        if (startdate == null || startdate.Trim() == "")
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;
                            int temp = int.Parse(loai.ToString());
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;


                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiMua == null && CT_Loai.Any(l1 => l1.MaDauGia == p.MaDauGia))
                                                .OrderByDescending(m => m.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;
                            int temp = int.Parse(loai.ToString());
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;
                            DateTime ngay = DateTime.Parse(startdate);

                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiMua == null && CT_Loai.Any(l1 => l1.MaDauGia == p.MaDauGia) && p.NgayBatDau >= ngay).OrderByDescending(m => m.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                    }
                    else
                    {
                        if (startdate == null || startdate.Trim() == "")
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;

                            int temp = int.Parse(loai.ToString());
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;

                            int price = int.Parse(gia);

                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiMua == null && CT_Loai.Any(l1 => l1.MaDauGia == p.MaDauGia) && p.GiaBanDau >= price).OrderByDescending(m => m.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;

                            int temp = int.Parse(loai.ToString());
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;
                            DateTime ngay = DateTime.Parse(startdate);
                            int price = int.Parse(gia);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiMua == null && CT_Loai.Any(l1 => l1.MaDauGia == p.MaDauGia) && p.NgayBatDau >= ngay && p.GiaBanDau >= price).OrderByDescending(m => m.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                    }
                }
            }
            else
            {
                if (loai == "0" || loai == null)
                {
                    if (gia == "" || gia == null)
                    {
                        if (startdate == null || startdate.Trim() == "")
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;

                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiMua == null && p.TenSanPham == ten).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;
                            DateTime ngay = DateTime.Parse(startdate);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiMua == null && p.NgayBatDau >= ngay && p.TenSanPham == ten).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                    }
                    else
                    {
                        if (startdate == null || startdate.Trim() == "")
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;
                            int price = int.Parse(gia);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiMua == null && p.GiaBanDau >= price && p.TenSanPham == ten).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;
                            DateTime ngay = DateTime.Parse(startdate);
                            int price = int.Parse(gia);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiMua == null && p.NgayBatDau >= ngay && p.GiaBanDau >= price && p.TenSanPham == ten).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                    }
                }
                else
                {
                    if (gia.Trim() == "" || gia == null)
                    {
                        if (startdate == null || startdate.Trim() == "")
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;
                            int temp = int.Parse(loai.ToString());
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;

                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiMua == null && CT_Loai.Any(l1 => l1.MaDauGia == p.MaDauGia) && p.TenSanPham == ten)
                                                .OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;
                            int temp = int.Parse(loai.ToString());
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;
                            DateTime ngay = DateTime.Parse(startdate);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiMua == null && CT_Loai.Any(l1 => l1.MaDauGia == p.MaDauGia) && p.NgayBatDau >= ngay && p.TenSanPham == ten).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                    }
                    else
                    {
                        if (startdate == null || startdate.Trim() == "")
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;

                            int temp = int.Parse(loai.ToString());
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;

                            int price = int.Parse(gia);

                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiMua == null && CT_Loai.Any(l1 => l1.MaDauGia == p.MaDauGia) && p.GiaBanDau >= price && p.TenSanPham == ten).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;

                            int temp = int.Parse(loai.ToString());
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;
                            DateTime ngay = DateTime.Parse(startdate);
                            int price = int.Parse(gia);

                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiMua == null && CT_Loai.Any(l1 => l1.MaDauGia == p.MaDauGia) && p.NgayBatDau >= ngay && p.GiaBanDau >= price && p.TenSanPham == ten).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                    }
                }

            }
        }
        public ActionResult ListFavorite(int? page)
        {
            IQueryable<DauGia> DauGia;
            ViewModel view = new ViewModel();
            int ID = int.Parse(Session["MaNguoiDung"].ToString());
            var yeuthich = db.YeuThiches.Where(m => m.MaNguoiDung == ID).ToList();
            var daugia = from y in db.YeuThiches
                         join dg in db.DauGias on y.MaDauGia equals dg.MaDauGia
                         where y.MaNguoiDung == ID
                         select dg;
            var tt = db.TrangThaiDauGias.Where(m => m.TenTrangThai == "UnActive").SingleOrDefault();
            var DauGia_DaXoa = from y in db.YeuThiches
                               join dg in db.DauGias on y.MaDauGia equals dg.MaDauGia
                               join ct in db.CT_TrangThai on y.MaDauGia equals ct.MaDauGia
                               where y.MaNguoiDung == ID && ct.MaTrangThai == tt.MaTrangThai
                               select dg;

            DauGia = daugia.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia)).OrderBy(x => x.NgayDang);
            int pageNumber = page ?? 1;
            int pageSize = 5;
            List<DauGiaViewModel> DGViewModel;
            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);

            return View("ListFavorite", DGViewModel.ToPagedList(pageNumber, pageSize));
        }
        //[HttpPost, ActionName("Favorite")]
        public ActionResult Favorite(int id, string loai)
        {
            if (Session["MaNguoiDung"] == null)
            {
                TempData["toastr-warning"] = "Vui lòng đăng nhập để thêm sản phẩm yêu thích!";
                return RedirectToAction("SignIn", "SignIn", new { area = "" });
            }
            else
            {
                int ID = int.Parse(Session["MaNguoiDung"].ToString());
                YeuThich yt = new YeuThich();
                yt.MaNguoiDung = ID;
                yt.MaDauGia = id;
                yt.NgayThem = DateTime.Now;
                db.YeuThiches.Add(yt);
                db.SaveChanges();
                TempData["toastr-success"] = "Thêm vào yêu thích thành công";
                if (loai == "0")
                {
                    return RedirectToAction("Bid", new { id = id });

                }
                else if (loai == "1")
                {
                    return RedirectToAction("Home");

                }
                else
                {
                    return RedirectToAction("ListFavorite");

                }
            }
        }

        //[HttpPost, ActionName("UnFavorite")]
        public ActionResult UnFavorite(int id, string loai)
        {
            if (Session["MaNguoiDung"] == null)
            {
                TempData["toastr-warning"] = "Vui lòng đăng nhập để bỏ sản phẩm yêu thích!";
                return RedirectToAction("SignIn", "SignIn", new { area = "" });
            }
            else
            {
                int ID = int.Parse(Session["MaNguoiDung"].ToString());
                YeuThich yt = new YeuThich();
                yt = db.YeuThiches.Where(m => m.MaDauGia == id && m.MaNguoiDung == ID).SingleOrDefault();
                db.YeuThiches.Remove(yt);
                db.SaveChanges();
                TempData["toastr-success"] = "Bỏ yêu thích thành công";
                if (loai == "0")
                {
                    return RedirectToAction("Bid", new { id = id });

                }
                else if (loai == "1")
                {
                    return RedirectToAction("Home");

                }
                else
                {
                    return RedirectToAction("ListFavorite");
                }
            }
        }
        // Trang đấu giá - Chi tiết sản phẩm
        public ActionResult Bid(int id)
        {
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
            NotificationComponents components = new NotificationComponents(id);
            components.RegisterLiveAuction(id);
            TempData["MaDauGia"] = id;
            return View(view);
        }
        [HttpPost]
        public ActionResult Bid_1(string bid_1, string id)
        {
            if (Session["MaNguoiDung"] == null)
            {
                TempData["toastr-warning"] = "Vui lòng đăng nhập để đấu giá sản phẩm!";
                return RedirectToAction("SignIn", "SignIn", new { area = "" });
            }
            else
            {

                if (bid_1 == null || bid_1.Trim() == "" || id == null || id.Trim() == "")
                {
                    return RedirectToAction("Bid", new { id = int.Parse(id) });
                }

                int mucnang = int.Parse(bid_1);
                int ma = int.Parse(id);
                int ID = int.Parse(Session["MaNguoiDung"].ToString());
                var l = db.MucNangs.Where(m => m.MaDauGia == ma).ToList();
                var dg = db.DauGias.Where(m => m.MaDauGia == ma).SingleOrDefault();
                var trangthai = db.CT_TrangThai.Where(m => m.MaTrangThai == 2 && m.MaDauGia == ma).SingleOrDefault();
                if (DateTime.Now <= dg.NgayKetThuc)
                {
                    if (trangthai == null)
                    {
                        if (l.Count() > 0)
                        {
                            var max = db.MucNangs.Where(m => m.MaDauGia == ma).Max(m => m.GiaTri);
                            if (max < mucnang)
                            {
                                MucNang mn = new MucNang();
                                mn.MaDauGia = ma;
                                mn.MaNguoiDung = ID;
                                mn.GiaTri = mucnang;
                                mn.ThoiGian = DateTime.Now;
                                db.MucNangs.Add(mn);
                                db.SaveChanges();
                                this.AddNotification("Nâng giá thành công", NotificationType.SUCCESS);
                            }
                            else
                            {
                                this.AddNotification("Vui lòng nâng giá cao hơn", NotificationType.ERROR);

                            }
                        }
                        else
                        {
                            MucNang mn = new MucNang();
                            mn.MaDauGia = ma;
                            mn.MaNguoiDung = ID;
                            mn.GiaTri = mucnang;
                            mn.ThoiGian = DateTime.Now;
                            db.MucNangs.Add(mn);
                            db.SaveChanges();
                            this.AddNotification("Nâng giá thành công", NotificationType.SUCCESS);

                        }
                        return RedirectToAction("Bid", new { id = ma });
                    }
                    else
                    {
                        this.AddNotification("Buổi đấu giá đã bị vô hiệu hóa", NotificationType.ERROR);
                        return RedirectToAction("Bid", new { id = ma });
                    }

                }
                else
                {
                    this.AddNotification("Đã quá thời gian có thể đấu giá", NotificationType.ERROR);
                    return RedirectToAction("Bid", new { id = ma });
                }
            }
        }

        [HttpGet]
        public JsonResult DSNguoiDungDauGiaLive()
        {
            TempData.Keep();
            NotificationComponents components = new NotificationComponents();
            int maDauGia = Convert.ToInt32(TempData["MaDauGia"]);
            var list = components.GetNguoiDungDauGia(maDauGia);
            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult Rating(int id)
        {
            ViewModel view = new ViewModel();
            var dg = db.DauGias.Where(m => m.MaDauGia == id).SingleOrDefault();
            var loai = from l in db.Loais
                       join ct in db.CT_LoaiDauGia on l.MaLoai equals ct.MaLoai
                       join b in db.DauGias on ct.MaDauGia equals b.MaDauGia
                       where b.MaDauGia == id
                       select l;
            view.ListLoaiSanPham = loai.ToList().ConvertAll<LoaiViewModel>(x => x);
            var hinh = db.HinhAnhs.Where(m => m.MaDauGia == id).Select(m => m.LinkAnh).ToArray();
            view.Hinh = hinh;
            view.DauGia = dg;
            return View(view);
        }
        [HttpPost]
        public ActionResult Rating(string id, string temp, string cmt)
        {
            if (id == null || id.Trim() == "" || temp == null || temp.Trim() == "" || cmt.Trim() == null || cmt == null)
            {
                return RedirectToAction("Rating", new { id = int.Parse(id) });
            }

            int ma = int.Parse(id);
            int rate = int.Parse(temp);
            var dga = db.DauGias.Where(m => m.MaDauGia == ma).SingleOrDefault();
            DanhGia dg = new DanhGia();
            dg.FromID = int.Parse(dga.MaNguoiMua.ToString());
            dg.ToID = int.Parse(dga.MaNguoiBan.ToString());
            dg.Rating = rate;
            dg.NoiDung = cmt;
            db.DanhGias.Add(dg);
            db.SaveChanges();
            return RedirectToAction("ListBuy", "Home");
        }

        public ActionResult ListBuy(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;

            IQueryable<DauGia> DauGia;
            List<DauGiaViewModel> DGViewModel;
            var DauGia_full = db.DauGias.OrderBy(x => x.NgayDang);

            int ID = int.Parse(Session["MaNguoiDung"].ToString());
            DauGia = DauGia_full.Where(p => p.MaNguoiMua == ID).OrderByDescending(m => m.NgayKetThuc);

            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);

            return View("ListBuy", DGViewModel.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ChiTietDaMua(int id)
        {

            ViewModel view = new ViewModel();
            var dg = db.DauGias.Where(m => m.MaDauGia == id).SingleOrDefault();
            var loai = from l in db.Loais
                       join ct in db.CT_LoaiDauGia on l.MaLoai equals ct.MaLoai
                       join b in db.DauGias on ct.MaDauGia equals b.MaDauGia
                       where b.MaDauGia == id
                       select l;
            view.ListLoaiSanPham = loai.ToList().ConvertAll<LoaiViewModel>(x => x);
            view.DauGia = dg;
            var hi = db.HinhAnhs.Where(m => m.MaDauGia == id).Select(m => m.LinkAnh).ToArray();
            /*            view.Hinh = new string[99];
            */
            view.Hinh = hi;
            return View(view);
        }

        public ActionResult ThanhToan(int id)
        {
            int ID = int.Parse(Session["MaNguoiDung"].ToString());
            ViewModel view = new ViewModel();
            var dg = db.DauGias.Where(m => m.MaDauGia == id).SingleOrDefault();
            var loai = from l in db.Loais
                       join ct in db.CT_LoaiDauGia on l.MaLoai equals ct.MaLoai
                       join b in db.DauGias on ct.MaDauGia equals b.MaDauGia
                       where b.MaDauGia == id
                       select l;
            view.ListLoaiSanPham = loai.ToList().ConvertAll<LoaiViewModel>(x => x);
            view.DauGia = dg;
            var hi = db.HinhAnhs.Where(m => m.MaDauGia == id).Select(m => m.LinkAnh).ToArray();
            view.Hinh = hi;
            var nguoi = db.NguoiDungs.Where(m => m.MaNguoiDung == ID).SingleOrDefault();
            view.NguoiDung = nguoi;
            return View(view);
        }
        [HttpPost]
        public ActionResult ThanhToan(string submit)
        {

            int id = int.Parse(submit);
            var dg = db.DauGias.Where(m => m.MaDauGia == id).SingleOrDefault();
            dg.NgayThanhToan = DateTime.Now;
            int ID = int.Parse(Session["MaNguoiDung"].ToString());
            var nd = db.NguoiDungs.Where(m => m.MaNguoiDung == ID).SingleOrDefault();
            if (nd.SoDuVi >= int.Parse(dg.GiaCuoi.ToString()))
            {
                nd.SoDuVi -= int.Parse(dg.GiaCuoi.ToString());
                var nguoiban = db.NguoiDungs.Where(n => n.MaNguoiDung == dg.MaNguoiBan).SingleOrDefault();
                nguoiban.SoDuVi += int.Parse(dg.GiaCuoi.ToString()) * 80 / 100;
                db.SaveChanges();
                return RedirectToAction("ListBuy", "Home");

            }
            else
            {
                this.AddNotification("Số tiền trong ví không đủ", NotificationType.WARNING);
                return RedirectToAction("ThanhToan", new { id = id });
            }



        }
        [HttpPost]
        public ActionResult XacNhan(string submit)
        {
            int id = int.Parse(submit);
            var tt = db.TrangThaiDauGias.Where(m => m.TenTrangThai == "Done").SingleOrDefault();
            CT_TrangThai ct = new CT_TrangThai();
            ct.MaDauGia = id;
            ct.MaTrangThai = tt.MaTrangThai;
            ct.ThoiGian = DateTime.Now;
            db.CT_TrangThai.Add(ct);
            db.SaveChanges();

            return RedirectToAction("Rating", new { id = id });
        }
        public ActionResult NapTien()
        {
            return View();
        }

        public ActionResult Payment(int value)
        {
            //request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOE9NN20211208";
            string accessKey = "RmpFc3sFvJPdoQ3s";
            string serectkey = "kUnpWmcCuXSZC1tiEYW7zpDCgygsiBY6";
            string orderInfo = "test";
            string returnUrl = "https://daugiatructuyen.azurewebsites.net/User/Home/ConfirmPaymentClient";
            string notifyurl = "https://daugiatructuyen.azurewebsites.net/User/Home/SavePayment"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test
            string amount = value.ToString();
            string orderid = DateTime.Now.Ticks.ToString();
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = Session["MaNguoiDung"].ToString();

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            return Redirect(jmessage.GetValue("payUrl").ToString());
        }

        //Khi thanh toán xong ở cổng thanh toán Momo, Momo sẽ trả về một số thông tin, trong đó có errorCode để check thông tin thanh toán
        //errorCode = 0 : thanh toán thành công (Request.QueryString["errorCode"])
        //Tham khảo bảng mã lỗi tại: https://developers.momo.vn/#/docs/aio/?id=b%e1%ba%a3ng-m%c3%a3-l%e1%bb%97i
        public ActionResult ConfirmPaymentClient()
        {
            //hiển thị thông báo cho người dùng
            return View();
        }

        [HttpPost]
        public void SavePayment()
        {
            int id = int.Parse(Request["extraData"].ToString());
            var nd = db.NguoiDungs.Where(m => m.MaNguoiDung == id).SingleOrDefault();
            nd.SoDuVi += int.Parse(Request["amount"].ToString());
            db.SaveChanges();
            //cập nhật dữ liệu vào db
        }


        public ActionResult RutTien()
        {
            //hiển thị thông báo cho người dùng
            return View();
        }
        [HttpPost]
        public ActionResult RutTien(string value)
        {
            int price = int.Parse(value);
            int ID = int.Parse(Session["MaNguoiDung"].ToString());
            var nd = db.NguoiDungs.Where(m => m.MaNguoiDung == ID).SingleOrDefault();
            if (nd.SoDuVi > price)
            {
                nd.SoDuVi -= price;
                this.AddNotification("Rút tiền thành công", NotificationType.SUCCESS);
                db.SaveChanges();
            }
            else
            {
                this.AddNotification("Rút tiền thất bại", NotificationType.WARNING);
            }
            //hiển thị thông báo cho người dùng
            return View();
        }
    }

}