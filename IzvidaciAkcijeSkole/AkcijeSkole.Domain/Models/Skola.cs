using BaseLibrary;
using AkcijeSkole.Commons;

namespace AkcijeSkole.Domain.Models;
public class Skola : AggregateRoot<int>
{
    private string _NazivSkole;
    private int _MjestoPbr;
    private int _Organizator;
    private int _KontaktOsoba;
    private readonly List<Edukacija> _edukacijeUSkoli;
    private readonly List<PolaznikNaEdukaciji> _polazniciSkole;

    public Skola(int id, string nazivSkole, int mjestoPbr, int organizator, int kontaktOsoba, IEnumerable<Edukacija>? edukacijaUSkoli = null, IEnumerable<PolaznikNaEdukaciji>? polazniciSkole = null) : base(id)
    {
        if (string.IsNullOrEmpty(nazivSkole))
        {
            throw new ArgumentException($"'{nameof(nazivSkole)}' cannot be null or empty.", nameof(nazivSkole));
        }
        _NazivSkole = nazivSkole;
        _MjestoPbr = mjestoPbr;
        _Organizator = organizator;
        _KontaktOsoba = kontaktOsoba;
        _edukacijeUSkoli = edukacijaUSkoli?.ToList() ?? new List<Edukacija>();
        _polazniciSkole = polazniciSkole?.ToList() ?? new List<PolaznikNaEdukaciji>();
    }

    public string NazivSkole { get => _NazivSkole; set => _NazivSkole = value; }
    public int Organizator { get => _Organizator; set => _Organizator = value; }
    public int MjestoPbr { get => _MjestoPbr; set => _MjestoPbr = value; }
    public int KontaktOsoba { get => _KontaktOsoba; set => _KontaktOsoba = value; }
    public IReadOnlyList<Edukacija> EdukacijeUSkoli => _edukacijeUSkoli.ToList();
    public IReadOnlyList<PolaznikNaEdukaciji> PolazniciSkole => _polazniciSkole.ToList();

    public override bool Equals(object? obj)
    {
        return obj is not null &&
               obj is Skola skola &&
               Id.Equals(skola.Id) &&
               NazivSkole.Equals(skola.NazivSkole) &&
               MjestoPbr.Equals(skola.MjestoPbr) &&
               KontaktOsoba.Equals(skola.KontaktOsoba) &&
               Organizator.Equals(skola.Organizator) &&
               _edukacijeUSkoli.SequenceEqual(skola.EdukacijeUSkoli) &&
               _polazniciSkole.SequenceEqual(skola.PolazniciSkole);

    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, NazivSkole);
    }

    public override Result IsValid()
        => Validation.Validate(
            (() => _NazivSkole.Length <= 50, "Naziv skole lenght must be less than 50 characters"),
            (() => !string.IsNullOrEmpty(_NazivSkole.Trim()), "Naziv skole name can't be null, empty, or whitespace")
            );


    public bool newEdukacija(Edukacija edukacija)
    {
        try
        {
            _edukacijeUSkoli.Add(edukacija);
            return true;
        }
        catch (Exception)
        {
            return false;

        }
    }

    public bool removeEdukacija(int idEdukacija)
    {
        var targetAssignment = _edukacijeUSkoli.FirstOrDefault(obj => obj.Id.Equals(idEdukacija));
        return targetAssignment != null && _edukacijeUSkoli.Remove(targetAssignment);
    }

}

