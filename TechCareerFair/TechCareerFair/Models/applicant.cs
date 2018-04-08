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

        /*[Required]
        [StringLength(64)]
        public string Password { get; set; }*/

        /*[Required]
        [StringLength(320)]
        public string Email { get; set; }*/

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
}
