/*
using AkcijeSkole.Commons;
using BaseLibrary;

namespace AkcijeSkole.Domain.Models;
public class AktivnostAssignment : ValueObject
{

    private Aktivnost _aktivnost;

    public AktivnostAssignment(Aktivnost aktivnost)
    {
        _aktivnost = aktivnost ?? throw new ArgumentNullException(nameof(aktivnost));
    }

    public Aktivnost Aktivnost { get => _aktivnost; set => _aktivnost = value; }
    public override bool Equals(object? other)
    {
        return other is not null &&
                other is AktivnostAssignment assignment &&
               _aktivnost.Equals(assignment._aktivnost);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_aktivnost);
    }

    public override Result IsValid()
    => Validation.Validate(
                (() => _aktivnost != null, "Aktivnost ne smije biti null")
            );
}
*/


