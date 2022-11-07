﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseLibrary;
using AkcijeSkole.Commons;

namespace AkcijeSkole.Domain.Models
{
    public class Akcija : Entity<int>
    {
        private string _Naziv;
        private int _MjestoPbr;
        private int _Organizator;
        private int _KontaktOsoba;

        public Akcija(int id, string naziv, int mjestoPbr, int organizator, int kontaktOsoba) : base(id)
        {
            if(string.IsNullOrEmpty(naziv))
            {
                throw new ArgumentNullException($"'{nameof(naziv)}' cannot be null or empty.", nameof(naziv));
            }
            _Naziv = naziv;
            _MjestoPbr = mjestoPbr;
            _Organizator = organizator;
            _KontaktOsoba = kontaktOsoba;
        }

        public string Naziv { get => _Naziv; set => _Naziv = value; }

        public int MjestoPbr { get => _MjestoPbr; set => _MjestoPbr = value; }  

        public int Organizator { get => _Organizator; set => _Organizator = value; }    

        public int KontaktOsoba { get => _KontaktOsoba; set => _KontaktOsoba = value;}

        public override bool Equals(object? obj)
        {
            return obj is not null &&
                obj is Akcija akcija &&
                Id.Equals(akcija.Id) &&
                Naziv.Equals(akcija.Naziv) &&
                MjestoPbr.Equals(akcija.MjestoPbr) &&
                Organizator.Equals(akcija.Organizator);
    
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Naziv);
        }

        public override Result IsValid()
        => Validation.Validate(
            (() => _Naziv.Length <= 50, "Naziv akcije lenght must be less than 50 characters"),
            (() => !string.IsNullOrEmpty(_Naziv.Trim()), "Naziv akcije name can't be null, empty, or whitespace")
            );

    }

}
