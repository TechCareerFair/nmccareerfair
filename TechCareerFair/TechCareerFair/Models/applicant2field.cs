namespace TechCareerFair.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("careerfair.applicant2field")]
    public partial class applicant2field
    {
        [Key]
        public int ApplicantFieldID { get; set; }

        public int Applicant { get; set; }

        public int Field { get; set; }
    }
}
