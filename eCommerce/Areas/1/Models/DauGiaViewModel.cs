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

        public int MaDauGia { get; set; }
        public int MaNguoiBan { get; set; }
        public Nullable<int> MaNguoiMua { get; set; }
        public string TenSanPham { get; set; }
        public string MoTa { get; set; }
        public int GiaBanDau { get; set; }
        public int MucNangToiThieu { get; set; }
        public Nullable<int> GiaCuoi { get; set; }
        public System.DateTime NgayBatDau { get; set; }
        public System.DateTime NgayKetThuc { get; set; }
        public string ViTri { get; set; }
        public Nullable<System.DateTime> NgayThanhToan { get; set; }
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