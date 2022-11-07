using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using DomainModels = AkcijeSkole.Domain.Models;

namespace AkcijeSkoleWebApi.DTOs;

public class TerenskaLokacija
{
    public int IdTerenskaLokacija { get; set; }

    [Required(ErrorMessage = "Naziv terenske lokacije can't be null", AllowEmptyStrings = false)]
    [StringLength(50, ErrorMessage = "Naziv terenske lokacije can't be longer than 50 characters")]
    public string NazivTerenskaLokacija { get; set; } = string.Empty;

    [Required(ErrorMessage = "Slika can't be null")]
    public byte[]? Slika { get; set; }

    public bool ImaSanitarniCvor { get; set; }

    public int MjestoPbr { get; set; }

    [Required(ErrorMessage = "Opis can't be null", AllowEmptyStrings = false)]
    public string Opis { get; set; } = string.Empty;
}

public static partial class DtoMapping
{
    public static TerenskaLokacija ToDto(this DomainModels.TerenskaLokacija terenskaLokacija)
        => new TerenskaLokacija()
        {
            IdTerenskaLokacija = terenskaLokacija.Id,
            NazivTerenskaLokacija = terenskaLokacija.NazivTerenskaLokacija,
            Slika = terenskaLokacija.Slika,
            ImaSanitarniCvor = terenskaLokacija.ImaSanitarniCvor,
            MjestoPbr = terenskaLokacija.MjestoPbr,
            Opis = terenskaLokacija.Opis
        };

    public static DomainModels.TerenskaLokacija ToDomain(this TerenskaLokacija terenskaLokacija)
        => new DomainModels.TerenskaLokacija(
            terenskaLokacija.IdTerenskaLokacija,
            terenskaLokacija.NazivTerenskaLokacija,
            terenskaLokacija.Slika,
            terenskaLokacija.ImaSanitarniCvor,
            terenskaLokacija.MjestoPbr,
            terenskaLokacija.Opis
            );
}