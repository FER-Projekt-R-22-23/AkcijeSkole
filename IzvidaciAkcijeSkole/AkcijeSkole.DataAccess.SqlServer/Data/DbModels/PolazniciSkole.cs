﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AkcijeSkole.DataAccess.SqlServer.Data.DbModels
{
    public partial class PolazniciSkole
    {
        public int Polaznik { get; set; }
        public int SkolaId { get; set; }
        public int EdukacijaId { get; set; }

        public virtual Edukacije Edukacija { get; set; }
        public virtual Skole Skola { get; set; }
    }
}