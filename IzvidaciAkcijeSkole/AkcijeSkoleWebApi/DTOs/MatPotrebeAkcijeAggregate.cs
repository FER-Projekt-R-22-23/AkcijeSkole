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
    public IEnumerable<Akcije> Akcije { get; set; } = Enumerable.Empty<Akcije>();
}

public static partial class DtoMapping
{
    public static MatPotrebeAkcijeAggregate ToAggregateDto(this MaterijalnePotrebe potreba)
        => new MatPotrebeAkcijeAggregate()
        {
            IdMaterijalnePotrebe = potreba.IdMaterijalnePotrebe,
            Naziv = potreba.Naziv,
            Organizator = potreba.Organizator,
            Davatelj = potreba.Davatelj,
            Zadovoljeno = potreba.Zadovoljeno,
            Akcije = potreba.Akcije == null
                            ? new List<MaterijalnePotrebe>()
                            : potreba.Akcije.Select(pr => pr.ToDto()).ToList()
        };

    public static MaterijalnePotrebe ToDbModel(MatPotrebeAkcijeAggregate potreba)
        => new MaterijalnePotrebe()
        {
            IdMaterijalnePotrebe = potreba.IdMaterijalnePotrebe,
            Naziv = potreba.Naziv,
            Organizator = potreba.Organizator,
            Davatelj = potreba.Davatelj,
            Zadovoljeno = potreba.Zadovoljeno,
            Akcije = potreba.Akcije == null
                            ? new List<MaterijalnePotrebe>()
                            : potreba.Akcije.Select(pr => pr.ToDto()).ToList()
        };
}

