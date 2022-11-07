using BaseLibrary;
using AkcijeSkole.Commons;

namespace AkcijeSkole.Domain.Models;

public class TerenskaLokacija : AggregateRoot<int>
{
    private string _nazivTerenskaLokacija;
    private byte[] _slika;
    private bool _imaSanitarniCvor;
    private int _mjestoPbr;
    private string _opis;
    private readonly List<CvrstiNamjenskiObjekti> _cvrstiNamjenskiObjekti;
    private readonly List<CvrstiObjektiZaObitavanje> _cvrstiObjektiZaObitavanje;
    private readonly List<Logorista> _logorista;
    private readonly List<PrivremeniObjekti> _privremeniObjekti;

    public string NazivTerenskaLokacija { get => _nazivTerenskaLokacija; set => _nazivTerenskaLokacija = value; }
    public byte[] Slika { get => _slika; set => _slika = value; }
    public bool ImaSanitarniCvor { get => _imaSanitarniCvor; set => _imaSanitarniCvor = value; }
    public int MjestoPbr { get => _mjestoPbr; set => _mjestoPbr = value; }
    public string Opis { get => _opis; set => _opis = value; }
    public IReadOnlyList<CvrstiNamjenskiObjekti> CvrstiNamjenskiObjekti => _cvrstiNamjenskiObjekti.ToList();
    public IReadOnlyList<CvrstiObjektiZaObitavanje> CvrstiObjektiZaObitavanje => _cvrstiObjektiZaObitavanje.ToList();
    public IReadOnlyList<Logorista> Logorista => _logorista.ToList();
    public IReadOnlyList<PrivremeniObjekti> PrivremeniObjekti => _privremeniObjekti.ToList();


    public TerenskaLokacija(int id, string nazivTerenskaLokacija, byte[]? slika, bool imaSanitarniCvor, int mjestoPbr, string opis,
        IEnumerable<CvrstiNamjenskiObjekti>? cvrstiNamjenskiObjekt = null, IEnumerable<CvrstiObjektiZaObitavanje>? cvrstiObjektZaObitavanje = null,
        IEnumerable<Logorista>? logoriste = null, IEnumerable<PrivremeniObjekti>? privremeniObjekt = null) : base(id)
    {
        if (string.IsNullOrEmpty(nazivTerenskaLokacija))
        {
            throw new ArgumentException($"'{nameof(nazivTerenskaLokacija)}' cannot be null or empty.", nameof(nazivTerenskaLokacija));
        }

        if(slika == null || slika.Length == 0)
        {
            throw new ArgumentException($"'{nameof(slika)}' cannot be null or empty.", nameof(slika));
        }

        if (string.IsNullOrEmpty(opis))
        {
            throw new ArgumentException($"'{nameof(opis)}' cannot be null or empty.", nameof(opis));
        }

        _nazivTerenskaLokacija = nazivTerenskaLokacija;
        _slika = slika;
        _imaSanitarniCvor = imaSanitarniCvor;
        _mjestoPbr = mjestoPbr;
        _opis = opis;
        _cvrstiNamjenskiObjekti = cvrstiNamjenskiObjekt?.ToList() ?? new List<CvrstiNamjenskiObjekti>();
        _cvrstiObjektiZaObitavanje = cvrstiObjektZaObitavanje?.ToList() ?? new List<CvrstiObjektiZaObitavanje>();
        _logorista = logoriste?.ToList() ?? new List<Logorista>();
        _privremeniObjekti = privremeniObjekt?.ToList() ?? new List<PrivremeniObjekti>();
    }

    public bool newCvrstiNamjenskiObjekt(CvrstiNamjenskiObjekti cvrstiNamjenskiObjekt)
    {
        try
        {
            _cvrstiNamjenskiObjekti.Add(cvrstiNamjenskiObjekt);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool removeCvrstiNamjenskiObjekt(int id)
    {
        var targetAssignment = _cvrstiNamjenskiObjekti.FirstOrDefault(obj => obj.IdCvrstiNamjenskiObjekt.Equals(id));
        return targetAssignment != null && _cvrstiNamjenskiObjekti.Remove(targetAssignment);
    }

    public bool newCvrstiObjektZaObitavanje(CvrstiObjektiZaObitavanje cvrstiObjektZaObitavanje)
    {
        try
        {
            _cvrstiObjektiZaObitavanje.Add(cvrstiObjektZaObitavanje);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool removeCvrstiObjektZaObitavanje(int id)
    {
        var targetAssignment = _cvrstiObjektiZaObitavanje.FirstOrDefault(obj => obj.IdObjektZaObitavanje.Equals(id));
        return targetAssignment != null && _cvrstiObjektiZaObitavanje.Remove(targetAssignment);
    }

    public bool newLogoriste(Logorista logoriste)
    {
        try
        {
            _logorista.Add(logoriste);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool removeLogoriste(int id)
    {
        var targetAssignment = _logorista.FirstOrDefault(obj => obj.IdLogoriste.Equals(id));
        return targetAssignment != null && _logorista.Remove(targetAssignment);
    }

    public bool newPrivremeniObjekt(PrivremeniObjekti privremeniObjekt)
    {
        try
        {
            _privremeniObjekti.Add(privremeniObjekt);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool removePrivremeniObjekt(int id)
    {
        var targetAssignment = _privremeniObjekti.FirstOrDefault(obj => obj.IdPrivremeniObjekt.Equals(id));
        return targetAssignment != null && _privremeniObjekti.Remove(targetAssignment);
    }

    public override bool Equals(object? obj)
    {
        return obj is not null &&
               obj is TerenskaLokacija TerenskaLokacija &&
               _id == TerenskaLokacija._id &&
               _nazivTerenskaLokacija == TerenskaLokacija._nazivTerenskaLokacija &&
               _slika == TerenskaLokacija._slika &&
               _imaSanitarniCvor == TerenskaLokacija._imaSanitarniCvor &&
               _mjestoPbr == TerenskaLokacija._mjestoPbr &&
               _opis == TerenskaLokacija._opis;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_id, _nazivTerenskaLokacija, _slika, _imaSanitarniCvor, _mjestoPbr, _opis);
    }

    public override Result IsValid()
        => Validation.Validate(
            (() => _nazivTerenskaLokacija.Length <= 50, "Naziv terenske lokacije length must be less than 50 characters"),
            (() => !string.IsNullOrEmpty(_nazivTerenskaLokacija.Trim()), "Naziv terenske lokacije can't be null, empty or whitespace"),
            (() => !(_slika == null || _slika.Length == 0), "Slika can't be null" ),
            (() => !string.IsNullOrEmpty(_opis.Trim()), "Opis can't be null, empty or whitespace")
            );
}