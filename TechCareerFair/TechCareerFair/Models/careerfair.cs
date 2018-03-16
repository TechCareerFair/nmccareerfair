namespace TechCareerFair.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("careerfair.careerfair")]
    public partial class careerfair
    {
        public careerfair()
        {
            Phone = "";
            Address = "";
            Date = new DateTime();
        }

        public int CareerFairID { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }

        [StringLength(50)]
        public string Address { get; set; }
    }
}
