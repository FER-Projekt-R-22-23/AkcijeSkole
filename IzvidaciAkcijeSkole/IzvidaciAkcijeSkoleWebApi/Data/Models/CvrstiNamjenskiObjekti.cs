﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IzvidaciAkcijeSkoleWebApi.Data.Models
{
    public partial class CvrstiNamjenskiObjekti
    {
        [Key]
        public int IdNamjenskiObjekt { get; set; }
        [Required]
        [Unicode(false)]
        public string Opis { get; set; }

        [ForeignKey("IdNamjenskiObjekt")]
        [InverseProperty("CvrstiNamjenskiObjekti")]
        public virtual TerenskeLokacije IdNamjenskiObjektNavigation { get; set; }
    }
}