using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AkcijeSkoleWebApi.DTO_s
{
    public class Mjesto
    {
        public int PbrMjesta { get; set; }
        [Required(ErrorMessage = "Naziv mjesta ne smije biti null.")]
        [StringLength(50, ErrorMessage = "Naziv mjesta ne smije biti null.")]
        [Unicode(false)]
        public string NazivMjesta { get; set; } = string.Empty;

        [InverseProperty("MjestoPbrNavigation")]
        public virtual ICollection<Akcije> Akcije { get; set; }
        [InverseProperty("MjestoPbrNavigation")]
        public virtual ICollection<Aktivnosti> Aktivnosti { get; set; }
        [InverseProperty("MjestoPbrNavigation")]
        public virtual ICollection<Edukacije> Edukacije { get; set; }
        [InverseProperty("MjestoPbrNavigation")]
        public virtual ICollection<Skole> Skole { get; set; }
        [InverseProperty("MjestoPbrNavigation")]
        public virtual ICollection<TerenskeLokacije> TerenskeLokacije { get; set; }
    }
}
