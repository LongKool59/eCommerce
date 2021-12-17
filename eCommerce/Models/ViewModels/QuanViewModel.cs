using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace eCommerce.Models.ViewModels
{
    public class QuanViewModel
    {
        public QuanViewModel()
        {
            this.NguoiDungs = new HashSet<NguoiDung>();
            this.Phuongs = new HashSet<Phuong>();
        }
        [DisplayName("Mã quận")]
        public string MaQuan { get; set; }
        [DisplayName("Quận/Huyện")]
        public string TenQuan { get; set; }
        [DisplayName("Mã thành phố")]
        public string MaTP { get; set; }

        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
        public virtual ICollection<Phuong> Phuongs { get; set; }
        public virtual ThanhPho ThanhPho { get; set; }
    }
}