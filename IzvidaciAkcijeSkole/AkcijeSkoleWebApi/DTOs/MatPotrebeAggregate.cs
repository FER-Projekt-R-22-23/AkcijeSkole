
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AkcijeSkole.Repositories.SqlServer;
using AkcijeSkole.Domain.Models;

namespace AkcijeSkoleWebApi.DTOs;

    public class MatPotrebeAkcijeAggregate
    {
    public int IdMaterijalnePotrebe { get; set; }
    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Naziv { get; set; }
    public int Organizator { get; set; }
    public int Davatelj { get; set; }
    public bool Zadovoljeno { get; set; }
    public IEnumerable<DTOs.Akcija> AkcijaAssignment { get; set; } = Enumerable.Empty<DTOs.Akcija>();
}

public class MatPotrebeSkoleAggregate
{
    public int IdMaterijalnePotrebe { get; set; }
    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Naziv { get; set; }
    public int Organizator { get; set; }
    public int Davatelj { get; set; }
    public bool Zadovoljeno { get; set; }
    public IEnumerable<DTOs.Skola> SkolaAssignment { get; set; } = Enumerable.Empty<DTOs.Skola>();
}

public class MatPotrebeTerLokacijeAggregate
{
    public int IdMaterijalnePotrebe { get; set; }
    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Naziv { get; set; }
    public int Organizator { get; set; }
    public int Davatelj { get; set; }
    public bool Zadovoljeno { get; set; }
    public IEnumerable<DTOs.TerenskaLokacija> TerenskaLokacijaAssignment { get; set; } = Enumerable.Empty<DTOs.TerenskaLokacija>();
}

public static partial class DtoMapping
{
    public static MatPotrebeAkcijeAggregate ToAkcijeAggregateDto(this AkcijeSkole.Domain.Models.MaterijalnaPotreba potreba)
        => new MatPotrebeAkcijeAggregate()
        {
            IdMaterijalnePotrebe = potreba.Id,
            Naziv = potreba.Naziv,
            Organizator = potreba.Organizator,
            Davatelj = potreba.Davatelj,
            Zadovoljeno = potreba.Zadovoljeno,
            AkcijaAssignment = potreba.Akcije == null
                            ? new List<DTOs.Akcija>()
                            : potreba.Akcije.Select(pr => pr.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.MaterijalnaPotreba ToDomainWithAkcije(MatPotrebeAkcijeAggregate potreba)
        => new AkcijeSkole.Domain.Models.MaterijalnaPotreba(
            potreba.IdMaterijalnePotrebe,
            potreba.Naziv,
            potreba.Organizator,
            potreba.Davatelj,
            potreba.Zadovoljeno,
            potreba.AkcijaAssignment.Select(ToDomain)
            );

    public static MatPotrebeSkoleAggregate ToSkoleAggregateDto(this AkcijeSkole.Domain.Models.MaterijalnaPotreba potreba)
        => new MatPotrebeSkoleAggregate()
        {
            IdMaterijalnePotrebe = potreba.Id,
            Naziv = potreba.Naziv,
            Organizator = potreba.Organizator,
            Davatelj = potreba.Davatelj,
            Zadovoljeno = potreba.Zadovoljeno,
            SkolaAssignment = potreba.Skole == null
                            ? new List<DTOs.Skola>()
                            : potreba.Skole.Select(pr => pr.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.MaterijalnaPotreba ToDomainlWithSkole(MatPotrebeSkoleAggregate potreba)
        => new AkcijeSkole.Domain.Models.MaterijalnaPotreba(

            potreba.IdMaterijalnePotrebe,
            potreba.Naziv,
            potreba.Organizator,
            potreba.Davatelj,
            potreba.Zadovoljeno,
            null,
            potreba.SkolaAssignment.Select(toDomain)
        );


    public static MatPotrebeTerLokacijeAggregate ToTerLokacijeAggregateDto(this AkcijeSkole.Domain.Models.MaterijalnaPotreba potreba)
        => new MatPotrebeTerLokacijeAggregate()
        {
            IdMaterijalnePotrebe = potreba.Id,
            Naziv = potreba.Naziv,
            Organizator = potreba.Organizator,
            Davatelj = potreba.Davatelj,
            Zadovoljeno = potreba.Zadovoljeno,
            TerenskaLokacijaAssignment = potreba.TerenskeLokacije == null
                            ? new List<DTOs.TerenskaLokacija>()
                            : potreba.TerenskeLokacije.Select(pr => pr.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.MaterijalnaPotreba ToDomainlWithTerLokacije(MatPotrebeTerLokacijeAggregate potreba)
        => new AkcijeSkole.Domain.Models.MaterijalnaPotreba(
            potreba.IdMaterijalnePotrebe,
            potreba.Naziv,
            potreba.Organizator,
            potreba.Davatelj,
            potreba.Zadovoljeno,
            null,
            null,
            potreba.TerenskaLokacijaAssignment.Select(DtoMapping.ToDomain)
            );
}


