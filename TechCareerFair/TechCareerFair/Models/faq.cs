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
        [DataType(DataType.MultilineText)]
        public string Question { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Answer { get; set; }

        [Url]
        public string Website { get; set; }

        [Display(Name = "This FAQ is for an applicant")]
        public bool IsApplicantQ { get; set; }
    }
}
