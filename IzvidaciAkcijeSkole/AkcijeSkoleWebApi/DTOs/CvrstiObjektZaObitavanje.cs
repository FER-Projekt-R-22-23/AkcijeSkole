using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using AkcijeSkole.Domain.Models;
using System.ComponentModel.DataAnnotations;
using DomainModels = AkcijeSkole.Domain.Models;

namespace AkcijeSkoleWebApi.DTOs;

public class CvrstiObjektZaObitavanje : TerenskaLokacija
{
    public int BrojPredvidenihSpavacihMjesta { get; set; }
}

public static partial class DtoMapping
{
    public static CvrstiObjektZaObitavanje ToDto(this DomainModels.CvrstiObjektZaObitavanje cvrstiObjektZaObitavanje)
        => new CvrstiObjektZaObitavanje()
        {
            BrojPredvidenihSpavacihMjesta = cvrstiObjektZaObitavanje.BrojPredvidenihSpavacihMjesta,
            IdTerenskaLokacija = cvrstiObjektZaObitavanje.Id,
            NazivTerenskaLokacija = cvrstiObjektZaObitavanje.NazivTerenskaLokacija,
            Slika = cvrstiObjektZaObitavanje.Slika,
            ImaSanitarniCvor = cvrstiObjektZaObitavanje.ImaSanitarniCvor,
            MjestoPbr = cvrstiObjektZaObitavanje.MjestoPbr,
            Opis = cvrstiObjektZaObitavanje.Opis
        };

    public static DomainModels.CvrstiObjektZaObitavanje ToDomain(this CvrstiObjektZaObitavanje cvrstiObjektZaObitavanje)
        => new DomainModels.CvrstiObjektZaObitavanje(
            cvrstiObjektZaObitavanje.IdTerenskaLokacija,
            cvrstiObjektZaObitavanje.NazivTerenskaLokacija,
            cvrstiObjektZaObitavanje.Slika,
            cvrstiObjektZaObitavanje.ImaSanitarniCvor,
            cvrstiObjektZaObitavanje.MjestoPbr,
            cvrstiObjektZaObitavanje.Opis,
            cvrstiObjektZaObitavanje.BrojPredvidenihSpavacihMjesta
            );
}