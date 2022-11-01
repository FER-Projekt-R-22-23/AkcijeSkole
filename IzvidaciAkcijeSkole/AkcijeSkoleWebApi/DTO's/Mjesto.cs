using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace AkcijeSkoleWebApi.DTO_s;
public class Mjesto
{
    public int PbrMjesta { get; set; }

    [Required(ErrorMessage = "Naziv mjesta ne smije biti null.")]
    [StringLength(50, ErrorMessage = "Naziv mjesta ne smije biti null.")]
    [Unicode(false)]
    public string NazivMjesta { get; set; } = string.Empty;

}

public static partial class DtoMapping
{
    public static Mjesto ToDto(this AkcijeSkole.DataAccess.SqlServer.Data.DbModels.Mjesta mjesto)
        => new Mjesto()
        {
            PbrMjesta = mjesto.PbrMjesta,
            NazivMjesta = mjesto.NazivMjesta
        };

    public static AkcijeSkole.DataAccess.SqlServer.Data.DbModels.Mjesta ToDbModel(this Mjesto mjesto)
        => new AkcijeSkole.DataAccess.SqlServer.Data.DbModels.Mjesta()
        {
            PbrMjesta = mjesto.PbrMjesta,
            NazivMjesta = mjesto.NazivMjesta
        };
}

