using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DbModels = AkcijeSkole.DataAccess.SqlServer.Data.DbModels;

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
        public static Skola ToDto(this DbModels.Skole skola)
        {
            return new Skola()
            {
                IdSkole = skola.IdSkole,
                NazivSkole = skola.NazivSkole,
                MjestoPbr = skola.MjestoPbr,
                Organizator = skola.Organizator,
                KontaktOsoba = skola.KontaktOsoba,
            };
        }
        public static DbModels.Skole toDbModel(this Skola skola)
        {
            return new DbModels.Skole()
            {
                IdSkole = skola.IdSkole,
                NazivSkole = skola.NazivSkole,
                MjestoPbr = skola.MjestoPbr,
                Organizator = skola.Organizator,
                KontaktOsoba = skola.KontaktOsoba,

            };
        }

    }
}


