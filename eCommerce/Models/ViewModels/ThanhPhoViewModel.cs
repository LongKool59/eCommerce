using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace eCommerce.Models.ViewModels
{
    public class ThanhPhoViewModel
    {
        public ThanhPhoViewModel()
        {
            this.NguoiDungs = new HashSet<NguoiDung>();
            this.Quans = new HashSet<Quan>();
        }
        [DisplayName("Mã thành phố")]
        public string MaTP { get; set; }
        [DisplayName("Tỉnh/Thành phố")]
        public string TenTP { get; set; }

        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
        public virtual ICollection<Quan> Quans { get; set; }
    }
}