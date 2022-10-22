﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AkcijeSkoleWebApi.Data.DbModels
{
    public partial class Edukacije
    {
        public Edukacije()
        {
            PolazniciSkole = new HashSet<PolazniciSkole>();
            Predavaci = new HashSet<Predavaci>();
            PrijavljeniPolazniciSkole = new HashSet<PrijavljeniPolazniciSkole>();
        }

        [Key]
        public int IdEdukacija { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string NazivEdukacija { get; set; }
        public int MjestoPbr { get; set; }
        [Required]
        public string OpisEdukacije { get; set; }
        public int SkolaId { get; set; }

        [ForeignKey("MjestoPbr")]
        [InverseProperty("Edukacije")]
        public virtual Mjesta MjestoPbrNavigation { get; set; }
        [ForeignKey("SkolaId")]
        [InverseProperty("Edukacije")]
        public virtual Skole Skola { get; set; }
        [InverseProperty("Edukacija")]
        public virtual ICollection<PolazniciSkole> PolazniciSkole { get; set; }
        [InverseProperty("Edukacija")]
        public virtual ICollection<Predavaci> Predavaci { get; set; }
        [InverseProperty("Edukacija")]
        public virtual ICollection<PrijavljeniPolazniciSkole> PrijavljeniPolazniciSkole { get; set; }
    }
}