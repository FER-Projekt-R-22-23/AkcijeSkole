using AkcijeSkole.Domain.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using DomainModels = AkcijeSkole.Domain.Models;

namespace AkcijeSkoleWebApi.DTOs
{
    public class PredavacNaEdukaciji
    {
        public int IdPredavac { get; set; }
        public int IdClan { get; set; }

    }

    public static partial class DtoMapping 
    {
        public static PredavacNaEdukaciji ToDto(this DomainModels.PredavacNaEdukaciji predavacNaEdukaciji)
        {
            return new PredavacNaEdukaciji()
            {
                IdPredavac = predavacNaEdukaciji.idPredavac,
                IdClan = predavacNaEdukaciji.idClan
            };
        }

        public static DomainModels.PredavacNaEdukaciji ToDomain(this PredavacNaEdukaciji predavacNaEdukaciji) {
            return new DomainModels.PredavacNaEdukaciji(
                predavacNaEdukaciji.IdPredavac,
                predavacNaEdukaciji.IdClan
                );
        }
    }
}
