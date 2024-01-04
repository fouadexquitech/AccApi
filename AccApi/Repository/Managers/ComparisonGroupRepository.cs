using AccApi.Repository.Interfaces;
using AccApi.Repository.View_Models;
using AccApi.Repository.Models;
using System;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using AccApi.Repository.View_Models.Request;
using Microsoft.EntityFrameworkCore;

namespace AccApi.Repository.Managers
{
    public class ComparisonGroupRepository : IComparisonGroupRepository
    {
        private readonly AccDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly GlobalLists _globalLists;

        public ComparisonGroupRepository(AccDbContext dbContext, IMapper mapper, GlobalLists globalLists)
        { 
            _mapper = mapper;
            _globalLists = globalLists;
            _dbContext = new AccDbContext(_globalLists.GetAccDbconnectionString());   
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
            IEnumerable<BoqRessourcesList> condQuery = (from o in _dbContext.TblOriginalBoqs
                                                        join b in _dbContext.TblBoqs on o.ItemO equals b.BoqItem
                                                        join r in _dbContext.TblResources on b.BoqResSeq equals r.ResSeq
                                                        where o.Scope == packageId
                                                        select new BoqRessourcesList
                                                        {
                                                            RowNumber = o.RowNumber,
                                                            SectionO = o.SectionO,
                                                            ItemO = o.ItemO,
                                                            DescriptionO = o.DescriptionO,
                                                            UnitO = o.UnitO,
                                                            QtyO = o.QtyO,
                                                            UnitRateO = o.UnitRate,
                                                            ScopeO = o.Scope,
                                                            BoqSeq = b.BoqSeq,
                                                            BoqCtg = b.BoqCtg,
                                                            BoqUnitMesure = b.BoqUnitMesure,
                                                            BoqQty = b.BoqQty,
                                                            BoqUprice = b.BoqUprice,
                                                            BoqDiv = b.BoqDiv,
                                                            BoqPackage = b.BoqPackage,
                                                            BoqScope = b.BoqScope,
                                                            ResSeq = r.ResSeq,
                                                            ResDescription = r.ResDescription
                                                        });

            if (input.BOQDiv.Length > 0) condQuery = condQuery.Where(w => input.BOQDiv.Contains(w.SectionO));
            if (!string.IsNullOrEmpty(input.BOQItem)) condQuery = condQuery.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower()));
            if (!string.IsNullOrEmpty(input.BOQDesc)) condQuery = condQuery.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower()));
            if (!string.IsNullOrEmpty(input.SheetDesc)) condQuery = condQuery.Where(w => w.ObSheetDesc == input.SheetDesc);
            if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) condQuery = condQuery.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
            if (input.Package > 0) condQuery = condQuery.Where(w => w.ScopeO == input.Package);
            if (input.RESDiv.Length > 0) condQuery = condQuery.Where(w => input.RESDiv.Contains(w.BoqDiv));
            if (input.RESType.Length > 0) condQuery = condQuery.Where(w => input.RESType.Contains(w.BoqCtg));
            if (!string.IsNullOrEmpty(input.RESDesc)) condQuery = condQuery.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));

            var items = condQuery
                .GroupBy(x => new { x.RowNumber, x.SectionO, x.ItemO, x.DescriptionO, x.UnitO })
                .Select(p => p.FirstOrDefault())
                .Select(p => new GroupingBoqModel
                {
                    ItemO = p.ItemO,
                    DescriptionO = p.DescriptionO,
                    RowNumber = p.RowNumber.Value
                }).ToList();


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


            foreach (var item in items)
            {
                 item.GroupingResources = condQuery.Where(x => x.ItemO == item.ItemO).Select(y => new GroupingResourceModel
                {
                    BoqSeq = y.BoqSeq,
                    ResourceSeq = y.ResSeq,
                    ResourceDescription = y.ResDescription,
                    IsSelected = (list == null ? false : (list.Where(x=>x.BoqSeq == y.BoqSeq).ToList().Count > 0))
                }).ToList();
            }

            return items;
        }

        public List<GroupingBoqModel> GetBoqListOnly(int packageId, int groupId, SearchInput input)
        {
            IEnumerable<BoqRessourcesList> condQuery = (from o in _dbContext.TblOriginalBoqs
                                                        join b in _dbContext.TblBoqs on o.ItemO equals b.BoqItem
                                                        join r in _dbContext.TblResources on b.BoqResSeq equals r.ResSeq
                                                        where o.Scope == packageId
                                                        select new BoqRessourcesList
                                                        {
                                                            RowNumber = o.RowNumber,
                                                            SectionO = o.SectionO,
                                                            ItemO = o.ItemO,
                                                            DescriptionO = o.DescriptionO,
                                                            UnitO = o.UnitO,
                                                            QtyO = o.QtyO,
                                                            UnitRateO = o.UnitRate,
                                                            ScopeO = o.Scope,
                                                            BoqSeq = b.BoqSeq,
                                                            BoqCtg = b.BoqCtg,
                                                            BoqUnitMesure = b.BoqUnitMesure,
                                                            BoqQty = b.BoqQty,
                                                            BoqUprice = b.BoqUprice,
                                                            BoqDiv = b.BoqDiv,
                                                            BoqPackage = b.BoqPackage,
                                                            BoqScope = b.BoqScope,
                                                            ResSeq = r.ResSeq,
                                                            ResDescription = r.ResDescription
                                                        });

            if (input.BOQDiv.Length > 0) condQuery = condQuery.Where(w => input.BOQDiv.Contains(w.SectionO));
            if (!string.IsNullOrEmpty(input.BOQItem)) condQuery = condQuery.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower()));
            if (!string.IsNullOrEmpty(input.BOQDesc)) condQuery = condQuery.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower()));
            if (!string.IsNullOrEmpty(input.SheetDesc)) condQuery = condQuery.Where(w => w.ObSheetDesc == input.SheetDesc);
            if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) condQuery = condQuery.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
            if (input.Package > 0) condQuery = condQuery.Where(w => w.ScopeO == input.Package);
            if (input.RESDiv.Length > 0) condQuery = condQuery.Where(w => input.RESDiv.Contains(w.BoqDiv));
            if (input.RESType.Length > 0) condQuery = condQuery.Where(w => input.RESType.Contains(w.BoqCtg));
            if (!string.IsNullOrEmpty(input.RESDesc)) condQuery = condQuery.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));



            var items = condQuery
                .GroupBy(x => new { x.RowNumber, x.SectionO, x.ItemO, x.DescriptionO, x.UnitO })
                .Select(p => p.FirstOrDefault())
                .Select(p => new GroupingBoqModel
                {
                    ItemO = p.ItemO,
                    DescriptionO = p.DescriptionO,
                    RowNumber = p.RowNumber.Value
                }).ToList();



            List<BoqModel> listByBoq = (from c in _dbContext.ComparisonPackageGroups
                                        join g in _dbContext.TblOriginalBoqs on c.Id equals g.GroupId
                                        where c.PackageId == packageId && g.GroupId == groupId
                                        select new BoqModel
                                        {
                                            BoqItem = g.ItemO
                                        }
                        ).ToList();

            


            foreach (var item in items)
            {
                item.IsSelected = (listByBoq == null ? false : (listByBoq.Where(x => x.BoqItem == item.ItemO).ToList().Count > 0));
                
            }

            return items;

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

        public bool AttachToGroupByBoq(int groupId, List<GroupingBoqModel> list)
        {
            foreach (var r in list)
            {
                var boq = _dbContext.TblOriginalBoqs.Where(x => x.ItemO == r.ItemO).FirstOrDefault();
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

        public bool DetachFromGroupByBoq(int groupId, List<GroupingBoqModel> list)
        {
            foreach (var r in list)
            {
                var boq = _dbContext.TblOriginalBoqs.Where(x => x.ItemO == r.ItemO && x.GroupId == groupId).FirstOrDefault();
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
            Package = new Package { IDPkge = packageId }
           } ).ToList();

            return lst;
        }

    }
}
