﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AkcijeSkoleWebApi.Models
{
    public partial class PrivremeniObjekti
    {
        public int IdPrivremeniObjekt { get; set; }
        public string Opis { get; set; }

        public virtual TerenskeLokacije IdPrivremeniObjektNavigation { get; set; }
    }
}