using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace eCommerce.Models.ViewModels
{
    public class PhuongViewModel
    {
        public PhuongViewModel()
        {
            this.NguoiDungs = new HashSet<NguoiDung>();
        }
        [DisplayName("Mã phường")]
        public string MaPhuong { get; set; }
        [DisplayName("Phường/Xã")]
        public string TenPhuong { get; set; }
        [DisplayName("Mã quận")]
        public string MaQuan { get; set; }

        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
        public virtual Quan Quan { get; set; }
    }
}