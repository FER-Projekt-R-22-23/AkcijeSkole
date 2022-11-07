using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DbModels = AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using DomainModels = AkcijeSkole.Domain.Models;

namespace AkcijeSkoleWebApi.DTOs
{
    public class Skola
    {
        public int IdSkole { get; set; }

        [Required(ErrorMessage = "Naziv skole can't be null")]
        [StringLength(50, ErrorMessage = "Naziv skole cant't be longer than 50 characters")]
        public string NazivSkole { get; set; } = String.Empty;
        public int MjestoPbr { get; set; }
        public int Organizator { get; set; }
        public int KontaktOsoba { get; set; }
    }

    public static partial class DtoMapping
    {
        public static Skola ToDto(this DomainModels.Skola skola)
        {
            return new Skola()
            {
                IdSkole = skola.Id,
                NazivSkole = skola.NazivSkole,
                MjestoPbr = skola.MjestoPbr,
                Organizator = skola.Organizator,
                KontaktOsoba = skola.KontaktOsoba,
            };
        }
        public static DomainModels.Skola ToDomain(this Skola skola)
        {
            return new DomainModels.Skola(skola.IdSkole, skola.NazivSkole, skola.MjestoPbr, skola.Organizator, skola.KontaktOsoba);
        }

    }
}


