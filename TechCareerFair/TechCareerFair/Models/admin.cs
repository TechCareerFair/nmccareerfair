namespace TechCareerFair.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("careerfair.admin")]
    public partial class admin
    {
        public int AdminID { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Support's Email")]
        public string ContactEmail { get; set; }
    }
}
