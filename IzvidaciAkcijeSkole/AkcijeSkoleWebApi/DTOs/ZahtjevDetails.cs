using DomainModels = AkcijeSkole.Domain.Models;

namespace AkcijeSkoleWebApi.DTOs
{
    public class ZahtjevDetails
    {
        public int IdZahtjev { get; set; }
        public int IdMaterijalnaPotreba { get; set; }
        public string NazivMaterijalnaPotreba { get; set; } = String.Empty;
        public double Kolicina { get; set; }
        public string MjernaJedinica { get; set; } = String.Empty;
        public string Kooridinate { get; set; }
        public int Organizator { get; set; }
    }

    public static partial class DtoMapping
    {
        public static ZahtjevDetails ToDto(this DomainModels.ZahtjevDetails zahtjev)
        {
            return new ZahtjevDetails()
            {
                IdZahtjev = zahtjev.IdZahtjev,
                IdMaterijalnaPotreba = zahtjev.IdMatPotreba,
                NazivMaterijalnaPotreba = zahtjev.NazivMatPotreba,
                Kolicina = zahtjev.Kolicina,
                MjernaJedinica = zahtjev.MjernaJedinica,
                Kooridinate = zahtjev.Koordinate,
                Organizator = zahtjev.Organizator
            };
        }
        public static DomainModels.ZahtjevDetails ToDomain(this ZahtjevDetails zahtjev)
        {
            return new DomainModels.ZahtjevDetails(zahtjev.IdZahtjev, zahtjev.IdMaterijalnaPotreba, zahtjev.NazivMaterijalnaPotreba, zahtjev.Kolicina, zahtjev.MjernaJedinica, zahtjev.Kooridinate, zahtjev.Organizator);
        }

    }
}
