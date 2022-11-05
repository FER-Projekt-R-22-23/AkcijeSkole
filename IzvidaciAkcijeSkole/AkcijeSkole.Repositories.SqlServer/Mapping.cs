﻿using AkcijeSkole.Domain.Models;
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



    public static PredavacNaEdukaciji ToDomain(this DbModels.Predavaci predavaci)
        => new PredavacNaEdukaciji(predavaci.IdPredavac, predavaci.ClanId);

    public static DbModels.Predavaci ToDbModel(this PredavacNaEdukaciji predavacNaEdukaciji, int edukacijaId)
        => new DbModels.Predavaci()
        {
            IdPredavac = predavacNaEdukaciji.idPredavac,
            ClanId = predavacNaEdukaciji.idClan,
            EdukacijaId = edukacijaId
        };
    public static Edukacija ToDomain(this DbModels.Edukacije edukacija)
        => new Edukacija(edukacija.IdEdukacija, edukacija.NazivEdukacija, edukacija.MjestoPbr, edukacija.OpisEdukacije, edukacija.SkolaId, edukacija.Predavaci.Select(ToDomain));

    public static DbModels.Edukacije ToDbModel(this Edukacija edukacija)
    {
        return new DbModels.Edukacije()
        {
            IdEdukacija = edukacija.Id,
            NazivEdukacija = edukacija.NazivEdukacije,
            MjestoPbr = edukacija.MjestoPbr,
            OpisEdukacije = edukacija.OpisEdukacije,
            SkolaId = edukacija.SkolaId,
            Predavaci = edukacija.PredavaciNaEdukaciji.Select(obj => obj.ToDbModel(edukacija.Id)).ToList()
        };
    }


}