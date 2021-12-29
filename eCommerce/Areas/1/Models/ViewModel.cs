using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCommerce.Models;
namespace eCommerce.Areas.User.Models
{
    public class ViewModel
    {
        public DauGiaViewModel DauGia { get; set; }
        public UserViewModel NguoiDung { get; set; }
        public LoaiViewModel Loai { get; set; }
        public string[] Hinh { get; set; }
       public bool YeuThich { get; set; }
        public IEnumerable<DauGiaViewModel> ListSanPham { get; set; }
        public IEnumerable<LoaiViewModel> ListLoaiSanPham { get; set; }
    }
}