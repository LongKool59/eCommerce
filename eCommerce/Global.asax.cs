using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FluentScheduler;
using eCommerce.Models;
namespace eCommerce
{
    public class MvcApplication : System.Web.HttpApplication
    {
        string connectionStr = ConfigurationManager.ConnectionStrings["sqlConString"].ConnectionString;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //start sql dependency
            SqlDependency.Start(connectionStr);
            NotificationComponents NC = new NotificationComponents();

            NC.RegisterNotification(true);
            UpdateTrangThai();

        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }
        protected void Application_End()
        {
            //stop sql dependecy
            SqlDependency.Stop(connectionStr);
        }
        DauGiaEntities db = new DauGiaEntities();
        public void UpdateTrangThai()
        {
            DateTime now = DateTime.Now;
            var hethan = db.DauGias.Where(m => m.NgayKetThuc <= now).ToList();
            var DauGia_DaXoa = from d in db.DauGias
                               join ct in db.CT_TrangThai on d.MaDauGia equals ct.MaDauGia
                               where ct.MaTrangThai == 2
                               select d;


            var DauGia = hethan.Where(p => !DauGia_DaXoa.Any(p2 => p2.MaDauGia == p.MaDauGia) && p.MaNguoiMua == null).ToList(); 
            foreach (var dg in DauGia)
            {
                var ten = db.TrangThaiDauGias.Where(m => m.TenTrangThai == "UnActive").SingleOrDefault();
                CT_TrangThai tt = new CT_TrangThai();
                tt.MaDauGia = dg.MaDauGia;
                tt.MaTrangThai = ten.MaTrangThai;
                tt.ThoiGian = dg.NgayKetThuc;
                db.CT_TrangThai.Add(tt);
                db.SaveChanges();

                var mucnang = db.MucNangs.Where(m=>m.MaDauGia == dg.MaDauGia).ToList();
                if(mucnang.Count()>0)
                {
                    var max = db.MucNangs.Where(m => m.MaDauGia == dg.MaDauGia).Max(m => m.GiaTri);
                    var mn = db.MucNangs.Where(m => m.GiaTri == max && m.MaDauGia == dg.MaDauGia).SingleOrDefault();
                    dg.MaNguoiMua = mn.MaNguoiDung;
                    dg.GiaCuoi = mn.GiaTri;
                    db.SaveChanges();    
                }


            }    
        }
        public class MyRegistry : Registry
        {
            public MyRegistry()
            {
                DauGiaEntities ql = new DauGiaEntities();
                MvcApplication mvcApplication = new MvcApplication();

                Action taoDSNhanVienNghiVaTinhLuongThang = new Action(() =>
                {
                    mvcApplication.UpdateTrangThai();
                });
                this.Schedule(taoDSNhanVienNghiVaTinhLuongThang).ToRunEvery(1).Days().At(0, 0);
            }
        }
    }
}
