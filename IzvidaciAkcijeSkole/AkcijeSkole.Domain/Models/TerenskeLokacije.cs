using BaseLibrary;
using AkcijeSkole.Commons;

namespace AkcijeSkole.Domain.Models;

public class TerenskeLokacije : AggregateRoot<int>
{
    private string _nazivTerenskeLokacije;
    private byte[] _slika;
    private bool _imaSanitarniCvor;
    private int _mjestoPbr;
    private string _opis;

    public string NazivTerenskeLokacije { get => _nazivTerenskeLokacije; set => _nazivTerenskeLokacije = value; }
    public byte[] Slika { get => _slika; set => _slika = value; }
    public bool ImaSanitarniCvor { get => _imaSanitarniCvor; set => _imaSanitarniCvor = value; }
    public int MjestoPbr { get => _mjestoPbr; set => _mjestoPbr = value; }
    public string Opis { get => _opis; set => _opis = value; }

    public TerenskeLokacije(int id, string nazivTerenskeLokacije, byte[]? slika, bool imaSanitarniCvor, int mjestoPbr, string opis) : base(id)
    {
        if (string.IsNullOrEmpty(nazivTerenskeLokacije))
        {
            throw new ArgumentException($"'{nameof(nazivTerenskeLokacije)}' cannot be null or empty.", nameof(nazivTerenskeLokacije));
        }

        if(slika == null || slika.Length == 0)
        {
            throw new ArgumentException($"'{nameof(slika)}' cannot be null or empty.", nameof(slika));
        }

        if (string.IsNullOrEmpty(opis))
        {
            throw new ArgumentException($"'{nameof(opis)}' cannot be null or empty.", nameof(opis));
        }

        _nazivTerenskeLokacije = nazivTerenskeLokacije;
        _slika = slika;
        _imaSanitarniCvor = imaSanitarniCvor;
        _mjestoPbr = mjestoPbr;
        _opis = opis;
    }

    public override bool Equals(object? obj)
    {
        return obj is not null &&
               obj is TerenskeLokacije terenskeLokacije &&
               _id == terenskeLokacije._id &&
               _nazivTerenskeLokacije == terenskeLokacije._nazivTerenskeLokacije &&
               _slika == terenskeLokacije._slika &&
               _imaSanitarniCvor == terenskeLokacije._imaSanitarniCvor &&
               _mjestoPbr == terenskeLokacije._mjestoPbr &&
               _opis == terenskeLokacije._opis;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_id, _nazivTerenskeLokacije, _slika, _imaSanitarniCvor, _mjestoPbr, _opis);
    }

    public override Result IsValid()
        => Validation.Validate(
            (() => _nazivTerenskeLokacije.Length <= 50, "Naziv terenske lokacije length must be less than 50 characters"),
            (() => !string.IsNullOrEmpty(_nazivTerenskeLokacije.Trim()), "Naziv terenske lokacije can't be null, empty or whitespace"),
            (() => !(_slika == null || _slika.Length == 0), "Slika can't be null" ),
            (() => !string.IsNullOrEmpty(_opis.Trim()), "Opis can't be null, empty or whitespace")
            );
}