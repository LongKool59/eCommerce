using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using eCommerce.Models;

namespace eCommerce.Areas.User.Models
{
    public class DauGiaViewModel
    {
        public DauGiaViewModel()
        {
            this.CT_LoaiDauGia = new HashSet<CT_LoaiDauGia>();
            this.CT_TrangThai = new HashSet<CT_TrangThai>();
            this.HinhAnh = new HashSet<HinhAnh>();
            this.MucNang = new HashSet<MucNang>();
            this.ThongBao = new HashSet<ThongBao>();
        }

        [DisplayName("Mã đấu giá")]
        [Required(ErrorMessage = "Mã đấu giá không được trống...")]
        public int MaDauGia { get; set; }
        [DisplayName("Mã người bán")]
        [Required(ErrorMessage = "Mã người bán không được trống...")]
        public int MaNguoiBan { get; set; }
        [DisplayName("Mã người mua")]
        [Required(ErrorMessage = "Mã người mua không được trống...")]
        public Nullable<int> MaNguoiMua { get; set; }
        [DisplayName("Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được trống...")]
        public string TenSanPham { get; set; }
        [DisplayName("Mô tả")]
        [Required(ErrorMessage = "Mô tả không được trống...")]
        public string MoTa { get; set; }
        [DisplayName("Giá bán đầu")]
        [Required(ErrorMessage = "Giá bán đầu không được trống...")]
        [Range(0, int.MaxValue, ErrorMessage = "Phải nhập số và không được là số âm")]
        public int GiaBanDau { get; set; }
        [DisplayName("Mức nâng tối thiểu")]
        [Required(ErrorMessage = "Mức nâng tối thiểu không được trống...")]
        [Range(0, int.MaxValue, ErrorMessage = "Phải nhập số và không được là số âm")]
        public int MucNangToiThieu { get; set; }
        [DisplayName("Giá cuối")]
        [Required(ErrorMessage = "Giá cuối không được trống...")]
        [Range(0, int.MaxValue, ErrorMessage = "Phải nhập số và không được là số âm")]
        public Nullable<int> GiaCuoi { get; set; }
        [DisplayName("Ngày bắt đầu")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ngày bắt đầu không được trống...")]
        public System.DateTime NgayBatDau { get; set; }
        [DisplayName("Ngày kết thúc")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ngày kết thúc không được trống...")]
        public System.DateTime NgayKetThuc { get; set; }
        [Required(ErrorMessage = "Ngày kết thúc không được trống...")]
        public string ViTri { get; set; }
        [DisplayName("Ngày thanh toán")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ngày thanh toán không được trống...")]
        public Nullable<System.DateTime> NgayThanhToan { get; set; }
        [DisplayName("Ngày đăng")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ngày đăng không được trống...")]
        public System.DateTime NgayDang { get; set; }
        /*        public bool Delete { get; set; }
        */
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
                TenSanPham = dauGia.TenSanPham

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
                TenSanPham = dauGia.TenSanPham


            };
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_LoaiDauGia> CT_LoaiDauGia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_TrangThai> CT_TrangThai { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }
        public virtual NguoiDung NguoiDung1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HinhAnh> HinhAnh { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MucNang> MucNang { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongBao> ThongBao { get; set; }
    }


}