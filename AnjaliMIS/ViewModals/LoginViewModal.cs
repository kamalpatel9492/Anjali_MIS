using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AnjaliMIS.ViewModals
{
    public class LoginViewModal
    {
        public int UserID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter username.")]
        public string UserName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter password.")]
        public string Password { get; set; }
    }
}