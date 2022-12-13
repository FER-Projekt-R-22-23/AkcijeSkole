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
    //private CvrstiNamjenskiObjekti _cvrstiNamjenskiObjekt;
    //private CvrstiObjektiZaObitavanje _cvrstiObjektZaObitavanje;
    //private Logorista _logoriste;
    //private PrivremeniObjekti _privremeniObjekt;

    public string NazivTerenskaLokacija { get => _nazivTerenskaLokacija; set => _nazivTerenskaLokacija = value; }
    public byte[] Slika { get => _slika; set => _slika = value; }
    public bool ImaSanitarniCvor { get => _imaSanitarniCvor; set => _imaSanitarniCvor = value; }
    public int MjestoPbr { get => _mjestoPbr; set => _mjestoPbr = value; }
    public string Opis { get => _opis; set => _opis = value; }
    //public CvrstiNamjenskiObjekti CvrstiNamjenskiObjekt { get => _cvrstiNamjenskiObjekt; set => _cvrstiNamjenskiObjekt = value; }
    //public CvrstiObjektiZaObitavanje CvrstiObjektZaObitavanje { get => _cvrstiObjektZaObitavanje; set => _cvrstiObjektZaObitavanje = value; }
    //public Logorista Logoriste { get => _logoriste; set => _logoriste = value; }
    //public PrivremeniObjekti PrivremeniObjekt { get => _privremeniObjekt; set => _privremeniObjekt = value; }


    public TerenskaLokacija(int id, string nazivTerenskaLokacija, byte[]? slika, bool imaSanitarniCvor, int mjestoPbr, string opis,
        IEnumerable<CvrstiNamjenskiObjekti>? cvrstiNamjenskiObjekt = null, IEnumerable<CvrstiObjektiZaObitavanje>? cvrstiObjektZaObitavanje = null,
        IEnumerable<Logorista>? logoriste = null, IEnumerable<PrivremeniObjekti>? privremeniObjekt = null) : base(id)
    {
        if (string.IsNullOrEmpty(nazivTerenskaLokacija))
        {
            throw new ArgumentException($"'{nameof(nazivTerenskaLokacija)}' cannot be null or empty.", nameof(nazivTerenskaLokacija));
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
        //_cvrstiNamjenskiObjekt = cvrstiNamjenskiObjekt;
        //_cvrstiObjektZaObitavanje = cvrstiObjektZaObitavanje;
        //_logoriste = logoriste;
        //_privremeniObjekt = privremeniObjekt;
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
               //_cvrstiNamjenskiObjekt == TerenskaLokacija._cvrstiNamjenskiObjekt &&
               //_cvrstiObjektZaObitavanje == TerenskaLokacija._cvrstiObjektZaObitavanje &&
               //_logoriste == TerenskaLokacija._logoriste &&
               //_privremeniObjekt == TerenskaLokacija._privremeniObjekt;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_id, _nazivTerenskaLokacija, _slika, _imaSanitarniCvor, _mjestoPbr, _opis);
    }

    public override Result IsValid()
        => Validation.Validate(
            (() => _nazivTerenskaLokacija.Length <= 50, "Naziv terenske lokacije length must be less than 50 characters"),
            (() => !string.IsNullOrEmpty(_nazivTerenskaLokacija.Trim()), "Naziv terenske lokacije can't be null, empty or whitespace"),
            (() => !string.IsNullOrEmpty(_opis.Trim()), "Opis can't be null, empty or whitespace")
            );
}