using eCommerce.Models;
using eCommerce.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.Areas.Admin.Models
{
    public class DauGiaViewModel
    {
        public DauGiaViewModel()
        {
            CT_LoaiDauGia = new HashSet<CT_LoaiDauGia>();
            CT_TrangThai = new HashSet<CT_TrangThai>();
            HinhAnhs = new HashSet<HinhAnh>();
            MucNangs = new HashSet<MucNang>();
            ThongBaos = new HashSet<ThongBao>();
            YeuThiches = new HashSet<YeuThich>();
        }
        [DisplayName("Mã đấu giá")]
        public int MaDauGia { get; set; }
        [DisplayName("Mã người bán")]
        public int MaNguoiBan { get; set; }
        [DisplayName("Mã người mua")]
        public int? MaNguoiMua { get; set; }
        [DisplayName("Tên sản phẩm")]
        public string TenSanPham { get; set; }
        [DisplayName("Mô tả")]
        public string MoTa { get; set; }
        [DisplayName("Giá ban đầu")]
        public int GiaBanDau { get; set; }
        [DisplayName("Mức nâng tối thiểu")]
        public int MucNangToiThieu { get; set; }
        [DisplayName("Giá cuối cùng")]
        public int? GiaCuoi { get; set; }
        [DisplayName("Ngày bắt đầu")]
        public DateTime NgayBatDau { get; set; }
        [DisplayName("Ngày kết thúc")]
        public DateTime NgayKetThuc { get; set; }
        [DisplayName("Vị trí")]
        public string ViTri { get; set; }
        [DisplayName("Ngày thanh toán")]
        public DateTime? NgayThanhToan { get; set; }
        [DisplayName("Ngày đăng")]
        public DateTime NgayDang { get; set; }
        public bool IsChecked { get; set; }

        public static implicit operator DauGiaViewModel(DauGia dauGia)
        {
            return new DauGiaViewModel
            {
                MaDauGia = dauGia.MaDauGia,
                MaNguoiBan = dauGia.MaNguoiBan,
                MaNguoiMua = dauGia.MaNguoiMua,
                MoTa = dauGia.MoTa,
                GiaBanDau = dauGia.GiaBanDau,
                MucNangToiThieu = dauGia.MucNangToiThieu,
                GiaCuoi = dauGia.GiaCuoi,
                NgayBatDau = dauGia.NgayBatDau,
                NgayKetThuc = dauGia.NgayKetThuc,
                ViTri = dauGia.ViTri,
                NgayThanhToan = dauGia.NgayThanhToan,
                NgayDang = dauGia.NgayDang,
                TenSanPham = dauGia.TenSanPham,
                NguoiDung = dauGia.NguoiDung,
            };
        }
        public static implicit operator DauGia(DauGiaViewModel dauGia)
        {
            return new DauGia
            {
                MaDauGia = dauGia.MaDauGia,
                MaNguoiBan = dauGia.MaNguoiBan,
                MaNguoiMua = dauGia.MaNguoiMua,
                MoTa = dauGia.MoTa,
                GiaBanDau = dauGia.GiaBanDau,
                MucNangToiThieu = dauGia.MucNangToiThieu,
                GiaCuoi = dauGia.GiaCuoi,
                NgayBatDau = dauGia.NgayBatDau,
                NgayKetThuc = dauGia.NgayKetThuc,
                ViTri = dauGia.ViTri,
                NgayThanhToan = dauGia.NgayThanhToan,
                NgayDang = dauGia.NgayDang,
                TenSanPham = dauGia.TenSanPham,
                NguoiDung = dauGia.NguoiDung
            };
        }
        public virtual ICollection<CT_LoaiDauGia> CT_LoaiDauGia { get; set; }
        public virtual ICollection<CT_TrangThai> CT_TrangThai { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }
        public virtual NguoiDung NguoiDung1 { get; set; }
        public virtual ICollection<HinhAnh> HinhAnhs { get; set; }
        public virtual ICollection<MucNang> MucNangs { get; set; }
        public virtual ICollection<ThongBao> ThongBaos { get; set; }
        public virtual ICollection<YeuThich> YeuThiches { get; set; }
        public virtual NguoiDungViewModel NguoiDungViewModel { get; set; }
        public virtual NguoiDungViewModel NguoiDungViewModel1 { get; set; }
    }
}