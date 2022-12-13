//using Microsoft.AspNetCore.Authorization;
//using System.ComponentModel.DataAnnotations;
//using DomainModels = AkcijeSkole.Domain.Models;

//namespace AkcijeSkoleWebApi.DTOs;

//public class TerenskaLokacijaCvrstiNamjenski
//{
//    public int IdTerenskaLokacija { get; set; }

//    [Required(ErrorMessage = "Naziv terenske lokacije can't be null", AllowEmptyStrings = false)]
//    [StringLength(50, ErrorMessage = "Naziv terenske lokacije can't be longer than 50 characters")]
//    public string NazivTerenskaLokacija { get; set; } = string.Empty;
//    public byte[]? Slika { get; set; }

//    public bool ImaSanitarniCvor { get; set; }

//    public int MjestoPbr { get; set; }

//    [Required(ErrorMessage = "Opis can't be null", AllowEmptyStrings = false)]
//    public string Opis { get; set; } = string.Empty;

//    public DTOs.CvrstiNamjenskiObjekt CvrstiNamjenskiObjekt { get; set; }
//}

//public class TerenskaLokacijaCvrstiObitavanje
//{
//    public int IdTerenskaLokacija { get; set; }

//    [Required(ErrorMessage = "Naziv terenske lokacije can't be null", AllowEmptyStrings = false)]
//    [StringLength(50, ErrorMessage = "Naziv terenske lokacije can't be longer than 50 characters")]
//    public string NazivTerenskaLokacija { get; set; } = string.Empty;
//    public byte[]? Slika { get; set; }

//    public bool ImaSanitarniCvor { get; set; }

//    public int MjestoPbr { get; set; }

//    [Required(ErrorMessage = "Opis can't be null", AllowEmptyStrings = false)]
//    public string Opis { get; set; } = string.Empty;

//    public DTOs.CvrstiObjektZaObitavanje CvrstiObjektZaObitavanje { get; set; }
//}

//public class TerenskaLokacijaLogoriste
//{
//    public int IdTerenskaLokacija { get; set; }

//    [Required(ErrorMessage = "Naziv terenske lokacije can't be null", AllowEmptyStrings = false)]
//    [StringLength(50, ErrorMessage = "Naziv terenske lokacije can't be longer than 50 characters")]
//    public string NazivTerenskaLokacija { get; set; } = string.Empty;
//    public byte[]? Slika { get; set; }

//    public bool ImaSanitarniCvor { get; set; }

//    public int MjestoPbr { get; set; }

//    [Required(ErrorMessage = "Opis can't be null", AllowEmptyStrings = false)]
//    public string Opis { get; set; } = string.Empty;

//    public DTOs.Logoriste Logoriste { get; set; }
//}

//public class TerenskaLokacijaPrivremeniObjekt
//{
//    public int IdTerenskaLokacija { get; set; }

//    [Required(ErrorMessage = "Naziv terenske lokacije can't be null", AllowEmptyStrings = false)]
//    [StringLength(50, ErrorMessage = "Naziv terenske lokacije can't be longer than 50 characters")]
//    public string NazivTerenskaLokacija { get; set; } = string.Empty;
//    public byte[]? Slika { get; set; }

//    public bool ImaSanitarniCvor { get; set; }

//    public int MjestoPbr { get; set; }

//    [Required(ErrorMessage = "Opis can't be null", AllowEmptyStrings = false)]
//    public string Opis { get; set; } = string.Empty;

//    public DTOs.PrivremeniObjekt PrivremeniObjekt { get; set; }
//}

//public class TerenskaLokacijaObjekti
//{
//    public int IdTerenskaLokacija { get; set; }

//    [Required(ErrorMessage = "Naziv terenske lokacije can't be null", AllowEmptyStrings = false)]
//    [StringLength(50, ErrorMessage = "Naziv terenske lokacije can't be longer than 50 characters")]
//    public string NazivTerenskaLokacija { get; set; } = string.Empty;
//    public byte[]? Slika { get; set; }

//    public bool ImaSanitarniCvor { get; set; }

//    public int MjestoPbr { get; set; }

//    [Required(ErrorMessage = "Opis can't be null", AllowEmptyStrings = false)]
//    public string Opis { get; set; } = string.Empty;

//    public DTOs.CvrstiNamjenskiObjekt CvrstiNamjenskiObjekt { get; set; }

//    public DTOs.CvrstiObjektZaObitavanje CvrstiObjektZaObitavanje { get; set; }

//    public DTOs.Logoriste Logoriste { get; set; }

//    public DTOs.PrivremeniObjekt PrivremeniObjekt { get; set; }
//}

//public static partial class DtoMapping
//{
//    public static TerenskaLokacijaCvrstiNamjenski ToCvrstiNamjenskiDto(this DomainModels.TerenskaLokacija terenskaLokacija)
//        => new TerenskaLokacijaCvrstiNamjenski()
//        {
//            IdTerenskaLokacija = terenskaLokacija.Id,
//            NazivTerenskaLokacija = terenskaLokacija.NazivTerenskaLokacija,
//            Slika = terenskaLokacija.Slika,
//            ImaSanitarniCvor = terenskaLokacija.ImaSanitarniCvor,
//            MjestoPbr = terenskaLokacija.MjestoPbr,
//            Opis = terenskaLokacija.Opis,
//            //CvrstiNamjenskiObjekt = terenskaLokacija.CvrstiNamjenskiObjekt.ToDto()
//        };

//    public static DomainModels.TerenskaLokacija ToDomainCvrstiNamjenski(this TerenskaLokacijaCvrstiNamjenski terenskaLokacijaCvrstiNamjenski)
//        => new DomainModels.TerenskaLokacija(
//            terenskaLokacijaCvrstiNamjenski.IdTerenskaLokacija,
//            terenskaLokacijaCvrstiNamjenski.NazivTerenskaLokacija,
//            terenskaLokacijaCvrstiNamjenski.Slika,
//            terenskaLokacijaCvrstiNamjenski.ImaSanitarniCvor,
//            terenskaLokacijaCvrstiNamjenski.MjestoPbr,
//            terenskaLokacijaCvrstiNamjenski.Opis,
//            terenskaLokacijaCvrstiNamjenski.CvrstiNamjenskiObjekt.ToDomain()
//            );

//    public static TerenskaLokacijaCvrstiObitavanje ToCvrstiObitavanjeDto(this DomainModels.TerenskaLokacija terenskaLokacija)
//        => new TerenskaLokacijaCvrstiObitavanje()
//        {
//            IdTerenskaLokacija = terenskaLokacija.Id,
//            NazivTerenskaLokacija = terenskaLokacija.NazivTerenskaLokacija,
//            Slika = terenskaLokacija.Slika,
//            ImaSanitarniCvor = terenskaLokacija.ImaSanitarniCvor,
//            MjestoPbr = terenskaLokacija.MjestoPbr,
//            Opis = terenskaLokacija.Opis,
//            //CvrstiObjektZaObitavanje = terenskaLokacija.CvrstiObjektZaObitavanje.ToDto()
//        };

//    public static DomainModels.TerenskaLokacija ToDomainCvrstiObitavanje(this TerenskaLokacijaCvrstiObitavanje terenskaLokacijaCvrstiObitavanje)
//        => new DomainModels.TerenskaLokacija(
//            terenskaLokacijaCvrstiObitavanje.IdTerenskaLokacija,
//            terenskaLokacijaCvrstiObitavanje.NazivTerenskaLokacija,
//            terenskaLokacijaCvrstiObitavanje.Slika,
//            terenskaLokacijaCvrstiObitavanje.ImaSanitarniCvor,
//            terenskaLokacijaCvrstiObitavanje.MjestoPbr,
//            terenskaLokacijaCvrstiObitavanje.Opis,
//            null,
//            terenskaLokacijaCvrstiObitavanje.CvrstiObjektZaObitavanje.ToDomain()
//            );

//    public static TerenskaLokacijaLogoriste ToLogoristeDto(this DomainModels.TerenskaLokacija terenskaLokacija)
//        => new TerenskaLokacijaLogoriste()
//        {
//            IdTerenskaLokacija = terenskaLokacija.Id,
//            NazivTerenskaLokacija = terenskaLokacija.NazivTerenskaLokacija,
//            Slika = terenskaLokacija.Slika,
//            ImaSanitarniCvor = terenskaLokacija.ImaSanitarniCvor,
//            MjestoPbr = terenskaLokacija.MjestoPbr,
//            Opis = terenskaLokacija.Opis,
//            //Logoriste = terenskaLokacija.Logoriste.ToDto()
//        };

//    public static DomainModels.TerenskaLokacija ToDomainLogoriste(this TerenskaLokacijaLogoriste terenskaLokacijaLogoriste)
//        => new DomainModels.TerenskaLokacija(
//            terenskaLokacijaLogoriste.IdTerenskaLokacija,
//            terenskaLokacijaLogoriste.NazivTerenskaLokacija,
//            terenskaLokacijaLogoriste.Slika,
//            terenskaLokacijaLogoriste.ImaSanitarniCvor,
//            terenskaLokacijaLogoriste.MjestoPbr,
//            terenskaLokacijaLogoriste.Opis,
//            null,
//            null,
//            terenskaLokacijaLogoriste.Logoriste.ToDomain()
//            );

//    public static TerenskaLokacijaPrivremeniObjekt ToPrivremeniDto(this DomainModels.TerenskaLokacija terenskaLokacija)
//        => new TerenskaLokacijaPrivremeniObjekt()
//        {
//            IdTerenskaLokacija = terenskaLokacija.Id,
//            NazivTerenskaLokacija = terenskaLokacija.NazivTerenskaLokacija,
//            Slika = terenskaLokacija.Slika,
//            ImaSanitarniCvor = terenskaLokacija.ImaSanitarniCvor,
//            MjestoPbr = terenskaLokacija.MjestoPbr,
//            Opis = terenskaLokacija.Opis,
//           // PrivremeniObjekt = terenskaLokacija.PrivremeniObjekt.ToDto()
//        };

//    public static DomainModels.TerenskaLokacija ToDomainPrivremeni(this TerenskaLokacijaPrivremeniObjekt terenskaLokacijaPrivremeniObjekt)
//        => new DomainModels.TerenskaLokacija(
//            terenskaLokacijaPrivremeniObjekt.IdTerenskaLokacija,
//            terenskaLokacijaPrivremeniObjekt.NazivTerenskaLokacija,
//            terenskaLokacijaPrivremeniObjekt.Slika,
//            terenskaLokacijaPrivremeniObjekt.ImaSanitarniCvor,
//            terenskaLokacijaPrivremeniObjekt.MjestoPbr,
//            terenskaLokacijaPrivremeniObjekt.Opis,
//            null,
//            null,
//            null,
//            terenskaLokacijaPrivremeniObjekt.PrivremeniObjekt.ToDomain()
//            );

//    public static TerenskaLokacijaObjekti ToObjektiDto(this DomainModels.TerenskaLokacija terenskaLokacija)
//        => new TerenskaLokacijaObjekti()
//        {
//            IdTerenskaLokacija = terenskaLokacija.Id,
//            NazivTerenskaLokacija = terenskaLokacija.NazivTerenskaLokacija,
//            Slika = terenskaLokacija.Slika,
//            ImaSanitarniCvor = terenskaLokacija.ImaSanitarniCvor,
//            MjestoPbr = terenskaLokacija.MjestoPbr,
//            Opis = terenskaLokacija.Opis/*,
//            CvrstiNamjenskiObjekt = terenskaLokacija.CvrstiNamjenskiObjekt.ToDto(),
//            CvrstiObjektZaObitavanje = terenskaLokacija.CvrstiObjektZaObitavanje.ToDto(),
//            Logoriste = terenskaLokacija.Logoriste.ToDto(),
//            PrivremeniObjekt = terenskaLokacija.PrivremeniObjekt.ToDto()*/
//        };

//    public static DomainModels.TerenskaLokacija ToDomainObjekti(this TerenskaLokacijaObjekti terenskaLokacijaObjekti)
//        => new DomainModels.TerenskaLokacija(
//            terenskaLokacijaObjekti.IdTerenskaLokacija,
//            terenskaLokacijaObjekti.NazivTerenskaLokacija,
//            terenskaLokacijaObjekti.Slika,
//            terenskaLokacijaObjekti.ImaSanitarniCvor,
//            terenskaLokacijaObjekti.MjestoPbr,
//            terenskaLokacijaObjekti.Opis,
//            terenskaLokacijaObjekti.CvrstiNamjenskiObjekt.ToDomain(),
//            terenskaLokacijaObjekti.CvrstiObjektZaObitavanje.ToDomain(),
//            terenskaLokacijaObjekti.Logoriste.ToDomain(),
//            terenskaLokacijaObjekti.PrivremeniObjekt.ToDomain()
//            );
//}