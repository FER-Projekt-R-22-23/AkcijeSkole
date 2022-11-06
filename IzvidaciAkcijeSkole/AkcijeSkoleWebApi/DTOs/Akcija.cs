using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DbModels = AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using DomainModels = AkcijeSkole.Domain.Models;

namespace AkcijeSkoleWebApi.DTOs
{
    public class Akcija
    {
        public int IdAkcije { get; set; }

        [Required(ErrorMessage = "Naziv akcije can't be null")]
        [StringLength(50, ErrorMessage = "Naziv akcije cant't be longer than 50 characters")]
        public string Naziv { get; set; } = String.Empty;
        public int MjestoPbr { get; set; }
        public int Organizator { get; set; }
        public int KontaktOsoba { get; set; }
    }

    public static partial class DtoMapping
    {
        public static Akcija ToDto(this DomainModels.Akcija akcija)
        {
            return new Akcija()
            {
                IdAkcije = akcija.Id,
                Naziv = akcija.Naziv,
                MjestoPbr = akcija.MjestoPbr,
                Organizator = akcija.Organizator,
                KontaktOsoba = akcija.KontaktOsoba,
            };
        }
        public static DomainModels.Akcija toDomain(this Akcija akcija)
        {
            return new DomainModels.Akcija(akcija.IdAkcije, akcija.Naziv, akcija.MjestoPbr, akcija.Organizator, akcija.KontaktOsoba);
        }

    }
}
