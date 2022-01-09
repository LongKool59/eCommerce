using eCommerce.Areas.User.Models;
using eCommerce.Models;
using eCommerce.Models.ViewModels;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace eCommerce
{
    public class NotificationComponents
    {
        private int maDG { get; set; }

        public NotificationComponents() { }

        public NotificationComponents(int maDG)
        {
            this.maDG = maDG;
        }

        public void RegisterNotification(bool status)
        {
            string conStr = ConfigurationManager.ConnectionStrings["sqlConString"].ConnectionString;
            string sqlCommand = @"SELECT [MaNguoiDung], [HoTen], [IsRequesting], [TimeRequesting] from [dbo].[NguoiDung] where [IsRequesting] = @Status";
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, con);
                cmd.Parameters.AddWithValue("@Status", status);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.Notification = null;
                SqlDependency sqlDep = new SqlDependency(cmd);
                sqlDep.OnChange += sqlDep_OnChange;
                //we have to execute the commnad here
                using (SqlDataReader reader = cmd.ExecuteReader()) { }
            }
        }
        public void RegisterLiveAuction(int maDauGia)
        {
            string conStr = ConfigurationManager.ConnectionStrings["sqlConString"].ConnectionString;
            string sqlCommand = @"SELECT [MaMucNang], [MaNguoiDung], [MaDauGia], [ThoiGian], [GiaTri] from [dbo].[MucNang] where [MaDauGia] = @MaDauGia";
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, con);
                cmd.Parameters.AddWithValue("@MaDauGia", maDauGia);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.Notification = null;
                SqlDependency sqlDep = new SqlDependency(cmd);
                sqlDep.OnChange += sqlDep_OnChangeLiveAuction;
                //we have to execute the commnad here
                using (SqlDataReader reader = cmd.ExecuteReader()) { }
            }
        }

        void sqlDep_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                SqlDependency sqlDep = sender as SqlDependency;
                sqlDep.OnChange -= sqlDep_OnChange;

                //from here we will send notification message to client
                var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                notificationHub.Clients.All.notify("added");

                //re-register notification
                RegisterNotification(true);
            }
        }
        void sqlDep_OnChangeLiveAuction(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                SqlDependency sqlDep = sender as SqlDependency;
                sqlDep.OnChange -= sqlDep_OnChangeLiveAuction;

                //from here we will send live auction to client
                var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                notificationHub.Clients.All.notify("addlive");

                //re-register live auction
                RegisterLiveAuction(maDG);
            }
        }
        public List<ThongBaoNguoiDung> GetNguoiDungs(bool status)
        {
            DauGiaEntities db = new DauGiaEntities();
            IQueryable<NguoiDung> nguoi = db.NguoiDungs.Where(a => a.IsRequesting == status).OrderByDescending(a => a.TimeRequesting);/*.Select(s => new { s.MaNguoiDung, s.HoTen, s.IsRequesting, s.TimeRequesting })*/
            List<ThongBaoNguoiDung> listNguoiDung = new List<ThongBaoNguoiDung>();
            listNguoiDung = nguoi.ToList().ConvertAll<ThongBaoNguoiDung>(s => s);
            return listNguoiDung;
        }

        public List<NangGiaViewModel> GetNguoiDungDauGia(int maDauGia)
        {
            DauGiaEntities db = new DauGiaEntities();
            var mucNangs = (from ma in db.MucNangs
                            join nguoiDung in db.NguoiDungs on ma.MaNguoiDung equals nguoiDung.MaNguoiDung
                            join dauGia in db.DauGias on ma.MaDauGia equals dauGia.MaDauGia
                            where dauGia.MaDauGia == maDauGia
                            orderby ma.GiaTri descending
                            select new
                            {
                                MaMucNang = ma.MaMucNang,
                                MaNguoiDung = nguoiDung.MaNguoiDung,
                                TenNguoiDung = nguoiDung.HoTen,
                                MaDauGia = dauGia.MaDauGia,
                                ThoiGian = ma.ThoiGian,
                                GiaTri = ma.GiaTri,
                            }).ToList();
            List<NangGiaViewModel> NangGiaList = new List<NangGiaViewModel>();
            foreach (var item in mucNangs)
            {
                NangGiaViewModel model = new NangGiaViewModel();
                model.MaMucNang = item.MaMucNang;
                model.MaDauGia = item.MaDauGia;
                model.MaNguoiDung = item.MaNguoiDung;
                model.TenNguoiDung = item.TenNguoiDung;
                model.ThoiGian = item.ThoiGian;
                model.GiaTri = item.GiaTri;
                NangGiaList.Add(model);
            }
            return NangGiaList;
        }
    }
}