using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace TechCareerFair.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }

    public class ApplicantViewModel
    {
        public ApplicantViewModel()
        {
            Fields = new List<string>();
        }

        public string Password { get; set; }
        
        public string Email { get; set; }

        public int ApplicantID { get; set; }

        public string UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string University { get; set; }

        public bool Alumni { get; set; }

        [Display(Name = "Profile Description")]
        public string Profile { get; set; }

        [Display(Name = "Social Media")]
        public string SocialMedia { get; set; }

        public string Resume { get; set; }

        [Display(Name = "Years of Experience")]
        [Range(0, 100)]
        public byte? YearsExperience { get; set; }

        public bool Internship { get; set; }

        public bool Active { get; set; }

        public List<string> Fields { get; set; }
    }

    public class BusinessViewModel
    {
        public BusinessViewModel()
        {
            Fields = new List<string>();
            Positions = new List<position>();
        }

        public string Password { get; set; }
        
        public string Email { get; set; }

        public int BusinessID { get; set; }

        public string UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string BusinessName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "LastName")]
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

        public bool Alumni { get; set; }

        public bool NonProfit { get; set; }

        [Display(Name = "Does Your Display Require Power?")]
        public bool Outlet { get; set; }

        [Display(Name = "Do You Have A Display?")]
        public bool Display { get; set; }

        [Display(Name = "Display Description")]
        [DataType(DataType.MultilineText)]
        public string DisplayDescription { get; set; }

        [Range(0, 100)]
        public byte Attendees { get; set; }

        [Display(Name = "Business Description")]
        [DataType(DataType.MultilineText)]
        public string BusinessDescription { get; set; }

        public string Website { get; set; }

        [Display(Name = "Social Media")]
        public string SocialMedia { get; set; }

        public string Photo { get; set; }

        [Display(Name = "Location Preference")]
        public string LocationPreference { get; set; }

        [Display(Name = "Contact Me")]
        public bool ContactMe { get; set; }

        [Display(Name = "Contact Me By Email")]
        public bool PreferEmail { get; set; }

        public bool Approved { get; set; }

        public bool Active { get; set; }

        public List<string> Fields { get; set; }

        public List<position> Positions { get; set; }
    }
}