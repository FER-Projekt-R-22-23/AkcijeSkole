﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AkcijeSkole.DataAccess.SqlServer.Data.DbModels
{
    public partial class TerenskeLokacije
    {
        public TerenskeLokacije()
        {
            Akcije = new HashSet<Akcije>();
            MaterijalnePotrebe = new HashSet<MaterijalnePotrebe>();
            Skola = new HashSet<Skole>();
        }

        [Key]
        public int IdTerenskeLokacije { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string NazivTerenskeLokacije { get; set; }
        public byte[] Slika { get; set; }
        public bool ImaSanitarniCvor { get; set; }
        public int MjestoPbr { get; set; }
        [Required]
        [Unicode(false)]
        public string Opis { get; set; }

        [ForeignKey("MjestoPbr")]
        [InverseProperty("TerenskeLokacije")]
        public virtual Mjesta MjestoPbrNavigation { get; set; }
        [InverseProperty("IdNamjenskiObjektNavigation")]
        public virtual CvrstiNamjenskiObjekti CvrstiNamjenskiObjekti { get; set; }
        [InverseProperty("IdObjektaZaObitavanjeNavigation")]
        public virtual CvrstiObjektiZaObitavanje CvrstiObjektiZaObitavanje { get; set; }
        [InverseProperty("IdLogoristeNavigation")]
        public virtual Logorista Logorista { get; set; }
        [InverseProperty("IdPrivremeniObjektNavigation")]
        public virtual PrivremeniObjekti PrivremeniObjekti { get; set; }

        [ForeignKey("TerenskeLokacijeId")]
        [InverseProperty("TerenskeLokacije")]
        public virtual ICollection<Akcije> Akcije { get; set; }
        [ForeignKey("TerenskeLokacijeId")]
        [InverseProperty("TerenskeLokacije")]
        public virtual ICollection<MaterijalnePotrebe> MaterijalnePotrebe { get; set; }
        [ForeignKey("TerenskaLokacijaId")]
        [InverseProperty("TerenskaLokacija")]
        public virtual ICollection<Skole> Skola { get; set; }
    }
}