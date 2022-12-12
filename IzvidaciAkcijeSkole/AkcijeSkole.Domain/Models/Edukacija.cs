using BaseLibrary;
using AkcijeSkole.Commons;
using System;
using System.Data;

namespace AkcijeSkole.Domain.Models;
public class Edukacija : AggregateRoot<int>
{
    private string _NazivEdukacija;
    private int _MjestoPbr;
    private string _OpisEdukacije;
    private int _SkolaId;
    private readonly List<PredavacNaEdukaciji> _predavaciNaEdukaciji;
    private readonly List<PolaznikNaEdukaciji> _polazniciNaEdukaciji;
    private readonly List<PrijavljeniClanNaEdukaciji> _prijavljeniNaEdukaciju;

    public Edukacija(int id, string nazivEdukacije, int mjestoPbr, string opisEdukacije, int skolaId, IEnumerable<PredavacNaEdukaciji>? predavaciNaEdukaciji = null, IEnumerable<PolaznikNaEdukaciji>? polazniciEdukacije = null, IEnumerable<PrijavljeniClanNaEdukaciji>? prijavljeniNaEdukaciju = null) : base(id)
    {
        if (string.IsNullOrEmpty(nazivEdukacije))
        {
            throw new ArgumentException($"'{nameof(nazivEdukacije)}' cannot be null or empty.", nameof(nazivEdukacije));
        }
        _NazivEdukacija = nazivEdukacije;
        _MjestoPbr = mjestoPbr;
        _OpisEdukacije = opisEdukacije;
        _SkolaId = skolaId;
        _predavaciNaEdukaciji = predavaciNaEdukaciji?.ToList() ?? new List<PredavacNaEdukaciji>();
        _polazniciNaEdukaciji = polazniciEdukacije?.ToList() ?? new List<PolaznikNaEdukaciji>();
        _prijavljeniNaEdukaciju = prijavljeniNaEdukaciju?.ToList() ?? new List<PrijavljeniClanNaEdukaciji>();
    }

    public string NazivEdukacije { get => _NazivEdukacija; set => _NazivEdukacija = value; }
    public int MjestoPbr { get => _MjestoPbr; set => _MjestoPbr = value; }
    public string OpisEdukacije { get => _OpisEdukacije; set => _OpisEdukacije = value; }
    public int SkolaId { get => _SkolaId; set => _SkolaId = value; }
    public IReadOnlyList<PredavacNaEdukaciji> PredavaciNaEdukaciji => _predavaciNaEdukaciji.ToList();
    public IReadOnlyList<PolaznikNaEdukaciji> PolazniciEdukacije => _polazniciNaEdukaciji.ToList();
    public IReadOnlyList<PrijavljeniClanNaEdukaciji> PrijavljeniNaEdukaciji => _prijavljeniNaEdukaciju.ToList();

    public override bool Equals(object? obj)
    {
        return obj is not null &&
               obj is Edukacija edukacija &&
               Id.Equals(edukacija.Id) &&
               NazivEdukacije.Equals(edukacija.NazivEdukacije) &&
               MjestoPbr.Equals(edukacija.MjestoPbr) &&
               OpisEdukacije.Equals(edukacija.OpisEdukacije) &&
               SkolaId.Equals(edukacija.SkolaId) &&
               _predavaciNaEdukaciji.SequenceEqual(edukacija._predavaciNaEdukaciji) &&
               _polazniciNaEdukaciji.SequenceEqual(edukacija._polazniciNaEdukaciji) &&
               _prijavljeniNaEdukaciju.SequenceEqual(edukacija._prijavljeniNaEdukaciju);


    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, NazivEdukacije);
    }

    public override Result IsValid()
        => Validation.Validate(
            (() => _NazivEdukacija.Length <= 50, "Naziv edukacije lenght must be less than 50 characters"),
            (() => !string.IsNullOrEmpty(_NazivEdukacija.Trim()), "Naziv edukacije name can't be null, empty, or whitespace")
            );

    public bool newPredavac(PredavacNaEdukaciji predavacNaEdukaciji)
    {
        try
        {
            _predavaciNaEdukaciji.Add(predavacNaEdukaciji);
            return true;
        }
        catch (Exception)
        {
            return false;

        }
    }

    public bool removePredavac(int predavacId)
    {
        var targetAssignment = _predavaciNaEdukaciji.FirstOrDefault(obj => obj.idPredavac.Equals(predavacId));
        return targetAssignment != null && _predavaciNaEdukaciji.Remove(targetAssignment);
    }

    public bool newPrijavljeni(PrijavljeniClanNaEdukaciji prijavljeni)
    {
        try
        {
            _prijavljeniNaEdukaciju.Add(prijavljeni);
            return true;
        }
        catch (Exception)
        {
            return false;

        }
    }

    public bool removePrijavljeni(int prijavljeniId)
    {
        var targetAssignment = _prijavljeniNaEdukaciju.FirstOrDefault(obj => obj.idPolaznik.Equals(prijavljeniId));
        return targetAssignment != null && _prijavljeniNaEdukaciju.Remove(targetAssignment);
    }

    public bool newPolaznik(PolaznikNaEdukaciji polaznik)
    {
        try
        {
            _polazniciNaEdukaciji.Add(polaznik);
            return true;
        }
        catch (Exception)
        {
            return false;

        }
    }

    public bool removePolaznik(int polaznikId)
    {
        var targetAssignment = _polazniciNaEdukaciji.FirstOrDefault(obj => obj.idPolaznik.Equals(polaznikId));
        return targetAssignment != null && _polazniciNaEdukaciji.Remove(targetAssignment);
    }
}

