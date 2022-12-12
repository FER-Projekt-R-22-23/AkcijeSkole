using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DbModels = AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using DomainModels = AkcijeSkole.Domain.Models;

namespace AkcijeSkoleWebApi.DTOs
{
    public class AkcijaAggregate
    {
        public int IdAkcije { get; set; }

        [Required(ErrorMessage = "Naziv akcije can't be null")]
        [StringLength(50, ErrorMessage = "Naziv akcije cant't be longer than 50 characters")]
        public string Naziv { get; set; } = String.Empty;
        public int MjestoPbr { get; set; }
        public int Organizator { get; set; }
        public int KontaktOsoba { get; set; }

        public string? Vrsta { get; set; }

        public IEnumerable<Aktivnost> AktivnostiAkcije { get; set; } = Enumerable.Empty<Aktivnost>();
    }

    public partial class DtoMapping
    {
        public static AkcijaAggregate ToAktivnostiAggregateDto(this DomainModels.Akcija akcija)
        {
            return new AkcijaAggregate()
            {
                IdAkcije = akcija.Id,
                Naziv = akcija.Naziv,
                MjestoPbr = akcija.MjestoPbr,
                Organizator = akcija.Organizator,
                KontaktOsoba = akcija.KontaktOsoba,
                Vrsta = akcija.Vrsta,
                AktivnostiAkcije = akcija.AktivnostiAkcije == null ? new List<Aktivnost>() : akcija.AktivnostiAkcije.Select(a => a.ToDto()).ToList()
            };
        }

        public static DomainModels.Akcija toDomain(this AkcijaAggregate akcija)
        {
            return new DomainModels.Akcija(akcija.IdAkcije, akcija.Naziv, akcija.MjestoPbr, akcija.Organizator, akcija.KontaktOsoba, akcija.Vrsta, akcija.AktivnostiAkcije.Select(ToDomain));
        }
    }
}
