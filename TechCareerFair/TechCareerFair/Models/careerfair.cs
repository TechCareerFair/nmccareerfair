namespace TechCareerFair.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Drawing;
    using GoogleMaps.LocationServices;

    [Table("careerfair.careerfair")]
    public partial class careerfair
    {
        public careerfair()
        {
            Date = new DateTime();
        }

        public int CareerFairID { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [Column(TypeName = "datetime2")]
        [Display(Name = "Date (dd/mm/yyyy hh:mm:ss meridiem)")]
        public DateTime Date { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [DataType(DataType.MultilineText)]
        public string About { get; set; }

        public MapPoint GeoCodeAddress()
        {
            try
            {
                var locationservice = new GoogleLocationService();
                return locationservice.GetLatLongFromAddress(Address);
            }
            catch(Exception e)
            {
                MapPoint mp = new MapPoint();
                mp.Latitude = 44.765159;
                mp.Longitude = -85.606783;

                return mp;
            }
        }

    }
}
