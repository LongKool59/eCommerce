using eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.Areas.User.Models
{
    public class NangGiaViewModel
    {
        public int MaMucNang { get; set; }
        public int MaNguoiDung { get; set; }
        public string TenNguoiDung { get; set; }
        public int MaDauGia { get; set; }
        public DateTime ThoiGian { get; set; }
        public int GiaTri { get; set; }

        public static implicit operator NangGiaViewModel(MucNang mucNang)
        {
            return new NangGiaViewModel
            {
                MaMucNang = mucNang.MaMucNang,
                MaNguoiDung = mucNang.MaNguoiDung,
                TenNguoiDung = mucNang.NguoiDung.HoTen,
                MaDauGia = mucNang.MaDauGia,
                ThoiGian = mucNang.ThoiGian,
                GiaTri = mucNang.GiaTri,
                DauGia = mucNang.DauGia,
                NguoiDung = mucNang.NguoiDung
            };
        }

        public virtual DauGiaViewModel DauGia { get; set; }
        public virtual NguoiDungViewModel NguoiDung { get; set; }
    }
}