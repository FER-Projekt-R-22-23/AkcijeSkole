using DomainModels = AkcijeSkole.Domain.Models;

namespace AkcijeSkoleWebApi.DTOs
{
    public class PolaznikNaEdukaciji
    {
        public int IdPolaznik { get; set; }

    }

    public static partial class DtoMapping
    {
        public static PolaznikNaEdukaciji ToDto(this DomainModels.PolaznikNaEdukaciji polaznikSkole)
        {
            return new PolaznikNaEdukaciji()
            {
                IdPolaznik = polaznikSkole.idPolaznik
            };
        }

        public static DomainModels.PolaznikNaEdukaciji ToDomain(this PolaznikNaEdukaciji polaznikSkole)
        {
            return new DomainModels.PolaznikNaEdukaciji(
                polaznikSkole.IdPolaznik
                );
        }
    }
}
