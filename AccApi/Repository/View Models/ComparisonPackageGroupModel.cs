using System.Collections.Generic;

namespace AccApi.Repository.View_Models
{
    public class ComparisonPackageGroupModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public PackageList? Package { get; set; }

        public string UserId  { get; set; }

    }
}
