
using domainmodels = AkcijeSkole.Domain.Models;
namespace akcijeskole.dtos;
using System.ComponentModel.DataAnnotations;

public class TerenskaLokacijaAsignment
{
    [Required(ErrorMessage = "terenska lokacija to assign must be provided")]
    public terenskalokacija terenskalokacija { get; set; }
}


public static partial class dtomapping
{
    public static terenskalokacijaassignment todto(this domainmodels.terenskalokacijaassignment terenskalokacijaassignment)
        => new terenskalokacijaassignment
        ()
        {
            terenskalokacija = terenskalokacijaassignment.terenskalokacija.todto()
        };

    public static domainmodels.terenskalokacijaassignment todomain(this terenskalokacijaassignment terenskalokacijaassignment, int id)
        => new domainmodels.terenskalokacijaassignment(
            terenskalokacijaassignment.terenskalokacija.todomain()
            );
}
