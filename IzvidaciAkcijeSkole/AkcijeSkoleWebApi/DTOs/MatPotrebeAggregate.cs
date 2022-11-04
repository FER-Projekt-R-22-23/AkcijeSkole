using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
    public static MatPotrebeAkcijeAggregate ToAkcijeAggregateDto(this MaterijalnePotrebe potreba)
        => new MatPotrebeAkcijeAggregate()
        {
            IdMaterijalnePotrebe = potreba.IdMaterijalnePotrebe,
            Naziv = potreba.Naziv,
            Organizator = potreba.Organizator,
            Davatelj = potreba.Davatelj,
            Zadovoljeno = potreba.Zadovoljeno,
            Akcije = potreba.Akcije == null
                            ? new List<Akcija>()
                            : potreba.Akcije.Select(pr => pr.ToDto()).ToList()
        };

    public static MaterijalnePotrebe ToDbModelWithAkcije(MatPotrebeAkcijeAggregate potreba)
        => new MaterijalnePotrebe()
        {
            IdMaterijalnePotrebe = potreba.IdMaterijalnePotrebe,
            Naziv = potreba.Naziv,
            Organizator = potreba.Organizator,
            Davatelj = potreba.Davatelj,
            Zadovoljeno = potreba.Zadovoljeno,
            Akcije = potreba.Akcije == null
                            ? new List<Akcije>()
                            : potreba.Akcije.Select(pr => pr.ToDbModel()).ToList()
        };

    public static MatPotrebeSkoleAggregate ToSkoleAggregateDto(this MaterijalnePotrebe potreba)
        => new MatPotrebeSkoleAggregate()
        {
            IdMaterijalnePotrebe = potreba.IdMaterijalnePotrebe,
            Naziv = potreba.Naziv,
            Organizator = potreba.Organizator,
            Davatelj = potreba.Davatelj,
            Zadovoljeno = potreba.Zadovoljeno,
            Skole = potreba.Skole == null
                            ? new List<Skola>()
                            : potreba.Skole.Select(pr => pr.ToDto()).ToList()
        };

    public static MaterijalnePotrebe ToDbModelWithSkole(MatPotrebeSkoleAggregate potreba)
        => new MaterijalnePotrebe()
        {
            IdMaterijalnePotrebe = potreba.IdMaterijalnePotrebe,
            Naziv = potreba.Naziv,
            Organizator = potreba.Organizator,
            Davatelj = potreba.Davatelj,
            Zadovoljeno = potreba.Zadovoljeno,
            Skole = potreba.Skole == null
                            ? new List<Skole>()
                            : potreba.Skole.Select(pr => pr.toDbModel()).ToList()
        };


    public static MatPotrebeTerLokacijeAggregate ToTerLokacijeAggregateDto(this MaterijalnePotrebe potreba)
        => new MatPotrebeTerLokacijeAggregate()
        {
            IdMaterijalnePotrebe = potreba.IdMaterijalnePotrebe,
            Naziv = potreba.Naziv,
            Organizator = potreba.Organizator,
            Davatelj = potreba.Davatelj,
            Zadovoljeno = potreba.Zadovoljeno,
            TerenskeLokacije = potreba.TerenskeLokacije == null
                            ? new List<TerenskaLokacija>()
                            : potreba.TerenskeLokacije.Select(pr => pr.ToDto()).ToList()
        };

    public static MaterijalnePotrebe ToDbModelWithTerLokacije(MatPotrebeTerLokacijeAggregate potreba)
        => new MaterijalnePotrebe()
        {
            IdMaterijalnePotrebe = potreba.IdMaterijalnePotrebe,
            Naziv = potreba.Naziv,
            Organizator = potreba.Organizator,
            Davatelj = potreba.Davatelj,
            Zadovoljeno = potreba.Zadovoljeno,
            TerenskeLokacije = potreba.TerenskeLokacije == null
                            ? new List<TerenskeLokacije>()
                            : potreba.TerenskeLokacije.Select(pr => pr.toDbModel()).ToList()
        };
}

