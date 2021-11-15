using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.Models.ViewModels
{
    public class NguoiDungViewModel
    {
        [DisplayName("Mã người dùng")]
        public int MaNguoiDung { get; set; }

        [DisplayName("Họ tên")]
        public string HovaTen { get; set; }
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Email không được để trống...")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ. Ví dụ: example@gmail.com")]
        public string Email { get; set; }
        [DisplayName("Số điện thoại")]
        public string SDT { get; set; }
        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }
        [DisplayName("Ngày đăng kí")]
        public System.DateTime NgayDangKy { get; set; }
        [DisplayName("Is admin")]
        public bool IsAdmin { get; set; }
        public bool IsApproved { get; set; }
        [DisplayName("Số dư ví")]
        public int SoDuVi { get; set; }

        public static implicit operator NguoiDungViewModel(NguoiDung nguoiDung)
        {
            return new NguoiDungViewModel
            {
                MaNguoiDung = nguoiDung.MaNguoiDung,
                HovaTen = nguoiDung.HovaTen,
                Password = nguoiDung.Password,
                Email = nguoiDung.Email,
                SDT = nguoiDung.SDT,
                DiaChi = nguoiDung.DiaChi,
                NgayDangKy = nguoiDung.NgayDangKy,
                IsAdmin = nguoiDung.IsAdmin,
                IsApproved = nguoiDung.IsApproved,
                SoDuVi = nguoiDung.SoDuVi
            };
        }

        public static implicit operator NguoiDung(NguoiDungViewModel nguoiDungViewModel)
        {
            return new NguoiDung
            {
                MaNguoiDung = nguoiDungViewModel.MaNguoiDung,
                HovaTen = nguoiDungViewModel.HovaTen,
                Password = nguoiDungViewModel.Password,
                Email = nguoiDungViewModel.Email,
                SDT = nguoiDungViewModel.SDT,
                DiaChi = nguoiDungViewModel.DiaChi,
                NgayDangKy = nguoiDungViewModel.NgayDangKy,
                IsAdmin = nguoiDungViewModel.IsAdmin,
                IsApproved = nguoiDungViewModel.IsApproved,
                SoDuVi = nguoiDungViewModel.SoDuVi
            };
        }

        public virtual ICollection<ChatRieng> ChatRiengs { get; set; }
        public virtual ICollection<ChatRieng> ChatRiengs1 { get; set; }
        public virtual ICollection<DanhGia> DanhGias { get; set; }
        public virtual ICollection<DanhGia> DanhGias1 { get; set; }
        public virtual ICollection<MucNang> MucNangs { get; set; }
        public virtual ICollection<ThongBao> ThongBaos { get; set; }
    }
}