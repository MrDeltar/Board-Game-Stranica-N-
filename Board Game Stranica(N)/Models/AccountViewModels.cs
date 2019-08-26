using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Board_Game_Stranica_N_.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Zapamtiti ovaj preglednik?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [Display(Name = "Zapamti me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        //email
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //lozinka
        [Display(Name = "Lozinka")]
        [StringLength(100, ErrorMessage = "{0} mora imati barem {2} karaktera.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} je obavezna")]
        public string Password { get; set; }

        //potvrda lozinke
        [Display(Name = "Potvrda Lozinke")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Lozinka i Potvrda Lozinke moraju biti isti.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} je obavezno")]
        public string ConfirmPassword { get; set; }

        // ime
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} je obavezno")]
        public string Ime { get; set; }

        // prezime
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} je obavezo")]
        public string Prezime { get; set; }


        // Datum rodenja
        [Display(Name = "Datum rođenja")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "{0} je obavezan")]
        public DateTime DatumRodenja { get; set; }

        //opis
        [Display(Name = "Ukratko opišite sebe")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Morate nešto napisati o sebi")]
        public string Opis { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} mora imati barem {2} karaktera.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrda Lozinke")]
        [Compare("Password", ErrorMessage = "Lozinka i Potvrda Lozinke moraju biti isti.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
