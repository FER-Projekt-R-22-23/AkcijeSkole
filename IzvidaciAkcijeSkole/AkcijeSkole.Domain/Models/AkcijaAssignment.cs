/*
using AkcijeSkole.Commons;
using BaseLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkcijeSkole.Domain.Models;
public class AkcijaAssignment : ValueObject
{

    private Akcija _akcija;

    public AkcijaAssignment( Akcija akcija)
    {
        _akcija = akcija ?? throw new ArgumentNullException(nameof(akcija));
    }

    public Akcija Akcija { get => _akcija; set => _akcija = value; }
    public override bool Equals(object? other)
    {
        return other is not null &&
                other is AkcijaAssignment assignment&&
               _akcija.Equals(assignment._akcija);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_akcija);
    }

    public override Result IsValid()
    => Validation.Validate(
                (() => _akcija != null, "Akcija ne smije biti null")
            );
}
*/

