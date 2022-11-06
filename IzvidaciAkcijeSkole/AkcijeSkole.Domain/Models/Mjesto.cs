using AkcijeSkole.Commons;
using BaseLibrary;

namespace AkcijeSkole.Domain.Models;
public class Mjesto : AggregateRoot<int>
{
    private string _nazivMjesta;
    private readonly List<AkcijaAssignment> _akcijaAssignments;
    private readonly List<AktivnostAssignment> _aktivnostAssignments;
    private readonly List<EdukacijaAssignment> _edukacijaAssignments;
    private readonly List<SkolaAssignment> _skolaAssignments;
    private readonly List<TerenskaLokacijaAssignment> _terenskaLokacijaAssignments;

    public string NazivMjesta { get => _nazivMjesta; set => _nazivMjesta = value; }
    public IReadOnlyList<AkcijaAssignment> AkcijaAssignments => _akcijaAssignments.ToList();
    public IReadOnlyList<AktivnostAssignment> AktivnostAssignments => _aktivnostAssignments.ToList();
    public IReadOnlyList<EdukacijaAssignment> EdukacijaAssignments => _edukacijaAssignments.ToList();
    public IReadOnlyList<SkolaAssignment> SkolaAssignments => _skolaAssignments.ToList();
    public IReadOnlyList<TerenskaLokacijaAssignment> TerenskaLokacijaAssignments => _terenskaLokacijaAssignments.ToList();
    public Mjesto(int id, string naziv, IEnumerable<AkcijaAssignment>? akcije = null, IEnumerable<AktivnostAssignment>? aktivnosti = null,
        IEnumerable<EdukacijaAssignment>? edukacije = null, IEnumerable<SkolaAssignment>? skole = null,
        IEnumerable<TerenskaLokacijaAssignment>? terLokacije = null) : base(id)
    {
        if (string.IsNullOrEmpty(naziv))
        {
            throw new ArgumentException($"{nameof(naziv)} cannot be null or empty.", nameof(naziv));
        }

        _nazivMjesta = naziv;
        _akcijaAssignments = akcije?.ToList() ?? new List<AkcijaAssignment>();
        _aktivnostAssignments = aktivnosti?.ToList() ?? new List<AktivnostAssignment>();
        _edukacijaAssignments = edukacije?.ToList() ?? new List<EdukacijaAssignment>();
        _skolaAssignments = skole?.ToList() ?? new List<SkolaAssignment>();
        _terenskaLokacijaAssignments = terLokacije?.ToList() ?? new List<TerenskaLokacijaAssignment>();
    }

    public bool AssignAkcija(Akcija akcija) { 

        var akcijaAssignment = new AkcijaAssignment(akcija);

        _akcijaAssignments.Add(akcijaAssignment);

        return true;
    }

    public bool AssignAkcija(AkcijaAssignment akcijaAssignment)
    {
        return AssignAkcija(akcijaAssignment.Akcija);
    }

    public bool DismissFromAkcija(AkcijaAssignment akcijaAssignment)
    {
        return _akcijaAssignments.Remove(akcijaAssignment);
    }

    public bool DismissFromAkcija(Akcija akcija)
    {
        var targetAssignment = _akcijaAssignments.FirstOrDefault(ra => ra.Akcija.Equals(akcija));

        return targetAssignment != null &&
               _akcijaAssignments.Remove(targetAssignment);
    }


    public bool AssignAktivnost(Aktivnost aktivnost)
    {

        var aktivnostAssignment = new AktivnostAssignment(aktivnost);

        _aktivnostAssignments.Add(aktivnostAssignment);

        return true;
    }

    public bool AssignAktivnost(AktivnostAssignment aktivnostAssignemnt)
    {
        return AssignAktivnost(aktivnostAssignemnt.Aktivnost);
    }

    public bool DismissFromAktivnost(AktivnostAssignment aktivnostAssignment)
    {
        return _aktivnostAssignments.Remove(aktivnostAssignment);
    }

    public bool DismissFromAktivnost(Aktivnost aktivnost)
    {
        var targetAssignment = _aktivnostAssignments.FirstOrDefault(ra => ra.Aktivnost.Equals(aktivnost));

        return targetAssignment != null &&
               _aktivnostAssignments.Remove(targetAssignment);
    }

    public bool AssignEdukacija(Edukacija edukacija)
    {

        var edukacijaAssignment = new EdukacijaAssignment(edukacija);

        _edukacijaAssignments.Add(edukacijaAssignment);

        return true;
    }

    public bool AssignEdukacija(EdukacijaAssignment edukacijaAssignment)
    {
        return AssignEdukacija(edukacijaAssignment.Edukacija);
    }

    public bool DismissFromEdukacija(EdukacijaAssignment edukacijaAssignment)
    {
        return _edukacijaAssignments.Remove(edukacijaAssignment);
    }

    public bool DismissFromEdukacija(Edukacija edukacija)
    {
        var targetAssignment = _edukacijaAssignments.FirstOrDefault(ra => ra.Edukacija.Equals(edukacija));

        return targetAssignment != null &&
               _edukacijaAssignments.Remove(targetAssignment);
    }

    public bool AssignSkola(Skola skola)
    {

        var skolaAssignment = new SkolaAssignment(skola);

        _skolaAssignments.Add(skolaAssignment);

        return true;
    }

    public bool AssignSkola(SkolaAssignment skolaAssignment)
    {
        return AssignSkola(skolaAssignment.Skola);
    }

    public bool DismissFromSkola(SkolaAssignment skolaAssignment)
    {
        return _skolaAssignments.Remove(skolaAssignment);
    }

    public bool DismissFromSkola(Skola skola)
    {
        var targetAssignment = _skolaAssignments.FirstOrDefault(ra => ra.Skola.Equals(skola));

        return targetAssignment != null &&
               _skolaAssignments.Remove(targetAssignment);
    }

    public bool AssignTerenskaLokacija(TerenskaLokacija terenskaLokacija)
    {

        var terenskaLokacijaAssignment = new TerenskaLokacijaAssignment(terenskaLokacija);

        _terenskaLokacijaAssignments.Add(terenskaLokacijaAssignment);

        return true;
    }

    public bool AssignTerenskaLokacija(TerenskaLokacijaAssignment terenskaLokacijaAssignment)
    {
        return AssignTerenskaLokacija(terenskaLokacijaAssignment.TerenskaLokacija);
    }

    public bool DismissFromTerenskaLokacija(TerenskaLokacijaAssignment terenskaLokacijaAssignment)
    {
        return _terenskaLokacijaAssignments.Remove(terenskaLokacijaAssignment);
    }

    public bool DismissFromTerenskaLokacija(TerenskaLokacija terenskaLokacija)
    {
        var targetAssignment = _terenskaLokacijaAssignments.FirstOrDefault(ra => ra.TerenskaLokacija.Equals(terenskaLokacija));

        return targetAssignment != null &&
               _terenskaLokacijaAssignments.Remove(targetAssignment);
    }

    public override bool Equals(object? other)
    {
        return other is not null &&
               other is Mjesto mjesto &&
              _id == mjesto._id &&
              _nazivMjesta == mjesto._nazivMjesta &&
              _akcijaAssignments.SequenceEqual(mjesto._akcijaAssignments) &&
              _aktivnostAssignments.SequenceEqual(mjesto._aktivnostAssignments) &&
              _edukacijaAssignments.SequenceEqual(mjesto._edukacijaAssignments) &&
              _skolaAssignments.SequenceEqual(mjesto._skolaAssignments) &&
              _terenskaLokacijaAssignments.SequenceEqual(mjesto._terenskaLokacijaAssignments);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_id, _nazivMjesta, _akcijaAssignments, _aktivnostAssignments, _edukacijaAssignments, _skolaAssignments, _terenskaLokacijaAssignments);
    }

    public override Result IsValid()
    => Validation.Validate(
            (() => _nazivMjesta.Length <= 50, "Naziv mjesta ne smije biti duži od 50 znakova"),
            (() => !string.IsNullOrEmpty(_nazivMjesta.Trim()), "Naziv mjesta ne smije biti null, prazan ili empty space.")
            );
}

