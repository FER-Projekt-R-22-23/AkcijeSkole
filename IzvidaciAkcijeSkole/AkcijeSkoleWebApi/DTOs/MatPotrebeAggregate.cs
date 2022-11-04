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
    public IEnumerable<AkcijeSkole.Domain.Models.AkcijaAssignment> AkcijaAssignment { get; set; } = Enumerable.Empty<AkcijeSkole.Domain.Models.AkcijaAssignment>();
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
    public IEnumerable<AkcijeSkole.Domain.Models.SkolaAssignment> SkolaAssignment { get; set; } = Enumerable.Empty<AkcijeSkole.Domain.Models.SkolaAssignment>();
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
    public IEnumerable<AkcijeSkole.Domain.Models.TerenskaLokacijaAssignment> TerenskaLokacijaAssignment { get; set; } = Enumerable.Empty<AkcijeSkole.Domain.Models.TerenskaLokacijaAssignment>();
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
            AkcijaAssignment = potreba.AkcijaAssignments == null
                            ? new List<AkcijeSkole.Domain.Models.AkcijaAssignment>()
                            : potreba.AkcijaAssignments.Select(pr => pr.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.MaterijalnaPotreba ToDomainWithAkcije(MatPotrebeAkcijeAggregate potreba)
        => new AkcijeSkole.Domain.Models.MaterijalnaPotreba(
            potreba.IdMaterijalnePotrebe,
            potreba.Naziv,
            potreba.Organizator,
            potreba.Davatelj,
            potreba.Zadovoljeno,
            potreba.AkcijaAssignment == null
            ? new List<AkcijeSkole.Domain.Models.AkcijaAssignment>() 
            : potreba.AkcijaAssignment.Select(ToDomain)
            );

    public static MatPotrebeSkoleAggregate ToSkoleAggregateDto(this AkcijeSkole.Domain.Models.MaterijalnaPotreba potreba)
        => new MatPotrebeSkoleAggregate()
        {
            IdMaterijalnePotrebe = potreba.Id,
            Naziv = potreba.Naziv,
            Organizator = potreba.Organizator,
            Davatelj = potreba.Davatelj,
            Zadovoljeno = potreba.Zadovoljeno,
            SkolaAssignment = potreba.SkolaAssignments == null
                            ? new List<AkcijeSkole.Domain.Models.SkolaAssignment>()
                            : potreba.SkolaAssignments.Select(pr => pr.ToDto()).ToList()
        };

    public static MaterijalnePotrebe ToDomainlWithSkole(AkcijeSkole.Domain.Models.MaterijalnaPotreba potreba)
        => new AkcijeSkole.Domain.Models.MaterijalnaPotreba(

            potreba.Id,
            potreba.Naziv,
            potreba.Organizator,
            potreba.Davatelj,
            potreba.Zadovoljeno,
            potreba.SkolaAssignments == null
                            ? new List<Skole>()
                            : potreba.SkolaAssignments.Select(ToDomain)
        );


    public static MatPotrebeTerLokacijeAggregate ToTerLokacijeAggregateDto(this AkcijeSkole.Domain.Models.MaterijalnaPotreba potreba)
        => new MatPotrebeTerLokacijeAggregate()
        {
            IdMaterijalnePotrebe = potreba.Id,
            Naziv = potreba.Naziv,
            Organizator = potreba.Organizator,
            Davatelj = potreba.Davatelj,
            Zadovoljeno = potreba.Zadovoljeno,
            TerenskaLokacijaAssignment = potreba.TerenskaLokacijaAssignments == null
                            ? new List<AkcijeSkole.Domain.Models.TerenskaLokacijaAssignment>()
                            : potreba.TerenskaLokacijaAssignments.Select(pr => pr.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.MaterijalnaPotreba ToDomainlWithTerLokacije(MatPotrebeTerLokacijeAggregate potreba)
        => new AkcijeSkole.Domain.Models.MaterijalnaPotreba(
            potreba.IdMaterijalnePotrebe,
            potreba.Naziv,
            potreba.Organizator,
            potreba.Davatelj,
            potreba.Zadovoljeno,
            potreba.TerenskaLokacijaAssignment == null
                            ? new List<AkcijeSkole.Domain.Models.TerenskaLokacijaAssignment>()
                            : potreba.TerenskaLokacijaAssignment.Select(ToDomain)
            );
}

