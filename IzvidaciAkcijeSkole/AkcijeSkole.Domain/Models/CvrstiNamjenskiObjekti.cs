using AkcijeSkole.Commons;
using BaseLibrary;

namespace AkcijeSkole.Domain.Models;

public class CvrstiNamjenskiObjekti : ValueObject
{
    private int _idNamjenskiObjekt;
    private string _opis;

    public CvrstiNamjenskiObjekti(int idCvrstiNamjenskiObjekt, string opis)
    {
        _idNamjenskiObjekt = idCvrstiNamjenskiObjekt;
        _opis = opis;
    }

    public int IdCvrstiNamjenskiObjekt { get => _idNamjenskiObjekt; set => _idNamjenskiObjekt = value; }
    public string Opis { get => _opis; set => _opis = value; }

    public override bool Equals(object? obj)
    {
        return obj is not null &&
                obj is CvrstiNamjenskiObjekti cvrstiNamjenskiObjekti &&
                _idNamjenskiObjekt == cvrstiNamjenskiObjekti._idNamjenskiObjekt &&
                _opis == cvrstiNamjenskiObjekti._opis;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_idNamjenskiObjekt, _opis);
    }

    public override Result IsValid()
        => Validation.Validate(
                (() => _idNamjenskiObjekt != null, "IdNamjenskiObjekt can't be null")
            );
}