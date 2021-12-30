using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCommerce.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Areas.User.Models
{
    public class LoaiViewModel
    {
        public LoaiViewModel()
        {
            this.CT_LoaiDauGia = new HashSet<CT_LoaiDauGia>();
        }

        public int MaLoai { get; set; }
        public string TenLoai { get; set; }
        public bool TrangThai { get; set; }
        public int MaNguoiDung { get; set; }
        public System.DateTime Ngay { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [DisplayName("Tên Loại")]
        [Required(ErrorMessage = "Quận/Huyện không được trống...")]

        public int [] ListLoai { get; set; }    

        public static implicit operator LoaiViewModel(Loai Loai)
        {
            return new LoaiViewModel
            {
                MaLoai = Loai.MaLoai,
                TenLoai = Loai.TenLoai,
                TrangThai = Loai.TrangThai,
                MaNguoiDung=Loai.MaNguoiDung,
                Ngay=Loai.Ngay
                
            };
        }

        public static implicit operator Loai(LoaiViewModel Loai)
        {
            return new Loai
            {
                MaLoai = Loai.MaLoai,
                TenLoai = Loai.TenLoai,
                TrangThai = Loai.TrangThai,
                MaNguoiDung = Loai.MaNguoiDung,
                Ngay = Loai.Ngay

            };
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_LoaiDauGia> CT_LoaiDauGia { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }
    }
}