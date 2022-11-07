using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DbModels = AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using DomainModels = AkcijeSkole.Domain.Models;

namespace AkcijeSkoleWebApi.DTOs
{
    public class Aktivnost
    {
        public int IdAktivnost { get; set; }
        public int MjestoPbr { get; set; }
        public int KontaktOsoba { get; set; }
        public string Opis { get; set; } = string.Empty;
        public int AkcijaId { get; set; }
    }

    public static partial class DtoMapping
    {
        public static Aktivnost ToDto(this DomainModels.Aktivnost aktivnost)
        {
            return new Aktivnost()
            {
                IdAktivnost = aktivnost.Id,
                MjestoPbr = aktivnost.MjestoPbr,
                KontaktOsoba = aktivnost.KontaktOsoba,
                Opis = aktivnost.Opis, 
                AkcijaId = aktivnost.AkcijaId
            };
        }
        public static DomainModels.Aktivnost ToDomain(this Aktivnost aktivnost)
        {
            return new DomainModels.Aktivnost(aktivnost.IdAktivnost, aktivnost.MjestoPbr, aktivnost.KontaktOsoba, aktivnost.Opis, aktivnost.AkcijaId);
        }

    }
}
