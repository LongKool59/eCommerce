using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCommerce.Models;

namespace eCommerce.Areas.User.Models
{
    public class YeuThichViewModel
    {
        public int MaYeuThich { get; set; }
        public int MaDauGia { get; set; }
        public int MaNguoiDung { get; set; }
        public System.DateTime NgayThem { get; set; }

        public virtual DauGia DauGia { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }
        public static implicit operator YeuThichViewModel(YeuThich yt)
        {
            return new YeuThichViewModel
            {
                MaYeuThich=yt.MaYeuThich,
                MaDauGia=yt.MaDauGia,
                MaNguoiDung=yt.MaNguoiDung,
                NgayThem=yt.NgayThem
            };
        }
        public static implicit operator YeuThich(YeuThichViewModel yt)
        {
            return new YeuThich
            {
                MaYeuThich = yt.MaYeuThich,
                MaDauGia = yt.MaDauGia,
                MaNguoiDung = yt.MaNguoiDung,
                NgayThem = yt.NgayThem
            };
        }
    }
}