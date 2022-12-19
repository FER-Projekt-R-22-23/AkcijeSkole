using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using AkcijeSkole.Domain.Models;
using System.ComponentModel.DataAnnotations;
using DomainModels = AkcijeSkole.Domain.Models;

namespace AkcijeSkoleWebApi.DTOs;

public class Logoriste : TerenskaLokacija
{
    public string? KoordinateMreze { get; set; }
    public int PredvideniBrojClanova { get; set; }
}

public static partial class DtoMapping
{
    public static Logoriste ToDto(this DomainModels.Logoriste logoriste)
        => new Logoriste()
        {
            KoordinateMreze = logoriste.KoordinateMreze,
            PredvideniBrojClanova = logoriste.PredvideniBrojClanova,
            IdTerenskaLokacija = logoriste.Id,
            NazivTerenskaLokacija = logoriste.NazivTerenskaLokacija,
            Slika = logoriste.Slika,
            ImaSanitarniCvor = logoriste.ImaSanitarniCvor,
            MjestoPbr = logoriste.MjestoPbr,
            Opis = logoriste.Opis
        };

    public static DomainModels.Logoriste ToDomain(this Logoriste logoriste)
    => new DomainModels.Logoriste(
            logoriste.IdTerenskaLokacija,
            logoriste.NazivTerenskaLokacija,
            logoriste.Slika,
            logoriste.ImaSanitarniCvor,
            logoriste.MjestoPbr,
            logoriste.Opis,
            logoriste.KoordinateMreze,
            logoriste.PredvideniBrojClanova
            );
}