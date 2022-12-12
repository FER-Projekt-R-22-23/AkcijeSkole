using System.ComponentModel.DataAnnotations;
using DomainModels = AkcijeSkole.Domain.Models;


namespace AkcijeSkoleWebApi.DTOs;
public class PrivremeniObjekt
{
    public int IdPrivremeniObjekt { get; set; }
    public string Opis { get; set; }
}

public static partial class DtoMapping
{
    public static PrivremeniObjekt ToDto(this DomainModels.PrivremeniObjekti privremeniObjekt)
        => new PrivremeniObjekt()
        {
            IdPrivremeniObjekt = privremeniObjekt.IdPrivremeniObjekt,
            Opis = privremeniObjekt.Opis
        };

    public static DomainModels.PrivremeniObjekti ToDomain(this PrivremeniObjekt privremeniObjekt)
        => new DomainModels.PrivremeniObjekti(
            privremeniObjekt.IdPrivremeniObjekt,
            privremeniObjekt.Opis
            );
}