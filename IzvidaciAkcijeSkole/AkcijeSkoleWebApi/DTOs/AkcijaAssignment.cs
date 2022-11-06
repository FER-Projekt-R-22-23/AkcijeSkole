using System.ComponentModel.DataAnnotations;
using System.Data;
using AkcijeSkole.Domain;

namespace AkcijeSkoleWebApi.DTOs;
    public class AkcijaAssignment
    {

    [Required(ErrorMessage = "Akcija to assign must be provided")]
    public Akcija Akcija { get; set; }
}

public static partial class DtoMapping
{
    public static AkcijaAssignment ToDto(this AkcijeSkole.Domain.Models.AkcijaAssignment akcijaAssignment)
        => new AkcijaAssignment()
        {
            Akcija = akcijaAssignment.Akcija.ToDto()
        };

    public static AkcijeSkole.Domain.Models.AkcijaAssignment ToDomain(this AkcijaAssignment akcijaAssignment, int personId)
        => new AkcijeSkole.Domain.Models.AkcijaAssignment(
            akcijaAssignment.Akcija.ToDomain()
            );
}

