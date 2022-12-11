using DomainModels = AkcijeSkole.Domain.Models;

namespace AkcijeSkoleWebApi.DTOs
{
    public class PolaznikSkole
    {
        public int IdPolaznik { get; set; }
        public int IdEdukacija { get; set; }

    }

    public static partial class DtoMapping
    {
        public static PolaznikSkole ToDto(this DomainModels.PolaznikSkole polaznikSkole)
        {
            return new PolaznikSkole()
            {
                IdPolaznik = polaznikSkole.idPolaznik,
                IdEdukacija = polaznikSkole.idEdukacija
            };
        }

        public static DomainModels.PolaznikSkole ToDomain(this PolaznikSkole polaznikSkole)
        {
            return new DomainModels.PolaznikSkole(
                polaznikSkole.IdPolaznik,
                polaznikSkole.IdEdukacija
                );
        }
    }
}
