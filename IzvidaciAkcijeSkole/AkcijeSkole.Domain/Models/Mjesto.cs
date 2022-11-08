
using AkcijeSkole.Commons;
using BaseLibrary;

namespace AkcijeSkole.Domain.Models;
public class Mjesto : AggregateRoot<int>
{
    private string _nazivMjesta;
    private readonly List<Akcija> _akcije;
    private readonly List<Aktivnost> _aktivnosti;
    private readonly List<Edukacija> _edukacije;
    private readonly List<Skola> _skole;
    private readonly List<TerenskaLokacija> _terenskeLokacije;

    public string NazivMjesta { get => _nazivMjesta; set => _nazivMjesta = value; }
    public IReadOnlyList<Akcija> Akcije => _akcije.ToList();
    public IReadOnlyList<Aktivnost> Aktivnosti => _aktivnosti.ToList();
    public IReadOnlyList<Edukacija> Edukacije => _edukacije.ToList();
    public IReadOnlyList<Skola> Skole => _skole.ToList();
    public IReadOnlyList<TerenskaLokacija> TerenskeLokacije => _terenskeLokacije.ToList();
    public Mjesto(int id, string naziv, IEnumerable<Akcija>? akcije = null, IEnumerable<Aktivnost>? aktivnosti = null,
        IEnumerable<Edukacija>? edukacije = null, IEnumerable<Skola>? skole = null,
        IEnumerable<TerenskaLokacija>? terLokacije = null) : base(id)
    {
        if (string.IsNullOrEmpty(naziv))
        {
            throw new ArgumentException($"{nameof(naziv)} cannot be null or empty.", nameof(naziv));
        }

        _nazivMjesta = naziv;
        _akcije = akcije?.ToList() ?? new List<Akcija>();
        _aktivnosti = aktivnosti?.ToList() ?? new List<Aktivnost>();
        _edukacije = edukacije?.ToList() ?? new List<Edukacija>();
        _skole = skole?.ToList() ?? new List<Skola>();
        _terenskeLokacije = terLokacije?.ToList() ?? new List<TerenskaLokacija>();
    }

    public bool AssignAkcija(Akcija akcija) { 
        _akcije.Add(akcija);

        return true;
    }

    public bool DismissFromAkcija(Akcija akcija)
    {
        return _akcije.Remove(akcija);
    }


    public bool AssignAktivnost(Aktivnost aktivnost) { 
        _aktivnosti.Add(aktivnost);

        return true;
    }

    public bool DismissFromAktivnost(Aktivnost aktivnost)
    {
        return _aktivnosti.Remove(aktivnost);
    }


    public bool AssignEdukacija(Edukacija edukacija) { 
        _edukacije.Add(edukacija);

        return true;
    }

    public bool DismissFromEdukacija(Edukacija edukacija)
    {
        return _edukacije.Remove(edukacija);
    }

    public bool AssignSkola(Skola skola) { 
        _skole.Add(skola);

        return true;
    }


    public bool DismissFromSkola(Skola skola)
    {
        return _skole.Remove(skola);
    }


    public bool AssignTerenskaLokacija(TerenskaLokacija terenskaLokacija)
    {
        _terenskeLokacije.Add(terenskaLokacija);

        return true;
    }

    public bool DismissFromTerenskaLokacija(TerenskaLokacija terenskaLokacija)
    {
        return _terenskeLokacije.Remove(terenskaLokacija);
    }

    public override bool Equals(object? other)
    {
        return other is not null &&
               other is Mjesto mjesto &&
              _id == mjesto._id &&
              _nazivMjesta == mjesto._nazivMjesta &&
              _akcije.SequenceEqual(mjesto._akcije) &&
              _aktivnosti.SequenceEqual(mjesto._aktivnosti) &&
              _edukacije.SequenceEqual(mjesto._edukacije) &&
              _skole.SequenceEqual(mjesto._skole) &&
              _terenskeLokacije.SequenceEqual(mjesto._terenskeLokacije);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_id, _nazivMjesta, _akcije, _aktivnosti, _edukacije, _skole, _terenskeLokacije);
    }

    public override Result IsValid()
    => Validation.Validate(
            (() => _nazivMjesta.Length <= 50, "Naziv mjesta ne smije biti duži od 50 znakova"),
            (() => !string.IsNullOrEmpty(_nazivMjesta.Trim()), "Naziv mjesta ne smije biti null, prazan ili empty space.")
            );
}


