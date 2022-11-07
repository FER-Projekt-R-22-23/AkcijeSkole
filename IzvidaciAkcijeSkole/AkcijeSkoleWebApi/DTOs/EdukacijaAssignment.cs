/*
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using AkcijeSkole.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using DomainModels = AkcijeSkole.Domain.Models;
namespace AkcijeSkole.DTOs;

public class EdukacijaAssignment
{
    [Required(ErrorMessage = "Edukacija to assign must be provided")]
    public Edukacija Edukacija { get; set; }
}


public static partial class DtoMapping
{
    public static EdukacijaAssignment ToDto(this DomainModels.EdukacijaAssignment edukacijaAssignment)
        => new EdukacijaAssignment()
        {
            Edukacija = edukacijaAssignment.Edukacija.ToDto()
        };

    public static DomainModels.EdukacijaAssignment ToDomain(this EdukacijaAssignment edukacijaAssignment, int id)
        => new DomainModels.EdukacijaAssignment(
            edukacijaAssignment.Edukacija.ToDomain()
            );
}
*/