//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eCommerce.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Loai
    {
        public Loai()
        {
            this.DauGias = new HashSet<DauGia>();
        }
    
        public int MaLoai { get; set; }
        public string TenLoai { get; set; }
    
        public virtual ICollection<DauGia> DauGias { get; set; }
    }
}
