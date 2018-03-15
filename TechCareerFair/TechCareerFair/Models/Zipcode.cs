﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechCareerFair.Models
{
    public class Zipcode
    {
        public Zipcode()
        {
            ZipCode = "";
            City = "";
            State = "";
        }

        public int ZipCodeID { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int Business { get; set; }
    }
}