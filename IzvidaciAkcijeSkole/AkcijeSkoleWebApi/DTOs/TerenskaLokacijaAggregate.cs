using AkcijeSkole.Domain.Models;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.ComponentModel.DataAnnotations;

namespace AkcijeSkoleWebApi.DTOs;

public class TerenskeLokacijeNamjenskiObjektiAggregate
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

    public IEnumerable<AkcijeSkole.Domain.Models.CvrstiNamjenskiObjekti> CvrstiNamjenskiObjekti { get; set; } = Enumerable.Empty<AkcijeSkole.Domain.Models.CvrstiNamjenskiObjekti>();
}

public class TerenskeLokacijeObjektiZaObitavanjeAggregate
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

    public IEnumerable<AkcijeSkole.Domain.Models.CvrstiObjektiZaObitavanje> CvrstiObjektiZaObitavanje { get; set; } = Enumerable.Empty<AkcijeSkole.Domain.Models.CvrstiObjektiZaObitavanje>();
}

public class TerenskeLokacijeLogoristaAggregate
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

    public IEnumerable<AkcijeSkole.Domain.Models.Logorista> Logorista { get; set; } = Enumerable.Empty<AkcijeSkole.Domain.Models.Logorista>();
}

public class TerenskeLokacijePrivremeniObjektiAggregate
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

    public IEnumerable<AkcijeSkole.Domain.Models.PrivremeniObjekti> PrivremeniObjekti { get; set; } = Enumerable.Empty<AkcijeSkole.Domain.Models.PrivremeniObjekti>();
}

public static partial class DtoMapping
{
    public static TerenskeLokacijeNamjenskiObjektiAggregate ToNamjenskiObjektiAggregateDto(this AkcijeSkole.Domain.Models.TerenskaLokacija terenskaLokacija)
        => new TerenskeLokacijeNamjenskiObjektiAggregate()
        {
            IdTerenskaLokacija = terenskaLokacija.Id,
            NazivTerenskaLokacija = terenskaLokacija.NazivTerenskaLokacija,
            Slika = terenskaLokacija.Slika,
            ImaSanitarniCvor = terenskaLokacija.ImaSanitarniCvor,
            MjestoPbr = terenskaLokacija.MjestoPbr,
            Opis = terenskaLokacija.Opis,
            CvrstiNamjenskiObjekti = terenskaLokacija.CvrstiNamjenskiObjekti == null ? new List<CvrstiNamjenskiObjekti>() : terenskaLokacija.CvrstiNamjenskiObjekti.Select(no => no.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.TerenskaLokacija ToDomain(this TerenskeLokacijeNamjenskiObjektiAggregate terenskaLokacijaNamjenskiObjekt)
        => new AkcijeSkole.Domain.Models.TerenskaLokacija(
            terenskaLokacijaNamjenskiObjekt.IdTerenskaLokacija,
            terenskaLokacijaNamjenskiObjekt.NazivTerenskaLokacija,
            terenskaLokacijaNamjenskiObjekt.Slika,
            terenskaLokacijaNamjenskiObjekt.ImaSanitarniCvor,
            terenskaLokacijaNamjenskiObjekt.MjestoPbr,
            terenskaLokacijaNamjenskiObjekt.Opis,
            terenskaLokacijaNamjenskiObjekt.CvrstiNamjenskiObjekti.Select(ToDomain)
            );

    public static TerenskeLokacijeObjektiZaObitavanjeAggregate ToObjektiZaObitavanjeAggregateDto(this AkcijeSkole.Domain.Models.TerenskaLokacija terenskaLokacija)
        => new TerenskeLokacijeObjektiZaObitavanjeAggregate()
        {
            IdTerenskaLokacija = terenskaLokacija.Id,
            NazivTerenskaLokacija = terenskaLokacija.NazivTerenskaLokacija,
            Slika = terenskaLokacija.Slika,
            ImaSanitarniCvor = terenskaLokacija.ImaSanitarniCvor,
            MjestoPbr = terenskaLokacija.MjestoPbr,
            Opis = terenskaLokacija.Opis,
            CvrstiObjektiZaObitavanje = terenskaLokacija.CvrstiObjektiZaObitavanje == null ? new List<CvrstiObjektiZaObitavanje>() : terenskaLokacija.CvrstiObjektiZaObitavanje.Select(ozo => ozo.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.TerenskaLokacija ToDomain(this TerenskeLokacijeObjektiZaObitavanjeAggregate terenskaLokacijaObjektZaObitavanje)
        => new AkcijeSkole.Domain.Models.TerenskaLokacija(
            terenskaLokacijaObjektZaObitavanje.IdTerenskaLokacija,
            terenskaLokacijaObjektZaObitavanje.NazivTerenskaLokacija,
            terenskaLokacijaObjektZaObitavanje.Slika,
            terenskaLokacijaObjektZaObitavanje.ImaSanitarniCvor,
            terenskaLokacijaObjektZaObitavanje.MjestoPbr,
            terenskaLokacijaObjektZaObitavanje.Opis,
            terenskaLokacijaObjektZaObitavanje.CvrstiObjektiZaObitavanje.Select(ToDomain)
            );

    public static TerenskeLokacijeLogoristaAggregate ToLogoristaAggregateDto(this AkcijeSkole.Domain.Models.TerenskaLokacija terenskaLokacija)
        => new TerenskeLokacijeLogoristaAggregate()
        {
            IdTerenskaLokacija = terenskaLokacija.Id,
            NazivTerenskaLokacija = terenskaLokacija.NazivTerenskaLokacija,
            Slika = terenskaLokacija.Slika,
            ImaSanitarniCvor = terenskaLokacija.ImaSanitarniCvor,
            MjestoPbr = terenskaLokacija.MjestoPbr,
            Opis = terenskaLokacija.Opis,
            Logorista = terenskaLokacija.Logorista == null ? new List<Logorista>() : terenskaLokacija.Logorista.Select(l => l.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.TerenskaLokacija ToDomain(this TerenskeLokacijeLogoristaAggregate terenskeLokacijeLogoriste)
        => new AkcijeSkole.Domain.Models.TerenskaLokacija(
            terenskeLokacijeLogoriste.IdTerenskaLokacija,
            terenskeLokacijeLogoriste.NazivTerenskaLokacija,
            terenskeLokacijeLogoriste.Slika,
            terenskeLokacijeLogoriste.ImaSanitarniCvor,
            terenskeLokacijeLogoriste.MjestoPbr,
            terenskeLokacijeLogoriste.Opis,
            terenskeLokacijeLogoriste.Logorista.Select(ToDomain)
            );

    public static TerenskeLokacijePrivremeniObjektiAggregate ToPrivremeniObjektiAggregateDto(this AkcijeSkole.Domain.Models.TerenskaLokacija terenskaLokacija)
        => new TerenskeLokacijePrivremeniObjektiAggregate()
        {
            IdTerenskaLokacija = terenskaLokacija.Id,
            NazivTerenskaLokacija = terenskaLokacija.NazivTerenskaLokacija,
            Slika = terenskaLokacija.Slika,
            ImaSanitarniCvor = terenskaLokacija.ImaSanitarniCvor,
            MjestoPbr = terenskaLokacija.MjestoPbr,
            Opis = terenskaLokacija.Opis,
            PrivremeniObjekti = terenskaLokacija.PrivremeniObjekti == null ? new List<PrivremeniObjekti>() : terenskaLokacija.PrivremeniObjekti.Select(po => po.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.TerenskaLokacija ToDomain(this TerenskeLokacijePrivremeniObjektiAggregate terenskeLokacijePrivremeniObjekti)
        => new AkcijeSkole.Domain.Models.TerenskaLokacija(
            terenskeLokacijePrivremeniObjekti.IdTerenskaLokacija,
            terenskeLokacijePrivremeniObjekti.NazivTerenskaLokacija,
            terenskeLokacijePrivremeniObjekti.Slika,
            terenskeLokacijePrivremeniObjekti.ImaSanitarniCvor,
            terenskeLokacijePrivremeniObjekti.MjestoPbr,
            terenskeLokacijePrivremeniObjekti.Opis,
            terenskeLokacijePrivremeniObjekti.PrivremeniObjekti.Select(ToDomain)
            );
}