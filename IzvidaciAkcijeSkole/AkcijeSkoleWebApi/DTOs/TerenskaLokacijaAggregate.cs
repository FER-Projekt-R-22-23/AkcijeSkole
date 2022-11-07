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
