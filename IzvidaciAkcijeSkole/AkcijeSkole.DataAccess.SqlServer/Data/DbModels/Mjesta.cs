﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AkcijeSkole.DataAccess.SqlServer.Data.DbModels
{
    public partial class Mjesta
    {
        public Mjesta()
        {
            Akcije = new HashSet<Akcije>();
            Aktivnosti = new HashSet<Aktivnosti>();
            Edukacije = new HashSet<Edukacije>();
            Skole = new HashSet<Skole>();
            TerenskeLokacije = new HashSet<TerenskeLokacije>();
        }

        public int PbrMjesta { get; set; }
        public string NazivMjesta { get; set; }

        public virtual ICollection<Akcije> Akcije { get; set; }
        public virtual ICollection<Aktivnosti> Aktivnosti { get; set; }
        public virtual ICollection<Edukacije> Edukacije { get; set; }
        public virtual ICollection<Skole> Skole { get; set; }
        public virtual ICollection<TerenskeLokacije> TerenskeLokacije { get; set; }
    }
}