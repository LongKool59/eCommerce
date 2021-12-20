using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.Models.ViewModels
{
    public class LoaiDauGiaViewModel
    {
        public LoaiDauGiaViewModel()
        {
            this.CT_LoaiDauGia = new HashSet<CT_LoaiDauGia>();
        }
        [DisplayName("Mã loại")]
        public int MaLoai { get; set; }
        [DisplayName("Tên loại")]
        [Required(ErrorMessage = "Tên loại không được trống...")]
        public string TenLoai { get; set; }
        [DisplayName("Trạng thái")]
        public bool TrangThai { get; set; }
        [DisplayName("Người sửa")]
        public int MaNguoiDung { get; set; }
        [DisplayName("Ngày sửa")]
        public DateTime Ngay { get; set; }

        public bool IsChecked { get; set; }

        public static implicit operator LoaiDauGiaViewModel(Loai loaiDauGia)
        {
            return new LoaiDauGiaViewModel
            {
                MaLoai = loaiDauGia.MaLoai,
                TenLoai = loaiDauGia.TenLoai,
                TrangThai = loaiDauGia.TrangThai,
                MaNguoiDung = loaiDauGia.MaNguoiDung,
                Ngay = loaiDauGia.Ngay,
                NguoiDung = loaiDauGia.NguoiDung,
            };
        }
        public static implicit operator Loai(LoaiDauGiaViewModel loaiDauGiaViewModel)
        {
            return new Loai
            {
                MaLoai = loaiDauGiaViewModel.MaLoai,
                TenLoai = loaiDauGiaViewModel.TenLoai,
                TrangThai = loaiDauGiaViewModel.TrangThai,
                MaNguoiDung = loaiDauGiaViewModel.MaNguoiDung,
                Ngay = loaiDauGiaViewModel.Ngay,
                NguoiDung = loaiDauGiaViewModel.NguoiDung,
            };
        }

        public virtual ICollection<CT_LoaiDauGia> CT_LoaiDauGia { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }
        public virtual NguoiDungViewModel NguoiDungViewModel { get; set; }
    }
}