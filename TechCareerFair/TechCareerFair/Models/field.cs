namespace TechCareerFair.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("careerfair.field")]
    public partial class field
    {

        public int FieldID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
