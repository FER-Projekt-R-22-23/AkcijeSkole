using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using AkcijeSkole.Domain.Models;
using System.ComponentModel.DataAnnotations;
using DomainModels = AkcijeSkole.Domain.Models;


namespace AkcijeSkoleWebApi.DTOs;
public class PrivremeniObjekt : TerenskaLokacija
{
}

public static partial class DtoMapping
{
    public static PrivremeniObjekt ToDto(this DomainModels.PrivremeniObjekt privremeniObjekt)
        => new PrivremeniObjekt()
        {
            IdTerenskaLokacija = privremeniObjekt.Id,
            NazivTerenskaLokacija = privremeniObjekt.NazivTerenskaLokacija,
            Slika = privremeniObjekt.Slika,
            ImaSanitarniCvor = privremeniObjekt.ImaSanitarniCvor,
            MjestoPbr = privremeniObjekt.MjestoPbr,
            Opis = privremeniObjekt.Opis
        };

    public static DomainModels.PrivremeniObjekt ToDomain(this PrivremeniObjekt privremeniObjekt)
    => new DomainModels.PrivremeniObjekt(
            privremeniObjekt.IdTerenskaLokacija,
            privremeniObjekt.NazivTerenskaLokacija,
            privremeniObjekt.Slika,
            privremeniObjekt.ImaSanitarniCvor,
            privremeniObjekt.MjestoPbr,
            privremeniObjekt.Opis
            );
}