using BaseLibrary;
using AkcijeSkole.Commons;

namespace AkcijeSkole.Domain.Models;
public class Skola : Entity<int>
{
    private string _NazivSkole;
    private int _MjestoPbr;
    private int _Organizator;
    private int _KontaktOsoba;

    public Skola(int id, string nazivSkole, int mjestoPbr, int organizator, int kontaktOsoba) : base(id)
    {
        if (string.IsNullOrEmpty(nazivSkole))
        {
            throw new ArgumentException($"'{nameof(nazivSkole)}' cannot be null or empty.", nameof(nazivSkole));
        }
        _NazivSkole = nazivSkole;
        _MjestoPbr = mjestoPbr;
        _Organizator = organizator;
        _KontaktOsoba = kontaktOsoba;
    }

    public string NazivSkole { get => _NazivSkole; set => _NazivSkole = value; }
    public int Organizator { get => _Organizator; set => _Organizator = value; }
    public int MjestoPbr { get => _MjestoPbr; set => _MjestoPbr = value; }
    public int KontaktOsoba { get => _KontaktOsoba; set => _KontaktOsoba = value; }

    public override bool Equals(object? obj)
    {
        return obj is not null &&
               obj is Skola skola &&
               Id.Equals(skola.Id) &&
               NazivSkole.Equals(skola.NazivSkole) &&
               MjestoPbr.Equals(skola.MjestoPbr) &&
               KontaktOsoba.Equals(skola.KontaktOsoba) &&
               Organizator.Equals(skola.Organizator);

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
}

