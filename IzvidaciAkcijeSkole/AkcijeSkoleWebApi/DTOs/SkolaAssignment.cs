/*
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using System.ComponentModel.DataAnnotations;
using System.Data;
using DomainModels = AkcijeSkole.Domain.Models;
namespace AkcijeSkole.DTOs;

public class SkolaAssignment
{
    [Required(ErrorMessage = "Skola to assign must be provided")]
    public Skola Skola { get; set; }
}


public static partial class DtoMapping
{
    public static SkolaAssignment ToDto(this DomainModels.SkolaAssignment skolaAssignment)
        => new SkolaAssignment()
        {
            Skola = skolaAssignment.Skola.ToDto()
        };

    public static DomainModels.SkolaAssignment ToDomain(this SkolaAssignment skolaAssignment, int id)
        => new DomainModels.SkolaAssignment(
            skolaAssignment.Skola.ToDomain()
            );
}
*/