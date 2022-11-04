using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using System.ComponentModel.DataAnnotations;
using System.Data;
using DomainModels = AkcijeSkole.Domain.Models;
namespace AkcijeSkole.DTOs;

public class TerenskaLokacijaAssignment
{
    [Required(ErrorMessage = "Terenska lokacija to assign must be provided")]
    public TerenskaLokacija TerenskaLokacija { get; set; }
}


public static partial class DtoMapping
{
    public static TerenskaLokacijaAssignment ToDto(this DomainModels.TerenskaLokacijaAssignment terenskaLokacijaAssignment)
        => new TerenskaLokacijaAssignment
        ()
        {
            TerenskaLokacija = terenskaLokacijaAssignment.TerenskaLokacija.ToDto()
        };

    public static DomainModels.TerenskaLokacijaAssignment ToDomain(this TerenskaLokacijaAssignment terenskaLokacijaAssignment, int id)
        => new DomainModels.TerenskaLokacijaAssignment(
            terenskaLokacijaAssignment.TerenskaLokacija.ToDomain()
            );
}
