﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AkcijeSkoleWebApi.Models
{
    public partial class Logorista
    {
        public int IdLogoriste { get; set; }
        public string KoodinateMreze { get; set; }
        public int PredvideniBrojClanova { get; set; }

        public virtual TerenskeLokacije IdLogoristeNavigation { get; set; }
    }
}