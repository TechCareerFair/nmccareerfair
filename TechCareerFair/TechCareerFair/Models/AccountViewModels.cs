using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechCareerFair.Models
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

        [Display(Name = "Remember this browser?")]
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
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
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

    public class EditUserViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ApplicantViewModel
    {
        public ApplicantViewModel()
        {
            Fields = new List<string>();
        }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [EmailAddress]
        [Display(Name = "Old Email")]
        public string OldEmail { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public int ApplicantID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(50)]
        [Display(Name = "Current or previous university")]
        public string University { get; set; }

        [Display(Name = "Graduated from a college or university")]
        public bool Alumni { get; set; }

        [Display(Name = "Profile Description")]
        [StringLength(255, ErrorMessage = "Reached max length of 255 characters")]
        public string Profile { get; set; }

        [Display(Name = "Social Media")]
        [StringLength(255, ErrorMessage = "Reached max length of 255 characters")]
        public string SocialMedia { get; set; }

        public string Resume { get; set; }

        [Display(Name = "Years of Experience")]
        [Range(0, 100)]
        public byte? YearsExperience { get; set; }

        [Display(Name = "Looking for internship")]
        public bool Internship { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Fields of Study")]
        public List<string> Fields { get; set; }
    }

    public class BusinessViewModel
    {
        public BusinessViewModel()
        {
            Fields = new List<string>();
            Positions = new List<position>();
            Attendees = 0;
            City = "";
            State = "";
            Zip = "";
        }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [EmailAddress]
        [Display(Name = "Old Email")]
        public string OldEmail { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public int BusinessID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Business Name")]
        public string BusinessName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(50)]
        [Display(Name = "Address")]
        public string Street { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(2)]
        public string State { get; set; }

        [StringLength(10)]
        public string Zip { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [Display(Name = "Is the business run by an NMC alumni?")]
        public bool Alumni { get; set; }

        [Display(Name = "Is your business a not-for-profit?")]
        public bool NonProfit { get; set; }

        [Display(Name = "Does your display require a power outlet?")]
        public bool Outlet { get; set; }

        [Display(Name = "Do you have a display?")]
        public bool Display { get; set; }

        [Display(Name = "Display Description")]
        [StringLength(255, ErrorMessage = "Reached max length of 255 characters")]
        [DataType(DataType.MultilineText)]
        public string DisplayDescription { get; set; }

        [Range(0, 100)]
        [Display(Name = "How many people will be attending from the business?")]
        public byte Attendees { get; set; }

        [Display(Name = "Business Description")]
        [StringLength(255, ErrorMessage = "Reached max length of 255 characters")]
        [DataType(DataType.MultilineText)]
        public string BusinessDescription { get; set; }

        public string Website { get; set; }

        [Display(Name = "Social Media")]
        [StringLength(255, ErrorMessage = "Reached max length of 255 characters")]
        public string SocialMedia { get; set; }

        public string Photo { get; set; }

        [Display(Name = "Location Preference")]
        public string LocationPreference { get; set; }

        [Display(Name = "Contact Me")]
        public bool ContactMe { get; set; }

        [Display(Name = "Only contact me by email")]
        public bool PreferEmail { get; set; }

        public bool Approved { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Fields of Business")]
        public List<string> Fields { get; set; }

        public List<position> Positions { get; set; }
    }

    public class AdminViewModel
    {
        public int AdminID { get; set; }

        [Required]
        //[EmailAddress]
        [Display(Name = "Email")]
        public string Username { get; set; }

        [Required]
        //[EmailAddress]
        public string OldUsername { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Support's Email")]
        public string ContactEmail { get; set; }
    }

    public class RoleViewModel
    {
        public RoleViewModel() { }

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        public string Name { get; set; }
    }
}
