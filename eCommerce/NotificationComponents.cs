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
        public List<ThongBaoNguoiDung> GetNguoiDungs(bool status)
        {
            DauGiaEntities db = new DauGiaEntities();
            IQueryable<NguoiDung> nguoi = db.NguoiDungs.Where(a => a.IsRequesting == status).OrderByDescending(a => a.TimeRequesting);/*.Select(s => new { s.MaNguoiDung, s.HoTen, s.IsRequesting, s.TimeRequesting })*/
            List<ThongBaoNguoiDung> listNguoiDung = new List<ThongBaoNguoiDung>();
            listNguoiDung = nguoi.ToList().ConvertAll<ThongBaoNguoiDung>(s => s);
            return listNguoiDung;
        }
    }
}