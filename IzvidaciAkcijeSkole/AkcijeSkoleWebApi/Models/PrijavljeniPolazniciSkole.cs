﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AkcijeSkoleWebApi.Models
{
    public partial class PrijavljeniPolazniciSkole
    {
        public int SkolaId { get; set; }
        public int EdukacijaId { get; set; }
        public int PrijavljenClan { get; set; }
        public DateTime DatumPrijave { get; set; }

        public virtual Edukacije Edukacija { get; set; }
        public virtual Skole Skola { get; set; }
    }
}