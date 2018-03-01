namespace TechCareerFair.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("careerfair.gallery")]
    public partial class gallery
    {
        public int GalleryID { get; set; }

        public byte[] Photo { get; set; }

        public string Video { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
