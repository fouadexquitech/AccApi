using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Request;
using System.Collections.Generic;

namespace AccApi.Repository.Interfaces
{
    public interface IPackageRepository
    {
        List<BoqRessourcesList> GetOriginalBoqList(SearchInput input);
        List<BoqModel> GetBoqList(string ItemO, SearchInput input);
        List<BoqModel> GetAllBoqList(SearchInput input);
        PackageDetailsModel GetPackageById(int IdPkge);
        bool AssignPackages(AssignPackages input);
        List<PackageSuppliersPrice> GetPackageSuppliersPrice(int IdPkge);
    }
}
