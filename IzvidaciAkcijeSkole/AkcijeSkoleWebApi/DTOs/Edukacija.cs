using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DbModels = AkcijeSkole.DataAccess.SqlServer.Data.DbModels;

namespace AkcijeSkoleWebApi.DTOs
{
    public class Edukacija
    {
        public int IdEdukacija { get; set; }
        [Required(ErrorMessage = "Naziv edukcije can't be null")]
        [StringLength(50, ErrorMessage = "Naziv edukacije cant't be longer than 50 characters")]
        public string NazivEdukacija { get; set; } = string.Empty;
        public int MjestoPbr { get; set; }
        public string OpisEdukacije { get; set; } = String.Empty;
        public int SkolaId { get; set; }
    }

    public static partial class DtoMapping
    {
        public static Edukacija ToDto(this DbModels.Edukacije edukacija)
        {
            return new Edukacija()
            {
                IdEdukacija = edukacija.IdEdukacija,
                NazivEdukacija = edukacija.NazivEdukacija,
                MjestoPbr = edukacija.MjestoPbr,
                OpisEdukacije = edukacija.OpisEdukacije,
                SkolaId = edukacija.SkolaId
            };
        }
        public static DbModels.Edukacije toDbModel(this Edukacija edukacija)
        {
            return new DbModels.Edukacije()
            {
                IdEdukacija = edukacija.IdEdukacija,
                NazivEdukacija = edukacija.NazivEdukacija,
                MjestoPbr = edukacija.MjestoPbr,
                OpisEdukacije = edukacija.OpisEdukacije,
                SkolaId = edukacija.SkolaId

            };
        }
    }
}
