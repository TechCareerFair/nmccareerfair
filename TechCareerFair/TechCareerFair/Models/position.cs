namespace TechCareerFair.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("careerfair.position")]
    public partial class position
    {
        public position()
        {
        }

        public int PositionID { get; set; }

        public int Business { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255, ErrorMessage = "Reached max length of 255 characters")]
        public string Description { get; set; }

        [Url]
        public string Website { get; set; }
        
        public bool Internship { get; set; }

        public string IsInternship
        {
            get
            {
                return (Internship ? "Yes" : "No");
            }
        }
    }
}
