using AkcijeSkole.Commons;
using BaseLibrary;

namespace AkcijeSkole.Domain.Models;

public class PrivremeniObjekt : TerenskaLokacija
{

    public PrivremeniObjekt(int id, string nazivTerenskaLokacija, byte[]? slika, bool imaSanitarniCvor, int mjestoPbr, string opis)
                                    : base(id, nazivTerenskaLokacija, slika, imaSanitarniCvor, mjestoPbr, opis)
    {
    }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj) &&
                obj is PrivremeniObjekt;
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