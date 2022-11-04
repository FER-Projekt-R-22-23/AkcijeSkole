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
    public IEnumerable<Akcija> Akcije { get; set; } = Enumerable.Empty<Akcija>();
}
public class MjestoAktivnostiAggregate
{
    public int pbrMjesto { get; set; }
    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Naziv { get; set; }
    public IEnumerable<Aktivnost> Aktivnosti { get; set; } = Enumerable.Empty<Aktivnosti>();
}
public class MjestoEdukacijeAggregate
{
    public int pbrMjesto { get; set; }
    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Naziv { get; set; }
    public IEnumerable<Edukacija> Edukacije { get; set; } = Enumerable.Empty<Edukacija>();
}
public class MjestoSkoleAggregate
{

    public int pbrMjesto { get; set; }
    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Naziv { get; set; }
    public IEnumerable<Skola> Skole { get; set; } = Enumerable.Empty<Skola>();
}
public class MjestoTerLokacijeAggregate
{
    public int pbrMjesto { get; set; }
    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Naziv { get; set; }
    public IEnumerable<TerenskaLokacija> TerenskeLokacije { get; set; } = Enumerable.Empty<TerenskaLokacija>();
}

public static partial class DtoMapping
{
    public static MjestoAkcijeAggregate ToAkcijeAggregateDto(this AkcijeSkole.Domain.Models.Mjesto mjesto)
        => new MjestoAkcijeAggregate()
        {
            pbrMjesto = mjesto.Id,
            Naziv = mjesto.NazivMjesta,
            Akcije = mjesto.AkcijaAssignments == null
                            ? new List<AkcijaAssignment>()
                            : mjesto.AkcijaAssignments.Select(ra => ra.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.Mjesto ToDbModelWithAkcije(MjestoAkcijeAggregate mjesto)
        => new AkcijeSkole.Domain.Models.Mjesto(
                mjesto.pbrMjesto,
                mjesto.Naziv,
                mjesto.Akcije.Select(ToDomain);

    public static MjestoAktivnostiAggregate ToAktivostiAggregateDto(this AkcijeSkole.Domain.Models.Mjesto mjesto)
        => new MjestoAktivnostiAggregate()
        {
            pbrMjesto = mjesto.Id,
            Naziv = mjesto.NazivMjesta,
            Aktivnosti = mjesto.AktivnostAssignments == null
            ? new List<Aktivnost>()
                            : mjesto.AktivnostAssignments.Select(ra => ra.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.Mjesto ToDbModelWithAktivnosti(MjestoAktivnostiAggregate mjesto)
        => new AkcijeSkole.Domain.Models.Mjesto(
            mjesto.pbrMjesto,
            mjesto.Naziv,
            mjesto.Aktivnosti.Select(ToDomain);


    public static MjestoEdukacijeAggregate ToEdukacijaAggregateDto(this AkcijeSkole.Domain.Models.Mjesto mjesto)
        => new MjestoEdukacijeAggregate()
        {
            pbrMjesto = mjesto.Id,
            Naziv = mjesto.NazivMjesta,
            Edukacije = mjesto.EdukacijaAssignments == null
            ? new List<Edukacija>()
                            : mjesto.EdukacijaAssignments.Select(pr => pr.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.Mjesto ToDbModelWithEdukacije(MjestoEdukacijeAggregate mjesto)
        => new AkcijeSkole.Domain.Models.Mjesto(
            mjesto.pbrMjesto,
            mjesto.Naziv,
            mjesto.Edukacije.Select(ToDomain);

    public static MjestoSkoleAggregate ToSkolaAggregateDto(this AkcijeSkole.Domain.Models.Mjesto mjesto)
        => new MjestoSkoleAggregate()
        {
            pbrMjesto = mjesto.Id,
            Naziv = mjesto.NazivMjesta,
            Skole = mjesto.SkolaAssignments == null
            ? new List<Skola>()
                            : mjesto.SkolaAssignments.Select(pr => pr.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.Mjesto ToDbModelWithSkole(MjestoSkoleAggregate mjesto)
        => new AkcijeSkole.Domain.Models.Mjesto(
            mjesto.pbrMjesto,
            mjesto.Naziv,
            mjesto.Skole.Select(ToDomain);

    public static MjestoTerLokacijeAggregate ToTerLokacijaAggregateDto(this AkcijeSkole.Domain.Models.Mjesto mjesto)
        => new MjestoTerLokacijeAggregate()
        {
            pbrMjesto = mjesto.Id,
            Naziv = mjesto.NazivMjesta,
            TerenskeLokacije = mjesto.TerenskaLokacijaAssignments == null
            ? new List<TerenskaLokacija>()
                            : mjesto.TerenskaLokacijaAssignments.Select(pr => pr.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.Mjesto ToDbModelWithTerLokacije(MjestoTerLokacijeAggregate mjesto)
        => new AkcijeSkole.Domain.Models.Mjesto(
            mjesto.pbrMjesto,
            mjesto.Naziv,
            mjesto.TerenskeLokacije.Select(ToDomain);
}

