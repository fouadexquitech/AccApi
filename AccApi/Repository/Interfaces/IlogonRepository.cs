using AccApi.Repository.Models.MasterModels;
using AccApi.Repository.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.Interfaces
{
    public interface IlogonRepository
    {
        List<ProjectCountries> GetProjectCountries();
        List<Project> GetProjects(int dbSeq);
        User GetLogin(string user, string pass, int projSeq);
        ProjectCurrency GetProjectCurrency();
        EmailTemplate GetSuppliersEmailTemplate(string Lang);
        bool SaveEmailTemplate(int id, string emailbody);
        List<TopManagement> GetManagementEmail();
    }
}
