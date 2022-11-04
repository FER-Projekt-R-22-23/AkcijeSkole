using AkcijeSkole.Domain.Models;
using DbModels = AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
namespace AkcijeSkole.Repositories.SqlServer;
public static class Mapping
{
    public static Skola ToDomain(this DbModels.Skole skola)
        => new Skola(skola.IdSkole, skola.NazivSkole, skola.MjestoPbr, skola.Organizator, skola.KontaktOsoba);

    public static DbModels.Skole ToDbModel(this Skola skola)
    {
        return new DbModels.Skole()
        {
            IdSkole = skola.Id,
            NazivSkole = skola.NazivSkole,
            MjestoPbr = skola.MjestoPbr,
            Organizator = skola.Organizator,
            KontaktOsoba = skola.KontaktOsoba,

        };
    }
}