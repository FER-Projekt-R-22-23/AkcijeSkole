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
    public IEnumerable<AkcijeSkole.Domain.Models.AkcijaAssignment> AkcijaAssignments { get; set; } = Enumerable.Empty<AkcijeSkole.Domain.Models.AkcijaAssignment>();
}
public class MjestoAktivnostiAggregate
{
    public int pbrMjesto { get; set; }
    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Naziv { get; set; }
    public IEnumerable<AkcijeSkole.Domain.Models.AktivnostAssignment> AktivnostAssignments { get; set; } = Enumerable.Empty<AkcijeSkole.Domain.Models.AktivnostAssignment>();
}
public class MjestoEdukacijeAggregate
{
    public int pbrMjesto { get; set; }
    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Naziv { get; set; }
    public IEnumerable<AkcijeSkole.Domain.Models.EdukacijaAssignment> EdukacijaAssignments { get; set; } = Enumerable.Empty<AkcijeSkole.Domain.Models.EdukacijaAssignment>();
}
public class MjestoSkoleAggregate
{

    public int pbrMjesto { get; set; }
    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Naziv { get; set; }
    public IEnumerable<AkcijeSkole.Domain.Models.SkolaAssignment> SkolaAssignments { get; set; } = Enumerable.Empty<AkcijeSkole.Domain.Models.SkolaAssignment>();
}
public class MjestoTerLokacijeAggregate
{
    public int pbrMjesto { get; set; }
    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Naziv { get; set; }
    public IEnumerable<AkcijeSkole.Domain.Models.TerenskaLokacijaAssignment> TerenskaLokacijaAssignments { get; set; } = Enumerable.Empty<AkcijeSkole.Domain.Models.TerenskaLokacijaAssignment>();
}

public static partial class DtoMapping
{
    public static MjestoAkcijeAggregate ToAkcijeAggregateDto(this AkcijeSkole.Domain.Models.Mjesto mjesto)
        => new MjestoAkcijeAggregate()
        {
            pbrMjesto = mjesto.Id,
            Naziv = mjesto.NazivMjesta,
            AkcijaAssignments = mjesto.AkcijaAssignments == null
                            ? new List<AkcijeSkole.Domain.Models.AkcijaAssignment>()
                            : mjesto.AkcijaAssignments.Select(ra => ra.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.Mjesto ToDomainWithAkcije(MjestoAkcijeAggregate mjesto)
        => new AkcijeSkole.Domain.Models.Mjesto(
                mjesto.pbrMjesto,
                mjesto.Naziv,
                mjesto.AkcijaAssignments.Select(ToDomain);

    public static MjestoAktivnostiAggregate ToAktivostiAggregateDto(this AkcijeSkole.Domain.Models.Mjesto mjesto)
        => new MjestoAktivnostiAggregate()
        {
            pbrMjesto = mjesto.Id,
            Naziv = mjesto.NazivMjesta,
            AktivnostAssignments = mjesto.AktivnostAssignments == null
            ? new List<AkcijeSkole.Domain.Models.AktivnostAssignment>()
                            : mjesto.AktivnostAssignments.Select(ra => ra.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.Mjesto ToDomainWithAktivnosti(MjestoAktivnostiAggregate mjesto)
        => new AkcijeSkole.Domain.Models.Mjesto(
            mjesto.pbrMjesto,
            mjesto.Naziv,
            mjesto.AktivnostAssignments.Select(ToDomain);


    public static MjestoEdukacijeAggregate ToEdukacijaAggregateDto(this AkcijeSkole.Domain.Models.Mjesto mjesto)
        => new MjestoEdukacijeAggregate()
        {
            pbrMjesto = mjesto.Id,
            Naziv = mjesto.NazivMjesta,
            EdukacijaAssignments = mjesto.EdukacijaAssignments == null
            ? new List<AkcijeSkole.Domain.Models.EdukacijaAssignment>()
                            : mjesto.EdukacijaAssignments.Select(pr => pr.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.Mjesto ToDomainlWithEdukacije(MjestoEdukacijeAggregate mjesto)
        => new AkcijeSkole.Domain.Models.Mjesto(
            mjesto.pbrMjesto,
            mjesto.Naziv,
            mjesto.EdukacijaAssignments.Select(ToDomain);

    public static MjestoSkoleAggregate ToSkolaAggregateDto(this AkcijeSkole.Domain.Models.Mjesto mjesto)
        => new MjestoSkoleAggregate()
        {
            pbrMjesto = mjesto.Id,
            Naziv = mjesto.NazivMjesta,
            SkolaAssignments = mjesto.SkolaAssignments == null
            ? new List<AkcijeSkole.Domain.Models.SkolaAssignment>()
                            : mjesto.SkolaAssignments.Select(pr => pr.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.Mjesto ToDomainWithSkole(MjestoSkoleAggregate mjesto)
        => new AkcijeSkole.Domain.Models.Mjesto(
            mjesto.pbrMjesto,
            mjesto.Naziv,
            mjesto.SkolaAssignments.Select(ToDomain);

    public static MjestoTerLokacijeAggregate ToTerLokacijaAggregateDto(this AkcijeSkole.Domain.Models.Mjesto mjesto)
        => new MjestoTerLokacijeAggregate()
        {
            pbrMjesto = mjesto.Id,
            Naziv = mjesto.NazivMjesta,
            TerenskaLokacijaAssignments = mjesto.TerenskaLokacijaAssignments == null
            ? new List<AkcijeSkole.Domain.Models.TerenskaLokacijaAssignment>()
                            : mjesto.TerenskaLokacijaAssignments.Select(pr => pr.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.Mjesto ToDomainWithTerLokacije(MjestoTerLokacijeAggregate mjesto)
        => new AkcijeSkole.Domain.Models.Mjesto(
            mjesto.pbrMjesto,
            mjesto.Naziv,
            mjesto.TerenskaLokacijaAssignments.Select(ToDomain);
}

