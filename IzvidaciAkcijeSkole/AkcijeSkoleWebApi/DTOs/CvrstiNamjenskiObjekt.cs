using System.ComponentModel.DataAnnotations;
using DomainModels = AkcijeSkole.Domain.Models;

namespace AkcijeSkoleWebApi.DTOs;

public class CvrstiNamjenskiObjekt : TerenskaLokacija
{
}

public static partial class DtoMapping
{
    public static CvrstiNamjenskiObjekt ToDto(this DomainModels.CvrstiNamjenskiObjekt cvrstiNamjenskiObjekt)
        => new CvrstiNamjenskiObjekt()
        {
            IdTerenskaLokacija = cvrstiNamjenskiObjekt.Id,
            NazivTerenskaLokacija = cvrstiNamjenskiObjekt.NazivTerenskaLokacija,
            Slika = cvrstiNamjenskiObjekt.Slika,
            ImaSanitarniCvor = cvrstiNamjenskiObjekt.ImaSanitarniCvor,
            MjestoPbr = cvrstiNamjenskiObjekt.MjestoPbr,
            Opis = cvrstiNamjenskiObjekt.Opis
        };

    public static DomainModels.CvrstiNamjenskiObjekt ToDomain(this CvrstiNamjenskiObjekt cvrstiNamjenskiObjekt)
        => new DomainModels.CvrstiNamjenskiObjekt(
            cvrstiNamjenskiObjekt.IdTerenskaLokacija,
            cvrstiNamjenskiObjekt.NazivTerenskaLokacija,
            cvrstiNamjenskiObjekt.Slika,
            cvrstiNamjenskiObjekt.ImaSanitarniCvor,
            cvrstiNamjenskiObjekt.MjestoPbr,
            cvrstiNamjenskiObjekt.Opis
            );
}