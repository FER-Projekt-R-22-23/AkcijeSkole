using AkcijeSkole.Commons;
using BaseLibrary;

namespace AkcijeSkole.Domain.Models;

public class CvrstiNamjenskiObjekt : TerenskaLokacija
{

    public CvrstiNamjenskiObjekt(int id, string nazivTerenskaLokacija, byte[]? slika, bool imaSanitarniCvor, int mjestoPbr, string opis)
                                    : base(id, nazivTerenskaLokacija, slika, imaSanitarniCvor, mjestoPbr, opis)
    {
    }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj) &&
               obj is CvrstiNamjenskiObjekt;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override Result IsValid()
    {
        return base.IsValid();
    }
}