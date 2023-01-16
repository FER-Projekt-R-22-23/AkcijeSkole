﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AkcijeSkole.DataAccess.SqlServer.Data.DbModels
{
    public partial class Akcije
    {
        public Akcije()
        {
            Aktivnosti = new HashSet<Aktivnosti>();
            PolazniciAkcije = new HashSet<PolazniciAkcije>();
            PrijavljeniPolazniciAkcije = new HashSet<PrijavljeniPolazniciAkcije>();
            MaterijalnePotrebe = new HashSet<MaterijalnePotrebe>();
            TerenskeLokacije = new HashSet<TerenskeLokacije>();
        }

        public int IdAkcija { get; set; }
        public string Naziv { get; set; }
        public int MjestoPbr { get; set; }
        public int Organizator { get; set; }
        public int KontaktOsoba { get; set; }
        public string Vrsta { get; set; }
        public string Koordinate { get; set; }

        public virtual Mjesta MjestoPbrNavigation { get; set; }
        public virtual ICollection<Aktivnosti> Aktivnosti { get; set; }
        public virtual ICollection<PolazniciAkcije> PolazniciAkcije { get; set; }
        public virtual ICollection<PrijavljeniPolazniciAkcije> PrijavljeniPolazniciAkcije { get; set; }

        public virtual ICollection<MaterijalnePotrebe> MaterijalnePotrebe { get; set; }
        public virtual ICollection<TerenskeLokacije> TerenskeLokacije { get; set; }
    }
}