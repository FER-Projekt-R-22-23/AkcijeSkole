using DomainModels = AkcijeSkole.Domain.Models;
namespace AkcijeSkoleWebApi.DTOs { 
    public class Zahtjev
    {
        public int IdZahtjev { get; set; }
        public int IdMaterijalnaPotreba { get; set; }
        public string Status { get; set; } = String.Empty;
    }

    public static partial class DtoMapping
    {
        public static Zahtjev ToDto(this DomainModels.Zahtjev zahtjev)
        {
            return new Zahtjev()
            {
                IdZahtjev = zahtjev.Id,
                IdMaterijalnaPotreba = zahtjev.IdMatPotreba,
                Status = zahtjev.Status,
            };
        }
        public static DomainModels.Zahtjev ToDomain(this Zahtjev zahtjev)
        {
            return new DomainModels.Zahtjev(zahtjev.IdZahtjev, zahtjev.IdMaterijalnaPotreba, zahtjev.Status);
        }

    }
}
