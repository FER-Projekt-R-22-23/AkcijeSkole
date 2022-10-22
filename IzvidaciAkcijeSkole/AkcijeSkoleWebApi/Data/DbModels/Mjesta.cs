﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AkcijeSkoleWebApi.Data.DbModels
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

        [Key]
        public int PbrMjesta { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string NazivMjesta { get; set; }

        [InverseProperty("MjestoPbrNavigation")]
        public virtual ICollection<Akcije> Akcije { get; set; }
        [InverseProperty("MjestoPbrNavigation")]
        public virtual ICollection<Aktivnosti> Aktivnosti { get; set; }
        [InverseProperty("MjestoPbrNavigation")]
        public virtual ICollection<Edukacije> Edukacije { get; set; }
        [InverseProperty("MjestoPbrNavigation")]
        public virtual ICollection<Skole> Skole { get; set; }
        [InverseProperty("MjestoPbrNavigation")]
        public virtual ICollection<TerenskeLokacije> TerenskeLokacije { get; set; }
    }
}