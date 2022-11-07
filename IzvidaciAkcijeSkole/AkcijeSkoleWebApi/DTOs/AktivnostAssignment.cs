/*
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using System.ComponentModel.DataAnnotations;
using System.Data;
using DomainModels = AkcijeSkole.Domain.Models;
namespace AkcijeSkole.DTOs;

public class AktivnostAssignment
{
    [Required(ErrorMessage = "Aktivnost to assign must be provided")]
    public Aktivnost Aktivnost { get; set; }
}


public static partial class DtoMapping
{
    public static AktivnostAssignment ToDto(this DomainModels.AktivnostAssignment aktivnostAssignment)
        => new AktivnostAssignment()
        {
            Aktivnost = aktivnostAssignment.Aktivnost.ToDto()
        };

    public static DomainModels.AktivnostAssignment ToDomain(this AktivnostAssignment aktivnostAssignment, int id)
        => new DomainModels.AktivnostAssignment(
            aktivnostAssignment.Aktivnost.ToDomain()
            );
}
*/
