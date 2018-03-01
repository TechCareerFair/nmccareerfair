namespace TechCareerFair.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("careerfair.business2field")]
    public partial class business2field
    {
        [Key]
        public int BusinessFieldID { get; set; }

        public int Business { get; set; }

        public int Field { get; set; }

        public virtual business business1 { get; set; }

        public virtual field field1 { get; set; }
    }
}
