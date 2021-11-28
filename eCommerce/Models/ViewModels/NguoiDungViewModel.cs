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
        [Required(ErrorMessage = "Họ tên không được trống...")]
        public string HovaTen { get; set; }
        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được trống...")]
        public string Password { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Email không được để trống...")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ. Ví dụ: example@gmail.com")]
        public string Email { get; set; }
        [DisplayName("Ngày sinh")]
        [Required(ErrorMessage = "Ngày sinh không được trống...")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime NgaySinh { get; set; }
        [DisplayName("Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại không được trống...")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại bắt đầu là 0, không bao gồm chữ, gồm 10 số. ")]
        public string SDT { get; set; }
        [DisplayName("Hình ảnh")]
        [Required(ErrorMessage = "Hình ảnh không được trống...")]
        public string HinhAnh { get; set; }
        [DisplayName("Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ không được trống...")]
        public string DiaChi { get; set; }
        [DisplayName("Ngày đăng kí")]
        public DateTime NgayDangKy { get; set; }
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
                SoDuVi = nguoiDung.SoDuVi,
                NgaySinh = nguoiDung.NgaySinh,
                HinhAnh = nguoiDung.HinhAnh
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
                SoDuVi = nguoiDungViewModel.SoDuVi,
                NgaySinh = nguoiDungViewModel.NgaySinh,
                HinhAnh = nguoiDungViewModel.HinhAnh
            };
        }

        public virtual ICollection<DanhGia> DanhGias { get; set; }
        public virtual ICollection<DanhGia> DanhGias1 { get; set; }
        public virtual ICollection<MucNang> MucNangs { get; set; }
        public virtual ICollection<ThongBao> ThongBaos { get; set; }
    }
}