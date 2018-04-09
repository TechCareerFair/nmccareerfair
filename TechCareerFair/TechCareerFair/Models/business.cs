namespace TechCareerFair.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("careerfair.business")]
    public partial class business
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public business()
        {
            Fields = new List<string>();
            Positions = new List<position>();
            Attendees = 0;
        }

        public int BusinessID { get; set; }

        /*[Required]
        [StringLength(64)]
        public string Password { get; set; }

        [Required]
        [StringLength(320)]
        public string Email { get; set; }*/

        public string Email { get; set; }

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

        [Display(Name ="Does Your Display Require Power?")]
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
