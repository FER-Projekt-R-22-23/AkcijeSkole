using AkcijeSkole.Commons;
using BaseLibrary;

namespace AkcijeSkole.Domain.Models;

public class CvrstiObjektZaObitavanje : TerenskaLokacija
{
    private int _brojPredvidenihSpavacihMjesta;

    public CvrstiObjektZaObitavanje(int id, string nazivTerenskaLokacija, byte[]? slika, bool imaSanitarniCvor, int mjestoPbr, string opis, int brojPredvidenihSpavacihMjesta)
                                       : base(id, nazivTerenskaLokacija, slika, imaSanitarniCvor, mjestoPbr, opis)
    {
        _brojPredvidenihSpavacihMjesta = brojPredvidenihSpavacihMjesta;
    }

    public int BrojPredvidenihSpavacihMjesta { get => _brojPredvidenihSpavacihMjesta; set => _brojPredvidenihSpavacihMjesta = value; }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj) &&
               obj is CvrstiObjektZaObitavanje objektiZaObitavanje &&
               _brojPredvidenihSpavacihMjesta == objektiZaObitavanje._brojPredvidenihSpavacihMjesta;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), _brojPredvidenihSpavacihMjesta);
    }

    public override Result IsValid()
    {
        return base.IsValid();
    }
}