
using AkcijeSkole.Commons;
using BaseLibrary;

namespace AkcijeSkole.Domain.Models;
public class MaterijalnaPotreba : AggregateRoot<int>
{ 
    private string _naziv;
    private int _organizator;
    private int _davatelj;
    private bool _zadovoljeno;
    private readonly List<Akcija> _akcije;
    private readonly List<Skola> _skole;
    private readonly List<TerenskaLokacija> _terenskeLokacije;

    public string Naziv { get => _naziv; set => _naziv = value; }
    public int Organizator { get => _organizator; set => _organizator = value; }
    public int Davatelj { get => _davatelj; set => _davatelj = value; }
    public bool Zadovoljeno { get => _zadovoljeno; set => _zadovoljeno = value; }
    public IReadOnlyList<Akcija> Akcije => _akcije.ToList();
    public IReadOnlyList<Skola> Skole => _skole.ToList();
    public IReadOnlyList<TerenskaLokacija> TerenskeLokacije => _terenskeLokacije.ToList();
    public MaterijalnaPotreba(int id, string naziv,int organitator, int davatelj, bool zadovoljeno, IEnumerable<Akcija>? akcije = null, IEnumerable<Skola>? skole = null,
        IEnumerable<TerenskaLokacija>? terLokacije = null) : base(id)
    {
        if (string.IsNullOrEmpty(naziv))
        {
            throw new ArgumentException($"'{nameof(naziv)}' cannot be null or empty.", nameof(naziv));
        }

        _naziv = naziv;
        _organizator = organitator;
        _davatelj = davatelj;
        _zadovoljeno = zadovoljeno;
        _akcije = akcije?.ToList() ?? new List<Akcija>();
        _skole = skole?.ToList() ?? new List<Skola>();
        _terenskeLokacije = terLokacije?.ToList() ?? new List<TerenskaLokacija>();
    }

    public bool AssignAkcija(Akcija akcija)
    {
        _akcije.Add(akcija);

        return true;
    }

    public bool DismissFromAkcija(Akcija akcija)
    {
        return _akcije.Remove(akcija);
    }


    public bool AssignSkola(Skola skola)
    {
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

    public override Result IsValid()
    => Validation.Validate(
            (() => _naziv.Length <= 50, "Naziv ne smije biti duži od 50 znakova."),
            (() => !string.IsNullOrEmpty(_naziv.Trim()), "Naziv ne smije biti null, prazan ili whitespace.")
            );

    public override bool Equals(object? other)
    {
        return other is not null &&
                other is MaterijalnaPotreba potreba &&
               _id == potreba._id &&
               _naziv == potreba._naziv &&
               _organizator == potreba._organizator &&
               _davatelj == potreba._davatelj &&
               _zadovoljeno == potreba._zadovoljeno &&
               _akcije.SequenceEqual(potreba._akcije) &&
               _skole.SequenceEqual(potreba._skole) &&
               _terenskeLokacije.SequenceEqual(potreba._terenskeLokacije);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_id, _naziv, _organizator, _davatelj, _zadovoljeno, _akcije, _skole, _terenskeLokacije);
    }
}


