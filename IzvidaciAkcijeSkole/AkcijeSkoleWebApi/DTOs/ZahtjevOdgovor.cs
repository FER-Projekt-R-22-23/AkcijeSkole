using DomainModels = AkcijeSkole.Domain.Models;
namespace AkcijeSkoleWebApi.DTOs { 
    public class ZahtjevOdgovor
    {
        public int IdZahtjev { get; set; }
        public int Davatelj { get; set; }
        public string Status { get; set; } = String.Empty;
    }

    public static partial class DtoMapping
    {
        public static ZahtjevOdgovor ToDto(this DomainModels.ZahtjevOdgovor zahtjev)
        {
            return new ZahtjevOdgovor()
            {
                IdZahtjev = zahtjev.IdZahtjev,
                Davatelj = zahtjev.Davatelj,
                Status = zahtjev.Status
            };
        }
        public static DomainModels.ZahtjevOdgovor ToDomain(this ZahtjevOdgovor zahtjev)
        {
            return new DomainModels.ZahtjevOdgovor(zahtjev.IdZahtjev, zahtjev.Davatelj, zahtjev.Status);
        }

    }
}
