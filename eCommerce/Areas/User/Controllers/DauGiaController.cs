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
using System.IO;
using FluentScheduler;


namespace eCommerce.Areas.User.Controllers
{
    public class DauGiaController : Controller
    {
        DauGiaEntities db = new DauGiaEntities();
        // GET: User/DauGia
        public ActionResult Index(int? page, string ten, string loai, string gia, string startdate)
        {
            List<Loai> listL = db.Loais.ToList();
            Loai l = new Loai();
            l.MaLoai = 0;
            l.TenLoai = "Tất cả";
            l.Ngay = DateTime.Now;
            l.MaNguoiDung = 1;
            listL.Add(l);
            ViewBag.ListLoai = new SelectList(listL, "MaLoai", "TenLoai","0");
            int ID = int.Parse(Session["MaNguoiDung"].ToString());
            int pageNumber = page ?? 1;
            int pageSize = 5;
            IQueryable<DauGia> DauGia;
            List<DauGiaViewModel> DGViewModel;
            var DauGia_full = db.DauGias.OrderBy(x => x.NgayDang);
            /*            DauGia= from d in db.DauGias join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia where 
            */
            if(ten==null||ten.Trim()=="")
            {
                if (loai == "0" || loai==null)
                {
                    if (gia == "" || gia==null)
                    {
                        if (startdate == null||startdate.Trim()=="")
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                               where ct.MaTrangThai == 3 
                                               select d;

                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiBan == ID ).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Index", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == 3  
                                               select d;
                            DateTime ngay = DateTime.Parse(startdate);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiBan == ID&& p.NgayBatDau>=ngay).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Index", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                    }
                    else
                    {
                        if (startdate == null || startdate.Trim() == "")
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == 3  
                                               select d;
                            int price = int.Parse(gia);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiBan == ID&& p.GiaBanDau>=price).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Index", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai ==3 
                                               select d;
                            DateTime ngay = DateTime.Parse(startdate);
                            int price = int.Parse(gia);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiBan == ID && p.NgayBatDau >= ngay && p.GiaBanDau >= price).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Index", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                    }
                }
                else
                {
                    if (gia.Trim() == ""|| gia==null)
                    {
                        if (startdate == null||startdate.Trim()=="")
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                               where ct.MaTrangThai == 3 
                                               select d;
                            int temp = int.Parse(loai.ToString());
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;

                            DauGia = DauGia_full .Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia)&&CT_Loai.Any(l1=>l1.MaDauGia==p.MaDauGia) && p.MaNguoiBan == ID)
                                                .OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Index", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                               where ct.MaTrangThai == 3  
                                               select d;
                            int temp = int.Parse(loai.ToString());
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;
                            DateTime ngay = DateTime.Parse(startdate);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && CT_Loai.Any(l1 => l1.MaDauGia == p.MaDauGia) && p.MaNguoiBan == ID&&p.NgayBatDau>=ngay).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Index", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                    }
                    else
                    {
                        if (startdate == null || startdate.Trim() == "")
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                               where ct.MaTrangThai == 3 
                                               select d;

                            int temp = int.Parse(loai.ToString());
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;

                            int price = int.Parse(gia);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && CT_Loai.Any(l1 => l1.MaDauGia == p.MaDauGia) && p.MaNguoiBan == ID && p.GiaBanDau>=price).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Index", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                               where ct.MaTrangThai == 3 
                                               select d;

                            int temp = int.Parse(loai.ToString());
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;
                            DateTime ngay = DateTime.Parse(startdate);
                            int price = int.Parse(gia);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && CT_Loai.Any(l1 => l1.MaDauGia == p.MaDauGia) && p.MaNguoiBan == ID && p.NgayBatDau >= ngay && p.GiaBanDau >= price).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Index", DGViewModel.ToPagedList(pageNumber, pageSize));
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
                                               join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                               where ct.MaTrangThai == 3
                                               select d;

                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiBan == ID && p.TenSanPham == ten).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Index", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == 3
                                               select d;
                            DateTime ngay = DateTime.Parse(startdate);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiBan == ID && p.NgayBatDau >= ngay && p.TenSanPham == ten).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Index", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                    }
                    else
                    {
                        if (startdate == null || startdate.Trim() == "")
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == 3
                                               select d;
                            int price = int.Parse(gia);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiBan == ID && p.GiaBanDau >= price && p.TenSanPham == ten).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Index", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               where ct.MaTrangThai == 3
                                               select d;
                            DateTime ngay = DateTime.Parse(startdate);
                            int price = int.Parse(gia);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiBan == ID && p.NgayBatDau >= ngay && p.GiaBanDau >= price && p.TenSanPham == ten).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Index", DGViewModel.ToPagedList(pageNumber, pageSize));
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
                                               join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                               where ct.MaTrangThai == 3
                                               select d;
                            int temp = int.Parse(loai.ToString());
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;

                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && CT_Loai.Any(l1 => l1.MaDauGia == p.MaDauGia) && p.MaNguoiBan == ID && p.TenSanPham == ten)
                                                .OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Index", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                               where ct.MaTrangThai == 3
                                               select d;
                            int temp = int.Parse(loai.ToString());
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;
                            DateTime ngay = DateTime.Parse(startdate);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && CT_Loai.Any(l1 => l1.MaDauGia == p.MaDauGia) && p.MaNguoiBan == ID && p.NgayBatDau >= ngay && p.TenSanPham == ten).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Index", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                    }
                    else
                    {
                        if (startdate == null || startdate.Trim() == "")
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                               where ct.MaTrangThai == 3
                                               select d;

                            int temp = int.Parse(loai.ToString());
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;

                            int price = int.Parse(gia);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && CT_Loai.Any(l1 => l1.MaDauGia == p.MaDauGia) && p.MaNguoiBan == ID && p.GiaBanDau >= price && p.TenSanPham == ten).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Index", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            var DauGia_DaXoa = from d in db.DauGias
                                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                                               join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                               where ct.MaTrangThai == 3
                                               select d;

                            int temp = int.Parse(loai.ToString());
                            var CT_Loai = from d in db.DauGias
                                          join lo in db.CT_LoaiDauGia on d.MaDauGia equals lo.MaDauGia
                                          where lo.MaLoai == temp
                                          select d;
                            DateTime ngay = DateTime.Parse(startdate);
                            int price = int.Parse(gia);
                            DauGia = DauGia_full.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && CT_Loai.Any(l1 => l1.MaDauGia == p.MaDauGia) && p.MaNguoiBan == ID && p.NgayBatDau >= ngay && p.GiaBanDau >= price && p.TenSanPham == ten).OrderBy(x => x.NgayDang);
                            DGViewModel = DauGia.ToList().ConvertAll<DauGiaViewModel>(x => x);


                            return View("Index", DGViewModel.ToPagedList(pageNumber, pageSize));
                        }
                    }
                }

            }
            
        }
        public ActionResult Detail(int id)
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
        public ActionResult Add()
        {
            DauGiaViewModel dg = new DauGiaViewModel();

            List<Loai> listL = db.Loais.ToList();
            ViewBag.ListLoai = new MultiSelectList(listL, "MaLoai", "TenLoai");
            return View(dg);
        }
        [HttpPost]
        public ActionResult Add(DauGiaViewModel v)
        {
            List<Loai> listL = db.Loais.ToList();

            if (!ModelState.IsValid)
            {
                ViewBag.ListLoai = new MultiSelectList(listL, "MaLoai", "TenLoai");
                return View(v);
            }
            ViewBag.ListLoai = new MultiSelectList(listL, "MaLoai", "TenLoai");
            DauGia daugia = new DauGia();
            daugia.TenSanPham = v.TenSanPham;
            daugia.NgayDang = DateTime.Now;
            daugia.NgayBatDau = v.NgayBatDau;
            daugia.NgayKetThuc = v.NgayKetThuc;
            daugia.MucNangToiThieu = v.MucNangToiThieu;
            daugia.MoTa = v.MoTa;
            daugia.GiaBanDau = v.GiaBanDau;
            daugia.ViTri = v.ViTri;

            daugia.MaNguoiBan = int.Parse(Session["MaNguoiDung"].ToString());
            db.DauGias.Add(daugia);
            db.SaveChanges();
            int ID = db.DauGias.OrderByDescending(x => x.MaDauGia)
                             .Take(1)
                             .Select(x => x.MaDauGia)
                             .ToList()
                             .FirstOrDefault();
            foreach (var loai in v.ListLoaiSanPham)
            {
                CT_LoaiDauGia ct = new CT_LoaiDauGia();
                ct.MaLoai = int.Parse(loai);

                ct.MaDauGia = ID;
                db.CT_LoaiDauGia.Add(ct);
                db.SaveChanges();
            }
            CT_TrangThai ct_tt = new CT_TrangThai();
            ct_tt.MaDauGia = ID;
            var tt = db.TrangThaiDauGias.Where(m => m.TenTrangThai == "Active").SingleOrDefault();
            ct_tt.MaTrangThai = tt.MaTrangThai;
            ct_tt.ThoiGian = DateTime.Now;
            db.CT_TrangThai.Add(ct_tt);
            db.SaveChanges();
            foreach (var hinh in v.ImageFile)
            {
                var hi = db.HinhAnhs.Where(m => m.MaDauGia == ID).Count() + 1;
                string extension = Path.GetExtension(hinh.FileName);
                string fileName = hi + "_" + ID + extension;
                
                HinhAnh h = new HinhAnh();
                h.LinkAnh = "~/BidImage/"+ fileName;
                h.MaDauGia = ID;
                db.HinhAnhs.Add(h);
                db.SaveChanges();
                fileName = Path.Combine(Server.MapPath("~/BidImage/"), fileName);
                hinh.SaveAs(fileName);
            }
            var registry = new Registry();
/*            registry.Schedule<>
*/            return Redirect("Index");
        }
        public void DoiTrangThai()
        {


        }
        public ActionResult Edit(int id)
        {
        
            DauGiaViewModel view = new DauGiaViewModel();

            List<Loai> listL = db.Loais.ToList();
            var listLoai = from l in db.Loais
                           join ct in db.CT_LoaiDauGia on l.MaLoai equals ct.MaLoai
                           join b in db.DauGias on ct.MaDauGia equals b.MaDauGia
                           where b.MaDauGia == id
                           select l.MaLoai;
            ViewBag.ListLoai = new MultiSelectList(listL, "MaLoai", "TenLoai", listLoai);
            DauGia dg = db.DauGias.Find(id);
            view = dg;
            return View(view);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DauGiaViewModel v)
        {
            if (!ModelState.IsValid)
            {
                List<Loai> listL = db.Loais.ToList();
                var listLoai = from le in db.Loais
                               join ct in db.CT_LoaiDauGia on le.MaLoai equals ct.MaLoai
                               join b in db.DauGias on ct.MaDauGia equals b.MaDauGia
                               where b.MaDauGia == v.MaDauGia
                               select le.MaLoai;
                ViewBag.ListLoai = new MultiSelectList(listL, "MaLoai", "TenLoai", listLoai);
                return View(v);
            }

            DauGia daugia = db.DauGias.Where(m => m.MaDauGia == v.MaDauGia).SingleOrDefault();
            daugia.TenSanPham = v.TenSanPham;
            daugia.NgayBatDau = v.NgayBatDau;
            daugia.NgayKetThuc = v.NgayKetThuc;
            daugia.MucNangToiThieu = v.MucNangToiThieu;
            daugia.MoTa = v.MoTa;
            daugia.GiaBanDau = v.GiaBanDau;
            daugia.ViTri = v.ViTri;
            db.Entry(daugia).State = EntityState.Modified;
            db.SaveChanges();
            int ID = v.MaDauGia;

            var l = db.CT_LoaiDauGia.Where(m => m.MaDauGia == ID).ToList();
            

            foreach (var rm in l)
            {
                db.CT_LoaiDauGia.Remove(rm);
                db.SaveChanges();
            }
            
            foreach (var loai in v.ListLoaiSanPham)
            {
                CT_LoaiDauGia ct = new CT_LoaiDauGia();
                ct.MaLoai = int.Parse(loai);

                ct.MaDauGia = ID;
                db.CT_LoaiDauGia.Add(ct);
                db.SaveChanges();
            }
            if (v.ImageFile.First() != null)
            {/*
                *//*//*//*Xóa hình ảnh cũ trong folder*/
                var hinh_1 = db.HinhAnhs.Where(m => m.MaDauGia == ID).ToList();
                foreach(var item in hinh_1 )
                {
                    string oldImgPath = item.LinkAnh;
                    FileInfo fi = new FileInfo(oldImgPath);
                    if (fi != null)
                        System.IO.File.Delete(Server.MapPath(oldImgPath));
                }


                //thêm hình ảnh vào thư mục UserImages và lưu đường dẫn vào database
                foreach (var hinh in v.ImageFile)
                {
                    if (hinh != null)
                    {
                        var hi = db.HinhAnhs.Where(m => m.MaDauGia == ID).Count() + 1;
                        string extension = Path.GetExtension(hinh.FileName);
                        string fileName = hi + "_" + ID + extension;
                        HinhAnh h = new HinhAnh();
                        h.LinkAnh = "~/BidImage" + fileName;
                        h.MaDauGia = ID;
                        db.HinhAnhs.Add(h);
                        db.SaveChanges();
                        fileName = Path.Combine(Server.MapPath("~/BidImage/"), fileName);
                        hinh.SaveAs(fileName);

                    }
                    
                }
            }

            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int submit)
        {
            CT_TrangThai ct = new CT_TrangThai();
            ct.MaDauGia = submit;
            var tt = db.TrangThaiDauGias.Where(m => m.TenTrangThai == "UnActive").SingleOrDefault();
            ct.MaTrangThai = tt.MaTrangThai;
            ct.ThoiGian = DateTime.Now;
            db.CT_TrangThai.Add(ct);
            db.SaveChanges();
            return Redirect("Index");
        }
        // Trang chủ
       
    }
}