namespace TechCareerFair.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("careerfair.applicant")]
    public partial class applicant
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public applicant()
        {
            Fields = new List<string>();
        }

        public int ApplicantID { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

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
        [DataType(DataType.MultilineText)]
        [StringLength(255, ErrorMessage = "Reached max length of 255 characters")]
        public string Profile { get; set; }

        [Display(Name = "Social Media")]
        [Url]
        [StringLength(255, ErrorMessage = "Reached max length of 255 characters")]
        public string SocialMedia { get; set; }

        public string Resume { get; set; }

        [Display(Name = "Years of Experience")]
        [Range(0, 100)]
        public byte? YearsExperience { get; set; }

        [Display(Name = "Looking for Internship")]
        public bool Internship { get; set; }

        public bool Active { get; set; }
        
        [Display(Name = "Fields of Interest")]
        public List<string> Fields { get; set; }
    }
}
