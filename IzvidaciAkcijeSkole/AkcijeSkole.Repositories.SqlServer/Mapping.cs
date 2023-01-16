using AkcijeSkole.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using DbModels = AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using System.Linq.Expressions;

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

    public static CvrstiNamjenskiObjekt ToDomainCvrstiNamjenski(this DbModels.CvrstiNamjenskiObjekti cvrstiNamjenski)
        => new CvrstiNamjenskiObjekt(
            cvrstiNamjenski.IdNamjenskiObjekt,
            cvrstiNamjenski.IdNamjenskiObjektNavigation.NazivTerenskeLokacije,
            cvrstiNamjenski.IdNamjenskiObjektNavigation.Slika,
            cvrstiNamjenski.IdNamjenskiObjektNavigation.ImaSanitarniCvor,
            cvrstiNamjenski.IdNamjenskiObjektNavigation.MjestoPbr,
            cvrstiNamjenski.IdNamjenskiObjektNavigation.Opis
            );

    public static DbModels.CvrstiNamjenskiObjekti ToDbModelCvrstiNamjenski(this CvrstiNamjenskiObjekt cvrstiNamjenski)
    {
        return new DbModels.CvrstiNamjenskiObjekti()
        {
            IdNamjenskiObjekt = cvrstiNamjenski.Id,
            Opis = cvrstiNamjenski.Opis,
            IdNamjenskiObjektNavigation = new DbModels.TerenskeLokacije()
            {
                IdTerenskeLokacije = cvrstiNamjenski.Id,
                NazivTerenskeLokacije = cvrstiNamjenski.NazivTerenskaLokacija,
                Slika = cvrstiNamjenski.Slika,
                ImaSanitarniCvor = cvrstiNamjenski.ImaSanitarniCvor,
                MjestoPbr = cvrstiNamjenski.MjestoPbr,
                Opis = cvrstiNamjenski.Opis
            }
        };
    }

    public static CvrstiObjektZaObitavanje ToDomainCvrstiObitavanje(this DbModels.CvrstiObjektiZaObitavanje cvrstiObitavanje)
        => new CvrstiObjektZaObitavanje(
            cvrstiObitavanje.IdObjektaZaObitavanje,
            cvrstiObitavanje.IdObjektaZaObitavanjeNavigation.NazivTerenskeLokacije,
            cvrstiObitavanje.IdObjektaZaObitavanjeNavigation.Slika,
            cvrstiObitavanje.IdObjektaZaObitavanjeNavigation.ImaSanitarniCvor,
            cvrstiObitavanje.IdObjektaZaObitavanjeNavigation.MjestoPbr,
            cvrstiObitavanje.IdObjektaZaObitavanjeNavigation.Opis,
            cvrstiObitavanje.BrojPredvidenihSpavacihMjesta
            );

    public static DbModels.CvrstiObjektiZaObitavanje ToDbModelCvrstiObitavanje(this CvrstiObjektZaObitavanje cvrstiObitavanje)
    {
        return new DbModels.CvrstiObjektiZaObitavanje()
        {
            IdObjektaZaObitavanje = cvrstiObitavanje.Id,
            BrojPredvidenihSpavacihMjesta = cvrstiObitavanje.BrojPredvidenihSpavacihMjesta,
            IdObjektaZaObitavanjeNavigation = new DbModels.TerenskeLokacije()
            {
                IdTerenskeLokacije = cvrstiObitavanje.Id,
                NazivTerenskeLokacije = cvrstiObitavanje.NazivTerenskaLokacija,
                Slika = cvrstiObitavanje.Slika,
                ImaSanitarniCvor = cvrstiObitavanje.ImaSanitarniCvor,
                MjestoPbr = cvrstiObitavanje.MjestoPbr,
                Opis = cvrstiObitavanje.Opis
            }
        };
    }

    public static Logoriste ToDomainLogoriste(this DbModels.Logorista logoriste)
        => new Logoriste(
            logoriste.IdLogoriste,
            logoriste.IdLogoristeNavigation.NazivTerenskeLokacije,
            logoriste.IdLogoristeNavigation.Slika,
            logoriste.IdLogoristeNavigation.ImaSanitarniCvor,
            logoriste.IdLogoristeNavigation.MjestoPbr,
            logoriste.IdLogoristeNavigation.Opis,
            logoriste.KoodinateMreze,
            logoriste.PredvideniBrojClanova
            );

    public static DbModels.Logorista ToDbModelLogoriste(this Logoriste logoriste)
    {
        return new DbModels.Logorista()
        {
            IdLogoriste = logoriste.Id,
            KoodinateMreze = logoriste.KoordinateMreze,
            PredvideniBrojClanova = logoriste.PredvideniBrojClanova,
            IdLogoristeNavigation = new DbModels.TerenskeLokacije()
            {
                IdTerenskeLokacije = logoriste.Id,
                NazivTerenskeLokacije = logoriste.NazivTerenskaLokacija,
                Slika = logoriste.Slika,
                ImaSanitarniCvor = logoriste.ImaSanitarniCvor,
                MjestoPbr = logoriste.MjestoPbr,
                Opis = logoriste.Opis
            }
        };
    }

    public static PrivremeniObjekt ToDomainPrivremeni(this DbModels.PrivremeniObjekti privremeni)
        => new PrivremeniObjekt(
            privremeni.IdPrivremeniObjekt,
            privremeni.IdPrivremeniObjektNavigation.NazivTerenskeLokacije,
            privremeni.IdPrivremeniObjektNavigation.Slika,
            privremeni.IdPrivremeniObjektNavigation.ImaSanitarniCvor,
            privremeni.IdPrivremeniObjektNavigation.MjestoPbr,
            privremeni.IdPrivremeniObjektNavigation.Opis
            );

    public static DbModels.PrivremeniObjekti ToDbModelPrivremeni(this PrivremeniObjekt privremeni)
    {
        return new DbModels.PrivremeniObjekti()
        {
            IdPrivremeniObjekt = privremeni.Id,
            Opis = privremeni.Opis,
            IdPrivremeniObjektNavigation = new DbModels.TerenskeLokacije()
            {
                IdTerenskeLokacije = privremeni.Id,
                NazivTerenskeLokacije = privremeni.NazivTerenskaLokacija,
                Slika = privremeni.Slika,
                ImaSanitarniCvor = privremeni.ImaSanitarniCvor,
                MjestoPbr = privremeni.MjestoPbr,
                Opis = privremeni.Opis
            }
        };
    }

    public static Skola ToDomainSkola(this DbModels.Skole skola)
        => new Skola(skola.IdSkole, skola.NazivSkole, skola.MjestoPbr, skola.Organizator, skola.KontaktOsoba, skola.Edukacije.Select(ToDomainEdukacija));

    public static DbModels.Skole ToDbModel(this Skola skola)
    {
        return new DbModels.Skole()
        {
            IdSkole = skola.Id,
            NazivSkole = skola.NazivSkole,
            MjestoPbr = skola.MjestoPbr,
            Organizator = skola.Organizator,
            KontaktOsoba = skola.KontaktOsoba,
            Edukacije = skola.EdukacijeUSkoli.Select(obj => obj.ToDbModel()).ToList(),

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

    public static PolaznikNaEdukaciji ToDomain(this DbModels.PolazniciSkole polaznik)
        => new PolaznikNaEdukaciji(polaznik.Polaznik);

    public static DbModels.PolazniciSkole ToDbModel(this PolaznikNaEdukaciji polaznik, int skolaId, int edukacijaId)
        => new DbModels.PolazniciSkole()
        {
            Polaznik = polaznik.idPolaznik,
            EdukacijaId = edukacijaId,
            SkolaId = skolaId
        };

    public static PrijavljeniClanNaEdukaciji ToDomain(this DbModels.PrijavljeniPolazniciSkole prijavljeni)
        => new PrijavljeniClanNaEdukaciji(prijavljeni.PrijavljenClan, prijavljeni.DatumPrijave);

    public static DbModels.PrijavljeniPolazniciSkole ToDbModel(this PrijavljeniClanNaEdukaciji prijavljeni, int skolaId, int edukacijaId)
        => new DbModels.PrijavljeniPolazniciSkole()
        {
            PrijavljenClan = prijavljeni.idPolaznik,
            DatumPrijave = prijavljeni.datumPrijave,
            EdukacijaId = edukacijaId,
            SkolaId = skolaId
        };

    public static Edukacija ToDomainEdukacija(this DbModels.Edukacije edukacija)
        => new Edukacija(edukacija.IdEdukacija, edukacija.NazivEdukacija, edukacija.MjestoPbr, edukacija.OpisEdukacije, edukacija.SkolaId, edukacija.Predavaci.Select(ToDomain), edukacija.PolazniciSkole.Select(ToDomain), edukacija.PrijavljeniPolazniciSkole.Select(ToDomain));

    public static DbModels.Edukacije ToDbModel(this Edukacija edukacija)
    {
        return new DbModels.Edukacije()
        {
            IdEdukacija = edukacija.Id,
            NazivEdukacija = edukacija.NazivEdukacije,
            MjestoPbr = edukacija.MjestoPbr,
            OpisEdukacije = edukacija.OpisEdukacije,
            SkolaId = edukacija.SkolaId,
            Predavaci = edukacija.PredavaciNaEdukaciji.Select(obj => obj.ToDbModel(edukacija.Id)).ToList(),
            PrijavljeniPolazniciSkole = edukacija.PrijavljeniNaEdukaciji.Select(obj => obj.ToDbModel(edukacija.SkolaId,edukacija.Id)).ToList(),
            PolazniciSkole = edukacija.PolazniciEdukacije.Select(obj => obj.ToDbModel(edukacija.SkolaId, edukacija.Id)).ToList()
        };
    }

    public static Akcija ToDomainAkcija(this DbModels.Akcije akcija)
       => new Akcija(akcija.IdAkcija, akcija.Naziv, akcija.MjestoPbr, akcija.Organizator, akcija.KontaktOsoba, akcija.Vrsta, akcija.Aktivnosti.Select(ToDomainAktivnost));

    public static DbModels.Akcije ToDbModel(this Akcija akcija)
    {
        return new DbModels.Akcije()
        {
            IdAkcija = akcija.Id,
            Naziv = akcija.Naziv,
            MjestoPbr = akcija.MjestoPbr,
            Organizator = akcija.Organizator,
            KontaktOsoba = akcija.KontaktOsoba,
            Vrsta = akcija.Vrsta,
            Aktivnosti = akcija.AktivnostiAkcije.Select(a => a.ToDbModel()).ToList()

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

    public static Zahtjev ToDomainZahtjev(this DbModels.Zahtjevi zahtjev)
        => new Zahtjev(zahtjev.IdZahtjev, zahtjev.IdMatPotreba, zahtjev.Status);

    public static DbModels.Zahtjevi ToDbModel(this Zahtjev zahtjev)
    {
        return new DbModels.Zahtjevi()
        {
            IdZahtjev = zahtjev.Id,
            IdMatPotreba = zahtjev.IdMatPotreba,
            Status = zahtjev.Status
        };
    }

    internal static Expression<Func<CvrstiNamjenskiObjekti, int, object>> ToDomainCvrstiNamjenski()
    {
        throw new NotImplementedException();
    }
}
