/*
using akcijeskole.dataaccess.sqlserver.data.dbmodels;
using system.componentmodel.dataannotations;
using system.data;
using domainmodels = akcijeskole.domain.models;
namespace akcijeskole.dtos;

public class terenskalokacijaassignment
{
    [required(errormessage = "terenska lokacija to assign must be provided")]
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
*/