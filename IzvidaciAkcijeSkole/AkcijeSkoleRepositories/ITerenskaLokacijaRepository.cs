using AkcijeSkole.Domain.Models;
using BaseLibrary;

namespace AkcijeSkole.Repositories;

public interface ITerenskaLokacijaRepository 
    : IRepository<int, TerenskaLokacija>
{

    public Result<CvrstiObjektZaObitavanje> GetCvrsti(int id);

    public Result RemoveCvrsti(int id);

    public Result<IEnumerable<CvrstiNamjenskiObjekt>> GetAllCvrstiNamjenski();

    public Result<IEnumerable<CvrstiObjektZaObitavanje>> GetAllCvrstiObitavanje();

    public Result<IEnumerable<Logoriste>> GetAllLogoriste();

    public Result<IEnumerable<PrivremeniObjekt>> GetAllPrivremeni();

    public Result Insert(CvrstiNamjenskiObjekt model);

    public Result Insert(CvrstiObjektZaObitavanje model);

    public Result Insert(Logoriste model);

    public Result Insert(PrivremeniObjekt model);

    public Result Update(CvrstiNamjenskiObjekt model);

    public Result Update(CvrstiObjektZaObitavanje model);

    public Result Update(Logoriste model);

    public Result Update(PrivremeniObjekt model);

    public bool Exists(CvrstiNamjenskiObjekt model);

    public bool Exists(CvrstiObjektZaObitavanje model);

    public bool Exists(Logoriste model);

    public bool Exists(PrivremeniObjekt model);

    public bool ExistsCvrstiNamjenski(int id);

    public bool ExistsCvrstiObitavanje(int id);

    public bool ExistsLogoriste(int id);

    public bool ExistsPrivremeni(int id);
}