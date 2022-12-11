using AkcijeSkole.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using DbModels = AkcijeSkole.DataAccess.SqlServer.Data.DbModels;

namespace AkcijeSkole.Repositories.SqlServer;
public static class Mapping
{
    public static Mjesto ToDomain(this Mjesta mjesto)
        => new Mjesto(
            mjesto.PbrMjesta,
            mjesto.NazivMjesta,
            mjesto.Akcije.Select(ToDomainAkcija),
            mjesto.Aktivnosti.Select(ToDomainAktivnost),
            mjesto.Edukacije.Select(ToDomainEdukacija),
            mjesto.Skole.Select(ToDomainSkola),
            mjesto.TerenskeLokacije.Select(ToDomain)
    );

    public static Mjesta ToDbModel(this Mjesto mjesto)
        => new Mjesta
        {
            PbrMjesta = mjesto.Id,
            NazivMjesta = mjesto.NazivMjesta,
            Akcije = mjesto.Akcije.Select(a => a.ToDbModel()).ToList(),
            Aktivnosti = mjesto.Aktivnosti.Select(a => a.ToDbModel()).ToList(),
            Edukacije = mjesto.Edukacije.Select(e => e.ToDbModel()).ToList(),
            Skole = mjesto.Skole.Select(s => s.ToDbModel()).ToList(),
            TerenskeLokacije = mjesto.TerenskeLokacije.Select(tl => tl.ToDbModel()).ToList()

        };

    public static MaterijalnaPotreba ToDomain(this MaterijalnePotrebe potreba)
        => new MaterijalnaPotreba(
            potreba.IdMaterijalnePotrebe,
            potreba.Naziv,
            potreba.Organizator,
            potreba.Davatelj,
            potreba.Zadovoljeno,
            potreba.Akcije.Select(ToDomainAkcija),
            potreba.Skole.Select(ToDomainSkola),
            potreba.TerenskeLokacije.Select(ToDomain)
    );

    public static MaterijalnePotrebe ToDbModel(this MaterijalnaPotreba potreba)
        => new MaterijalnePotrebe
        {
            IdMaterijalnePotrebe = potreba.Id,
            Naziv = potreba.Naziv,
            Organizator = potreba.Organizator,
            Davatelj = potreba.Davatelj,
            Zadovoljeno = potreba.Zadovoljeno,
            Akcije = potreba.Akcije.Select(pr => pr.ToDbModel()).ToList(),
            Skole = potreba.Skole.Select(pr => pr.ToDbModel()).ToList(),
            TerenskeLokacije = potreba.TerenskeLokacije.Select(pr => pr.ToDbModel()).ToList()

        };

    public static TerenskaLokacija ToDomain(this DbModels.TerenskeLokacije terenskaLokacija)
        => new TerenskaLokacija(
            terenskaLokacija.IdTerenskeLokacije,
            terenskaLokacija.NazivTerenskeLokacije,
            terenskaLokacija.Slika,
            terenskaLokacija.ImaSanitarniCvor,
            terenskaLokacija.MjestoPbr,
            terenskaLokacija.Opis
            );

    public static DbModels.TerenskeLokacije ToDbModel(this TerenskaLokacija terenskaLokacija)
    {
        return new DbModels.TerenskeLokacije()
        {
            IdTerenskeLokacije = terenskaLokacija.Id,
            NazivTerenskeLokacije = terenskaLokacija.NazivTerenskaLokacija,
            Slika = terenskaLokacija.Slika,
            ImaSanitarniCvor = terenskaLokacija.ImaSanitarniCvor,
            MjestoPbr = terenskaLokacija.MjestoPbr,
            Opis = terenskaLokacija.Opis
        };
    }
    


    public static Skola ToDomainSkola(this DbModels.Skole skola)
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

    public static PolaznikSkole ToDomain(this DbModels.PolazniciSkole polaznikSkole)
        => new PolaznikSkole(polaznikSkole.Polaznik, polaznikSkole.EdukacijaId);

    public static DbModels.PolazniciSkole ToDbModel(this PolaznikSkole polaznikSkole, int skolaId)
        => new DbModels.PolazniciSkole()
        {
            Polaznik = polaznikSkole.idPolaznik,
            EdukacijaId = polaznikSkole.idPolaznik,
            SkolaId = skolaId
        };

    public static Edukacija ToDomainEdukacija(this DbModels.Edukacije edukacija)
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

    public static Akcija ToDomainAkcija(this DbModels.Akcije akcija)
       => new Akcija(akcija.IdAkcija, akcija.Naziv, akcija.MjestoPbr, akcija.Organizator, akcija.KontaktOsoba);

    public static DbModels.Akcije ToDbModel(this Akcija akcija)
    {
        return new DbModels.Akcije()
        {
            IdAkcija = akcija.Id,
            Naziv = akcija.Naziv,
            MjestoPbr = akcija.MjestoPbr,
            Organizator = akcija.Organizator,
            KontaktOsoba = akcija.KontaktOsoba

        };
    }

    public static Aktivnost ToDomainAktivnost(this DbModels.Aktivnosti aktivnost)
        => new Aktivnost(aktivnost.IdAktivnost, aktivnost.MjestoPbr, aktivnost.KontaktOsoba, aktivnost.Opis, aktivnost.AkcijaId);

    public static DbModels.Aktivnosti ToDbModel(this Aktivnost aktivnost)
    {
        return new DbModels.Aktivnosti()
        {
            IdAktivnost = aktivnost.Id,
            MjestoPbr = aktivnost.MjestoPbr,
            KontaktOsoba = aktivnost.KontaktOsoba,
            Opis = aktivnost.Opis,
            AkcijaId = aktivnost.AkcijaId
        };
    }
}
