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
            business2field = new HashSet<business2field>();
            positions = new HashSet<position>();
        }

        public int BusinessID { get; set; }

        [Required]
        [StringLength(64)]
        public string Password { get; set; }

        [Required]
        [StringLength(320)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string BusinessName { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Street { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(2)]
        public string State { get; set; }

        [StringLength(10)]
        public string Zip { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public bool? Alumni { get; set; }

        public bool? NonProfit { get; set; }

        public bool? Outlet { get; set; }

        public bool? Display { get; set; }

        public string DisplayDescription { get; set; }

        public byte? Attendees { get; set; }

        public string BusinessDescription { get; set; }

        public string Website { get; set; }

        public string SocialMedia { get; set; }

        public byte[] Photo { get; set; }

        public string LocationPreference { get; set; }

        public bool? ContactMe { get; set; }

        public bool? Approved { get; set; }

        public bool? Active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<business2field> business2field { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<position> positions { get; set; }
    }
}
