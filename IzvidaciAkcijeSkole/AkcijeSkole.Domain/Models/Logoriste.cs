using AkcijeSkole.Commons;
using BaseLibrary;

namespace AkcijeSkole.Domain.Models;

public class Logoriste : TerenskaLokacija
{
    private string _koordinateMreze;
    private int _predvideniBrojClanova;

    public Logoriste(int id, string nazivTerenskaLokacija, byte[]? slika, bool imaSanitarniCvor, int mjestoPbr, string opis, string koordinateMreze, int predvideniBrojClanova)
                       : base(id, nazivTerenskaLokacija, slika, imaSanitarniCvor, mjestoPbr, opis)
    {
        _koordinateMreze = koordinateMreze;
        _predvideniBrojClanova = predvideniBrojClanova;
    }

    public string KoordinateMreze { get => _koordinateMreze; set => _koordinateMreze = value; }
    public int PredvideniBrojClanova { get => _predvideniBrojClanova; set => _predvideniBrojClanova = value; }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj) &&
                obj is Logoriste logoriste &&
                _koordinateMreze == logoriste._koordinateMreze &
                _predvideniBrojClanova == logoriste._predvideniBrojClanova;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), _koordinateMreze, _predvideniBrojClanova);
    }

    public override Result IsValid()
    {
        return base.IsValid();
    }
}