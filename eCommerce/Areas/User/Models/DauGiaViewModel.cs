using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using eCommerce.Models;
using eCommerce.Models;
namespace eCommerce.Areas.User.Models
{
    public class DauGiaViewModel : IValidatableObject
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
        public int MaDauGia { get; set; }
        [DisplayName("Mã người bán")]
        public int MaNguoiBan { get; set; }
        [DisplayName("Mã người mua")]
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
        public Nullable<int> GiaCuoi { get; set; }
        [DisplayName("Ngày bắt đầu")]
        [Required(ErrorMessage = "Ngày bắt đầu không được trống...")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy HH:mm}")]
        public DateTime NgayBatDau { get; set; }
        [DisplayName("Ngày kết thúc")]
        [Required(ErrorMessage = "Ngày kết thúc không được trống...")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy HH:mm}")]
        public DateTime NgayKetThuc { get; set;}
        [DisplayName("Vị Trí")]
        [Required(ErrorMessage = "Vị Trí không được để trống...")]
        public string ViTri { get; set; }
        [DisplayName("Ngày thanh toán")]
        public Nullable<System.DateTime> NgayThanhToan { get; set; }
        [DisplayName("Ngày đăng")]
        public System.DateTime NgayDang { get; set; }
        /*        public bool Delete { get; set; }
        */
        /*        public string[] Hinh { get; set; }
        */  
        [DisplayName("Loại")]
        [Required(ErrorMessage = "Vị Trí không được để trống...")]
        public List<string> ListLoaiSanPham { get; set; }

        public List<HttpPostedFileBase> ImageFile { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateTime.Now.AddDays(2) > NgayBatDau)
            {
                yield return new ValidationResult(errorMessage: "Ngày bát đầu phải lớn hơn ngày đăng 2 ngày", memberNames: new[] { "NgayBatDau" });
            }
            if (NgayKetThuc < NgayBatDau)
            {
                yield return new ValidationResult(errorMessage: "Ngày kết thúc phải lớn hơn ngày bắt đầu", memberNames: new[] { "NgayKetThuc" });
            }
            if(ListLoaiSanPham.Count==0)
            {
                yield return new ValidationResult(errorMessage: "Vui lòng chọn loại sản phẩm", memberNames: new[] { "ListLoaiSanPham" });
            }
            DauGiaEntities db = new DauGiaEntities();
            var anh = db.HinhAnhs.Where(m => m.MaDauGia == MaDauGia).ToList();
            if (anh.Count() == 0)
            {
                foreach (var hinh in ImageFile)
                {

                    if (hinh == null)
                    {
                        yield return new ValidationResult(errorMessage: "Vui lòng chọn ảnh", memberNames: new[] { "ImageFile" });
                        break;
                    }
                }
            }
               
           
        }
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