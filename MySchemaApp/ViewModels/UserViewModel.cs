using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MySchemaApp.ViewModels
{
    public class UserViewModel
        
    {   [Required(ErrorMessage ="Kullanıcı adı gereklidir.")]
        [Display(Name ="Kullanıcı Adı")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email adresi gereklidir.")]
        [EmailAddress(ErrorMessage ="Lütfen geçerli bir email adresi giriniz.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre Gereklidir.")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
         public string Password { get; set; }
    }
}
