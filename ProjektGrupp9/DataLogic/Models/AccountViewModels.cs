using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DataLogic.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "E-Post")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    //public class SendCodeViewModel
    //{
    //    public string SelectedProvider { get; set; }
    //    public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    //    public string ReturnUrl { get; set; }
    //    public bool RememberMe { get; set; }
    //}

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Kod")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Kom ihåg webbläsaren?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "E-post")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "E-post")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }

        [Display(Name = "Kom ihåg mig?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Roll")]
        public string SelectedRole { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-Post")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Efternamn")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Rum")]
        public string Room { get; set; }

        [Required]
        [Display(Name = "Mobilnummer")]
        public string PhoneNmbr { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0}et måste vara minst {2} tecken långt.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bekräfta lösenord")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Lösenorden matchar inte.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-Post")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0}et måste vara minst {2} tecken långt.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenordet")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bekräfta lösenord")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Lösenorden matchar inte.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-Post")]
        public string Email { get; set; }
    }
}
