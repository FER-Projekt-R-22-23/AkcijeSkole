using AkcijeSkole.Commons;
using BaseLibrary;

namespace AkcijeSkole.Domain.Models;

public class Logorista : ValueObject
{
    private int _idLogoriste;
    private string _koordinateMreze;
    private int _predvideniBrojClanova;

    public Logorista(int idLogorista, string koordinateMreze, int predvideniBrojClanova)
    {
        _idLogoriste = idLogorista;
        _koordinateMreze = koordinateMreze;
        _predvideniBrojClanova = predvideniBrojClanova;
    }

    public int IdLogoriste { get => _idLogoriste; set => _idLogoriste = value; }
    public string KoordinateMreze { get => _koordinateMreze; set => _koordinateMreze = value; }
    public int PredvideniBrojClanova { get => _predvideniBrojClanova; set => _predvideniBrojClanova = value }

    public override bool Equals(object? obj)
    {
        return obj is not null &&
                obj is Logorista logoriste &&
                _idLogoriste == logoriste._idLogoriste &&
                _koordinateMreze == logoriste._koordinateMreze &
                _predvideniBrojClanova == logoriste._predvideniBrojClanova;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_idLogoriste, _koordinateMreze, _predvideniBrojClanova);
    }

    public override Result IsValid()
        => Validation.Validate(
                (() => _idLogoriste != null, "IdLogoriste can't be null")
            );
}