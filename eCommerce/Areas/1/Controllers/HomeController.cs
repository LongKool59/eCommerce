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
            int pageSize = 5;
            IQueryable<DauGia> DauGia;
            List<DauGiaViewModel> DGViewModel;
            var DauGia_full = db.DauGias.OrderBy(x => x.NgayDang);
            /*            DauGia= from d in db.DauGias join ct in db.CT_TrangThais on d.MaDauGia equals ct.MaDauGia where 
            */
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
                                               join ct in db.CT_TrangThais on d.MaDauGia equals ct.MaDauGia
                                               join lo in db.CT_LoaiDauGias on d.MaDauGia equals lo.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;

                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia)).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThais on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;
                            DateTime ngay = DateTime.Parse(startdate);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia)  && p.NgayBatDau >= ngay).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                    }
                    else
                    {
                        if (startdate == null || startdate.Trim() == "")
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThais on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;
                            int price = int.Parse(gia);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.GiaBanDau >= price).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThais on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;
                            DateTime ngay = DateTime.Parse(startdate);
                            int price = int.Parse(gia);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.NgayBatDau >= ngay && p.GiaBanDau >= price).OrderBy(x => x.NgayDang);
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
                                               join ct in db.CT_TrangThais on d.MaDauGia equals ct.MaDauGia
                                               join lo in db.CT_LoaiDauGias on d.MaDauGia equals lo.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;
                            int temp = int.Parse(loai.ToString());
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGias on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;

                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && CT_Loai.Any(l1 => l1.MaDauGia == p.MaDauGia) )
                                                .OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThais on d.MaDauGia equals ct.MaDauGia
                                               join lo in db.CT_LoaiDauGias on d.MaDauGia equals lo.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;
                            int temp = int.Parse(loai.ToString());
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGias on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;
                            DateTime ngay = DateTime.Parse(startdate);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && CT_Loai.Any(l1 => l1.MaDauGia == p.MaDauGia)  && p.NgayBatDau >= ngay).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                    }
                    else
                    {
                        if (startdate == null || startdate.Trim() == "")
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThais on d.MaDauGia equals ct.MaDauGia
                                               join lo in db.CT_LoaiDauGias on d.MaDauGia equals lo.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;

                            int temp = int.Parse(loai.ToString());
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGias on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;

                            int price = int.Parse(gia);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && CT_Loai.Any(l1 => l1.MaDauGia == p.MaDauGia)  && p.GiaBanDau >= price).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThais on d.MaDauGia equals ct.MaDauGia
                                               join lo in db.CT_LoaiDauGias on d.MaDauGia equals lo.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;

                            int temp = int.Parse(loai.ToString());
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGias on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;
                            DateTime ngay = DateTime.Parse(startdate);
                            int price = int.Parse(gia);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && CT_Loai.Any(l1 => l1.MaDauGia == p.MaDauGia) && p.NgayBatDau >= ngay && p.GiaBanDau >= price).OrderBy(x => x.NgayDang);
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
                                               join ct in db.CT_TrangThais on d.MaDauGia equals ct.MaDauGia
                                               join lo in db.CT_LoaiDauGias on d.MaDauGia equals lo.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;

                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia)  &&p.TenSanPham == ten).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThais on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;
                            DateTime ngay = DateTime.Parse(startdate);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.NgayBatDau >= ngay && p.TenSanPham == ten).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                    }
                    else
                    {
                        if (startdate == null || startdate.Trim() == "")
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThais on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;
                            int price = int.Parse(gia);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.GiaBanDau >= price && p.TenSanPham == ten).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThais on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;
                            DateTime ngay = DateTime.Parse(startdate);
                            int price = int.Parse(gia);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.NgayBatDau >= ngay && p.GiaBanDau >= price && p.TenSanPham == ten).OrderBy(x => x.NgayDang);
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
                                               join ct in db.CT_TrangThais on d.MaDauGia equals ct.MaDauGia
                                               join lo in db.CT_LoaiDauGias on d.MaDauGia equals lo.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;
                            int temp = int.Parse(loai.ToString());
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGias on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;

                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && CT_Loai.Any(l1 => l1.MaDauGia == p.MaDauGia)  && p.TenSanPham == ten)
                                                .OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThais on d.MaDauGia equals ct.MaDauGia
                                               join lo in db.CT_LoaiDauGias on d.MaDauGia equals lo.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;
                            int temp = int.Parse(loai.ToString()); 
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGias on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;
                            DateTime ngay = DateTime.Parse(startdate);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && CT_Loai.Any(l1 => l1.MaDauGia == p.MaDauGia) && p.NgayBatDau >= ngay && p.TenSanPham == ten).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                    }
                    else
                    {
                        if (startdate == null || startdate.Trim() == "")
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThais on d.MaDauGia equals ct.MaDauGia
                                               join lo in db.CT_LoaiDauGias on d.MaDauGia equals lo.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;

                            int temp = int.Parse(loai.ToString());
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGias on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;

                            int price = int.Parse(gia);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && CT_Loai.Any(l1 => l1.MaDauGia == p.MaDauGia)  && p.GiaBanDau >= price && p.TenSanPham == ten).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThais on d.MaDauGia equals ct.MaDauGia
                                               join lo in db.CT_LoaiDauGias on d.MaDauGia equals lo.MaDauGia
                                               where ct.MaTrangThai == tt.MaTrangThai
                                               select d;

                            int temp = int.Parse(loai.ToString());
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGias on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;
                            DateTime ngay = DateTime.Parse(startdate);
                            int price = int.Parse(gia);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && CT_Loai.Any(l1 => l1.MaDauGia == p.MaDauGia)  && p.NgayBatDau >= ngay && p.GiaBanDau >= price && p.TenSanPham == ten).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Home", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                    }
                }

            }
        }
        public ActionResult ListFavorite()
        {
            int ID = int.Parse(Session["MaNguoiDung"].ToString());
            var yeuthich = db.YeuThichs.Where(m => m.MaNguoiDung == ID).SingleOrDefault();
            YeuThichViewModel yt = new YeuThichViewModel();
            return View(yt);
        }
        [HttpPost, ActionName("Favorite")]
        public ActionResult Favorite(int id, string loai)
        {
            if(Session["MaNguoiDung"]==null)
            {
                return Redirect("/");
            }   
            else
            {
                int ID = int.Parse(Session["MaNguoiDung"].ToString());
                YeuThich yt = new YeuThich();
                yt.MaNguoiDung = ID;
                yt.MaDauGia = id;
                yt.NgayThem = DateTime.Now;
                db.YeuThichs.Add(yt);
                db.SaveChanges();
                if(loai=="0")
                {
                    return RedirectToAction("Bid", new { id = id });

                }
                else 
                {
                    return RedirectToAction("Home");

                }
                
            }    
            
        }

        [HttpPost, ActionName("UnFavorite")]
        public ActionResult UnFavorite(int id,string loai)
        {
            if (Session["MaNguoiDung"] == null)
            {
                return Redirect("/");
            }
            else
            {
                int ID = int.Parse(Session["MaNguoiDung"].ToString());
                YeuThich yt = new YeuThich();
                yt = db.YeuThichs.Where(m => m.MaDauGia == id&&m.MaNguoiDung==ID).SingleOrDefault();
                db.YeuThichs.Remove(yt);
                db.SaveChanges();
                if (loai == "0")
                {
                    return RedirectToAction("Bid", new { id = id });

                }
                else if(loai=="1")
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
                       join ct in db.CT_LoaiDauGias on l.MaLoai equals ct.MaLoai
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
                var yt = db.YeuThichs.Where(m => m.MaDauGia == id && m.MaNguoiDung == ID).SingleOrDefault();
                if(yt!=null)
                {
                    view.YeuThich = true;
                }    
            }
            TempData["time"] = view.DauGia.NgayKetThuc.ToString("MM/dd/yyyy HH:mm");

            return View(view);
        }
        [HttpPost]
        public ActionResult Bid_1(string bid, string id)
        {
            if (Session["MaNguoiDung"] == null)
            {
                return Redirect("/");
            }
            else
            {
                int mucnang = int.Parse(bid);
                int ma = int.Parse(id);
                int ID = int.Parse(Session["MaNguoiDung"].ToString());
                var l = db.MucNangs.Where(m => m.MaDauGia == ma).ToList();
                if (l.Count()>0)
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
                else {
                    MucNang mn = new MucNang();
                    mn.MaDauGia = ma;
                    mn.MaNguoiDung = ID;
                    mn.GiaTri = mucnang;
                    mn.ThoiGian = DateTime.Now;
                    db.MucNangs.Add(mn);
                    db.SaveChanges();
                    this.AddNotification("Nâng giá thành công", NotificationType.SUCCESS);

                }
                return RedirectToAction("Bid", new { id=ma});
            }

        }

        public ActionResult Rating(int id)
        {

            ViewModel view = new ViewModel();
            var dg = db.DauGias.Where(m => m.MaDauGia == id).SingleOrDefault();
            var loai = from l in db.Loais
                       join ct in db.CT_LoaiDauGias on l.MaLoai equals ct.MaLoai
                       join b in db.DauGias on ct.MaDauGia equals b.MaDauGia
                       where b.MaDauGia == id
                       select l;
            view.ListLoaiSanPham = loai.ToList().ConvertAll<LoaiViewModel>(x => x);
            view.DauGia = dg;
            return View(view);
        }
        [HttpPost]
        public ActionResult Rating(ViewModel v, int rating,string noidung)
        {
            DanhGia dg = new DanhGia();
            dg.FromID = int.Parse(v.DauGia.MaNguoiMua.ToString());
            dg.ToID = int.Parse(v.DauGia.MaNguoiMua.ToString());
            dg.Rating = rating;
            dg.NoiDung = noidung;
            return View();
        }

        public ActionResult ListBuy(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;
            IQueryable<DauGia> DauGia;
            List<DauGiaViewModel> DGViewModel;
            var DauGia_full = db.DauGias.OrderBy(x => x.NgayDang);

            int ID = int.Parse(Session["MaNguoiDung"].ToString());
            DauGia = DauGia_full.Where(p => p.MaNguoiMua == ID).OrderBy(x => x.NgayKetThuc);

            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);

            return View(DGViewModel);
        }
        public ActionResult ChiTietDaMua(int id)
        {

            ViewModel view = new ViewModel();
            var dg = db.DauGias.Where(m => m.MaDauGia == id).SingleOrDefault();
            var loai = from l in db.Loais
                       join ct in db.CT_LoaiDauGias on l.MaLoai equals ct.MaLoai
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
                       join ct in db.CT_LoaiDauGias on l.MaLoai equals ct.MaLoai
                       join b in db.DauGias on ct.MaDauGia equals b.MaDauGia
                       where b.MaDauGia == id
                       select l;
            view.ListLoaiSanPham = loai.ToList().ConvertAll<LoaiViewModel>(x => x);
            view.DauGia = dg;
            var hi = db.HinhAnhs.Where(m => m.MaDauGia == id).Select(m => m.LinkAnh).ToArray();
            view.Hinh = hi;
            return View(view);
        }
        [HttpPost]
        public ActionResult ThanhToan(ViewModel v)
        {
            var dg = db.DauGias.Where(m => m.MaDauGia == v.DauGia.MaDauGia).SingleOrDefault();
            dg.NgayThanhToan = DateTime.Now;
            var nd = db.NguoiDungs.Where(m => m.MaNguoiDung == int.Parse(Session["MaNguoiDung"].ToString())).SingleOrDefault();
            nd.SoDuVi -= int.Parse(dg.GiaCuoi.ToString());
            db.SaveChanges();
            return View();
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
            string returnUrl = "https://localhost:44366//User/Home/ConfirmPaymentClient";
            string notifyurl = "http://f07f-113-161-44-71.ngrok.io/User/Home/SavePayment"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test

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



/*
        [AcceptVerbs("post")]
        public ActionResult Rate(FormCollection form)
        {
            var rate = Convert.ToInt32(form["Score"]);
            var id = Convert.ToInt32(form["ArticleID"]);
            if (Request.Cookies["rating" + id] != null)
                return Content("false");
            Response.Cookies["rating" + id].Value = DateTime.Now.ToString();
            Response.Cookies["rating" + id].Expires = DateTime.Now.AddYears(1);
            ArticleRating ar = repository.IncrementArticleRating(rate, id);
            return Json(ar);
        }*/
    }

}