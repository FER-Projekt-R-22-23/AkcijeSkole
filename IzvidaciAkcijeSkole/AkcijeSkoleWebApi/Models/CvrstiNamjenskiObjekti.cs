﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AkcijeSkoleWebApi.Models
{
    public partial class CvrstiNamjenskiObjekti
    {
        public int IdNamjenskiObjekt { get; set; }
        public string Opis { get; set; }

        public virtual TerenskeLokacije IdNamjenskiObjektNavigation { get; set; }
    }
}