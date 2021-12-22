using eCommerce.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.Models
{
    public class ThongBaoNguoiDung
    {
        public int MaNguoiDung { get; set; }
        public string HoTen { get; set; }
        public bool IsRequesting { get; set; }
        public DateTime? TimeRequesting { get; set; }

        public static implicit operator ThongBaoNguoiDung(NguoiDung nguoiDung)
        {
            return new ThongBaoNguoiDung
            {
                MaNguoiDung = nguoiDung.MaNguoiDung,
                HoTen = nguoiDung.HoTen,
                IsRequesting = nguoiDung.IsRequesting,
                TimeRequesting = nguoiDung.TimeRequesting,
            };
        }

        public static implicit operator NguoiDung(ThongBaoNguoiDung thongBaoNguoiDung)
        {
            return new NguoiDung
            {
                MaNguoiDung = thongBaoNguoiDung.MaNguoiDung,
                HoTen = thongBaoNguoiDung.HoTen,
                IsRequesting = thongBaoNguoiDung.IsRequesting,
                TimeRequesting = thongBaoNguoiDung.TimeRequesting,
            };
        }
    }
}