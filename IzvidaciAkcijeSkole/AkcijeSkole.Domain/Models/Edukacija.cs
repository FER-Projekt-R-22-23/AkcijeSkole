using BaseLibrary;
using AkcijeSkole.Commons;

namespace AkcijeSkole.Domain.Models;
public class Edukacija : Entity<int>
{
    private string _NazivEdukacija;
    private int _MjestoPbr;
    private string _OpisEdukacije;
    private int _SkolaId;

    public Edukacija(int id, string nazivEdukacije, int mjestoPbr, string opisEdukacije, int skolaId) : base(id)
    {
        if (string.IsNullOrEmpty(nazivEdukacije))
        {
            throw new ArgumentException($"'{nameof(nazivEdukacije)}' cannot be null or empty.", nameof(nazivEdukacije));
        }
        _NazivEdukacija = nazivEdukacije;
        _MjestoPbr = mjestoPbr;
        _OpisEdukacije = opisEdukacije;
        _SkolaId = skolaId;
    }

    public string NazivEdukacije { get => _NazivEdukacija; set => _NazivEdukacija = value; }
    public int MjestoPbr { get => _MjestoPbr; set => _MjestoPbr = value; }
    public string OpisEdukacije { get => _OpisEdukacije; set => _OpisEdukacije = value; }
    public int SkolaId { get => _SkolaId; set => _SkolaId = value; }

    public override bool Equals(object? obj)
    {
        return obj is not null &&
               obj is Edukacija edukacija &&
               Id.Equals(edukacija.Id) &&
               NazivEdukacije.Equals(edukacija.NazivEdukacije) &&
               MjestoPbr.Equals(edukacija.MjestoPbr) &&
               OpisEdukacije.Equals(edukacija.OpisEdukacije) &&
               SkolaId.Equals(edukacija.SkolaId);


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
}

