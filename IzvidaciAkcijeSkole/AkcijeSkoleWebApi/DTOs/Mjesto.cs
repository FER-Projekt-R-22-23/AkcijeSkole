
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using System;

namespace AkcijeSkoleWebApi.DTOs;
public class Mjesto
{
    public int PbrMjesta { get; set; }

    [Required(ErrorMessage = "Naziv mjesta ne smije biti null.")]
    [StringLength(50, ErrorMessage = "Naziv mjesta ne smije biti duzi od 50 znakova.")]
    [Unicode(false)]
    public string NazivMjesta { get; set; } = string.Empty;

}

public static partial class DtoMapping
{
    public static Mjesto ToDto(this AkcijeSkole.Domain.Models.Mjesto mjesto)
        => new Mjesto()
        {
            PbrMjesta = mjesto.Id,
            NazivMjesta = mjesto.NazivMjesta
        };

    public static AkcijeSkole.Domain.Models.Mjesto ToDomain(this Mjesto mjesto)
        => new AkcijeSkole.Domain.Models.Mjesto(
            mjesto.PbrMjesta,
             mjesto.NazivMjesta
            );
       
}


