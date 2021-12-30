﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCommerce.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Areas.User.Models
{
    public class NguoiDungViewModel
    {
        [DisplayName("Mã người dùng")]
        public int MaNguoiDung { get; set; }

        [DisplayName("Họ tên")]
        [Required(ErrorMessage = "Họ tên không được trống...")]
        public string HoTen { get; set; }
        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được trống...")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*\d).{5,25}$", ErrorMessage = "Mật khẩu bao gồm chữ và số, từ 5 đến 25 kí tự")]
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
        //[Required(ErrorMessage = "Hình ảnh không được trống...")]
        public string HinhAnh { get; set; }
        [DisplayName("Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ không được trống...")]
        public string DiaChi { get; set; }
        [DisplayName("Chứng minh nhân dân")]
        [Required(ErrorMessage = "Số chứng minh nhân dân không được trống...")]
        public string SoCMND { get; set; }
        [DisplayName("Ngày đăng kí")]
        public DateTime NgayDangKy { get; set; }
        [DisplayName("Quyền admin")]
        public bool IsAdmin { get; set; }
        [DisplayName("Đã được cấp quyền đăng đấu giá")]
        public bool IsApproved { get; set; }
        [DisplayName("Số dư ví")]
        public int SoDuVi { get; set; }
        public bool IsRequesting { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        [DisplayName("Quận/Huyện")]
        [Required(ErrorMessage = "Quận/Huyện không được trống...")]
        public string MaQuan { get; set; }
        [DisplayName("Phường/xã")]
        [Required(ErrorMessage = "Phường/Xã không được trống...")]
        public string MaPhuong { get; set; }
        [DisplayName("Tỉnh/Thành phố")]
        [Required(ErrorMessage = "Tỉnh/Thành phố không được trống...")]
        public string MaTP { get; set; }
        public bool IsChecked { get; set; }
        [DisplayName("Trạng thái")]
        public bool TrangThai { get; set; }

        public DateTime? TimeRequesting { get; set; }

        public static implicit operator NguoiDungViewModel(NguoiDung nguoiDung)
        {
            return new NguoiDungViewModel
            {
                MaNguoiDung = nguoiDung.MaNguoiDung,
                HoTen = nguoiDung.HoTen,
                Password = nguoiDung.Password,
                Email = nguoiDung.Email,
                SDT = nguoiDung.SDT,
                DiaChi = nguoiDung.DiaChi,
                NgayDangKy = nguoiDung.NgayDangKy,
                IsAdmin = nguoiDung.IsAdmin,
                IsApproved = nguoiDung.IsApproved,
                SoDuVi = nguoiDung.SoDuVi,
                NgaySinh = nguoiDung.NgaySinh,
                HinhAnh = nguoiDung.HinhAnh,
                SoCMND = nguoiDung.SoCMND,
                IsRequesting = nguoiDung.IsRequesting,
                MaPhuong = nguoiDung.MaPhuong,
                MaQuan = nguoiDung.MaQuan,
                MaTP = nguoiDung.MaTP,
                Phuong = nguoiDung.Phuong,
                Quan = nguoiDung.Quan,
                ThanhPho = nguoiDung.ThanhPho,
                TrangThai = nguoiDung.TrangThai,
                TimeRequesting = nguoiDung.TimeRequesting,
            };
        }

        public static implicit operator NguoiDung(NguoiDungViewModel nguoiDungViewModel)
        {
            return new NguoiDung
            {
                MaNguoiDung = nguoiDungViewModel.MaNguoiDung,
                HoTen = nguoiDungViewModel.HoTen,
                Password = nguoiDungViewModel.Password,
                Email = nguoiDungViewModel.Email,
                SDT = nguoiDungViewModel.SDT,
                DiaChi = nguoiDungViewModel.DiaChi,
                NgayDangKy = nguoiDungViewModel.NgayDangKy,
                IsAdmin = nguoiDungViewModel.IsAdmin,
                IsApproved = nguoiDungViewModel.IsApproved,
                SoDuVi = nguoiDungViewModel.SoDuVi,
                NgaySinh = nguoiDungViewModel.NgaySinh,
                HinhAnh = nguoiDungViewModel.HinhAnh,
                SoCMND = nguoiDungViewModel.SoCMND,
                IsRequesting = nguoiDungViewModel.IsRequesting,
                MaPhuong = nguoiDungViewModel.MaPhuong,
                MaQuan = nguoiDungViewModel.MaQuan,
                MaTP = nguoiDungViewModel.MaTP,
                Phuong = nguoiDungViewModel.Phuong,
                Quan = nguoiDungViewModel.Quan,
                ThanhPho = nguoiDungViewModel.ThanhPho,
                TrangThai = nguoiDungViewModel.TrangThai,
                TimeRequesting = nguoiDungViewModel.TimeRequesting,
            };
        }
        public virtual ICollection<BienDongSoDu> BienDongSoDus { get; set; }
        public virtual ICollection<DanhGia> DanhGias { get; set; }
        public virtual ICollection<DanhGia> DanhGias1 { get; set; }
        public virtual ICollection<MucNang> MucNangs { get; set; }
        public virtual ICollection<ThongBao> ThongBaos { get; set; }
        public virtual ICollection<DauGia> DauGias { get; set; }
        public virtual ICollection<DauGia> DauGias1 { get; set; }
        public virtual ICollection<Loai> Loais { get; set; }
        public virtual ICollection<TrangThaiDauGia> TrangThaiDauGias { get; set; }
        public virtual Phuong Phuong { get; set; }
        public virtual Quan Quan { get; set; }
        public virtual ThanhPho ThanhPho { get; set; }

        public virtual PhuongViewModel PhuongViewModel { get; set; }
        public virtual QuanViewModel QuanViewModel { get; set; }
        public virtual ThanhPhoViewModel ThanhPhoViewModel { get; set; }
    }
}