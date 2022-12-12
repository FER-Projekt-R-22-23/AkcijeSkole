using DomainModels = AkcijeSkole.Domain.Models;

namespace AkcijeSkoleWebApi.DTOs
{
    public class PrijavljenClanNaEdukaciju
    {
        public int IdPolaznik { get; set; }
        public DateTime datum { get; set; }

    }

    public static partial class DtoMapping
    {
        public static PrijavljenClanNaEdukaciju ToDto(this DomainModels.PrijavljeniClanNaEdukaciji prijavljeni)
        {
            return new PrijavljenClanNaEdukaciju()
            {
                IdPolaznik = prijavljeni.idPolaznik,
                datum = prijavljeni.datumPrijave
            };
        }

        public static DomainModels.PrijavljeniClanNaEdukaciji ToDomain(this PrijavljenClanNaEdukaciju prijavljeni)
        {
            return new DomainModels.PrijavljeniClanNaEdukaciji(
                prijavljeni.IdPolaznik,
                prijavljeni.datum
                );
        }
    }
}
