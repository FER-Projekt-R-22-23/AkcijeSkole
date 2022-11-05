using System.ComponentModel.DataAnnotations;
using DomainModels = AkcijeSkole.Domain.Models;

namespace AkcijeSkoleWebApi.DTOs;

public class TerenskeLokacije
{
    public int IdTerenskeLokacije { get; set; }

    [Required(ErrorMessage = "Naziv terenske lokacije can't be null")]
    [StringLength(50, ErrorMessage = "Naziv terenske lokacije can't be longer than 50 characters")]
    public string NazivTerenskeLokacije { get; set; } = string.Empty;

    [Required(ErrorMessage = "Slika can't be null")]
    public byte[]? Slika { get; set; }

    public bool ImaSanitarniCvor { get; set; }

    public int MjestoPbr { get; set; }

    [Required(ErrorMessage = "Opis can't be null")]
    public string Opis { get; set; } = string.Empty;
}

public static partial class DtoMapping
{
    public static TerenskeLokacije ToDto(this DomainModels.TerenskeLokacije terenskeLokacije)
        => new TerenskeLokacije()
        {
            IdTerenskeLokacije = terenskeLokacije.IdTerenskeLokacije,
            NazivTerenskeLokacije = terenskeLokacije.NazivTerenskeLokacije,
            Slika = terenskeLokacije.Slika,
            ImaSanitarniCvor = terenskeLokacije.ImaSanitarniCvor,
            MjestoPbr = terenskeLokacije.MjestoPbr,
            Opis = terenskeLokacije.Opis
        };

    public static DomainModels.TerenskeLokacije ToDomain(this TerenskeLokacije terenskeLokacije)
        => new DomainModels.TerenskeLokacije(
            terenskeLokacije.IdTerenskeLokacije,
            terenskeLokacije.NazivTerenskeLokacije,
            terenskeLokacije.Slika,
            terenskeLokacije.ImaSanitarniCvor,
            terenskeLokacije.MjestoPbr,
            terenskeLokacije.Opis
            );
}