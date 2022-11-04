using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AkcijeSkole.Repositories.SqlServer;

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
    public IEnumerable<Akcija> Akcije { get; set; } = Enumerable.Empty<Akcija>();
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
    public IEnumerable<Skola> Skole { get; set; } = Enumerable.Empty<Skola>();
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
    public IEnumerable<TerenskaLokacija> TerenskeLokacije { get; set; } = Enumerable.Empty<TerenskaLokacija>();
}

public static partial class DtoMapping
{
    public static MatPotrebeAkcijeAggregate ToAkcijeAggregateDto(this AkcijeSkole.Domain.Models.MaterijalnaPotreba potreba)
        => new MatPotrebeAkcijeAggregate()
        {
            IdMaterijalnePotrebe = potreba.Id,
            Naziv = potreba.Naziv,
            Organizator = potreba.Organizator,
            Davatelj = potreba.Davateljj,
            Zadovoljeno = potreba.Zadovoljeno,
            Akcije = potreba.AkcijaAssignments == null
                            ? new List<Akcija>()
                            : potreba.Akcije.Select(pr => pr.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.MaterijalnaPotreba ToDbModelWithAkcije(MatPotrebeAkcijeAggregate potreba)
        => new AkcijeSkole.Domain.Models.MaterijalnaPotreba(
            potreba.IdMaterijalnePotrebe,
            potreba.Naziv,
            potreba.Organizator,
            potreba.Davatelj,
            potreba.Zadovoljeno,
            potreba.Akcije == null
            ? new List<Akcije>() 
            : potreba.Akcije.Select(pr => pr.toDbModel).ToList()
            );

    public static MatPotrebeSkoleAggregate ToSkoleAggregateDto(this AkcijeSkole.Domain.Models.MaterijalnaPotreba potreba)
        => new MatPotrebeSkoleAggregate()
        {
            IdMaterijalnePotrebe = potreba.Id,
            Naziv = potreba.Naziv,
            Organizator = potreba.Organizator,
            Davatelj = potreba.Davateljj,
            Zadovoljeno = potreba.Zadovoljeno,
            Skole = potreba.SkolaAssignments == null
                            ? new List<Skola>()
                            : potreba.SkolaAssignments.Select(pr => pr.ToDto()).ToList()
        };

    public static MaterijalnePotrebe ToDbModelWithSkole(AkcijeSkole.Domain.Models.MaterijalnaPotreba potreba)
        => new AkcijeSkole.Domain.Models.MaterijalnaPotreba(

            potreba.Id,
            potreba.Naziv,
            potreba.Organizator,
            potreba.Davateljj,
            potreba.Zadovoljeno,
            potreba.SkolaAssignments == null
                            ? new List<Skole>()
                            : potreba.SkolaAssignments.Select(pr => pr.ToDbModel).ToList()
        );


    public static MatPotrebeTerLokacijeAggregate ToTerLokacijeAggregateDto(this AkcijeSkole.Domain.Models.MaterijalnaPotreba potreba)
        => new MatPotrebeTerLokacijeAggregate()
        {
            IdMaterijalnePotrebe = potreba.Id,
            Naziv = potreba.Naziv,
            Organizator = potreba.Organizator,
            Davatelj = potreba.Davateljj,
            Zadovoljeno = potreba.Zadovoljeno,
            TerenskeLokacije = potreba.TerenskaLokacijaAssignments == null
                            ? new List<TerenskaLokacija>()
                            : potreba.TerenskeLokacije.Select(pr => pr.ToDto()).ToList()
        };

    public static AkcijeSkole.Domain.Models.MaterijalnaPotreba ToDbModelWithTerLokacije(MatPotrebeTerLokacijeAggregate potreba)
        => new AkcijeSkole.Domain.Models.MaterijalnaPotreba(
            potreba.IdMaterijalnePotrebe,
            potreba.Naziv,
            potreba.Organizator,
            potreba.Davatelj,
            potreba.Zadovoljeno,
            potreba.TerenskeLokacije == null
                            ? new List<Skole>()
                            : potreba.TerenskeLokacije.Select(pr => pr.ToDbModel).ToList());
}

