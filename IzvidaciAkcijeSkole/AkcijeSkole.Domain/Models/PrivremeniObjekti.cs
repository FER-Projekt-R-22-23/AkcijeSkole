using AkcijeSkole.Commons;
using BaseLibrary;

namespace AkcijeSkole.Domain.Models;

public class PrivremeniObjekti : ValueObject
{
    private int _idPrivremeniObjekt;
    private string _opis;

    public PrivremeniObjekti(int idPrivremeniObjekt, string opis)
    {
        _idPrivremeniObjekt = idPrivremeniObjekt;
        _opis = opis;
    }

    public int IdPrivremeniObjekt { get => _idPrivremeniObjekt; set => _idPrivremeniObjekt = value; }
    public string Opis { get => _opis; set => _opis = value; }

    public override bool Equals(object? obj)
    {
        return obj is not null &&
                obj is PrivremeniObjekti privremeniObjekt &&
                _idPrivremeniObjekt == privremeniObjekt._idPrivremeniObjekt &&
                _opis == privremeniObjekt._opis;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_idPrivremeniObjekt, _opis);
    }

    public override Result IsValid()
        => Validation.Validate(
                (() => _idPrivremeniObjekt != null, "IdPrivremeniObjekt can't be null")
            );
}