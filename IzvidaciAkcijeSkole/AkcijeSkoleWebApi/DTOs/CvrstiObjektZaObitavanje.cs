using System.ComponentModel.DataAnnotations;
using DomainModels = AkcijeSkole.Domain.Models;

namespace AkcijeSkoleWebApi.DTOs;

public class CvrstiObjektZaObitavanje
{
    public int IdObjektZaObitavanje { get; set; }
    public int BrojPredvidenihSpavacihMjesta { get; set; }
}

public static partial class DtoMapping
{
    public static CvrstiObjektZaObitavanje ToDto(this DomainModels.CvrstiObjektiZaObitavanje cvrstiObjektZaObitavanje)
        => new CvrstiObjektZaObitavanje()
        {
            IdObjektZaObitavanje = cvrstiObjektZaObitavanje.IdObjektZaObitavanje,
            BrojPredvidenihSpavacihMjesta = cvrstiObjektZaObitavanje.BrojPredvidenihSpavacihMjesta
        };

    public static DomainModels.CvrstiObjektiZaObitavanje ToDomain(this CvrstiObjektZaObitavanje cvrstiObjektZaObitavanje)
        => new DomainModels.CvrstiObjektiZaObitavanje(
            cvrstiObjektZaObitavanje.IdObjektZaObitavanje,
            cvrstiObjektZaObitavanje.BrojPredvidenihSpavacihMjesta
            );
}