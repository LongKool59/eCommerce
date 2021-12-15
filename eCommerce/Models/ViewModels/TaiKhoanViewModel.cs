using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.Models.ViewModels
{
    public class TaiKhoanViewModel
    {
        [DisplayName("Email")]
        [Required(ErrorMessage = "Email không được để trống...")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ. Ví dụ: example@gmail.com")]
        public string Email { get; set; }
        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được trống...")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*\d).{5,25}$", ErrorMessage = "Mật khẩu bao gồm chữ và số, từ 5 đến 25 kí tự")]
        public string Password { get; set; }
    }
}