using AccApi.Repository.Interfaces;
using AccApi.Repository.View_Models;
using AccApi.Repository.Models;
using System;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using AccApi.Repository.View_Models.Request;

namespace AccApi.Repository.Managers
{
    public class ComparisonGroupRepository : IComparisonGroupRepository
    {
        private readonly AccDbContext _dbContext;
        private readonly IMapper _mapper;
        public ComparisonGroupRepository(AccDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public bool AddGroup(ComparisonPackageGroupModel ComparisonPackageGroup)
        {
            var newGroup = new ComparisonPackageGroup
            {
               Name = ComparisonPackageGroup.Name,
               CreationDate = DateTime.Now,
               CreationUserId = ComparisonPackageGroup.UserId,
               PackageId = ComparisonPackageGroup.Package?.IDPkge
            };
            _dbContext.Add(newGroup);
            _dbContext.SaveChanges();
                        
            return true;
        }


        public List<GroupingBoqModel> GetBoqList(int packageId, int groupId, SearchInput input)
        {
            List<GroupingBoqModel> boqQuery = (from i in _dbContext.TblOriginalBoqs where i.Scope == packageId
                                               join b in _dbContext.TblBoqs on i.ItemO equals b.BoqItem
                                               select new GroupingBoqModel()
                                              {
                                                  DescriptionO = i.DescriptionO,
                                                  ItemO = i.ItemO,
                                                  BoqDiv = b.BoqDiv
                                                  
                                              }).ToList();

            if (input.BOQDiv.Length > 0) boqQuery = boqQuery.Where(w => input.BOQDiv.Contains(w.BoqDiv)).ToList();
            if (!string.IsNullOrEmpty(input.BOQItem)) boqQuery = boqQuery.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower())).ToList();
            if (!string.IsNullOrEmpty(input.BOQDesc)) boqQuery = boqQuery.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower())).ToList();
            if (!string.IsNullOrEmpty(input.SheetDesc)) boqQuery = boqQuery.Where(w => w.ObSheetDesc == input.SheetDesc).ToList();
            if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) boqQuery = boqQuery.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow)).ToList();
            

            IEnumerable<BoqModel> condQuery = (from b in _dbContext.TblBoqs
                                                  join i in _dbContext.TblOriginalBoqs on b.BoqItem equals i.ItemO
                                                  join r in _dbContext.TblResources on b.BoqResSeq equals r.ResSeq
                                                  where i.Scope == packageId
                                                  select new BoqModel()
                                                  {
                                                      BoqSeq = b.BoqSeq,
                                                      BoqResSeq = b.BoqResSeq,
                                                      BoqCtg = b.BoqCtg,
                                                      BoqUnitMesure = b.BoqUnitMesure,
                                                      BoqQty = b.BoqQty,
                                                      BoqUprice = b.BoqUprice,
                                                      BoqDiv = b.BoqDiv,
                                                      BoqPackage = b.BoqPackage,
                                                      BoqScope = b.BoqScope,
                                                      ResDescription = r.ResDescription,
                                                      BoqItem = b.BoqItem
                                                  });

            List<BoqModel> list = (from c in _dbContext.ComparisonPackageGroups
                        join g in _dbContext.TblBoqs on c.Id equals g.GroupId
                        where c.PackageId == packageId && g.GroupId == groupId
                        select new BoqModel 
                        { 
                            BoqSeq = g.BoqSeq,
                            BoqItem = g.BoqItem,
                            BoqResSeq = g.BoqResSeq
                        }
                        ).ToList();

            if (input.RESDiv.Length > 0) condQuery = condQuery.Where(w => input.RESDiv.Contains(w.BoqDiv));
            if (input.RESType.Length > 0) condQuery = condQuery.Where(w => input.RESType.Contains(w.BoqCtg));
            if (!string.IsNullOrEmpty(input.RESPackage)) condQuery = condQuery.Where(w => w.BoqPackage == input.RESPackage);
            if (!string.IsNullOrEmpty(input.RESDesc)) condQuery = condQuery.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));
            

            foreach (var item in boqQuery)
            {
                item.GroupingResources = condQuery.Where(x => x.BoqItem == item.ItemO).Select(y => new GroupingResourceModel
                {
                    BoqSeq = y.BoqSeq,
                    ResourceSeq = y.BoqResSeq,
                    ResourceDescription = y.ResDescription,
                    IsSelected = (list == null ? false : (list.Where(x=>x.BoqSeq == y.BoqSeq).ToList().Count > 0))
                }).ToList();
            }

            return boqQuery;

        }

        public bool AttachToGroup(int groupId, List<GroupingResourceModel> list)
        {
            foreach (var r in list)
            {
                var boq = _dbContext.TblBoqs.Where(x => x.BoqSeq == r.BoqSeq).FirstOrDefault();
                if (boq != null)
                {
                    boq.GroupId = groupId;
                    _dbContext.SaveChanges();
                }
               
            }
            return true;
        }

        public bool DetachFromGroup(int groupId, List<GroupingResourceModel> list)
        {
            foreach (var r in list)
            {
                var boq = _dbContext.TblBoqs.Where(x => x.BoqSeq == r.BoqSeq && x.GroupId == groupId).FirstOrDefault();
                if (boq != null)
                {
                    boq.GroupId = null;
                    _dbContext.SaveChanges();
                }

            }
            return true;
        }

        public List<ComparisonPackageGroupModel> GetGroups(int packageId)
        { 
           var lst = _dbContext.ComparisonPackageGroups.Where(x=>x.PackageId == packageId).Select(x => new ComparisonPackageGroupModel { 
            Id = x.Id,
            Name = x.Name,
            Package = new PackageList { IDPkge = packageId }
           } ).ToList();

            return lst;
        }

    }
}
