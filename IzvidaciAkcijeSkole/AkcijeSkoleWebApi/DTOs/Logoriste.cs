using System.ComponentModel.DataAnnotations;
using DomainModels = AkcijeSkole.Domain.Models;

namespace AkcijeSkoleWebApi.DTOs;

public class Logoriste
{
    public int IdLogoriste { get; set; }
    public string KoordinateMreze { get; set; }
    public int PredvideniBrojClanova { get; set; }
}

public static partial class DtoMapping
{
    public static Logoriste ToDto(this DomainModels.Logorista logoriste)
        => new Logoriste()
        {
            IdLogoriste = logoriste.IdLogoriste,
            KoordinateMreze = logoriste.KoordinateMreze,
            PredvideniBrojClanova = logoriste.PredvideniBrojClanova
        };

    public static DomainModels.Logorista ToDomain(this Logoriste logoriste)
        => new DomainModels.Logorista(
            logoriste.IdLogoriste,
            logoriste.KoordinateMreze,
            logoriste.PredvideniBrojClanova
            );
}