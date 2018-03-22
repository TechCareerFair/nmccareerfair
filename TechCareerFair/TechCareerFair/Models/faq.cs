namespace TechCareerFair.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("careerfair.faq")]
    public partial class faq
    {
        public faq()
        {
        }

        public int FaqID { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public string Answer { get; set; }

        public string Website { get; set; }

        public bool IsApplicantQ { get; set; }
    }
}
