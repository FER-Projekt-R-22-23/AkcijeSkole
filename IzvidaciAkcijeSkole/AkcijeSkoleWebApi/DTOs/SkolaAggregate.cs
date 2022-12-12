using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using AkcijeSkole.Domain.Models;
using AkcijeSkole.Repositories.SqlServer;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DbModels = AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using DomainModels = AkcijeSkole.Domain.Models;

namespace AkcijeSkoleWebApi.DTOs
{
    public class SkolaAggregate
    {
        public int IdSkole { get; set; }

        [Required(ErrorMessage = "Naziv skole can't be null")]
        [StringLength(50, ErrorMessage = "Naziv skole cant't be longer than 50 characters")]
        public string NazivSkole { get; set; } = String.Empty;
        public int MjestoPbr { get; set; }
        public int Organizator { get; set; }
        public int KontaktOsoba { get; set; }
        public IEnumerable<Edukacija> EdukacijeUSkoli { get; set; } = Enumerable.Empty<Edukacija>();
    }

    public static partial class DtoMapping
    {
        public static SkolaAggregate ToAggregateDto(this DomainModels.Skola skola)
        {
            return new SkolaAggregate()
            {
                IdSkole = skola.Id,
                NazivSkole = skola.NazivSkole,
                MjestoPbr = skola.MjestoPbr,
                Organizator = skola.Organizator,
                KontaktOsoba = skola.KontaktOsoba,
                EdukacijeUSkoli = skola.EdukacijeUSkoli == null ? new List<Edukacija>() : skola.EdukacijeUSkoli.Select(edukacija => edukacija.ToDto()).ToList()
            };
        }
        public static DomainModels.Skola toDomain(this SkolaAggregate skola)
        {
            return new DomainModels.Skola(skola.IdSkole, skola.NazivSkole, skola.MjestoPbr, skola.Organizator, skola.KontaktOsoba, skola.EdukacijeUSkoli.Select(toDomain));
        }

    }
}
