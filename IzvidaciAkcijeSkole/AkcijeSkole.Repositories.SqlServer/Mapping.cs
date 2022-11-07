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
{/*
    public static Mjesto ToDomain(this Mjesta mjesto)
        => new Mjesto(
            mjesto.PbrMjesta,
            mjesto.NazivMjesta,
            mjesto.Akcije.Select(ToDomain),
            mjesto.Aktivnosti.Select(ToDomain),
            mjesto.Edukacije.Select(ToDomain),
            mjesto.Skole.Select(ToDomain),
            mjesto.TerenskeLokacije.Select(ToDomain)
    );

    public static Mjesta ToDbModel(this Mjesto mjesto)
        => new Mjesta
        {
            PbrMjesta = mjesto.Id,
            NazivMjesta = mjesto.NazivMjesta,
            Akcije = mjesto.AkcijaAssignments.Select(a => a.ToDbModel(mjesto.Id)).ToList(),
            Aktivnosti = mjesto.AktivnostAssignments.Select(a => a.ToDbModel(mjesto.Id)).ToList(),
            Edukacije = mjesto.EdukacijaAssignments.Select(e => e.ToDbModel(mjesto.Id)).ToList(),
            Skole = mjesto.SkolaAssignments.Select(s => s.ToDbModel(mjesto.Id)).ToList(),
            TerenskeLokacije = mjesto.TerenskaLokacijaAssignments.Select(tl => tl.ToDbModel(mjesto.Id)).ToList()

        };

    public static MaterijalnaPotreba ToDomain(this MaterijalnePotrebe potreba)
        => new MaterijalnaPotreba(
            potreba.IdMaterijalnePotrebe,
            potreba.Naziv,
            potreba.Organizator,
            potreba.Davatelj,
            potreba.Zadovoljeno,
            potreba.Akcije.Select(ToDomain),
            potreba.Skole.Select(ToDomain),
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
            Akcije = potreba.AkcijaAssignments.Select(pr => pr.ToDbModel(potreba.Id)).ToList(),
            Skole = potreba.SkolaAssignments.Select(pr => pr.ToDbModel(potreba.Id)).ToList(),
            TerenskeLokacije = potreba.TerenskaLokacijaAssignments.Select(pr => pr.ToDbModel(potreba.Id)).ToList()

        };

    public static AkcijaAssignment ToDomain(this Akcije akcija)
        => new AkcijaAssignment(
                akcija.ToDomain()
            );

    public static Akcije ToDbModel(this AkcijaAssignment akcijaAssignment, int id)
        => new Akcije { 
            IdAkcija = id,
            Naziv = akcijaAssignment.Akcija.Naziv,
            MjestoPbr = akcijaAssignment.Akcija.MjestoPbr,
            Organizator = akcijaAssignment.Akcija.Organizator,
            KontaktOsoba = akcijaAssignment.Akcija.KontaktOsoba,
            Vrsta = akcijaAssignment.Akcija.Vrsta ,
            MjestoPbrNavigation = akcijaAssignment.Akcija.MjestoPbrNavigation,
            Aktivnosti = akcijaAssignment.Akcija.Aktivnosti,
            PolazniciAkcije = akcijaAssignment.Akcija.PolazniciAkcije,
            PrijavljeniPolazniciAkcije = akcijaAssignment.Akcija.PrijavljeniPolazniciAkcije,
            MaterijalnePotrebe = akcijaAssignment.Akcija.MaterijalnePotrebe,
            TerenskeLokacije = akcijaAssignment.Akcija.TerenskeLokacije

        };

    public static AktivnostAssignment ToDomain(this Aktivnosti aktivnost)
        => new AktivnostAssignment(
                aktivnost.ToDomain()
            );

    public static Aktivnosti ToDbModel(this AktivnostAssignment aktivnostAssignment, int id)
        => new Aktivnosti
        {
            IdAktivnost = id,
            MjestoPbr = aktivnostAssignment.Aktivnost.MjestoPbr,
            KontaktOsoba = aktivnostAssignment.Aktivnost.KontaktOsoba,
            Opis = aktivnostAssignment.Aktivnost.Opis,
            AkcijaId = aktivnostAssignment.Aktivnost.AkcijaId,
            Akcija = aktivnostAssignment.Aktivnost.Akcija,
            MjestoPbrNavigation = aktivnostAssignment.Aktivnost.MjestoPbrNavigation
            PolazniciAkcije = aktivnostAssignment.Aktivnost.PolazniciAkcije,
            PrijavljeniPolazniciAkcije = aktivnostAssignment.Aktivnost.PrijavljeniPolazniciAkcije

        };

    public static EdukacijaAssignment ToDomain(this Edukacije edukacija)
        => new EdukacijaAssignment(
                edukacija.ToDomain()
            );

    public static Edukacije ToDbModel(this EdukacijaAssignment edukacijaAssignment, int id)
        => new Edukacije
        {
            IdEdukacija = id,
            NazivEdukacija = edukacijaAssignment.Edukacija.nazivEdukacija,
            MjestoPbr = edukacijaAssignment.Edukacija.MjestoPbr,
            OpisEdukacije = edukacijaAssignment.Edukacija.OpisEdukacija,
            SkolaId = edukacijaAssignment.Edukacija.SkolaId, 
            MjestoPbrNavigation = edukacijaAssignment.Edukacija.MjestoPbrNavigation,
            Skola = edukacijaAssignment.Edukacija.Skola,
            PolazniciSkole = edukacijaAssignment.Edukacija.PolazniciSkole,
            Predavaci = edukacijaAssignment.Edukacija.Predavaci,
            PrijavljeniPolazniciSkole = edukacijaAssignment.Edukacija.PrijavljeniPolazniciSkole

        };

    public static SkolaAssignment ToDomain(this Skole skola)
        => new SkolaAssignment(
                skola.ToDomain()
            );

    public static Skole ToDbModel(this SkolaAssignment skolaAssignment, int id)
        => new Skole
        {
            IdSkole = id,
            NazivSkole = skolaAssignment.Skola.NazivSkole,
            MjestoPbr = skolaAssignment.Skola.MjestoPbr,
            Organizator = skolaAssignment.Skola.Organizator,
            KontaktOsoba = skolaAssignment.Skola.KontaktOsoba,
            MjestoPbrNavigation = skolaAssignment.Skola.MjestoPbrNavigation,
            Edukacije = skolaAssignment.Skola.Edukacije,
            PolazniciSkole = skolaAssignment.Skola.PolazniciSkole,
            PrijavljeniPolazniciSkole = skolaAssignment.Skola.PrijavljeniPolazniciSkole,
            MaterijalnePotreb = skolaAssignment.Skola.MaterijalnePotreb,
            TerenskaLokacija = skolaAssignment.Skola.TerenskaLokacija

        };

    public static TerenskaLokacijaAssignment ToDomain(this TerenskeLokacije lokacija)
        => new TerenskaLokacijaAssignment(
                lokacija.ToDomain()
            );

    public static TerenskeLokacije ToDbModel(this TerenskaLokacijaAssignment terenskaLokacijaAssignment, int id)
        => new TerenskeLokacije
        {
            IdTerenskeLokacije = id,
            NazivTerenskeLokacije = terenskaLokacijaAssignment.TerenskaLokacija.NazivTerenskeLokacije,
            Slika = terenskaLokacijaAssignment.TerenskaLokacija.Slika,
            ImaSanitarniCvor = terenskaLokacijaAssignment.TerenskaLokacija.ImaSanitarniCvor,
            MjestoPbr = terenskaLokacijaAssignment.TerenskaLokacija.MjestoPbr,
            Opis = terenskaLokacijaAssignment.TerenskaLokacija.Opis,
            MjestoPbrNavigation = terenskaLokacijaAssignment.TerenskaLokacija.MjestoPbrNavigation,
            CvrstiNamjenskiObjekti = terenskaLokacijaAssignment.TerenskaLokacija.CvrstiNamjenskiObjekti,
            CvrstiObjektiZaObitavanje = terenskaLokacijaAssignment.TerenskaLokacija.CvrstiObjektiZaObitavanje,
            Logorista = terenskaLokacijaAssignment.TerenskaLokacija.Logorista,
            PrivremeniObjekti = terenskaLokacijaAssignment.TerenskaLokacija.PrivremeniObjekti,
            Akcije = terenskaLokacijaAssignment.TerenskaLokacija.Akcije,
            MaterijalnePotrebe = terenskaLokacijaAssignment.TerenskaLokacija.MaterijalnePotrebe,
            Skola = terenskaLokacijaAssignment.TerenskaLokacija.Skola 

        };
    */


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
