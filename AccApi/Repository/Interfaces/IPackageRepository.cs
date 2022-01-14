using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Request;
using System.Collections.Generic;

namespace AccApi.Repository.Interfaces
{
    public interface IPackageRepository
    {
        List<OriginalBoqModel> GetOriginalBoqList(SearchInput input);
        List<BoqModel> GetBoqList(string ItemO, SearchInput input);
        PackageDetailsModel GetPackageById(int IdPkge);
        bool AssignPackages(AssignPackages input);
        List<PackageSuppliersPrice> GetPackageSuppliersPrice(int IdPkge);
    }
}
