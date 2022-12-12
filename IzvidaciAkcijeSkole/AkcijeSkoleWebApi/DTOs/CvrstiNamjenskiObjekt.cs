using System.ComponentModel.DataAnnotations;
using DomainModels = AkcijeSkole.Domain.Models;

namespace AkcijeSkoleWebApi.DTOs;

public class CvrstiNamjenskiObjekt
{
    public int IdCvrstiNamjenskiObjekt { get; set; }
    public string Opis { get; set; }
}

public static partial class DtoMapping
{
    public static CvrstiNamjenskiObjekt ToDto(this DomainModels.CvrstiNamjenskiObjekti cvrstiNamjenskiObjekt)
        => new CvrstiNamjenskiObjekt()
        {
            IdCvrstiNamjenskiObjekt = cvrstiNamjenskiObjekt.IdCvrstiNamjenskiObjekt,
            Opis = cvrstiNamjenskiObjekt.Opis
        };

    public static DomainModels.CvrstiNamjenskiObjekti ToDomain(this CvrstiNamjenskiObjekt cvrstiNamjenskiObjekt)
        => new DomainModels.CvrstiNamjenskiObjekti(
            cvrstiNamjenskiObjekt.IdCvrstiNamjenskiObjekt,
            cvrstiNamjenskiObjekt.Opis
            );
}