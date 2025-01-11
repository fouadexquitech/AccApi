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
        ProjectCurrency GetProjectCurrency(int projSeq);
        List<EmailTemplate> GetSuppliersEmailTemplate(string Lang, int packId, string projName);
        bool SaveEmailTemplate(int id, string emailbody);
        List<TopManagement> GetManagementEmail(string filter);
        bool AddManagementEmail(List<TopManagement> users);
        bool UpdateManagementEmail(TopManagement user);
        bool DeleteManagementEmail(int id);
        User GetUser(string username);

        bool ConnectToDB(string connString);
        bool hasPermission(string user, string functionId);

        EmailTemplate GetDefaultProjectEmailTemplate(string costDb);

    }
}
