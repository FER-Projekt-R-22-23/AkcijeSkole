
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using AkcijeSkole.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace AkcijeSkoleWebApi.DTOs;
public class MjestoAkcijeAggregate
{
    public int pbrMjesto { get; set; }
    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Naziv { get; set; }
    public IEnumerable<DTOs.Akcija> AkcijaAssignments { get; set; } = Enumerable.Empty<DTOs.Akcija>();
}
public class MjestoAktivnostiAggregate
{
    public int pbrMjesto { get; set; }
    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Naziv { get; set; }
    public IEnumerable<DTOs.Aktivnost> AktivnostAssignments { get; set; } = Enumerable.Empty<DTOs.Aktivnost>();
}
public class MjestoEdukacijeAggregate
{
    public int pbrMjesto { get; set; }
    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Naziv { get; set; }
    public IEnumerable<DTOs.Edukacija> EdukacijaAssignments { get; set; } = Enumerable.Empty<DTOs.Edukacija>();
}
public class MjestoSkoleAggregate
{

    public int pbrMjesto { get; set; }
    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Naziv { get; set; }
    public IEnumerable<DTOs.Skola> SkolaAssignments { get; set; } = Enumerable.Empty<DTOs.Skola>();
}
public class MjestoTerLokacijeAggregate
{
    public int pbrMjesto { get; set; }
    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Naziv { get; set; }
    public IEnumerable<DTOs.TerenskaLokacija> TerenskaLokacijaAssignments { get; set; } = Enumerable.Empty<DTOs.TerenskaLokacija>();
}

public static partial class DtoMapping
{
    public static MjestoAkcijeAggregate ToAkcijeAggregateDto(this AkcijeSkole.Domain.Models.Mjesto mjesto)
        => new MjestoAkcijeAggregate()
        {
            pbrMjesto = mjesto.Id,
            Naziv = mjesto.NazivMjesta,
            AkcijaAssignments = mjesto.Akcije == null
                            ? new List<DTOs.Akcija>()
                            : mjesto.Akcije.Select(ra => ra.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.Mjesto ToDomainWithAkcije(MjestoAkcijeAggregate mjesto)
        => new AkcijeSkole.Domain.Models.Mjesto(
                mjesto.pbrMjesto,
                mjesto.Naziv,
                mjesto.AkcijaAssignments.Select(ToDomain));

    public static MjestoAktivnostiAggregate ToAktivostiAggregateDto(this AkcijeSkole.Domain.Models.Mjesto mjesto)
        => new MjestoAktivnostiAggregate()
        {
            pbrMjesto = mjesto.Id,
            Naziv = mjesto.NazivMjesta,
            AktivnostAssignments = mjesto.Aktivnosti == null
            ? new List<DTOs.Aktivnost>()
                            : mjesto.Aktivnosti.Select(ra => ra.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.Mjesto ToDomainWithAktivnosti(MjestoAktivnostiAggregate mjesto)
        => new AkcijeSkole.Domain.Models.Mjesto(
            mjesto.pbrMjesto,
            mjesto.Naziv,
            null,
            mjesto.AktivnostAssignments.Select(ToDomain));


    public static MjestoEdukacijeAggregate ToEdukacijaAggregateDto(this AkcijeSkole.Domain.Models.Mjesto mjesto)
        => new MjestoEdukacijeAggregate()
        {
            pbrMjesto = mjesto.Id,
            Naziv = mjesto.NazivMjesta,
            EdukacijaAssignments = mjesto.Edukacije == null
            ? new List<DTOs.Edukacija>()
                            : mjesto.Edukacije.Select(pr => pr.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.Mjesto ToDomainlWithEdukacije(MjestoEdukacijeAggregate mjesto)
        => new AkcijeSkole.Domain.Models.Mjesto(
            mjesto.pbrMjesto,
            mjesto.Naziv,
            null,
            null,
            mjesto.EdukacijaAssignments.Select(toDomain));

    public static MjestoSkoleAggregate ToSkolaAggregateDto(this AkcijeSkole.Domain.Models.Mjesto mjesto)
        => new MjestoSkoleAggregate()
        {
            pbrMjesto = mjesto.Id,
            Naziv = mjesto.NazivMjesta,
            SkolaAssignments = mjesto.Skole == null
            ? new List<DTOs.Skola>()
                            : mjesto.Skole.Select(pr => pr.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.Mjesto ToDomainWithSkole(MjestoSkoleAggregate mjesto)
        => new AkcijeSkole.Domain.Models.Mjesto(
            mjesto.pbrMjesto,
            mjesto.Naziv,
            null,
            null,
            null,
            mjesto.SkolaAssignments.Select(toDomain));

    public static MjestoTerLokacijeAggregate ToTerLokacijaAggregateDto(this AkcijeSkole.Domain.Models.Mjesto mjesto)
        => new MjestoTerLokacijeAggregate()
        {
            pbrMjesto = mjesto.Id,
            Naziv = mjesto.NazivMjesta,
            TerenskaLokacijaAssignments = mjesto.TerenskeLokacije == null
            ? new List<DTOs.TerenskaLokacija>()
                            : mjesto.TerenskeLokacije.Select(pr => pr.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.Mjesto ToDomainWithTerLokacije(MjestoTerLokacijeAggregate mjesto)
        => new AkcijeSkole.Domain.Models.Mjesto(
            mjesto.pbrMjesto,
            mjesto.Naziv,
            null,
            null,
            null,
            null,
            mjesto.TerenskaLokacijaAssignments.Select(ToDomain));
}

