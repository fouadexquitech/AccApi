using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
using AccApi.Repository.Models.MasterModels;
using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace AccApi.Repository.Managers
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly MasterDbContext _mdbcontext;
        private readonly GlobalLists _globalLists;
        private readonly AccDbContext _dbcontext;

        private readonly HttpClient _httpClient;
        private IConfiguration _configuration { get; }

        public SupplierRepository(MasterDbContext mdbContext, GlobalLists globalLists, HttpClient httpClient, IConfiguration configuration)
        {
            _globalLists = globalLists;
            _mdbcontext = mdbContext;
            _dbcontext = new AccDbContext(_globalLists.GetAccDbconnectionString());
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public List<Supplier> SupplierList(int packID)
        {
            Models.MasterModels.TblPackage package = (from p in _mdbcontext.TblPackages
                                                      where p.PkgeId == packID
                                                      select p).First();

            //var results=from b in _mdbcontext.TblSuppliers
            //           join d in _mdbcontext.TblSupplierDivs
            //           on b.SupCode  equals d.SupCode
            //           where d.SupDiv == package.Division
            //            orderby b.SupName
            //           select new Supplier
            //           {
            //               SupID = b.SupCode,
            //               SupName = b.SupName,
            //               SupEmail=b.SupEmail
            //           };

            var results = from b in _mdbcontext.TblSuppliers
                          orderby b.SupName
                          select new Supplier
                          {
                              SupID = b.SupCode,
                              SupName = b.SupName,
                              SupEmail = b.SupEmail
                          };

            return results.ToList();
        }

        public List<Supplier> GetSupplierList_NotAssignetPackage(int packID, int portalStatus, string CostConn)
        {
            using (AccDbContext dbContext =
                new AccDbContext(CostConn))
            {
                if (packID <= 0)
                {
                    return new List<Supplier>();
                }

                if (
                    portalStatus != 0 &&
                    portalStatus != 1
                )
                {
                    throw new ArgumentException(
                        "Portal status must be 0 or 1.",
                        nameof(portalStatus)
                    );
                }

                /*
                 * SpSupplierId is nullable in TblSupplierPackages.
                 *
                 * Therefore:
                 * 1. Exclude NULL values.
                 * 2. Use .Value to return List<int>.
                 */
                List<int> assignedSupplierIds =
                    dbContext
                        .TblSupplierPackages
                        .AsNoTracking()
                        .Where(packageSupplier =>
                            packageSupplier.SpPackageId ==
                                packID &&
                            packageSupplier.SpSupplierId
                                .HasValue
                        )
                        .Select(packageSupplier =>
                            packageSupplier.SpSupplierId
                                .Value
                        )
                        .Distinct()
                        .ToList();

                bool requiresPortalAccount =
                    portalStatus == 1;

                /*
                 * Do not explicitly declare:
                 *
                 * IQueryable<TblSupplier>
                 *
                 * because the project contains two classes named
                 * TblSupplier:
                 *
                 * AccApi.Repository.Models.MasterModels.TblSupplier
                 * AccApi.Repository.Models.TblSupplier
                 *
                 * Using var lets the compiler take the correct entity
                 * type directly from _mdbcontext.TblSuppliers.
                 */
                var supplierQuery =
                    _mdbcontext
                        .TblSuppliers
                        .AsNoTracking()
                        .Where(supplier =>
                            !assignedSupplierIds.Contains(
                                supplier.SupCode
                            )
                        );

                /*
                 * Portal supplier group:
                 *
                 * portalStatus = 1
                 * IsAccountCreated = true
                 */
                if (requiresPortalAccount)
                {
                    supplierQuery =
                        supplierQuery.Where(
                            supplier =>
                                supplier.IsAccountCreated ==
                                true
                        );
                }
                else
                {
                    /*
                     * Non-portal supplier group:
                     *
                     * portalStatus = 0
                     * IsAccountCreated = false or NULL
                     */
                    supplierQuery =
                        supplierQuery.Where(
                            supplier =>
                                supplier.IsAccountCreated !=
                                true
                        );
                }

                List<Supplier> results =
                    supplierQuery
                        .Select(supplier =>
                            new Supplier
                            {
                                SupID =
                                    supplier.SupCode,

                                /*
                                 * Supplier name and email are displayed
                                 * once in the dropdown.
                                 */
                                SupName =
                                    (
                                        supplier.SupName ??
                                        string.Empty
                                    ) +
                                    (
                                        string.IsNullOrWhiteSpace(
                                            supplier.SupEmail
                                        )
                                            ? string.Empty
                                            : " \\ " +
                                              supplier.SupEmail
                                    ),

                                SupEmail = supplier.SupEmail ?? string.Empty,

                                IsAccountCreated = supplier.IsAccountCreated == true
                            }
                        )
                        .OrderBy(supplier =>
                            supplier.SupName
                        )
                        .ToList();

                return results;
            }
        }

        public DataTablesResponse<Supplier> GetSuppliers(DataTablesRequest dtRequest)
        {
            var sortColumnName = dtRequest.SortCol;
            var sortDirection = dtRequest.SortDirVal;
            var skip = dtRequest.Start;
            var take = dtRequest.Length;

            var result = (from b in _mdbcontext.TblSuppliers
                          select new Supplier
                          {
                              SupID = b.SupCode,
                              SupName = b.SupName,
                              SupEmail = b.SupEmail,
                              PhoneNumber = b.SupPhone,
                              IsAccountCreated = (b.IsAccountCreated == null) ? false : b.IsAccountCreated
                          }).ToList();

            var totalRecords = result.Count;

            if (dtRequest.SearchVal != null && dtRequest.SearchVal != "")
            {
                result = result.Where(x => string.Concat(x.SupName.ToUpper()).Contains(dtRequest.SearchVal.ToUpper())).ToList();
            }


            var list = result.AsQueryable().OrderBy($"{sortColumnName} {sortDirection}").Skip(skip).Take(take);

            return new DataTablesResponse<Supplier>
            {
                Data = list.ToList(),
                RecordsTotal = totalRecords,
                RecordsFiltered = result.Count
            };
        }

        public bool AddSupplier(List<Supplier> sups)
        {
            foreach (var item in sups)
            {
                var result = new Models.MasterModels.TblSupplier { SupName = item.SupName, SupEmail = item.SupEmail };
                _mdbcontext.Add<Models.MasterModels.TblSupplier>(result);
                _mdbcontext.SaveChanges();
            }

            return true;
        }

        public bool UpdateSupplier(Supplier sup)
        {
            var result = _mdbcontext.TblSuppliers.Where(x => x.SupCode == sup.SupID).FirstOrDefault();
            result.SupName = sup.SupName;
            result.SupEmail = sup.SupEmail;

            if (result != null)
            {
                _mdbcontext.TblSuppliers.Update(result);
                _mdbcontext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool DeleteSupplier(int id)
        {
            var supPackage = _dbcontext.TblSupplierPackages.Where(x => x.SpPackSuppId == id).FirstOrDefault();
            if (supPackage == null)
            {
                var result = _mdbcontext.TblSuppliers.Where(x => x.SupCode == id).FirstOrDefault();
                if (result != null)
                {
                    _mdbcontext.TblSuppliers.Remove(result);
                    _mdbcontext.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public async Task<bool> UpdatePortalAccountFlag(SupplierPortalAccountFlagViewModel model)
        {
            var suppliers = await _mdbcontext.TblSuppliers.Where(x => model.Suppliers.Contains(x.SupCode)).ToListAsync();
            foreach (var sup in suppliers)
            {
                sup.IsAccountCreated = model.AccountCreated;
            }

            _mdbcontext.TblSuppliers.UpdateRange(suppliers);
            await _mdbcontext.SaveChangesAsync();
            return true;
        }


        public async Task<ResponseModel<bool>> Register(List<RegisterModel> model)
        {
            //Post the portal API (Create supplier account on portal database)
            var body = System.Text.Json.JsonSerializer.Serialize(model);
            var portalApiPath = _configuration["PortalApiPath"];
            var key = _configuration["External:Key"];
            var requestContent = new StringContent(body, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", key);
            var response = await _httpClient.PostAsync(portalApiPath + "Account/Register", requestContent);
            response.EnsureSuccessStatusCode();

            dynamic content = await response.Content.ReadAsStringAsync();

            //var jsonObject = System.Text.Json.JsonSerializer.Serialize(content);
            JObject result = JsonConvert.DeserializeObject(content);

            if ((bool)result["success"] == true)
            {
                return new ResponseModel<bool>
                {
                    Success = true,
                    Message = "Registered successfully"
                };
            }
            else
            {
                return new ResponseModel<bool>
                {
                    Success = true,
                    Message = (string)result["message"]
                };
            }

        }
    }

}
