using System;
using System.ComponentModel.DataAnnotations;
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using Microsoft.EntityFrameworkCore;

namespace AkcijeSkoleWebApi.DTOs;
public class MaterijalnaPotreba
{
	

    public int IdMaterijalnaPotreba { get; set; }
    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Naziv { get; set; }
    public int Organizator { get; set; }
    public int Davatelj { get; set; }
    public bool Zadovoljeno { get; set; }

}

public static partial class DtoMapping
{
    public static MaterijalnaPotreba ToDto(this AkcijeSkole.DataAccess.SqlServer.Data.DbModels.MaterijalnePotrebe materijalnaPotreba)
        => new MaterijalnaPotreba()
        {
            IdMaterijalnaPotreba = materijalnaPotreba.IdMaterijalnePotrebe,
            Naziv = materijalnaPotreba.Naziv,
            Organizator = materijalnaPotreba.Organizator,
            Davatelj = materijalnaPotreba.Davatelj,
            Zadovoljeno = materijalnaPotreba.Zadovoljeno

        };

    public static AkcijeSkole.DataAccess.SqlServer.Data.DbModels.MaterijalnePotrebe ToDbModel(this MaterijalnaPotreba materijalnaPotreba)
        => new AkcijeSkole.DataAccess.SqlServer.Data.DbModels.MaterijalnePotrebe()
        {
            Naziv = materijalnaPotreba.Naziv,
            Organizator = materijalnaPotreba.Organizator,
            Davatelj = materijalnaPotreba.Davatelj,
            Zadovoljeno = materijalnaPotreba.Zadovoljeno
        };
}
