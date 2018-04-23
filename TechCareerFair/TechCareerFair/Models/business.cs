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
            City = "";
            State = "";
            Zip = "";

        }

        public int BusinessID { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

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

        [Display(Name = "Is the business a not-for-profit?")]
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

        [Url]
        public string Website { get; set; }

        [Display(Name = "Social Media")]
        [Url]
        [StringLength(255, ErrorMessage = "Reached max length of 255 characters")]
        public string SocialMedia { get; set; }

        [Display(Name = "Business Logo")]
        public string Photo { get; set; }

        [Display(Name = "Location Preference")]
        [StringLength(255, ErrorMessage = "Reached max length of 255 characters")]
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
}
