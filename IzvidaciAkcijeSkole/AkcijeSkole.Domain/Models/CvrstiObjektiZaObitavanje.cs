using AkcijeSkole.Commons;
using BaseLibrary;

namespace AkcijeSkole.Domain.Models;

public class CvrstiObjektiZaObitavanje : ValueObject
{
    private int _idObjektZaObitavanje;
    private int _brojPredvidenihSpavacihMjesta;

    public CvrstiObjektiZaObitavanje(int idObjektZaObitavanje, int brojPredvidenihSpavacihMjesta)
    {
        _idObjektZaObitavanje = idObjektZaObitavanje;
        _brojPredvidenihSpavacihMjesta = brojPredvidenihSpavacihMjesta;
    }

    public int IdObjektZaObitavanje { get => _idObjektZaObitavanje; set => _idObjektZaObitavanje = value; }
    public int BrojPredvidenihSpavacihMjesta { get => _brojPredvidenihSpavacihMjesta; set => _brojPredvidenihSpavacihMjesta = value; }

    public override bool Equals(object? obj)
    {
        return obj is not null &&
                obj is CvrstiObjektiZaObitavanje objektiZaObitavanje &&
                _idObjektZaObitavanje == objektiZaObitavanje._idObjektZaObitavanje &&
                _brojPredvidenihSpavacihMjesta == objektiZaObitavanje._brojPredvidenihSpavacihMjesta;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_idObjektZaObitavanje, _brojPredvidenihSpavacihMjesta);
    }

    public override Result IsValid()
        => Validation.Validate(
                (() => _idObjektZaObitavanje != null, "IdObjektZaObitavanje can't be null")
            );
}