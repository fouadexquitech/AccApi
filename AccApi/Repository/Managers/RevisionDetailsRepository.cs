using AccApi.Data_Layer;
using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
using AccApi.Repository.Models.MasterModels;
using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nancy.Extensions;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AccApi.Repository.Managers
{
    public class RevisionDetailsRepository : IRevisionDetailsRepository
    {
        private AccDbContext _dbContext;
        private PolicyDbContext _pdbContext;
        private MasterDbContext _mdbContext;
        private readonly IlogonRepository _logonRepository;
        private readonly GlobalLists _globalLists;

        MasterDbContext mdbcontext;
        IConfiguration configuration;

        public RevisionDetailsRepository(AccDbContext dbContext, PolicyDbContext pdbContext, MasterDbContext mdbContext, IlogonRepository logonRepository, GlobalLists globalLists)
        {
            //_dbContext = dbContext;
            //_pdbContext = pdbContext;
            _mdbContext = mdbContext;
            _logonRepository = logonRepository;
            _globalLists = globalLists;
            _dbContext = new AccDbContext(_globalLists.GetAccDbconnectionString());
            _pdbContext = new PolicyDbContext(_globalLists.GetTimeSheetDbconnectionString());
        }

        public List<LevelModel> GetRevisionDetails(int RevisionId, string itemDesc, string resource)
        {
            var supPackRev = _dbContext.TblSupplierPackageRevisions.SingleOrDefault(b => (b.PrRevId == RevisionId));
            int PackageSuppliersID = (int)supPackRev.PrPackSuppId;

            var supPack = _dbContext.TblSupplierPackages.Where(x => x.SpPackSuppId == PackageSuppliersID).FirstOrDefault();
            byte byBoq = (byte)((supPack.SpByBoq == null) ? 0 : supPack.SpByBoq);

            List<RevisionDetailsList> revDetailList = new List<RevisionDetailsList>();

            var curList = (from b in _mdbContext.TblCurrencies
                           select b).ToList();

            if (byBoq == 1)
            {
                var revDtlQry = (from bb in _dbContext.TblSupplierPackageRevisions 
                                 join a in _dbContext.TblSupplierPackages on bb.PrPackSuppId equals a.SpPackSuppId
                                 join b in _dbContext.TblRevisionDetails on bb.PrRevId equals b.RdRevisionId
                                 join o in _dbContext.TblOriginalBoqVds on b.RdBoqItem equals o.ItemO
                                 where b.RdRevisionId == RevisionId && (b.IsNew == false || b.IsNew == null)
                                 && (b.IsAlternative == false || b.IsAlternative == null)
                                 && (itemDesc == null || o.DescriptionO.ToUpper().Contains(itemDesc.ToUpper()))
                                 select new RevisionDetailsList
                                 {
                                     RdRevisionId = b.RdRevisionId,
                                     RdResourceSeq = b.RdResourceSeq,
                                     RdPrice = b.RdPrice,
                                     RdMissedPrice = b.RdMissedPrice,
                                     RdBoqItem = b.RdBoqItem,
                                     RdItemDescription = o.DescriptionO,
                                     RdQty = b.RdQty,
                                     RdQuotationQty = b.RdQuotationQty,
                                     RdUnitRate = o.UnitRate,
                                     RdTotalBudget = o.Submitted,
                                     ExchangeRate = bb.PrExchRate,
                                     RdOriginalPrice = b.RdPriceOrigCurrency,
                                     TotalSupplierPrice = b.RdAssignedPrice,
                                     currency = bb.PrCurrency.ToString(),
                                     RdMissedPriceReason = b.RdMissedPriceReason,
                                     RdDiscount = ((b.RdDiscount == null) ? 0 : b.RdDiscount),
                                     RdPriceAfterDiscount = Math.Round((double)(b.RdPrice - (b.RdPrice * ((b.RdDiscount == null) ? 0 : b.RdDiscount) / 100)), 3),
                                     RdTotalPrice = Math.Round((double)((b.RdPrice - (b.RdPrice * ((b.RdDiscount == null) ? 0 : b.RdDiscount) / 100)) * b.RdQty), 3),
                                     RdAddedItem = b.RdAddedItem,
                                     RdAddedItemOn = b.RdAddedItemOn,
                                     IsAlternative = b.IsAlternative,
                                     IsNew = b.IsNew,
                                     NewItemId = b.NewItemId,
                                     NewItemResourceId = b.NewItemResourceId,
                                     ParentItemO = b.ParentItemO,
                                     ParentResourceId = b.ParentResourceId,
                                     Unit = o.UnitO,
                                     Comments = b.RdComment,
                                     //L1 = o.L1,
                                     L2 = o.L2,
                                     L3 = o.L3,
                                     L4 = o.L4,
                                     L5 = o.L5,
                                     L6 = o.L6,
                                     C1 = o.C1,
                                     C2 = o.C2,
                                     C3 = o.C3,
                                     C4 = o.C4,
                                     C5 = o.C5,
                                     C6 = o.C6
                                 }).ToList();

                var revDtlQryNew = (from bb in _dbContext.TblSupplierPackageRevisions 
                                    join a in _dbContext.TblSupplierPackages on bb.PrPackSuppId equals a.SpPackSuppId
                                    join b in _dbContext.TblRevisionDetails on bb.PrRevId equals b.RdRevisionId
                                    join item in _dbContext.NewItems on b.NewItemId equals item.Id
                                    where b.RdRevisionId == RevisionId && (b.ItemCopiedFromRevision == 0 || b.ItemCopiedFromRevision == null)
                                    && (itemDesc == null || item.ItemDescription.ToUpper().Contains(itemDesc.ToUpper()))
                                    select new RevisionDetailsList
                                    {
                                        RdRevisionId = b.RdRevisionId,
                                        RdResourceSeq = b.RdResourceSeq,
                                        RdPrice = b.RdPrice,
                                        RdMissedPrice = b.RdMissedPrice,
                                        RdBoqItem = b.RdBoqItem,
                                        RdItemDescription = item.ItemDescription,
                                        RdQty = b.RdQty,
                                        RdQuotationQty = b.RdQuotationQty,
                                        RdUnitRate = b.RdPrice,
                                        RdTotalBudget = 0,
                                        ExchangeRate = bb.PrExchRate,
                                        RdOriginalPrice = b.RdPriceOrigCurrency,
                                        TotalSupplierPrice = b.RdAssignedPrice,
                                        currency = bb.PrCurrency.ToString(),
                                        RdMissedPriceReason = b.RdMissedPriceReason,
                                        RdDiscount = ((b.RdDiscount == null) ? 0 : b.RdDiscount),
                                        RdPriceAfterDiscount = Math.Round((double)(b.RdPrice - (b.RdPrice * ((b.RdDiscount == null) ? 0 : b.RdDiscount) / 100)), 3),
                                        RdTotalPrice = Math.Round((double)((b.RdPrice - (b.RdPrice * ((b.RdDiscount == null) ? 0 : b.RdDiscount) / 100)) * b.RdQty), 3),
                                        RdAddedItem = b.RdAddedItem,
                                        RdAddedItemOn = b.RdAddedItemOn,
                                        IsAlternative = b.IsAlternative,
                                        IsNew = b.IsNew,
                                        NewItemId = b.NewItemId,
                                        NewItemResourceId = b.NewItemResourceId,
                                        ParentItemO = b.ParentItemO,
                                        ParentResourceId = b.ParentResourceId,
                                        Unit = item.UnitO,
                                        Comments = b.RdComment,
                                        //L1 = item.L1,
                                        L2 = item.L2,
                                        L3 = item.L3,
                                        L4 = item.L4,
                                        L5 = item.L5,
                                        L6 = item.L6,
                                        C1 = item.C1,
                                        C2 = item.C2,
                                        C3 = item.C3,
                                        C4 = item.C4,
                                        C5 = item.C5,
                                        C6 = item.C6
                                    }).ToList();

                var revDtlQryAlt = (from bb in _dbContext.TblSupplierPackageRevisions
                                               join a in _dbContext.TblSupplierPackages on bb.PrPackSuppId equals a.SpPackSuppId
                                               join b in _dbContext.TblRevisionDetails on bb.PrRevId equals b.RdRevisionId
                                               join o in _dbContext.TblOriginalBoqVds on b.ParentItemO equals o.ItemO
                                               where (b.RdRevisionId == RevisionId && b.IsAlternative == true) && (b.ItemCopiedFromRevision == 0 || b.ItemCopiedFromRevision == null)
                                              && (itemDesc == null || b.ItemDescription.ToUpper().Contains(itemDesc.ToUpper()))
                                               select new RevisionDetailsList
                                               {
                                                   RdRevisionId=b.RdRevisionId,
                                                   RdResourceSeq = b.RdResourceSeq,
                                                   RdPrice = b.RdPrice,
                                                   RdMissedPrice = b.RdMissedPrice,
                                                   RdBoqItem = b.RdBoqItem,
                                                   RdItemDescription = b.ItemDescription,
                                                   RdQty = b.RdQty,
                                                   RdQuotationQty = b.RdQuotationQty,
                                                   RdUnitRate = o.UnitRate,
                                                   RdTotalBudget = o.Submitted,
                                                   ExchangeRate = bb.PrExchRate,
                                                   RdOriginalPrice = b.RdPriceOrigCurrency,
                                                   TotalSupplierPrice = b.RdAssignedPrice,
                                                   currency = bb.PrCurrency.ToString(),
                                                   RdMissedPriceReason = b.RdMissedPriceReason,
                                                   RdDiscount = ((b.RdDiscount == null) ? 0 : b.RdDiscount),
                                                   RdPriceAfterDiscount = Math.Round((double)(b.RdPrice - (b.RdPrice * ((b.RdDiscount == null) ? 0 : b.RdDiscount) / 100)), 3),
                                                   RdTotalPrice = Math.Round((double)((b.RdPrice - (b.RdPrice * ((b.RdDiscount == null) ? 0 : b.RdDiscount) / 100)) * b.RdQty), 3),
                                                   RdAddedItem = b.RdAddedItem,
                                                   RdAddedItemOn = b.RdAddedItemOn,
                                                   IsAlternative = b.IsAlternative,
                                                   IsNew = b.IsNew,
                                                   NewItemId = b.NewItemId,
                                                   NewItemResourceId = b.NewItemResourceId,
                                                   ParentItemO = b.ParentItemO,
                                                   ParentResourceId = b.ParentResourceId,
                                                   Unit = o.UnitO,
                                                   Comments=b.RdComment,
                                                   //L1 = o.L1,
                                                   L2 = o.L2,
                                                   L3 = o.L3,
                                                   L4 = o.L4,
                                                   L5 = o.L5,
                                                   L6 = o.L6,
                                                   C1 = o.C1,
                                                   C2 = o.C2,
                                                   C3 = o.C3,
                                                   C4 = o.C4,
                                                   C5 = o.C5,
                                                   C6 = o.C6
                                               }).ToList();

               revDetailList = revDtlQry;

               foreach (var itm in revDtlQryNew)
                    revDetailList.Add(itm);
               foreach (var itm in revDtlQryAlt)
                    revDetailList.Add(itm);
            }
            else
            {
                var revDtl = (from bb in _dbContext.TblSupplierPackageRevisions 
                             join a in _dbContext.TblSupplierPackages on bb.PrPackSuppId equals a.SpPackSuppId
                             join b in _dbContext.TblRevisionDetails on bb.PrRevId equals b.RdRevisionId
                             join c in _dbContext.TblBoqVds on b.RdResourceSeq equals c.BoqSeq
                             join o in _dbContext.TblOriginalBoqVds on c.BoqItem equals o.ItemO
                             join e in _dbContext.TblResources on c.BoqResSeq equals e.ResSeq
                             where (b.RdRevisionId == RevisionId) 
                             && (itemDesc == null || o.DescriptionO.ToUpper().Contains(itemDesc.ToUpper()))
                             && (resource == null || e.ResDescription.ToUpper().Contains(resource.ToUpper()))
                             select new RevisionDetailsList
                             {
                                 RdRevisionId = b.RdRevisionId,
                                 RdResourceSeq = b.RdResourceSeq,
                                 RdPrice = b.RdPrice,
                                 RdMissedPrice = b.RdMissedPrice,
                                 RdBoqItem = o.ItemO,
                                 RdBoqItemDescription = o.DescriptionO,
                                 RdItemDescription = e.ResDescription,
                                 RdQty = b.RdQty,
                                 RdQuotationQty = b.RdQuotationQty,
                                 RdUnitRate = c.BoqUprice,
                                 RdTotalBudget = (b.RdQty) * (c.BoqUprice),
                                 ExchangeRate = bb.PrExchRate,
                                 RdOriginalPrice = b.RdPriceOrigCurrency,
                                 TotalSupplierPrice = b.RdAssignedPrice,
                                 currency =  bb.PrCurrency.ToString(),
                                 RdMissedPriceReason = b.RdMissedPriceReason,
                                 RdDiscount = ((b.RdDiscount == null) ? 0 : b.RdDiscount),
                                 RdPriceAfterDiscount = Math.Round((double)(b.RdPrice - (b.RdPrice * ((b.RdDiscount == null) ? 0 : b.RdDiscount) / 100)), 3),
                                 RdTotalPrice = Math.Round((double)((b.RdPrice - (b.RdPrice * ((b.RdDiscount == null) ? 0 : b.RdDiscount) / 100)) * b.RdQty), 3),
                                 RdAddedItem = b.RdAddedItem,
                                 RdAddedItemOn = b.RdAddedItemOn,
                                 IsAlternative = b.IsAlternative,
                                 IsNew = b.IsNew,
                                 NewItemId = b.NewItemId,
                                 NewItemResourceId = b.NewItemResourceId,
                                 ParentItemO = b.ParentItemO,
                                 ParentResourceId = b.ParentResourceId,
                                 Unit = o.UnitO,
                                 Comments = b.RdComment,
                                 //L1 = o.L1,
                                 L2 = o.L2,
                                 L3 = o.L3,
                                 L4 = o.L4,
                                 L5 = o.L5,
                                 L6 = o.L6,
                                 C1 = o.C1,
                                 C2 = o.C2,
                                 C3 = o.C3,
                                 C4 = o.C4,
                                 C5 = o.C5,
                                 C6 = o.C6
                             }).ToList();

                var revDtlNew= (from bb in _dbContext.TblSupplierPackageRevisions
                                join a in _dbContext.TblSupplierPackages on bb.PrPackSuppId equals a.SpPackSuppId
                                join b in _dbContext.TblRevisionDetails on bb.PrRevId equals b.RdRevisionId
                                join item in _dbContext.NewItems on b.NewItemId equals item.Id
                                join newr in _dbContext.NewItemResources on b.NewItemResourceId equals newr.Id
                                //join item in _dbContext.NewItemResources on b.NewItemResourceId equals item.Id
                                //join o in _dbContext.TblOriginalBoqVds on b.RdBoqItem equals o.ItemO
                                where b.RdRevisionId == RevisionId
                                && (itemDesc == null || b.ItemDescription.ToUpper().Contains(itemDesc.ToUpper()))
                                && (resource == null || b.ResourceDescription.ToUpper().Contains(resource.ToUpper()))
                               select new RevisionDetailsList
                                {
                                    RdRevisionId = b.RdRevisionId,
                                    RdResourceSeq = b.RdResourceSeq,
                                    RdPrice = b.RdPrice,
                                    RdMissedPrice = b.RdMissedPrice,
                                    RdBoqItem = b.RdBoqItem,
                                    RdBoqItemDescription = b.ItemDescription,
                                    RdItemDescription = b.ResourceDescription,
                                    RdQty = b.RdQty,
                                    RdQuotationQty = b.RdQuotationQty,
                                    RdUnitRate = b.RdPrice,
                                    RdTotalBudget = (b.RdQty) * (b.RdPrice),
                                    ExchangeRate = bb.PrExchRate,
                                    RdOriginalPrice = b.RdPriceOrigCurrency,
                                    TotalSupplierPrice = b.RdAssignedPrice,
                                    currency = bb.PrCurrency.ToString(),
                                    RdMissedPriceReason = b.RdMissedPriceReason,
                                    RdDiscount = ((b.RdDiscount == null) ? 0 : b.RdDiscount),
                                    RdPriceAfterDiscount = Math.Round((double)(b.RdPrice - (b.RdPrice * ((b.RdDiscount == null) ? 0 : b.RdDiscount) / 100)), 3),
                                    RdTotalPrice = Math.Round((double)((b.RdPrice - (b.RdPrice * ((b.RdDiscount == null) ? 0 : b.RdDiscount) / 100)) * b.RdQty), 3),
                                    RdAddedItem = b.RdAddedItem,
                                    RdAddedItemOn = b.RdAddedItemOn,
                                    IsAlternative = b.IsAlternative,
                                    IsNew = b.IsNew,
                                    NewItemId = b.NewItemId,
                                    NewItemResourceId = b.NewItemResourceId,
                                    ParentItemO = b.ParentItemO,
                                    ParentResourceId = b.ParentResourceId,
                                    Unit = newr.ResourceUnit,
                                    Comments = b.RdComment,
                                    //L1 = item.L1,
                                    L2 = item.L2,
                                    L3 = item.L3,
                                    L4 = item.L4,
                                    L5 = item.L5,
                                    L6 = item.L6,
                                    C1 = item.C1,
                                    C2 = item.C2,
                                    C3 = item.C3,
                                    C4 = item.C4,
                                    C5 = item.C5,
                                    C6 = item.C6
                                }).ToList();

                var revDtlNewRes=(from bb in _dbContext.TblSupplierPackageRevisions 
                                join a in _dbContext.TblSupplierPackages on bb.PrPackSuppId equals a.SpPackSuppId
                                join b in _dbContext.TblRevisionDetails on bb.PrRevId equals b.RdRevisionId
                                join o in _dbContext.TblOriginalBoqVds on b.RdBoqItem equals o.ItemO
                                join newr in _dbContext.NewItemResources on b.NewItemResourceId equals newr.Id
                                //join item in _dbContext.NewItemResources on b.NewItemResourceId equals item.Id
                                //join n in _dbContext.NewItems on b.NewItemId equals n.Id
                                where b.RdRevisionId == RevisionId
                                && (itemDesc == null || b.ItemDescription.ToUpper().Contains(itemDesc.ToUpper()))
                                && (resource == null || b.ResourceDescription.ToUpper().Contains(resource.ToUpper()))
                                select new RevisionDetailsList
                                {
                                    RdRevisionId = b.RdRevisionId,
                                    RdResourceSeq = b.RdResourceSeq,
                                    RdPrice = b.RdPrice,
                                    RdMissedPrice = b.RdMissedPrice,
                                    RdBoqItem = b.RdBoqItem,
                                    RdBoqItemDescription = b.ItemDescription,
                                    RdItemDescription = b.ResourceDescription,
                                    RdQty = b.RdQty,
                                    RdQuotationQty = b.RdQuotationQty,
                                    RdUnitRate = b.RdPrice,
                                    RdTotalBudget = (b.RdQty) * (b.RdPrice),
                                    ExchangeRate = bb.PrExchRate,
                                    RdOriginalPrice = b.RdPriceOrigCurrency,
                                    TotalSupplierPrice = b.RdAssignedPrice,
                                    currency = bb.PrCurrency.ToString(),
                                    RdMissedPriceReason = b.RdMissedPriceReason,
                                    RdDiscount = ((b.RdDiscount == null) ? 0 : b.RdDiscount),
                                    RdPriceAfterDiscount = Math.Round((double)(b.RdPrice - (b.RdPrice * ((b.RdDiscount == null) ? 0 : b.RdDiscount) / 100)), 3),
                                    RdTotalPrice = Math.Round((double)((b.RdPrice - (b.RdPrice * ((b.RdDiscount == null) ? 0 : b.RdDiscount) / 100)) * b.RdQty), 3),
                                    RdAddedItem = b.RdAddedItem,
                                    RdAddedItemOn = b.RdAddedItemOn,
                                    IsAlternative = b.IsAlternative,
                                    IsNew = b.IsNew,
                                    NewItemId = b.NewItemId,
                                    NewItemResourceId = b.NewItemResourceId,
                                    ParentItemO = b.ParentItemO,
                                    ParentResourceId = b.ParentResourceId,
                                    Unit = o.UnitO,
                                    Comments = b.RdComment,
                                    //L1 = o.L1,
                                    L2 = o.L2,
                                    L3 = o.L3,
                                    L4 = o.L4,
                                    L5 = o.L5,
                                    L6 = o.L6,
                                    C1 = o.C1,
                                    C2 = o.C2,
                                    C3 = o.C3,
                                    C4 = o.C4,
                                    C5 = o.C5,
                                    C6 = o.C6
                                }).ToList();

                var revDtlAlt = (from bb in _dbContext.TblSupplierPackageRevisions
                                 join a in _dbContext.TblSupplierPackages on bb.PrPackSuppId equals a.SpPackSuppId
                                 join b in _dbContext.TblRevisionDetails on bb.PrRevId equals b.RdRevisionId
                                 join o in _dbContext.TblOriginalBoqVds on b.RdBoqItem equals o.ItemO
                                 where (b.RdRevisionId == RevisionId && b.IsAlternative == true)
                                 && (itemDesc == null || b.ItemDescription.ToUpper().Contains(itemDesc.ToUpper()))
                                 && (resource == null || b.ResourceDescription.ToUpper().Contains(resource.ToUpper()))
                                 select new RevisionDetailsList
                                 {
                                     RdRevisionId = b.RdRevisionId,
                                     RdResourceSeq = b.RdResourceSeq,
                                     RdPrice = b.RdPrice,
                                     RdMissedPrice = b.RdMissedPrice,
                                     RdBoqItem = b.RdBoqItem,
                                     RdBoqItemDescription = b.ItemDescription,
                                     RdItemDescription = b.ResourceDescription,
                                     RdQty = b.RdQty,
                                     RdQuotationQty = b.RdQuotationQty,
                                     RdUnitRate = b.RdPrice,
                                     RdTotalBudget = (b.RdQty) * (b.RdPrice),
                                     ExchangeRate = bb.PrExchRate,
                                     RdOriginalPrice = b.RdPriceOrigCurrency,
                                     TotalSupplierPrice = b.RdAssignedPrice,
                                     currency = bb.PrCurrency.ToString(),
                                     RdMissedPriceReason = b.RdMissedPriceReason,
                                     RdDiscount = ((b.RdDiscount == null) ? 0 : b.RdDiscount),
                                     RdPriceAfterDiscount = Math.Round((double)(b.RdPrice - (b.RdPrice * ((b.RdDiscount == null) ? 0 : b.RdDiscount) / 100)), 3),
                                     RdTotalPrice = Math.Round((double)((b.RdPrice - (b.RdPrice * ((b.RdDiscount == null) ? 0 : b.RdDiscount) / 100)) * b.RdQty), 3),
                                     RdAddedItem = b.RdAddedItem,
                                     RdAddedItemOn = b.RdAddedItemOn,
                                     IsAlternative = b.IsAlternative,
                                     IsNew = b.IsNew,
                                     NewItemId = b.NewItemId,
                                     NewItemResourceId = b.NewItemResourceId,
                                     ParentItemO = b.ParentItemO,
                                     ParentResourceId = b.ParentResourceId,
                                     Unit = o.UnitO,
                                     Comments = b.RdComment,
                                     //L1 = o.L1,
                                     L2 = o.L2,
                                     L3 = o.L3,
                                     L4 = o.L4,
                                     L5 = o.L5,
                                     L6 = o.L6,
                                     C1 = o.C1,
                                     C2 = o.C2,
                                     C3 = o.C3,
                                     C4 = o.C4,
                                     C5 = o.C5,
                                     C6 = o.C6
                                 }).ToList();

                 revDetailList = revDtl;

                //foreach (var itm in revDtl)
                //    revDetailList.Add(itm);
                foreach (var itm in revDtlNew)
                    revDetailList.Add(itm);
                foreach (var itm in revDtlNewRes)
                    revDetailList.Add(itm);
                foreach (var itm in revDtlAlt)
                    revDetailList.Add(itm);             
            }

            foreach (var itm in revDetailList)
            {
                itm.currency = curList.Find(x => x.CurId.ToString() == itm.currency).CurCode;
            }
           


                var levels = revDetailList.Select(x => new LevelModel
            {
                LevelName = //(x.L1 != null ? "L1~" + x.L1 : "") +
                        (x.L2 != null ? "L2~" + x.L2 : "") +
                        (x.L3 != null ? "|L3~" + x.L3 : "") +
                        (x.L4 != null ? "|L4~" + x.L4 : "") +
                        (x.L5 != null ? "|L5~" + x.L5 : "") +
                        (x.L6 != null ? "|L6~" + x.L6 : "") +
                        (x.C1 != null ? "|C1~" + x.C1 : "") +
                        (x.C2 != null ? "|C2~" + x.C2 : "") +
                        (x.C3 != null ? "|C3~" + x.C3 : "") +
                        (x.C4 != null ? "|C4~" + x.C4 : "") +
                        (x.C5 != null ? "|C5~" + x.C5 : "") +
                        (x.C6 != null ? "|C6~" + x.C6 : "") 
            }).DistinctBy(x => x.LevelName).OrderBy(x => x.LevelName).ToList();


            if (levels != null)
            {
                foreach (var level in levels)
                {
                    level.Items = revDetailList.Select(x => new RevisionDetailsList
                    {
                        RdRevisionId = x.RdRevisionId,
                        RdBoqItem = x.RdBoqItem,//x.IsAlternative.HasValue && x.IsAlternative.Value ? (x.RdBoqItem + "-a" + x.RdRevisionId) : x.RdBoqItem,
                        RdBoqItemDescription=x.RdBoqItemDescription,
                        RdItemDescription = x.RdItemDescription,
                        Unit = x.Unit,
                        RdQty = x.RdQty,
                        RdQuotationQty = x.RdQuotationQty,
                        RdUnitRate = x.RdUnitRate,
                        RdPrice=x.RdPrice,
                        RdDiscount = x.RdDiscount,
                        RdPriceAfterDiscount = x.RdPriceAfterDiscount,
                        RdTotalPrice = x.RdTotalPrice,
                        Comments = x.Comments,
                        IsAlternative = x.IsAlternative,
                        IsNew = x.IsNew,
                        LevelName = //(x.L1 != null ? "L1~" + x.L1 : "") +
                             (x.L2 != null ? "L2~" + x.L2 : "") +
                             (x.L3 != null ? "|L3~" + x.L3 : "") +
                             (x.L4 != null ? "|L4~" + x.L4 : "") +
                             (x.L5 != null ? "|L5~" + x.L5 : "") +
                             (x.L6 != null ? "|L6~" + x.L6 : "") +
                             (x.C1 != null ? "|C1~" + x.C1 : "") +
                             (x.C2 != null ? "|C2~" + x.C2 : "") +
                             (x.C3 != null ? "|C3~" + x.C3 : "") +
                             (x.C4 != null ? "|C4~" + x.C4 : "") +
                             (x.C5 != null ? "|C5~" + x.C5 : "") +
                             (x.C6 != null ? "|C6~" + x.C6 : "") 

                    }).Where(x => x.LevelName == level.LevelName).OrderBy(a=>a.RdBoqItem).ToList();
                }
            }

            return levels;
            //return revDtlQry.ToList();
        }

        public bool AddRevision(int PackageSupplierId, DateTime PackSuppDate, IFormFile ExcelFile, int curId, double ExchRate,double discount,byte addedItem)
        {
            if (addedItem != 1)  //add Items to Last Revision
            {
                int LastRevNo = GetMaxRevisionNumber(PackageSupplierId);

                if (LastRevNo != -1)
                {
                    int i = LastRevNo;
                    do
                    {
                        var res = _dbContext.TblSupplierPackageRevisions.SingleOrDefault(b => b.PrRevNo == i && b.PrPackSuppId == PackageSupplierId);
                        if (res != null)
                        {
                            res.PrRevNo = i + 1;
                            _dbContext.SaveChanges();
                        }
                        i--;
                    }
                    while (i >= 0);
                }

                var result = new TblSupplierPackageRevision { PrRevNo = 0, PrPackSuppId = PackageSupplierId, PrTotPrice = 0, PrRevDate = PackSuppDate, PrCurrency = curId, PrExchRate = ExchRate };
                _dbContext.Add<TblSupplierPackageRevision>(result);
                _dbContext.SaveChanges();
            }

            //Get inserted Revison ID
            var Rev0 = _dbContext.TblSupplierPackageRevisions.SingleOrDefault(b => (b.PrPackSuppId == PackageSupplierId) && (b.PrRevNo == 0));
            int revId = Rev0.PrRevId;

            var packageSupp = _dbContext.TblSupplierPackages.Where(x => x.SpPackSuppId == PackageSupplierId).FirstOrDefault();
            byte byBoq = (byte)((packageSupp.SpByBoq == null) ? 0 : packageSupp.SpByBoq);

            if (!InsertRevisionDetail(revId, ExcelFile, byBoq, ExchRate, discount, addedItem))
                return false;
            else
            {
                UpdateTotalPrice(revId);
                return true;
            }
        }

        private bool InsertRevisionDetail(int revId, IFormFile ExcelFile, byte byBoq, double ExchRate, double disc, byte addedItem)
        {
            Boolean ret = true;

            if (ExcelFile?.Length > 0)
            {
                var stream = ExcelFile.OpenReadStream();
                List<TblRevisionDetail> LstRevDetails = new List<TblRevisionDetail>();
                List<TblMissingPrice> LstMissingPrice = new List<TblMissingPrice>();

                try
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets.First();
                        var rowCount = worksheet.Dimension.Rows;

                        //RemoveExistingMissing
                        //_dbContext.TblMissingPrices.RemoveRange(_dbContext.TblMissingPrices.Where(c => c.RevisionId == revId));
                        //_dbContext.SaveChanges();
                        string resComment, resCode = "", oldBoqRef = "";
                        double resQty, Price,discount=0;

                        if (disc > 0)
                            discount = disc;

                        for (var row = 2; row <= rowCount; row++)
                        {
                            try
                            {
                                string boqRef = worksheet.Cells[row, 1].Value == null ? "" : worksheet.Cells[row, 1].Value.ToString();

                                if ((byBoq != 1) & (oldBoqRef != "") & (boqRef == ""))
                                    boqRef = oldBoqRef;

                                string boqDesc = worksheet.Cells[row, 3].Value == null ? "" : worksheet.Cells[row, 3].Value.ToString();
                                double boqQty = worksheet.Cells[row, 5].Value == null ? 0 : (double)worksheet.Cells[row, 5].Value;

                                if (byBoq == 1)
                                {
                                    if (((boqRef != "") && (boqDesc != "") && (boqQty != 0)))
                                    {
                                        Price = (worksheet.Cells[row, 6].Value == null) ? 0 : (double)worksheet.Cells[row, 6].Value;
                                        if (discount==0)
                                        discount = (worksheet.Cells[row, 7].Value == null) ? 0 : (double)worksheet.Cells[row, 7].Value;
                                        resComment = worksheet.Cells[row, 10].Value == null ? "" : worksheet.Cells[row, 10].Value.ToString();
                                        resQty = boqQty;

                                        byte missPrice = 0;
                                        if (Price <= 0 && boqRef != "")
                                        {
                                            missPrice = 1;
                                        }

                                        if ((boqRef != "") && (resQty > 0) && (Price >= 0))
                                        {
                                            var revdtl = new TblRevisionDetail()
                                            {
                                                RdRevisionId = revId,
                                                RdResourceSeq = 0,
                                                RdBoqItem = boqRef,
                                                RdPrice = Math.Round(Price * (ExchRate > 0 ? ExchRate : 1),3),
                                                RdPriceOrigCurrency = Math.Round(Price, 3),
                                                RdQty = resQty,
                                                RdComment = resComment,                                                
                                                RdMissedPrice = missPrice,
                                                RdDiscount = discount,
                                                RdAddedItem= (byte?)(addedItem == null ? 0 : addedItem)
                                            };
                                            LstRevDetails.Add(revdtl);
                                        }
                                        oldBoqRef = boqRef;
                                    }
                                }
                                else
                                {

                                    if (((boqRef != "") && (boqDesc != "") && (boqQty != 0)) || ((oldBoqRef != "") && ((worksheet.Cells[row, 6].Value == null ? "" : worksheet.Cells[row, 6].Value.ToString()) != "")))
                                    {
                                        resQty = worksheet.Cells[row, 10].Value == null ? 0 : (double)worksheet.Cells[row, 10].Value;
                                        Price = (worksheet.Cells[row, 11].Value == null) ? 0 : (double)worksheet.Cells[row, 11].Value;
                                        if (discount == 0)
                                            discount = (worksheet.Cells[row, 12].Value == null) ? 0 : (double)worksheet.Cells[row, 12].Value;
                                        resComment = worksheet.Cells[row, 15].Value == null ? "" : worksheet.Cells[row, 12].Value.ToString();

                                        int resSeq = 0;
                                        string boqItem = boqRef == "" ? oldBoqRef : boqRef;

                                        resCode = worksheet.Cells[row, 7].Value == null ? "" : worksheet.Cells[row, 7].Value.ToString();

                                        var result = _dbContext.TblBoqVds.SingleOrDefault(b => b.BoqItem == boqItem && b.BoqPackage == resCode);
                                        if (result != null)
                                            resSeq = result.BoqSeq;

                                        byte missPrice = 0;
                                        //Insert missing prices
                                        if (Price <= 0 && resSeq != 0)
                                        {
                                            //var missPrice = new TblMissingPrice()
                                            //{
                                            //    RevisionId = revId,
                                            //    BoqResourceSeq = resSeq
                                            //};
                                            //LstMissingPrice.Add(missPrice);
                                            missPrice = 1;
                                        }

                                        if ((resCode != "") && (resQty > 0) && (Price >= 0))
                                        {
                                            var revdtl = new TblRevisionDetail()
                                            {
                                                RdRevisionId = revId,
                                                RdResourceSeq = resSeq,
                                                RdBoqItem = boqRef,
                                                RdPrice = Math.Round( Price * (ExchRate > 0 ? ExchRate : 1),3),
                                                RdQty = resQty,
                                                RdComment = resComment,
                                                RdPriceOrigCurrency = Math.Round(Price,3),
                                                RdMissedPrice = missPrice,
                                                RdDiscount=discount,
                                                RdAddedItem = (byte?)(addedItem == null ? 0 : addedItem)
                                            };
                                            LstRevDetails.Add(revdtl);
                                        }
                                        oldBoqRef = boqRef != "" ? boqRef : oldBoqRef;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }

                    //if (LstMissingPrice.Count()>0 )
                    //{ 
                    //    _dbContext.AddRange(LstMissingPrice);
                    //    ret = false;
                    //}
                    //else
                    //{ 
                    _dbContext.AddRange(LstRevDetails);
                    ret = true;
                    //}

                    _dbContext.SaveChanges();
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return ret;
        }

        public void UpdateTotalPrice(int revId)
        {
            var result = _dbContext.TblSupplierPackageRevisions.SingleOrDefault(b => b.PrRevNo == 0 && b.PrRevId == revId);
            if (result != null)
            {
                var TotalPrice = (from b in _dbContext.TblRevisionDetails
                                  where b.RdRevisionId == revId
                                  select b).Sum(e => (e.RdPrice * e.RdQty));

                result.PrTotPrice = (decimal)TotalPrice;
                _dbContext.SaveChanges();
            }
        }


        public int GetMaxRevisionNumber(int PackageSupplierId)
        {
            var query = _dbContext.TblSupplierPackageRevisions.Where(x => x.PrPackSuppId == PackageSupplierId);
            var MaxRevisionNumber = query.Any() ? query.Max(x => x.PrRevNo) : -1;
            return (int)MaxRevisionNumber;
        }

        public bool AssignSupplierPackage(int packId, List<SupplierPercent> SupPercentList)
        {
            foreach (var sup in SupPercentList)
            {
                var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                       join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                       join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                       where (a.SpPackageId == packId && a.SpSupplierId == sup.supID && b.PrRevNo == 0)

                                       select new AssignRevisionDetails
                                       {
                                           resourceID = c.RdResourceSeq,
                                           resourceQty = c.RdQty,
                                           price = c.RdPrice,
                                           assignpercent = sup.percent,
                                           assignQty = c.RdQty * (sup.percent / 100),
                                           assignPrice = c.RdPrice * c.RdQty * (sup.percent / 100),
                                           revisionId = b.PrRevId
                                       }).ToList();

                if (revisionDetails.Count > 0)
                {
                    foreach (var revDtl in revisionDetails)
                    {
                        UpdateRevDtlAssignedQty(revDtl.revisionId, revDtl.resourceID, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                    }
                }
            }
            return true;
        }

        public bool AssignSupplierRessource(int packId, List<SupplierResrouces> supplierResList, bool isPercent)
        {
           List<AssignRevisionDetails> revisionDetails = new List<AssignRevisionDetails>();

            foreach (var sup in supplierResList)
            {
                if (isPercent)
                {
                    foreach (var supPerc in sup.supplierPercents)
                    {
                         revisionDetails = (from a in _dbContext.TblSupplierPackages
                                               join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                               join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                               where (a.SpPackageId == packId && a.SpSupplierId == supPerc.supID && b.PrRevNo == 0 && c.RdResourceSeq == sup.resourceID)

                                               select new AssignRevisionDetails
                                               {
                                                   resourceID = c.RdResourceSeq,
                                                   resourceQty = c.RdQty,
                                                   price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                                   assignpercent = supPerc.percent,
                                                   assignQty = c.RdQty * (supPerc.percent / 100),
                                                   assignPrice = c.RdPrice * c.RdQty * (supPerc.percent / 100),
                                                   revisionId = b.PrRevId,
                                                   priceOrigCurrency = c.RdPriceOrigCurrency
                                               }).ToList();

                        if (revisionDetails.Count > 0)
                        {
                            foreach (var revDtl in revisionDetails)
                            {
                                UpdateRevDtlAssignedQty(revDtl.revisionId, revDtl.resourceID, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var supQty in sup.supplierQtys)
                    {
                        revisionDetails = (from a in _dbContext.TblSupplierPackages
                                           join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                           join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                           where (a.SpPackageId == packId && a.SpSupplierId == supQty.supID && b.PrRevNo == 0 && c.RdResourceSeq == sup.resourceID)

                                           select new AssignRevisionDetails
                                           {
                                               resourceID = c.RdResourceSeq,
                                               resourceQty = c.RdQty,
                                               price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                               assignpercent = (supQty.qty/ c.RdQty ) *100,
                                               assignQty = supQty.qty,
                                               assignPrice = c.RdPrice * supQty.qty,
                                               revisionId = b.PrRevId,
                                               priceOrigCurrency = c.RdPriceOrigCurrency
                                           }).ToList();

                        if (revisionDetails.Count > 0)
                        {
                            foreach (var revDtl in revisionDetails)
                            {
                                UpdateRevDtlAssignedQty(revDtl.revisionId, revDtl.resourceID, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                            }
                        }
                    }
                }                             
            }
            return true;
        }

        public bool AssignSupplierListRessourceList(int packId, AssignSuppliertRes item, bool isPercent)
        {
            List<AssignRevisionDetails> revisionDetails = new List<AssignRevisionDetails>();

            if (isPercent)
            {
                foreach (var sup in item.supplierPercentList)
                {
                    revisionDetails = (from a in _dbContext.TblSupplierPackages
                                       join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                       join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                       where (a.SpPackageId == packId && a.SpSupplierId == sup.supID && b.PrRevNo == 0)

                                       select new AssignRevisionDetails
                                       {
                                           resourceID = c.RdResourceSeq,
                                           resourceQty = c.RdQty,
                                           price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                           assignpercent = sup.percent,
                                           assignQty = c.RdQty * (sup.percent / 100),
                                           assignPrice = c.RdPrice * c.RdQty * (sup.percent / 100),
                                           revisionId = b.PrRevId,
                                           priceOrigCurrency = c.RdPriceOrigCurrency
                                       }).ToList();

                    var filtered = revisionDetails.Where(x => item.supplierResItemList.Contains(item.supplierResItemList.Where(y => y.resId == x.resourceID).FirstOrDefault()));

                    foreach (var revDtl in filtered)
                    {
                        UpdateRevDtlAssignedQty(revDtl.revisionId, revDtl.resourceID, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                    }
                }
            }
            else
            {
                foreach (var sup in item.supplierQtyList)
                {
                    revisionDetails = (from a in _dbContext.TblSupplierPackages
                                       join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                       join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                       where (a.SpPackageId == packId && a.SpSupplierId == sup.supID && b.PrRevNo == 0)

                                       select new AssignRevisionDetails
                                       {
                                           resourceID = c.RdResourceSeq,
                                           resourceQty = c.RdQty,
                                           price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                           assignpercent = (sup.qty/ c.RdQty ) *100,
                                           assignQty =  sup.qty,
                                           assignPrice = c.RdPrice * sup.qty,
                                           revisionId = b.PrRevId,
                                           priceOrigCurrency = c.RdPriceOrigCurrency
                                       }).ToList();

                    var filtered = revisionDetails.Where(x => item.supplierResItemList.Contains(item.supplierResItemList.Where(y => y.resId == x.resourceID).FirstOrDefault()));

                    foreach (var revDtl in filtered)
                    {
                        UpdateRevDtlAssignedQty(revDtl.revisionId, revDtl.resourceID, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                    }
                }
            }
                
            return true;
        }

        public bool AssignSupplierListBoqList(int packId, AssignSuppliertBoq item, bool isPercent)
        {
            List<AssignRevisionDetails> revisionDetails = new List<AssignRevisionDetails>();

            if (isPercent)
            {
                foreach (var sup in item.supplierPercentList)
                {
                    revisionDetails = (from a in _dbContext.TblSupplierPackages
                                           join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                           join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                           where (a.SpPackageId == packId && a.SpSupplierId == sup.supID && b.PrRevNo == 0)

                                           select new AssignRevisionDetails
                                           {
                                               boqItem = c.RdBoqItem,
                                               boqQty = c.RdQty,
                                               price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                               assignpercent = sup.percent,
                                               assignQty = c.RdQty * (sup.percent / 100),
                                               assignPrice = c.RdPrice * c.RdQty * (sup.percent / 100),
                                               revisionId = b.PrRevId,
                                               priceOrigCurrency = c.RdPriceOrigCurrency,
                                               IsNewItem    =c.IsNew,
                                               IsAlternative=c.IsAlternative,
                                               NewItemId=Convert.ToString(c.NewItemId)
                                           }).ToList();

                    var filtered = revisionDetails.Where(x => item.supplierBoqItemList.Contains(item.supplierBoqItemList.Where(y => y.BoqItemID == ((x.IsNewItem == true) ? Convert.ToString(x.NewItemId) :  x.boqItem) ).FirstOrDefault()));

                    foreach (var revDtl in filtered)
                    {
                        UpdateRevDtlAssignedQtyBOQ(revDtl.revisionId, revDtl.boqItem, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                    }
                }
            }
            else
            {
                foreach (var sup in item.supplierQtyList)
                {
                    revisionDetails = (from a in _dbContext.TblSupplierPackages
                                           join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                           join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                           where (a.SpPackageId == packId && a.SpSupplierId == sup.supID && b.PrRevNo == 0)

                                           select new AssignRevisionDetails
                                           {
                                               boqItem = c.RdBoqItem,
                                               boqQty = c.RdQty,
                                               price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                               assignpercent =( sup.qty / c.RdQty ) *100,
                                               assignQty =sup.qty,
                                               assignPrice = c.RdPrice * sup.qty,
                                               revisionId = b.PrRevId,
                                               priceOrigCurrency = c.RdPriceOrigCurrency
                                           }).ToList();

                    var filtered = revisionDetails.Where(x => item.supplierBoqItemList.Contains(item.supplierBoqItemList.Where(y => y.BoqItemID == x.boqItem).FirstOrDefault()));

                    foreach (var revDtl in filtered)
                    {
                        UpdateRevDtlAssignedQtyBOQ(revDtl.revisionId, revDtl.boqItem, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                    }
                }
            }
        
            return true;
        }

        public bool AssignSupplierGroup(int packId, bool byBoq, List<SupplierGroups> SupplierGroupList, bool isPercent)
        {
                if (!byBoq)
            {
                foreach (var sup in SupplierGroupList)
                {
                    if (isPercent)
                    {
                        foreach (var supPerc in sup.supplierPercents)
                        {
                            var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                                   join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                                   join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                                   join boq in _dbContext.TblBoqVds on c.RdResourceSeq equals boq.BoqSeq
                                                   where (a.SpPackageId == packId && boq.BoqScope == packId && a.SpSupplierId == supPerc.supID && b.PrRevNo == 0 && boq.GroupId == sup.GroupId)

                                                   select new AssignRevisionDetails
                                                   {
                                                       resourceID = c.RdResourceSeq,
                                                       resourceQty = c.RdQty,
                                                       price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                                       assignpercent = supPerc.percent,
                                                       assignQty = c.RdQty * (supPerc.percent / 100),
                                                       assignPrice = c.RdPrice * c.RdQty * (supPerc.percent / 100),
                                                       revisionId = b.PrRevId,
                                                       priceOrigCurrency = c.RdPriceOrigCurrency
                                                   }).ToList();

                            if (revisionDetails.Count > 0)
                            {
                                foreach (var revDtl in revisionDetails)
                                {
                                    UpdateRevDtlAssignedQty(revDtl.revisionId, revDtl.resourceID, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var supPerc in sup.supplierQtys)
                        {
                            var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                                   join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                                   join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                                   join boq in _dbContext.TblBoqVds on c.RdResourceSeq equals boq.BoqSeq
                                                   where (a.SpPackageId == packId && boq.BoqScope == packId && a.SpSupplierId == supPerc.supID && b.PrRevNo == 0 && boq.GroupId == sup.GroupId)

                                                   select new AssignRevisionDetails
                                                   {
                                                       resourceID = c.RdResourceSeq,
                                                       resourceQty = c.RdQty,
                                                       price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                                       assignpercent = (supPerc.qty/ c.RdQty) * 100,
                                                       assignQty = supPerc.qty,
                                                       assignPrice = c.RdPrice * supPerc.qty,
                                                       revisionId = b.PrRevId,
                                                       priceOrigCurrency = c.RdPriceOrigCurrency
                                                   }).ToList();

                            if (revisionDetails.Count > 0)
                            {
                                foreach (var revDtl in revisionDetails)
                                {
                                    UpdateRevDtlAssignedQty(revDtl.revisionId, revDtl.resourceID, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                                }
                            }
                        }
                    }

                }
            }
            else
            {
                foreach (var sup in SupplierGroupList)
                {
                    if (isPercent)
                    {
                        foreach (var supPerc in sup.supplierPercents)
                        {
                            var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                                   join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                                   join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                                   join boq in _dbContext.TblOriginalBoqVds on c.RdBoqItem equals boq.ItemO
                                                   where (a.SpPackageId == packId && boq.Scope == packId && a.SpSupplierId == supPerc.supID && b.PrRevNo == 0 && boq.GroupId == sup.GroupId)

                                                   select new AssignRevisionDetails
                                                   {
                                                       boqItem = c.RdBoqItem,
                                                       boqQty = c.RdQty,
                                                       price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                                       assignpercent = supPerc.percent,
                                                       assignQty = c.RdQty * (supPerc.percent / 100),
                                                       assignPrice = c.RdPrice * c.RdQty * (supPerc.percent / 100),
                                                       revisionId = b.PrRevId,
                                                       priceOrigCurrency = c.RdPriceOrigCurrency
                                                   }).ToList();

                            if (revisionDetails.Count > 0)
                            {
                                foreach (var revDtl in revisionDetails)
                                {
                                    UpdateRevDtlAssignedQtyBOQ(revDtl.revisionId, revDtl.boqItem, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                                }
                            }
                        }
                    }
                else
                {
                    foreach (var supPerc in sup.supplierQtys)
                        {
                            var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                                   join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                                   join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                                   join boq in _dbContext.TblOriginalBoqVds on c.RdBoqItem equals boq.ItemO
                                                   where (a.SpPackageId == packId && boq.Scope == packId && a.SpSupplierId == supPerc.supID && b.PrRevNo == 0 && boq.GroupId == sup.GroupId)

                                                   select new AssignRevisionDetails
                                                   {
                                                       boqItem = c.RdBoqItem,
                                                       boqQty = c.RdQty,
                                                       price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                                       assignpercent = (supPerc.qty/ c.RdQty) * 100,
                                                       assignQty = supPerc.qty,
                                                       assignPrice = c.RdPrice * supPerc.qty,
                                                       revisionId = b.PrRevId,
                                                       priceOrigCurrency = c.RdPriceOrigCurrency
                                                   }).ToList();

                            if (revisionDetails.Count > 0)
                            {
                                foreach (var revDtl in revisionDetails)
                                {
                                    UpdateRevDtlAssignedQtyBOQ(revDtl.revisionId, revDtl.boqItem, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

        public bool AssignSupplierListGroupList(int packId, bool byBoq, AssignSupplierGroup item, bool isPercent)
        {
            if (byBoq)
            {
                if (isPercent)
                {
                    foreach (var sup in item.supplierPercentList)
                    {
                        var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                               join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                               join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                               join boq in _dbContext.TblOriginalBoqVds on c.RdBoqItem equals boq.ItemO
                                               where (a.SpPackageId == packId && boq.Scope == packId && a.SpSupplierId == sup.supID && b.PrRevNo == 0)

                                               select new AssignRevisionDetails
                                               {
                                                   boqItem = c.RdBoqItem,
                                                   boqQty = c.RdQty,
                                                   price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                                   assignpercent = sup.percent,
                                                   assignQty = c.RdQty * (sup.percent / 100),
                                                   assignPrice = c.RdPrice * c.RdQty * (sup.percent / 100),
                                                   revisionId = b.PrRevId,
                                                   priceOrigCurrency = c.RdPriceOrigCurrency,
                                                   GroupId = boq.GroupId
                                               }).ToList();

                        var filtered = revisionDetails.Where(x => item.supplierGroupList.Contains(item.supplierGroupList.Where(y => y.Id == x.GroupId).FirstOrDefault()));

                        foreach (var revDtl in filtered)
                        {
                            UpdateRevDtlAssignedQtyBOQ(revDtl.revisionId, revDtl.boqItem, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                        }

                    }
                }
                else
                {
                    foreach (var sup in item.supplierQtyList)
                    {
                        var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                               join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                               join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                               join boq in _dbContext.TblOriginalBoqVds on c.RdBoqItem equals boq.ItemO
                                               where (a.SpPackageId == packId && boq.Scope == packId && a.SpSupplierId == sup.supID && b.PrRevNo == 0)

                                               select new AssignRevisionDetails
                                               {
                                                   boqItem = c.RdBoqItem,
                                                   boqQty = c.RdQty,
                                                   price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                                   assignpercent = (sup.qty/ c.RdQty)*100,
                                                   assignQty = sup.qty ,
                                                   assignPrice = c.RdPrice * sup.qty,
                                                   revisionId = b.PrRevId,
                                                   priceOrigCurrency = c.RdPriceOrigCurrency,
                                                   GroupId = boq.GroupId
                                               }).ToList();

                        var filtered = revisionDetails.Where(x => item.supplierGroupList.Contains(item.supplierGroupList.Where(y => y.Id == x.GroupId).FirstOrDefault()));

                        foreach (var revDtl in filtered)
                        {
                            UpdateRevDtlAssignedQtyBOQ(revDtl.revisionId, revDtl.boqItem, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                        }

                    }
                }
            }
            else
            {
                if (isPercent)
                {
                    foreach (var sup in item.supplierPercentList)
                    {
                        var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                               join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                               join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                               join boq in _dbContext.TblBoqVds on c.RdResourceSeq equals boq.BoqSeq
                                               where (a.SpPackageId == packId && a.SpSupplierId == sup.supID && b.PrRevNo == 0)

                                               select new AssignRevisionDetails
                                               {
                                                   resourceID = c.RdResourceSeq,
                                                   resourceQty = c.RdQty,
                                                   price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                                   assignpercent = sup.percent,
                                                   assignQty = c.RdQty * (sup.percent / 100),
                                                   assignPrice = c.RdPrice * c.RdQty * (sup.percent / 100),
                                                   revisionId = b.PrRevId,
                                                   priceOrigCurrency = c.RdPriceOrigCurrency,
                                                   GroupId = boq.GroupId
                                               }).ToList();

                        var filtered = revisionDetails.Where(x => item.supplierGroupList.Contains(item.supplierGroupList.Where(y => y.Id == x.GroupId).FirstOrDefault()));

                        foreach (var revDtl in filtered)
                        {
                            UpdateRevDtlAssignedQty(revDtl.revisionId, revDtl.resourceID, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                        }
                    }
                }
                else
                {
                    foreach (var sup in item.supplierQtyList)
                    {
                        var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                               join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                               join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                               join boq in _dbContext.TblBoqVds on c.RdResourceSeq equals boq.BoqSeq
                                               where (a.SpPackageId == packId && a.SpSupplierId == sup.supID && b.PrRevNo == 0)

                                               select new AssignRevisionDetails
                                               {
                                                   resourceID = c.RdResourceSeq,
                                                   resourceQty = c.RdQty,
                                                   price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                                   assignpercent = (sup.qty/ c.RdQty) * 100,
                                                   assignQty = sup.qty ,
                                                   assignPrice = c.RdPrice *  sup.qty  ,
                                                   revisionId = b.PrRevId,
                                                   priceOrigCurrency = c.RdPriceOrigCurrency,
                                                   GroupId = boq.GroupId
                                               }).ToList();

                        var filtered = revisionDetails.Where(x => item.supplierGroupList.Contains(item.supplierGroupList.Where(y => y.Id == x.GroupId).FirstOrDefault()));

                        foreach (var revDtl in filtered)
                        {
                            UpdateRevDtlAssignedQty(revDtl.revisionId, revDtl.resourceID, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                        }
                    }
                }
            }

            return true;
        }

        public bool AssignSupplierBOQ(int packId, List<SupplierBOQ> SupplierBOQList, bool isPercent)
        {
            foreach (var sup in SupplierBOQList)
            {
                if (isPercent)
                {
                    foreach (var supPerc in sup.supplierPercents)
                    {
                        var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                               join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                               join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                               where (a.SpPackageId == packId && a.SpSupplierId == supPerc.supID && b.PrRevNo == 0 &&
                                               ((sup.IsNewItem == false && c.RdBoqItem == sup.BoqItemID) || (sup.IsNewItem == true && sup.BoqItemID == Convert.ToString(c.NewItemId))))                                     
                                               select new AssignRevisionDetails
                                               {
                                                   boqItem = c.RdBoqItem,
                                                   boqQty = c.RdQty,
                                                   price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                                   assignpercent = supPerc.percent,
                                                   assignQty = c.RdQty * (supPerc.percent / 100),
                                                   assignPrice = c.RdPrice * c.RdQty * (supPerc.percent / 100),
                                                   revisionId = b.PrRevId,
                                                   priceOrigCurrency = c.RdPriceOrigCurrency
                                               }).ToList();

                        if (revisionDetails.Count > 0)
                        {
                            foreach (var revDtl in revisionDetails)
                            {
                                UpdateRevDtlAssignedQtyBOQ(revDtl.revisionId, revDtl.boqItem, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var supPerc in sup.supplierQtys)
                    {
                        //int itm = int.Parse( sup.BoqItemID);

                        var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                               join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                               join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                               where (a.SpPackageId == packId && a.SpSupplierId == supPerc.supID && b.PrRevNo == 0 &&
                                               ((sup.IsNewItem == false && c.RdBoqItem == sup.BoqItemID) || (sup.IsNewItem == true && int.Parse(sup.BoqItemID) == c.NewItemId)))

                                               select new AssignRevisionDetails
                                               {
                                                   boqItem = c.RdBoqItem,
                                                   boqQty = c.RdQty,
                                                   price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                                   assignpercent = (supPerc.qty/ c.RdQty)*100,
                                                   assignQty =  supPerc.qty ,
                                                   assignPrice = c.RdPrice *supPerc.qty ,
                                                   revisionId = b.PrRevId,
                                                   priceOrigCurrency = c.RdPriceOrigCurrency
                                               }).ToList();

                        if (revisionDetails.Count > 0)
                        {
                            foreach (var revDtl in revisionDetails)
                            {
                                UpdateRevDtlAssignedQtyBOQ(revDtl.revisionId, revDtl.boqItem, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void UpdateRevDtlAssignedQty(int revisionId, int resourceID, double assignpercent, double assignQty, double assignPrice)
        {
            var result = _dbContext.TblRevisionDetails.SingleOrDefault(b => b.RdRevisionId == revisionId && b.RdResourceSeq == resourceID);
            if (result != null)
            {
                result.RdAssignedPerc = assignpercent;
                result.RdAssignedQty = assignQty;
                result.RdAssignedPrice = assignPrice;
                _dbContext.SaveChanges();
            }
        }

        public void UpdateRevDtlAssignedQtyBOQ(int revisionId, string boqItem, double assignpercent, double assignQty, double assignPrice)
        {
            var result = _dbContext.TblRevisionDetails.SingleOrDefault(b => b.RdRevisionId == revisionId && b.RdBoqItem == boqItem);
            if (result != null)
            {
                result.RdAssignedPerc = assignpercent;
                result.RdAssignedQty = assignQty;
                result.RdAssignedPrice = assignPrice;
                _dbContext.SaveChanges();
            }
        }

        public bool UpdateRevisionDetailsPriceByBoq(List<RevisionDetailsList> revisionDetailsList)
        {
            var curList = (from b in _mdbContext.TblCurrencies
                           select b).ToList();

            var usedCur = from cur in curList
                          join b in _dbContext.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency   
                          join c in revisionDetailsList on b.PrRevId equals c.RdRevisionId                        
                          group cur by cur.CurCode into g
                          select new LiveExchange
                          {
                              fromCurrency = g.Key
                          };

            foreach (var item in revisionDetailsList)
            {
                var result = _dbContext.TblRevisionDetails.SingleOrDefault(b => b.RdRevisionId == item.RdRevisionId && b.RdBoqItem == item.RdBoqItem);
                if (result != null)
                {
                    result.RdPrice = item.RdPrice * GetExchange(usedCur.FirstOrDefault().fromCurrency);
                    result.RdPriceOrigCurrency = item.RdPrice;
                    result.RdDiscount = item.RdDiscount;
                    result.RdMissedPriceReason = item.RdMissedPriceReason;
                }
            }
            _dbContext.SaveChanges();
            return true;
        }

        public bool UpdateRevisionDetailsPrice(List<RevisionDetailsList> revisionDetailsList)
        {
            var curList = (from b in _mdbContext.TblCurrencies
                           select b).ToList();

            var usedCur = from cur in curList
                          join b in _dbContext.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                          join c in revisionDetailsList on b.PrRevId equals c.RdRevisionId
                          group cur by cur.CurCode into g
                          select new LiveExchange
                          {
                              fromCurrency = g.Key
                          };

            foreach (var item in revisionDetailsList)
            {
                var result = _dbContext.TblRevisionDetails.SingleOrDefault(b => b.RdRevisionId == item.RdRevisionId && b.RdResourceSeq == item.RdResourceSeq);
                if (result != null)
                {
                    result.RdPrice = item.RdPrice * GetExchange(usedCur.FirstOrDefault().fromCurrency);
                    result.RdPriceOrigCurrency = item.RdPrice;
                    result.RdDiscount = item.RdDiscount;
                    result.RdMissedPriceReason=item.RdMissedPriceReason;
                }
            }
            _dbContext.SaveChanges();
            return true;
        }

        public bool SendCompToManagement(TopManagementTemplateModel topManagementTemplate, List<IFormFile> attachments,  string UserName)
        {
            string send = "";

            string[] emails = new string[topManagementTemplate.TopManagements.Count];
            if (topManagementTemplate.TopManagements.Count > 0)
            {
                int packId = topManagementTemplate.PackageId;
                for (int i = 0; i < topManagementTemplate.TopManagements.Count; i++)
                {
                    emails[i] = topManagementTemplate.TopManagements[i].Mail;
                }
                var package = _mdbContext.TblPackages.Where(x => x.PkgeId == packId).FirstOrDefault();
                string PackageName = package.PkgeName;

                var p = _dbContext.TblParameters.FirstOrDefault();
                //var proj = _pdbContext.Tblprojects.Where(x => x.Seq == p.TsProjId).FirstOrDefault();
                //string ProjectName = proj.PrjName;
                string ProjectName = p.Project;

                //string excelName = $"{PackageName}-{ProjectName}.xlsx";

                //string path = @"C:\App\";

                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}
                //string FullPath = path + ExcelComparisonSheet;

                //if (File.Exists(FullPath))
                //    File.Delete(FullPath);

                //xlPackage.SaveAs(FullPath);


                //Send Email
                List<string> mylistTo = new List<string>();
                foreach (var email in emails)
                {
                    mylistTo.Add(email);
                }

 
                //BCC
                List<string> mylistBCC = new List<string>();
                //mylistBCC = null;
                //User user = new LogonRepository(mdbcontext, _pdbContext, _dbContext, configuration).GetUser(UserName);
                User user = _logonRepository.GetUser(UserName);

                if (user.UsrEmail != "")
                    mylistBCC.Add(user.UsrEmail);
               

                string Subject = "Project: " + ProjectName + ", Package: " + PackageName;

                /*string MailBody;

                MailBody = "Dear Sir,";
                MailBody += Environment.NewLine;
                MailBody += Environment.NewLine;
                MailBody += "Please find attached Comparison sheet.";
                MailBody += Environment.NewLine;
                MailBody += Environment.NewLine;
                MailBody += Environment.NewLine;
                MailBody += Environment.NewLine;
                MailBody += "Best regards";*/

                List<string> mylistCC = new List<string>();
                //mylistCC = null;
                if (topManagementTemplate.ListCC != null)
                {
                    foreach (var mail in topManagementTemplate.ListCC)
                    {
                        mylistCC.Add(mail);
                    }
                }

                var AttachmentList = new List<string>();
                //AttachmentList = null;
                if (topManagementTemplate.ListAttach != null)
                {
                    foreach (var attach in topManagementTemplate.ListAttach)
                    {
                        AttachmentList.Add(attach);
                    }
                }

                string userSignature = (user.UsrEmailSignature == null) ? "" : user.UsrEmailSignature;
                if (userSignature != "")
                {
                    topManagementTemplate.Template += @"<br><br>";
                    topManagementTemplate.Template += userSignature;
                }

                Mail m = new Mail();
                var res = m.SendMail(mylistTo, mylistCC, mylistBCC, Subject, topManagementTemplate.Template, AttachmentList, false, attachments);

                send = "sent";
            }

            return (send == "sent");
        }
     
        private string getCurDesc(int id)
        {
            var cur = _mdbContext.TblCurrencies.Where(x=> x.CurId==id).FirstOrDefault();

            if (cur != null)
                return cur.CurDesc;
            else
                return "";
        }

        public List<GroupingLevelModel> GetComparisonSheet(int packageId, SearchInput input,int supId)
        {
            IEnumerable<BoqRessourcesList> condQuery = (from bb in _dbContext.TblSupplierPackageRevisions
                                                        join a in _dbContext.TblSupplierPackages on bb.PrPackSuppId equals a.SpPackSuppId
                                                        join c in _dbContext.TblRevisionDetails on bb.PrRevId equals c.RdRevisionId
                                                        join b in _dbContext.TblBoqVds on c.RdResourceSeq equals b.BoqSeq
                                                        join o in _dbContext.TblOriginalBoqVds on b.BoqItem equals o.ItemO
                                                        where a.SpPackageId == packageId && (c.IsNew == false || c.IsNew == null)
                                                        && (c.IsAlternative == false || c.IsAlternative == null) && bb.PrRevNo == 0
                                                        select new BoqRessourcesList
                                                        {
                                                            RowNumber = o.RowNumber,
                                                            SectionO = Convert.ToString(o.SectionO),
                                                            ItemO = Convert.ToString(o.ItemO),
                                                            DescriptionO = Convert.ToString(o.DescriptionO),
                                                            UnitO = Convert.ToString(o.UnitO),
                                                            QtyO = (double)o.QtyO,
                                                            UnitRateO = (double)o.UnitRate,
                                                            ScopeO = o.Scope,
                                                            BoqSeq = b.BoqSeq,
                                                            BoqCtg = Convert.ToString(b.BoqCtg),
                                                            BoqUnitMesure = Convert.ToString(b.BoqUnitMesure),
                                                            BoqQty = (double)b.BoqQty,  //Final Qty
                                                            BoqScopeQty= (double)c.RdQty,//Quotation Qty
                                                            BoqUprice = (double)b.BoqUprice,
                                                            BoqDiv = Convert.ToString(b.BoqDiv),
                                                            BoqPackage = Convert.ToString(b.BoqPackage),
                                                            BoqScope = packageId,
                                                            ResSeq = Convert.ToString(b.BoqResSeq),
                                                            ResDescription = Convert.ToString(c.ResourceDescription),
                                                            IsAlternative = false,
                                                            IsNewItem = false,
                                                            NewItemId = (int)c.NewItemId,
                                                            NewItemResourceId = (int)c.NewItemResourceId,
                                                            ParentItemO = Convert.ToString(c.ParentItemO),
                                                            ParentResourceId = (int)c.ParentResourceId,
                                                            IsExcluded = (bool)c.IsExcluded,
                                                            SupplierId= (int)a.SpSupplierId,
                                                            L2 = Convert.ToString(o.L2),
                                                            L3 = Convert.ToString(o.L3),
                                                            L4 = Convert.ToString(o.L4),
                                                            L5 = Convert.ToString(o.L5),
                                                            L6 = Convert.ToString(o.L6),
                                                            C1 = Convert.ToString(o.C1),
                                                            C2 = Convert.ToString(o.C2),
                                                            C3 = Convert.ToString(o.C3),
                                                            C4 = Convert.ToString(o.C4),
                                                            C5 = Convert.ToString(o.C5),
                                                            C6 = Convert.ToString(o.C6)
                                                        }).Union(from bb in _dbContext.TblSupplierPackageRevisions
                                                                 join a in _dbContext.TblSupplierPackages on bb.PrPackSuppId equals a.SpPackSuppId
                                                                 join c in _dbContext.TblRevisionDetails on bb.PrRevId equals c.RdRevisionId
                                                                 join item in _dbContext.NewItems on c.NewItemId equals item.Id
                                                                 join newr in _dbContext.NewItemResources on c.NewItemResourceId equals newr.Id
                                                                 where a.SpPackageId == packageId && bb.PrRevNo == 0
                                                                 select new BoqRessourcesList
                                                                 {
                                                                     RowNumber = 0,
                                                                     SectionO = Convert.ToString(""),
                                                                     ItemO = Convert.ToString(item.Id),
                                                                     DescriptionO = Convert.ToString(item.ItemDescription),
                                                                     UnitO = Convert.ToString(item.UnitO),
                                                                     QtyO = (double)0,
                                                                     UnitRateO = (double)0,
                                                                     ScopeO = packageId,
                                                                     BoqSeq = (int)c.NewItemResourceId,
                                                                     BoqCtg = Convert.ToString(newr.ResourceType),
                                                                     BoqUnitMesure = Convert.ToString(newr.ResourceUnit),
                                                                     BoqQty = (double)c.RdQty,//Final Qty
                                                                     BoqScopeQty = (double)c.RdQty,//Quotation Qty
                                                                     BoqUprice = (double)c.RdPrice,
                                                                     BoqDiv = Convert.ToString(""),
                                                                     BoqPackage = Convert.ToString(""),
                                                                     BoqScope = packageId,
                                                                     ResSeq = Convert.ToString("0"),
                                                                     ResDescription = Convert.ToString(c.ResourceDescription),
                                                                     IsAlternative = false,
                                                                     IsNewItem = true,
                                                                     NewItemId = (int)c.NewItemId,
                                                                     NewItemResourceId = (int)c.NewItemResourceId,
                                                                     ParentItemO = Convert.ToString(c.ParentItemO),
                                                                     ParentResourceId = (int)c.ParentResourceId,
                                                                     IsExcluded = (bool)c.IsExcluded,
                                                                     SupplierId = (int)a.SpSupplierId,
                                                                     L2 = Convert.ToString(item.L2),
                                                                     L3 = Convert.ToString(item.L3),
                                                                     L4 = Convert.ToString(item.L4),
                                                                     L5 = Convert.ToString(item.L5),
                                                                     L6 = Convert.ToString(item.L6),
                                                                     C1 = Convert.ToString(item.C1),
                                                                     C2 = Convert.ToString(item.C2),
                                                                     C3 = Convert.ToString(item.C3),
                                                                     C4 = Convert.ToString(item.C4),
                                                                     C5 = Convert.ToString(item.C5),
                                                                     C6 = Convert.ToString(item.C6)
                                                                 }).Union(from bb in _dbContext.TblSupplierPackageRevisions
                                                                    join a in _dbContext.TblSupplierPackages on bb.PrPackSuppId equals a.SpPackSuppId
                                                                    join c in _dbContext.TblRevisionDetails on bb.PrRevId equals c.RdRevisionId
                                                                    join o in _dbContext.TblOriginalBoqVds on c.RdBoqItem equals o.ItemO
                                                                    where(a.SpPackageId == packageId && bb.PrRevNo == 0 && c.IsAlternative == true)
                                                                    select new BoqRessourcesList
                                                                    {
                                                                        RowNumber = o.RowNumber,
                                                                        SectionO = Convert.ToString(o.SectionO),
                                                                        ItemO = Convert.ToString(o.ItemO),
                                                                        DescriptionO = Convert.ToString(o.DescriptionO),
                                                                        UnitO = Convert.ToString(o.UnitO),
                                                                        QtyO = (double) o.QtyO,
                                                                        UnitRateO = (double)o.UnitRate,
                                                                        ScopeO = packageId,
                                                                        BoqSeq = (int)c.ParentResourceId,
                                                                        BoqCtg = Convert.ToString(""),
                                                                        BoqUnitMesure = Convert.ToString(""),
                                                                        BoqQty = (double)c.RdQty,//Final Qty
                                                                        BoqScopeQty = (double)c.RdQty,//Quotation Qty
                                                                        BoqUprice = (double)c.RdPrice,
                                                                        BoqDiv = Convert.ToString(""),
                                                                        BoqPackage = Convert.ToString(""),
                                                                        BoqScope = packageId,
                                                                        ResSeq = Convert.ToString("0"),
                                                                        ResDescription = Convert.ToString(c.ResourceDescription),
                                                                        IsAlternative = true,
                                                                        IsNewItem = false,
                                                                        NewItemId = (int)c.NewItemId,
                                                                        NewItemResourceId = (int)c.NewItemResourceId,
                                                                        ParentItemO = Convert.ToString(c.ParentItemO),
                                                                        ParentResourceId = (int)c.ParentResourceId,
                                                                        IsExcluded = (bool)c.IsExcluded,
                                                                        SupplierId = (int)a.SpSupplierId,
                                                                        L2 = Convert.ToString(o.L2),
                                                                        L3 = Convert.ToString(o.L3),
                                                                        L4 = Convert.ToString(o.L4),
                                                                        L5 = Convert.ToString(o.L5),
                                                                        L6 = Convert.ToString(o.L6),
                                                                        C1 = Convert.ToString(o.C1),
                                                                        C2 = Convert.ToString(o.C2),
                                                                        C3 = Convert.ToString(o.C3),
                                                                        C4 = Convert.ToString(o.C4),
                                                                        C5 = Convert.ToString(o.C5),
                                                                        C6 = Convert.ToString(o.C6)
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

            //var items = condQuery
            //    .GroupBy(x => new { x.RowNumber, x.ItemO, x.DescriptionO, x.UnitO, x.IsNewItem })
            //    //.Select(p => p.FirstOrDefault())
            //    .Select(p => new GroupingBoqModel
            //    {
            //        ItemO = p.First().ItemO,
            //        DescriptionO = p.First().DescriptionO,
            //        IsSelected = false,
            //        RowNumber = p.First().RowNumber.Value,
            //        IsNewItem = p.First().IsNewItem,
            //        IsAlternative = p.Min(x=> x.IsAlternative),
            //        IsExcluded = p.First().IsExcluded
            //    }).ToList();

            var curList = (from b in _mdbContext.TblCurrencies
                           select b).ToList();

            var usedCur = from cur in curList
                          join b in _dbContext.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                          join a in _dbContext.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                          where (a.SpPackageId == packageId && b.PrRevNo == 0)
                          group cur by cur.CurCode into g
                          select new LiveExchange
                          {
                              fromCurrency = g.Key
                          };

            var ExchNowList = (from cur in usedCur
                               select new LiveExchange
                               {
                                   fromCurrency = cur.fromCurrency,
                                   ExchRateNow = GetExchange(cur.fromCurrency)
                               }).ToList();

            //IEnumerable<GroupingPackageSupplierPriceModel> querySupp;

            List<GroupingPackageSupplierPriceModel> PackageSupplierPriceRevDetail;
            List<GroupingPackageSupplierPriceModel> PackageSupplierPriceRevDetailAlt;
            List<GroupingPackageSupplierPriceModel> PackageSupplierPriceRevDetailNew;

            var supList = (from b in _mdbContext.TblSuppliers
                           select b).ToList();

            PackageSupplierPriceRevDetail = (from cur in curList
                             join b in _dbContext.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                             join a in _dbContext.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                             join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                             join sup in supList on a.SpSupplierId equals sup.SupCode
                             where (a.SpPackageId == packageId && b.PrRevNo == 0 && (c.IsNew == false || c.IsNew == null)
                             && (c.IsAlternative == false || c.IsAlternative == null) && (supId == 0 || a.SpSupplierId == supId))
                             select new GroupingPackageSupplierPriceModel
                             {
                                 SupplierId = sup.SupCode,
                                 SupplierName = sup.SupName,
                                 LastRevisionDate = b.PrRevDate,
                                 AssignedPercentage = c.RdAssignedPerc,
                                 AssignedQty = c.RdAssignedQty,
                                 MissedPrice = c.RdMissedPrice,
                                 OriginalCurrencyPrice = c.RdPriceOrigCurrency,
                                 Qty = c.RdQty,
                                 UnitPrice = c.RdPrice,
                                 TotalPrice = (c.RdAssignedQty * c.RdPrice),
                                 BoqResourceId = c.RdResourceSeq,
                                 OriginalCurrency = cur.CurCode,
                                 ExchRate = b.PrExchRate,
                                 ExchRateNow = ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow,
                                 byBoq= (byte)a.SpByBoq,
                                 BoqItemO=c.RdBoqItem,
                                 Discount = c.RdDiscount,
                                 UPriceAfterDiscount = Math.Round((double)(c.UnitPriceAfterDiscount==null ? 0 : c.UnitPriceAfterDiscount), 2),// Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)),2)
                                 IsAlternative = false,
                                 IsNewItem = false,
                                 NewItemId = 0,
                                 NewItemResourceId = 0,
                                 ParentItemO = "",
                                 ParentResourceId =0,
                                 IsExcluded = c.IsExcluded
                             }).ToList();

            //Get all Suppliers of this revision
            var supListRevision = PackageSupplierPriceRevDetail
            .GroupBy(x => new { x.SupplierId, x.SupplierName, x.LastRevisionDate })
            .Select(p => p.FirstOrDefault()).ToList()
            .Select(p => new Supplier
            {
                SupID = p.SupplierId,
                SupName = p.SupplierName,
                RevisionDate = p.LastRevisionDate
            }).ToList();

            //New Items
            PackageSupplierPriceRevDetailNew = (from cur in curList
                                                join b in _dbContext.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                                join a in _dbContext.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                                join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                                join item in _dbContext.NewItems on c.NewItemId equals item.Id
                                                join newr in _dbContext.NewItemResources on c.NewItemResourceId equals newr.Id
                                                join sup in supList on a.SpSupplierId equals sup.SupCode
                                                where (a.SpPackageId == packageId && b.PrRevNo == 0 && c.IsNew == true)
                                                select new GroupingPackageSupplierPriceModel
                                                {
                                                    SupplierId = sup.SupCode,
                                                    SupplierName = sup.SupName,
                                                    LastRevisionDate = b.PrRevDate,
                                                    AssignedPercentage = c.RdAssignedPerc,
                                                    AssignedQty = c.RdAssignedQty,
                                                    MissedPrice = c.RdMissedPrice,
                                                    OriginalCurrencyPrice = c.RdPriceOrigCurrency,
                                                    Qty = c.RdQty,
                                                    UnitPrice = c.RdPrice,
                                                    TotalPrice = (c.RdAssignedQty * c.RdPrice),
                                                    BoqResourceId = c.NewItemResourceId,
                                                    OriginalCurrency = cur.CurCode,
                                                    ExchRate = b.PrExchRate,
                                                    ExchRateNow = ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow,
                                                    byBoq = (byte)a.SpByBoq,
                                                    BoqItemO = Convert.ToString(c.NewItemId),
                                                    Discount = c.RdDiscount,
                                                    UPriceAfterDiscount = Math.Round((double)(c.UnitPriceAfterDiscount), 2),// Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)),2)
                                                    IsAlternative = false,
                                                    IsNewItem = c.IsNew,
                                                    NewItemId = c.NewItemId,
                                                    NewItemResourceId = c.NewItemResourceId,
                                                    ParentItemO = c.ParentItemO,
                                                    ParentResourceId = c.ParentResourceId,
                                                    IsExcluded = c.IsExcluded
                                                }).ToList();

            foreach (var sup in supListRevision.OrderBy(x => x.SupName))
            {
                foreach (var itm in PackageSupplierPriceRevDetailNew)
                {
                    GroupingPackageSupplierPriceModel packSupRevDt = new GroupingPackageSupplierPriceModel
                    {
                        RevisionId = itm.RevisionId,
                        SupplierId = sup.SupID,
                        SupplierName = sup.SupName,
                        LastRevisionDate = sup.RevisionDate,
                        AssignedPercentage = itm.AssignedPercentage,
                        AssignedQty = itm.AssignedQty,
                        MissedPrice = itm.MissedPrice,
                        OriginalCurrencyPrice = itm.OriginalCurrencyPrice,
                        Qty = itm.Qty,
                        UnitPrice = itm.UnitPrice,
                        TotalPrice = itm.TotalPrice,
                        BoqResourceId = itm.BoqResourceId,
                        OriginalCurrency = itm.OriginalCurrency,
                        ExchRate = itm.ExchRate,
                        ExchRateNow = itm.ExchRateNow,
                        byBoq = itm.byBoq,
                        BoqItemO = itm.BoqItemO,
                        Discount = itm.Discount,
                        UPriceAfterDiscount = itm.UPriceAfterDiscount,
                        IsAlternative = itm.IsAlternative,
                        IsNewItem = itm.IsNewItem,
                        NewItemId = itm.NewItemId,
                        NewItemResourceId = itm.NewItemResourceId,
                        ParentItemO = itm.ParentItemO,
                        ParentResourceId = itm.ParentResourceId,
                        isCreatedByThisSupplier = (itm.SupplierId == sup.SupID),
                        IsExcluded = itm.IsExcluded
                    };

                    PackageSupplierPriceRevDetail.Add(packSupRevDt);
                }
            }

            //Alternative Items
            PackageSupplierPriceRevDetailAlt = (from cur in curList
                                                join b in _dbContext.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                                join a in _dbContext.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                                join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                                join sup in supList on a.SpSupplierId equals sup.SupCode
                                                where (a.SpPackageId == packageId && b.PrRevNo == 0 && c.IsAlternative == true)
                                                select new GroupingPackageSupplierPriceModel
                                                {
                                                    SupplierId = sup.SupCode,
                                                    SupplierName = sup.SupName,
                                                    LastRevisionDate = b.PrRevDate,
                                                    AssignedPercentage = c.RdAssignedPerc,
                                                    AssignedQty = c.RdAssignedQty,
                                                    MissedPrice = c.RdMissedPrice,
                                                    OriginalCurrencyPrice = c.RdPriceOrigCurrency,
                                                    Qty = c.RdQty,
                                                    UnitPrice = c.RdPrice,
                                                    TotalPrice = (c.RdAssignedQty * c.RdPrice),
                                                    BoqResourceId = c.ParentResourceId,
                                                    OriginalCurrency = cur.CurCode,
                                                    ExchRate = b.PrExchRate,
                                                    ExchRateNow = ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow,
                                                    byBoq = (byte)a.SpByBoq,
                                                    BoqItemO = c.RdBoqItem,
                                                    Discount = c.RdDiscount,
                                                    UPriceAfterDiscount = Math.Round((double)(c.UnitPriceAfterDiscount), 2),// Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)),2)
                                                    IsAlternative =true,
                                                    IsNewItem = c.IsNew,
                                                    NewItemId = c.NewItemId,
                                                    NewItemResourceId = c.NewItemResourceId,
                                                    ParentItemO = c.ParentItemO,
                                                    ParentResourceId = c.ParentResourceId,
                                                    IsExcluded = c.IsExcluded
                                                }).ToList();

            foreach (var sup in supListRevision.OrderBy(x => x.SupName))
            {
                foreach (var itm in PackageSupplierPriceRevDetailAlt)
                {
                    GroupingPackageSupplierPriceModel packSupRevDt = new GroupingPackageSupplierPriceModel
                    {
                        RevisionId = itm.RevisionId,
                        SupplierId = sup.SupID,
                        SupplierName = sup.SupName,
                        LastRevisionDate = sup.RevisionDate,
                        AssignedPercentage = (itm.SupplierId == sup.SupID) ? itm.AssignedPercentage : 0,
                        AssignedQty = (itm.SupplierId == sup.SupID) ? itm.AssignedQty : 0,
                        MissedPrice = (itm.SupplierId == sup.SupID) ? itm.MissedPrice : 0,
                        OriginalCurrencyPrice = (itm.SupplierId == sup.SupID) ? itm.OriginalCurrencyPrice : 0,
                        Qty = (itm.SupplierId == sup.SupID) ? itm.Qty : 0,
                        UnitPrice = (itm.SupplierId == sup.SupID) ? itm.UnitPrice : 0,
                        TotalPrice = (itm.SupplierId == sup.SupID) ? itm.TotalPrice : 0,
                        OriginalCurrency = itm.OriginalCurrency,
                        BoqResourceId = itm.BoqResourceId,
                        ExchRate = itm.ExchRate,
                        ExchRateNow = itm.ExchRateNow,
                        byBoq = itm.byBoq,
                        BoqItemO = itm.BoqItemO,
                        Discount = (itm.SupplierId == sup.SupID) ? itm.Discount : 0,
                        UPriceAfterDiscount = (itm.SupplierId == sup.SupID) ? itm.UPriceAfterDiscount : 0,
                        IsAlternative = itm.IsAlternative,
                        IsNewItem = itm.IsNewItem,
                        NewItemId = itm.NewItemId,
                        NewItemResourceId = itm.NewItemResourceId,
                        ParentItemO = itm.ParentItemO,
                        ParentResourceId = itm.ParentResourceId,
                        isCreatedByThisSupplier = (itm.SupplierId == sup.SupID),
                        IsExcluded = itm.IsExcluded
                    };

                    PackageSupplierPriceRevDetail.Add(packSupRevDt);
                }
            }


            var levels = condQuery.Select(x => new GroupingLevelModel
            {
                LevelName = (x.L2 != null ? "L2~" + x.L2 : "") +
                          (x.L3 != null ? "|L3~" + x.L3 : "") +
                          (x.L4 != null ? "|L4~" + x.L4 : "") +
                          (x.L5 != null ? "|L5~" + x.L5 : "") +
                          (x.L6 != null ? "|L6~" + x.L6 : "") +
                          (x.C1 != null ? "|C1~" + x.C1 : "") +
                          (x.C2 != null ? "|C2~" + x.C2 : "") +
                          (x.C3 != null ? "|C3~" + x.C3 : "") +
                          (x.C4 != null ? "|C4~" + x.C4 : "") +
                          (x.C5 != null ? "|C5~" + x.C5 : "") +
                          (x.C6 != null ? "|C6~" + x.C6 : "")
            }).DistinctBy(x => x.LevelName).OrderBy(x => x.LevelName).ToList();


            if (levels != null)
            {
                foreach (var level in levels)
                {
                    //        foreach (var item in items)
                    //{

                    level.Items = condQuery
                        .GroupBy(x => new { x.RowNumber, x.ItemO, x.DescriptionO, x.UnitO, x.IsNewItem })
                        //.Select(p => p.FirstOrDefault())
                        .Select(p => new GroupingBoqModel
                        {
                            ItemO = p.First().ItemO,
                            DescriptionO = p.First().DescriptionO,
                            IsSelected = false,
                            RowNumber = p.First().RowNumber.Value,
                            IsNewItem = p.First().IsNewItem,
                            IsAlternative = p.Min(x => x.IsAlternative),
                            IsExcluded = p.First().IsExcluded,
                            LevelName = (p.First().L2 != null ? "L2~" + p.First().L2 : "") +
                             (p.First().L3 != null ? "|L3~" + p.First().L3 : "") +
                             (p.First().L4 != null ? "|L4~" + p.First().L4 : "") +
                             (p.First().L5 != null ? "|L5~" + p.First().L5 : "") +
                             (p.First().L6 != null ? "|L6~" + p.First().L6 : "") +
                             (p.First().C1 != null ? "|C1~" + p.First().C1 : "") +
                             (p.First().C2 != null ? "|C2~" + p.First().C2 : "") +
                             (p.First().C3 != null ? "|C3~" + p.First().C3 : "") +
                             (p.First().C4 != null ? "|C4~" + p.First().C4 : "") +
                             (p.First().C5 != null ? "|C5~" + p.First().C5 : "") +
                             (p.First().C6 != null ? "|C6~" + p.First().C6 : "")
                        }).Where(x => x.LevelName == level.LevelName).OrderBy(a => a.ItemO).ToList();

                    foreach (var item in level.Items)
                    {
                        item.GroupingResources = condQuery.Where(x => x.ItemO == item.ItemO)
                        .GroupBy(x => new { x.ItemO, x.BoqSeq, x.ResSeq, x.IsAlternative, supplier = (x.IsAlternative == true ? x.SupplierId : 0) })
                        .Select(p => p.FirstOrDefault()).ToList()
                        .Select(y => new GroupingResourceModel
                        {
                            BoqSeq = y.BoqSeq,
                            ResourceSeq = y.ResSeq,
                            ResourceDescription = y.ResDescription,
                            Unit = y.BoqUnitMesure,
                            Qty = y.BoqQty,
                            UnitPrice = y.BoqUprice,
                            TotalPrice = (y.BoqQty * y.BoqUprice),
                            ValidPerc = true,
                            IsSelected = false,
                            GroupingPackageSuppliersPrices = PackageSupplierPriceRevDetail.Where(x => x.BoqResourceId == y.BoqSeq && ((x.IsAlternative == false && x.IsAlternative == y.IsAlternative) || (x.IsAlternative == true && x.IsAlternative == y.IsAlternative && x.SupplierId == y.SupplierId))).OrderBy(x => x.SupplierName).ToList(),
                            QuotationQty = y.BoqScopeQty,
                            QuotationAmt = (y.BoqUprice * y.BoqScopeQty),
                            IsNewItem = y.IsNewItem,
                            IsAlternative = y.IsAlternative,
                            IsExcluded = y.IsExcluded
                        }).ToList();


                    if (supId == 0)
                    {
                        byte byBoq;
                        byBoq = PackageSupplierPriceRevDetail.FirstOrDefault().byBoq;

                        if (byBoq == 1)
                        {
                            var minPrice = PackageSupplierPriceRevDetail.Where(p => p.BoqItemO == item.ItemO && p.UPriceAfterDiscount > 0).Min(p => p.UPriceAfterDiscount);
                            var IdealItem = PackageSupplierPriceRevDetail.Where(p => p.BoqItemO == item.ItemO && p.UPriceAfterDiscount == minPrice).FirstOrDefault();

                            item.GroupingPackageSuppliersPrices.Add(new GroupingPackageSupplierPriceModel
                            {
                                SupplierId = 0,
                                SupplierName = "Ideal",
                                LastRevisionDate = null,
                                AssignedPercentage = IdealItem.AssignedPercentage,
                                AssignedQty = IdealItem.Qty,
                                MissedPrice = IdealItem.MissedPrice,
                                OriginalCurrencyPrice = IdealItem.OriginalCurrencyPrice,
                                Qty = IdealItem.Qty,
                                UnitPrice = IdealItem.UnitPrice,
                                TotalPrice = IdealItem.Qty * IdealItem.OriginalCurrencyPrice * IdealItem.ExchRateNow,
                                BoqItemO = IdealItem.BoqItemO,
                                OriginalCurrency = IdealItem.OriginalCurrency,
                                ExchRate = IdealItem.ExchRate,
                                ExchRateNow = IdealItem.ExchRateNow,
                                byBoq = byBoq,
                                Discount = IdealItem.Discount,
                                UPriceAfterDiscount = (IdealItem.IsExcluded == true) ? 0 : Math.Round((double)(IdealItem.UPriceAfterDiscount), 2),//Math.Round((double)(IdealItem.OriginalCurrencyPrice - (IdealItem.OriginalCurrencyPrice * ((IdealItem.Discount == null) ? 0 : IdealItem.Discount) / 100)), 2)
                                IsAlternative = IdealItem.IsAlternative,
                                IsNewItem = IdealItem.IsNewItem,
                                NewItemId = IdealItem.NewItemId,
                                NewItemResourceId = IdealItem.NewItemResourceId,
                                ParentItemO = IdealItem.ParentItemO,
                                ParentResourceId = IdealItem.ParentResourceId
                            });
                        }
                        else
                        {
                            foreach (var res in item.GroupingResources)
                            {
                                double minPrice = 0;
                                if (res.IsAlternative == true)
                                    minPrice = (double)res.GroupingPackageSuppliersPrices.Where(p => p.BoqResourceId == res.BoqSeq && p.UPriceAfterDiscount > 0 && p.IsAlternative == res.IsAlternative).Min(p => p.UPriceAfterDiscount);
                                else
                                    if (PackageSupplierPriceRevDetail.Where(p => p.UPriceAfterDiscount > 0).FirstOrDefault() == null)
                                        minPrice = 0;
                                    else
                                        minPrice = (double)PackageSupplierPriceRevDetail.Where(p => p.BoqResourceId == res.BoqSeq && p.UPriceAfterDiscount > 0 && p.IsAlternative == res.IsAlternative).Min(p => p.UPriceAfterDiscount);

                                    var IdealItem = PackageSupplierPriceRevDetail.Where(p => p.BoqResourceId == res.BoqSeq && p.UPriceAfterDiscount == minPrice && p.IsAlternative == res.IsAlternative).FirstOrDefault();

                                    if (minPrice != null)
                                    res.GroupingPackageSuppliersPrices.Add(new GroupingPackageSupplierPriceModel
                                    {
                                        SupplierId = 0,
                                        SupplierName = "Ideal",
                                        LastRevisionDate = null,
                                        AssignedPercentage = IdealItem.AssignedPercentage,
                                        AssignedQty = IdealItem.Qty,
                                        MissedPrice = IdealItem.MissedPrice,
                                        OriginalCurrencyPrice = IdealItem.OriginalCurrencyPrice,
                                        Qty = IdealItem.Qty,
                                        UnitPrice = IdealItem.UnitPrice,
                                        TotalPrice = IdealItem.Qty * IdealItem.UPriceAfterDiscount * IdealItem.ExchRateNow,
                                        BoqItemO = IdealItem.BoqItemO,
                                        OriginalCurrency = IdealItem.OriginalCurrency,
                                        ExchRate = IdealItem.ExchRate,
                                        ExchRateNow = IdealItem.ExchRateNow,
                                        BoqResourceId = IdealItem.BoqResourceId,
                                        byBoq = byBoq,
                                        Discount = IdealItem.Discount,
                                        UPriceAfterDiscount = (IdealItem.IsExcluded == true) ? 0 : Math.Round((double)(IdealItem.UPriceAfterDiscount), 2),//Math.Round((double)(IdealItem.OriginalCurrencyPrice - (IdealItem.OriginalCurrencyPrice * ((IdealItem.Discount == null) ? 0 : IdealItem.Discount) / 100)), 2)
                                        IsAlternative = IdealItem.IsAlternative,
                                        IsNewItem = IdealItem.IsNewItem,
                                        NewItemId = IdealItem.NewItemId,
                                        NewItemResourceId = IdealItem.NewItemResourceId,
                                        ParentItemO = IdealItem.ParentItemO,
                                        ParentResourceId = IdealItem.ParentResourceId
                                    });
                            }
                        }
                    }

                }
                }
            }

            return levels.OrderBy(x=> x.Items.OrderBy(y=> y.IsNewItem).ThenBy(z => z.IsAlternative)).ToList();
        }

        public List<GroupingLevelModel> GetComparisonSheetByBoq(int packageId, SearchInput input,int supId)
        {
            //IEnumerable<BoqRessourcesList> condQuery
            var condQueryItm = (from bb in _dbContext.TblSupplierPackageRevisions
                             join a in _dbContext.TblSupplierPackages on bb.PrPackSuppId equals a.SpPackSuppId
                             join c in _dbContext.TblRevisionDetails on bb.PrRevId equals c.RdRevisionId
                             join o in _dbContext.TblOriginalBoqVds on c.RdBoqItem equals o.ItemO
                             join b in _dbContext.TblBoqVds on o.ItemO equals b.BoqItem
                             join r in _dbContext.TblResources on b.BoqResSeq equals r.ResSeq
                             where a.SpPackageId == packageId && bb.PrRevNo == 0 && (c.IsNew == false || c.IsNew == null)
                             && (c.IsAlternative == false || c.IsAlternative == null) && (supId == 0 || a.SpSupplierId == supId)
                                select new BoqRessourcesList
                             {
                                 RowNumber = o.RowNumber,
                                 SectionO = Convert.ToString(o.SectionO),
                                 ItemO = Convert.ToString(o.ItemO),
                                 DescriptionO = Convert.ToString(o.DescriptionO),
                                 UnitO = Convert.ToString(o.UnitO),
                                 QtyO = o.QtyO,
                                 ScopeQtyO = o.QtyScope,
                                 UnitRateO = o.UnitRate,
                                 ScopeO = o.Scope,
                                 BoqSeq = b.BoqSeq,
                                 BoqCtg = Convert.ToString(b.BoqCtg),
                                 BoqUnitMesure = Convert.ToString(b.BoqUnitMesure),
                                 BoqQty = b.BoqQty,
                                 BoqUprice = o.UnitRate,
                                 BoqDiv = Convert.ToString(b.BoqDiv),
                                 BoqPackage = Convert.ToString(b.BoqPackage),
                                 BoqScope = b.BoqScope,
                                 ResSeq = Convert.ToString(r.ResSeq),
                                 ResDescription = Convert.ToString(r.ResDescription),
                                 IsAlternative = false,
                                 IsNewItem = false,
                                 IsExcluded = c.IsExcluded,
                                 L2 = o.L2,
                                 L3 = o.L3,
                                 L4 = o.L4,
                                 L5 = o.L5,
                                 L6 = o.L6,
                                 C1 = o.C1,
                                 C2 = o.C2,
                                 C3 = o.C3,
                                 C4 = o.C4,
                                 C5 = o.C5,
                                 C6 = o.C6
                             });

            if (input.BOQDiv.Length > 0) condQueryItm = condQueryItm.Where(w => input.BOQDiv.Contains(w.SectionO));
            if (!string.IsNullOrEmpty(input.BOQItem)) condQueryItm = condQueryItm.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower()));
            if (!string.IsNullOrEmpty(input.BOQDesc)) condQueryItm = condQueryItm.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower()));
            if (!string.IsNullOrEmpty(input.SheetDesc)) condQueryItm = condQueryItm.Where(w => w.ObSheetDesc == input.SheetDesc);
            if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) condQueryItm = condQueryItm.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
            if (input.Package > 0) condQueryItm = condQueryItm.Where(w => w.ScopeO == input.Package);
            if (input.RESDiv.Length > 0) condQueryItm = condQueryItm.Where(w => input.RESDiv.Contains(w.BoqDiv));
            if (input.RESType.Length > 0) condQueryItm = condQueryItm.Where(w => input.RESType.Contains(w.BoqCtg));
            if (!string.IsNullOrEmpty(input.RESDesc)) condQueryItm = condQueryItm.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));

            //New Items
            var condQueryNew = (from bb in _dbContext.TblSupplierPackageRevisions
                                join a in _dbContext.TblSupplierPackages on bb.PrPackSuppId equals a.SpPackSuppId
                                join r in _dbContext.TblRevisionDetails on bb.PrRevId equals r.RdRevisionId
                                join item in _dbContext.NewItems on r.NewItemId equals item.Id
                                where a.SpPackageId == packageId && bb.PrRevNo == 0 && (r.ItemCopiedFromRevision == 0 || r.ItemCopiedFromRevision == null)
                                && (supId == 0 || a.SpSupplierId == supId)
                                select new BoqRessourcesList
                                {
                                    RowNumber = 0,
                                    SectionO = Convert.ToString(""),
                                    ItemO = Convert.ToString(item.Id.ToString()),
                                    DescriptionO = Convert.ToString(r.ItemDescription),
                                    UnitO = Convert.ToString(item.UnitO),
                                    QtyO = (double)r.RdQty,
                                    ScopeQtyO = (double)r.RdQty,
                                    UnitRateO = (double)r.RdPrice,
                                    ScopeO = packageId,
                                    BoqSeq = 0,
                                    BoqCtg = Convert.ToString(""),
                                    BoqUnitMesure = Convert.ToString(item.UnitO),
                                    BoqQty = (double)r.RdQty,
                                    BoqUprice = (double)r.RdPrice,
                                    BoqDiv = Convert.ToString(""),
                                    BoqPackage = Convert.ToString(""),
                                    BoqScope = packageId,
                                    ResSeq = Convert.ToString(""),
                                    ResDescription = Convert.ToString(""),
                                    IsAlternative = false,
                                    IsNewItem = true,
                                    IsExcluded = r.IsExcluded,
                                    L2 = item.L2,
                                    L3 = item.L3,
                                    L4 = item.L4,
                                    L5 = item.L5,
                                    L6 = item.L6,
                                    C1 = item.C1,
                                    C2 = item.C2,
                                    C3 = item.C3,
                                    C4 = item.C4,
                                    C5 = item.C5,
                                    C6 = item.C6
                                }).ToList();

            //Alternative Items
            var condQueryAlt = (from bb in _dbContext.TblSupplierPackageRevisions
                                join a in _dbContext.TblSupplierPackages on bb.PrPackSuppId equals a.SpPackSuppId
                                join r in _dbContext.TblRevisionDetails on bb.PrRevId equals r.RdRevisionId
                                join o in _dbContext.TblOriginalBoqVds on r.ParentItemO equals o.ItemO
                                where (a.SpPackageId == packageId && r.IsAlternative == true && bb.PrRevNo == 0 && (r.ItemCopiedFromRevision == 0 || r.ItemCopiedFromRevision == null))
                                && (supId == 0 || a.SpSupplierId == supId)
                                select new BoqRessourcesList
                                {
                                    RowNumber = o.RowNumber,
                                    SectionO = Convert.ToString(o.SectionO),
                                    ItemO = Convert.ToString(r.RdBoqItem),
                                    DescriptionO = Convert.ToString(r.ItemDescription),
                                    UnitO = Convert.ToString(o.UnitO),
                                    QtyO = r.RdQty,
                                    ScopeQtyO = (double)r.RdQty,
                                    UnitRateO = r.RdPrice,
                                    ScopeO = packageId,
                                    BoqSeq = 0,
                                    BoqCtg = Convert.ToString(""),
                                    BoqUnitMesure = Convert.ToString(o.UnitO),
                                    BoqQty = r.RdQty,
                                    BoqUprice = r.RdPrice,
                                    BoqDiv = Convert.ToString(""),
                                    BoqPackage = Convert.ToString(""),
                                    BoqScope = packageId,
                                    ResSeq = Convert.ToString(""),
                                    ResDescription = Convert.ToString(""),
                                    IsAlternative = true,
                                    IsNewItem = false,
                                    IsExcluded = r.IsExcluded,
                                    L2 = o.L2,
                                    L3 = o.L3,
                                    L4 = o.L4,
                                    L5 = o.L5,
                                    L6 = o.L6,
                                    C1 = o.C1,
                                    C2 = o.C2,
                                    C3 = o.C3,
                                    C4 = o.C4,
                                    C5 = o.C5,
                                    C6 = o.C6
                                }).ToList();

            var condQuery = condQueryItm.ToList();

            foreach (var itm in condQueryNew)
                condQuery.Add(itm);
            foreach (var itm in condQueryAlt)
                condQuery.Add(itm);

            //var items = condQuery
            //.GroupBy(x => new { x.RowNumber, x.SectionO, x.ItemO, x.DescriptionO, x.UnitO, x.QtyO, x.UnitRateO, x.ScopeQtyO, x.IsNewItem, x.IsAlternative, x.IsExcluded })
            //.Select(p => p.FirstOrDefault()).ToList()
            //.Select(p => new GroupingBoqModel
            //{
            //    ItemO = p.ItemO,
            //    DescriptionO = p.DescriptionO,
            //    IsSelected = false,
            //    ValidPerc = true,
            //    RowNumber = p.RowNumber.Value,
            //    Qty = p.QtyO,
            //    UnitPrice = p.UnitRateO,
            //    Unit = p.UnitO,
            //    TotalPrice = (p.QtyO * p.UnitRateO),
            //    QuotationQty = p.ScopeQtyO,
            //    QuotationAmt = (p.ScopeQtyO * p.UnitRateO),
            //    IsNewItem = p.IsNewItem,
            //    IsAlternative = p.IsAlternative,
            //    IsExcluded = p.IsExcluded
            //}).ToList();

            var curList = (from b in _mdbContext.TblCurrencies
                            select b).ToList();

            var usedCur = from cur in curList
                        join b in _dbContext.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                        join a in _dbContext.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                        where (a.SpPackageId == packageId && b.PrRevNo == 0)
                        group cur by cur.CurCode into g
                        select new LiveExchange
                        {
                            fromCurrency = g.Key
                        };

            var ExchNowList = (from cur in usedCur                       
                             select new LiveExchange
                             {                              
                                 fromCurrency = cur.fromCurrency,
                                 ExchRateNow = GetExchange(cur.fromCurrency)
                             }).ToList();

            List<GroupingPackageSupplierPriceModel> PackageSupplierPriceRevDetail;
            List<GroupingPackageSupplierPriceModel> PackageSupplierPriceRevDetailAlt;
            List<GroupingPackageSupplierPriceModel> PackageSupplierPriceRevDetailNew;

            var supList = (from b in _mdbContext.TblSuppliers
                           select b).ToList();

                PackageSupplierPriceRevDetail = (from cur in curList
                             join b in _dbContext.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                             join a in _dbContext.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                             join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                             join sup in supList on a.SpSupplierId equals sup.SupCode
                             where (a.SpPackageId == packageId && b.PrRevNo == 0 && (c.IsNew==false || c.IsNew ==null)
                             && (c.IsAlternative==false || c.IsAlternative ==null) && (supId == 0 || a.SpSupplierId == supId))
                             select new GroupingPackageSupplierPriceModel
                             {
                                 RevisionId=c.RdRevisionId,
                                 SupplierId = sup.SupCode,
                                 SupplierName = sup.SupName,
                                 LastRevisionDate = b.PrRevDate,
                                 AssignedPercentage = c.RdAssignedPerc,
                                 AssignedQty = c.RdAssignedQty,
                                 MissedPrice = c.RdMissedPrice,
                                 OriginalCurrencyPrice = c.RdPriceOrigCurrency,
                                 Qty = c.RdQty,
                                 UnitPrice = Math.Round((double)c.RdPrice, 2),
                                 TotalPrice = Math.Round((double)(c.RdQty * c.RdPrice) ,2),
                                 BoqItemO = c.RdBoqItem,// c.ParentItemO != null ? c.ParentItemO : c.RdBoqItem,
                                 OriginalCurrency = cur.CurCode,
                                 ExchRate = b.PrExchRate,
                                 ExchRateNow = ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow,
                                 Discount = c.RdDiscount != null ? c.RdDiscount:0,
                                 UPriceAfterDiscount = c.UnitPriceAfterDiscount!=null ?  Math.Round((double)(c.UnitPriceAfterDiscount), 2) :0,// Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)),2)
                                 IsAlternative = c.IsAlternative,
                                 IsNewItem = c.IsNew,
                                 NewItemId = c.NewItemId,
                                 NewItemResourceId = c.NewItemResourceId,
                                 ParentItemO = c.ParentItemO,
                                 ParentResourceId = c.ParentResourceId,
                                 IsExcluded= c.IsExcluded
                             }).ToList();

                //Get all Suppliers of this revision
                var supListRevision = PackageSupplierPriceRevDetail
                .GroupBy(x => new { x.SupplierId, x.SupplierName,x.LastRevisionDate,x.RevisionId})
                .Select(p => p.FirstOrDefault()).ToList()
                .Select(p => new Supplier
                {
                    SupID = p.SupplierId,
                    SupName = p.SupplierName,
                    RevisionDate=p.LastRevisionDate,
                    RevisionId=p.RevisionId
                }).ToList();


                //New Items
                PackageSupplierPriceRevDetailNew = (from cur in curList
                                                    join b in _dbContext.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                                    join a in _dbContext.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                                    join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                                    join sup in supList on a.SpSupplierId equals sup.SupCode
                                                    where (a.SpPackageId == packageId && b.PrRevNo == 0 && c.IsNew == true && (c.ItemCopiedFromRevision == 0 || c.ItemCopiedFromRevision == null) )
                                                    select new GroupingPackageSupplierPriceModel
                                                    {
                                                        RevisionId = c.RdRevisionId,
                                                        SupplierId = sup.SupCode,
                                                        SupplierName = sup.SupName,
                                                        LastRevisionDate = b.PrRevDate,
                                                        AssignedPercentage = c.RdAssignedPerc,
                                                        AssignedQty = c.RdAssignedQty,
                                                        MissedPrice = c.RdMissedPrice,
                                                        OriginalCurrencyPrice = c.RdPriceOrigCurrency,
                                                        Qty = c.RdQty,
                                                        UnitPrice = Math.Round((double)c.RdPrice, 2),
                                                        TotalPrice = Math.Round((double)(c.RdQty * c.RdPrice), 2),
                                                        BoqItemO = Convert.ToString(c.NewItemId),// c.ParentItemO != null ? c.ParentItemO : c.NewItemId,
                                                        OriginalCurrency = cur.CurCode,
                                                        ExchRate = b.PrExchRate,
                                                        ExchRateNow = ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow,
                                                        Discount = c.RdDiscount,
                                                        UPriceAfterDiscount = Math.Round((double)(c.UnitPriceAfterDiscount), 2),// Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)),2)
                                                        IsAlternative = c.IsAlternative,
                                                        IsNewItem = c.IsNew,
                                                        NewItemId = c.NewItemId,
                                                        NewItemResourceId = c.NewItemResourceId,
                                                        ParentItemO = c.ParentItemO,
                                                        ParentResourceId = c.ParentResourceId,
                                                        IsExcluded = c.IsExcluded,
                                                        ItemDescription = c.ItemDescription
                                                    }).ToList();

                foreach (var sup in supListRevision.OrderBy(x => x.SupName))
                {
                    foreach (var itm in PackageSupplierPriceRevDetailNew)
                    {
                        GroupingPackageSupplierPriceModel packSupRevDt = new GroupingPackageSupplierPriceModel
                        {
                            RevisionId = itm.RevisionId,
                            SupplierId = sup.SupID,
                            SupplierName = sup.SupName,
                            LastRevisionDate = sup.RevisionDate,
                            AssignedPercentage = itm.AssignedPercentage,
                            AssignedQty = itm.AssignedQty,//(itm.SupplierId == sup.SupID) ? itm.AssignedQty : 0,
                            MissedPrice = itm.MissedPrice,
                            OriginalCurrencyPrice = itm.OriginalCurrencyPrice,
                            Qty = itm.Qty,
                            UnitPrice = itm.UnitPrice,
                            TotalPrice = itm.TotalPrice,
                            BoqItemO = itm.BoqItemO,
                            OriginalCurrency = itm.OriginalCurrency,
                            ExchRate = itm.ExchRate,
                            ExchRateNow = itm.ExchRateNow,
                            Discount = itm.Discount,
                            UPriceAfterDiscount = itm.UPriceAfterDiscount,
                            IsAlternative = itm.IsAlternative,
                            IsNewItem = itm.IsNewItem,
                            NewItemId = itm.NewItemId,
                            NewItemResourceId = itm.NewItemResourceId,
                            ParentItemO = itm.ParentItemO,
                            ParentResourceId = itm.ParentResourceId,
                            isCreatedByThisSupplier = (itm.SupplierId == sup.SupID),
                            IsExcluded = itm.IsExcluded
                        };

                        PackageSupplierPriceRevDetail.Add(packSupRevDt);

                    /*Add Items those added by another Supplier */
                    var ItemRevDtl = _dbContext.TblRevisionDetails.Where(x => x.RdRevisionId == sup.RevisionId && x.RdBoqItem == itm.BoqItemO && x.IsNew==true).FirstOrDefault();
                    if (ItemRevDtl == null)
                    {
                        var revdtl = new TblRevisionDetail()
                        {
                            RdRevisionId = (int)sup.RevisionId,
                            RdResourceSeq = 0,
                            RdBoqItem = itm.BoqItemO,
                            RdPrice = itm.UnitPrice,
                            RdPriceOrigCurrency = itm.OriginalCurrencyPrice,
                            RdQty = itm.Qty,
                            RdQuotationQty = itm.Qty,
                            RdAssignedPerc = 0,
                            RdAssignedQty = 0,
                            RdMissedPrice = itm.MissedPrice,
                            RdDiscount = itm.Discount,
                            IsAlternative = itm.IsAlternative,
                            IsNew = itm.IsNewItem,
                            NewItemId = itm.NewItemId,
                            NewItemResourceId = itm.NewItemResourceId,
                            ParentItemO = itm.ParentItemO,
                            ParentResourceId = itm.ParentResourceId,
                            ResourceDescription = "",
                            ItemDescription = itm.ItemDescription,
                            UnitPriceAfterDiscount = itm.UPriceAfterDiscount,
                            TotalPrice = itm.TotalPrice,
                            ItemCopiedFromRevision = itm.RevisionId
                        };

                        _dbContext.Add<TblRevisionDetail>(revdtl);
                        _dbContext.SaveChanges();
                    }
                }
                }

                //Alternative Items
                PackageSupplierPriceRevDetailAlt = (from cur in curList
                                             join b in _dbContext.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                             join a in _dbContext.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                             join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                             join sup in supList on a.SpSupplierId equals sup.SupCode
                                             where (a.SpPackageId == packageId && b.PrRevNo == 0 && c.IsAlternative == true && (c.ItemCopiedFromRevision == 0 || c.ItemCopiedFromRevision == null))
                                             select new GroupingPackageSupplierPriceModel
                                             {
                                                 RevisionId = c.RdRevisionId,
                                                 SupplierId = sup.SupCode,
                                                 SupplierName = sup.SupName,
                                                 LastRevisionDate = b.PrRevDate,
                                                 AssignedPercentage = c.RdAssignedPerc,
                                                 AssignedQty = c.RdAssignedQty,
                                                 MissedPrice = c.RdMissedPrice,
                                                 OriginalCurrencyPrice = c.RdPriceOrigCurrency,
                                                 Qty = c.RdQty,
                                                 UnitPrice = Math.Round((double)c.RdPrice, 2),
                                                 TotalPrice = Math.Round((double)(c.RdQty * c.RdPrice), 2),
                                                 BoqItemO = c.RdBoqItem , // c.ParentItemO != null ? c.ParentItemO : c.RdBoqItem,
                                                 OriginalCurrency = cur.CurCode,
                                                 ExchRate = b.PrExchRate,
                                                 ExchRateNow = ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow,
                                                 Discount = c.RdDiscount,
                                                 UPriceAfterDiscount = Math.Round((double)(c.UnitPriceAfterDiscount), 2),// Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)),2)
                                                 IsAlternative = c.IsAlternative,
                                                 IsNewItem = c.IsNew,
                                                 NewItemId = c.NewItemId,
                                                 NewItemResourceId = c.NewItemResourceId,
                                                 ParentItemO = c.ParentItemO,
                                                 ParentResourceId = c.ParentResourceId,
                                                 IsExcluded = c.IsExcluded,
                                                 ItemDescription=c.ItemDescription
                                             }).ToList();

                foreach (var sup in supListRevision.OrderBy(x => x.SupName))
                {
                    foreach (var itm in PackageSupplierPriceRevDetailAlt)
                    {
                        GroupingPackageSupplierPriceModel packSupRevDt = new GroupingPackageSupplierPriceModel
                        {
                            RevisionId = itm.RevisionId,
                            SupplierId = sup.SupID,
                            SupplierName = sup.SupName,
                            LastRevisionDate = sup.RevisionDate,
                            AssignedPercentage = itm.AssignedPercentage,//(itm.SupplierId == sup.SupID) ? itm.AssignedPercentage : 0,
                            AssignedQty = itm.AssignedQty,//(itm.SupplierId == sup.SupID) ? itm.AssignedQty : 0,
                            MissedPrice = itm.MissedPrice,//(itm.SupplierId == sup.SupID) ? itm.MissedPrice : 0,
                            OriginalCurrencyPrice = itm.OriginalCurrencyPrice,//(itm.SupplierId == sup.SupID) ? itm.OriginalCurrencyPrice : 0,
                            Qty = itm.Qty,//(itm.SupplierId == sup.SupID) ? itm.Qty : 0,
                            UnitPrice = itm.UnitPrice,//(itm.SupplierId == sup.SupID) ? itm.UnitPrice : 0,
                            TotalPrice = itm.TotalPrice,//(itm.SupplierId == sup.SupID) ? itm.TotalPrice : 0,
                            BoqItemO = itm.BoqItemO,
                            OriginalCurrency = itm.OriginalCurrency,
                            ExchRate = itm.ExchRate,
                            ExchRateNow = itm.ExchRateNow,
                            Discount = itm.Discount, //(itm.SupplierId == sup.SupID) ? itm.Discount : 0,
                            UPriceAfterDiscount = itm.UPriceAfterDiscount,// (itm.SupplierId == sup.SupID) ? itm.UPriceAfterDiscount : 0,
                            IsAlternative = itm.IsAlternative,
                            IsNewItem = itm.IsNewItem,
                            NewItemId = itm.NewItemId,
                            NewItemResourceId = itm.NewItemResourceId,
                            ParentItemO = itm.ParentItemO,
                            ParentResourceId = itm.ParentResourceId,
                            isCreatedByThisSupplier = (itm.SupplierId == sup.SupID),
                            IsExcluded = itm.IsExcluded
                        };

                        PackageSupplierPriceRevDetail.Add(packSupRevDt);

                    /*Add Items those added from another Supplier */
                    var ItemRevDtl = _dbContext.TblRevisionDetails.Where(x => x.RdRevisionId == sup.RevisionId && x.RdBoqItem == itm.BoqItemO && x.IsAlternative == true).FirstOrDefault();
                        if (ItemRevDtl==null)
                        {
                            var revdtl = new TblRevisionDetail()
                            {
                                RdRevisionId =(int) sup.RevisionId,
                                RdResourceSeq = 0,
                                RdBoqItem = itm.BoqItemO,
                                RdPrice = itm.UnitPrice,
                                RdPriceOrigCurrency = itm.OriginalCurrencyPrice,
                                RdQty = itm.Qty,
                                RdQuotationQty = itm.Qty,
                                RdAssignedPerc=0,
                                RdAssignedQty=0,
                                RdMissedPrice = itm.MissedPrice,
                                RdDiscount = itm.Discount,
                                IsAlternative = itm.IsAlternative,
                                IsNew = itm.IsNewItem,
                                NewItemId = itm.NewItemId,
                                NewItemResourceId = itm.NewItemResourceId,
                                ParentItemO = itm.ParentItemO,
                                ParentResourceId = itm.ParentResourceId,
                                ResourceDescription = "",
                                ItemDescription = itm.ItemDescription,
                                UnitPriceAfterDiscount = itm.UPriceAfterDiscount,
                                TotalPrice = itm.TotalPrice,
                                ItemCopiedFromRevision= itm.RevisionId
                            };

                        _dbContext.Add<TblRevisionDetail>(revdtl);
                        _dbContext.SaveChanges();
                        }
                    }
                }


            var levels = condQuery.Select(x => new GroupingLevelModel
            {
                LevelName = (x.L2 != null ? "L2~" + x.L2 : "") +
                                      (x.L3 != null ? "|L3~" + x.L3 : "") +
                                      (x.L4 != null ? "|L4~" + x.L4 : "") +
                                      (x.L5 != null ? "|L5~" + x.L5 : "") +
                                      (x.L6 != null ? "|L6~" + x.L6 : "") +
                                      (x.C1 != null ? "|C1~" + x.C1 : "") +
                                      (x.C2 != null ? "|C2~" + x.C2 : "") +
                                      (x.C3 != null ? "|C3~" + x.C3 : "") +
                                      (x.C4 != null ? "|C4~" + x.C4 : "") +
                                      (x.C5 != null ? "|C5~" + x.C5 : "") +
                                      (x.C6 != null ? "|C6~" + x.C6 : "")
            }).DistinctBy(x => x.LevelName).OrderBy(x => x.LevelName).ToList();


            if (levels != null)
            {
                foreach (var level in levels)
                {
                    level.Items = condQuery
                    .GroupBy(x => new { x.RowNumber, x.SectionO, x.ItemO, x.DescriptionO, x.UnitO, x.QtyO, x.UnitRateO, x.ScopeQtyO, x.IsNewItem, x.IsAlternative, x.IsExcluded })
                    .Select(p => p.FirstOrDefault()).ToList()
                    .Select(p => new GroupingBoqModel
                    {
                        ItemO = p.ItemO,
                        DescriptionO = p.DescriptionO,
                        IsSelected = false,
                        ValidPerc = true,
                        RowNumber = p.RowNumber.Value,
                        Qty = p.QtyO,
                        UnitPrice = p.UnitRateO,
                        Unit = p.UnitO,
                        TotalPrice = (p.QtyO * p.UnitRateO),
                        QuotationQty = p.ScopeQtyO,
                        QuotationAmt = (p.ScopeQtyO * p.UnitRateO),
                        IsNewItem = p.IsNewItem,
                        IsAlternative = p.IsAlternative,
                        IsExcluded = p.IsExcluded,
                        LevelName = (p.L2 != null ? "L2~" + p.L2 : "") +
                             (p.L3 != null ? "|L3~" + p.L3 : "") +
                             (p.L4 != null ? "|L4~" + p.L4 : "") +
                             (p.L5 != null ? "|L5~" + p.L5 : "") +
                             (p.L6 != null ? "|L6~" + p.L6 : "") +
                             (p.C1 != null ? "|C1~" + p.C1 : "") +
                             (p.C2 != null ? "|C2~" + p.C2 : "") +
                             (p.C3 != null ? "|C3~" + p.C3 : "") +
                             (p.C4 != null ? "|C4~" + p.C4 : "") +
                             (p.C5 != null ? "|C5~" + p.C5 : "") +
                             (p.C6 != null ? "|C6~" + p.C6 : "")
                    }).Where(x => x.LevelName == level.LevelName).OrderBy(a => a.ItemO).ToList();


                    foreach (var item in level.Items)
                    {
                        item.GroupingPackageSuppliersPrices = PackageSupplierPriceRevDetail.Where(x => x.BoqItemO == item.ItemO).OrderBy(x => x.SupplierName).ToList();

                        if (supId == 0)
                        {
                            var minPrice = PackageSupplierPriceRevDetail.Where(p => p.BoqItemO == item.ItemO && p.UPriceAfterDiscount > 0).Min(p => p.UPriceAfterDiscount);
                            var IdealItem = PackageSupplierPriceRevDetail.Where(p => p.BoqItemO == item.ItemO && p.UPriceAfterDiscount == minPrice).FirstOrDefault();

                            if (IdealItem != null)
                            {
                                item.GroupingPackageSuppliersPrices.Add(new GroupingPackageSupplierPriceModel
                                {
                                    SupplierId = 0,
                                    SupplierName = "Ideal",
                                    LastRevisionDate = null,
                                    AssignedPercentage = IdealItem.AssignedPercentage,
                                    AssignedQty = (IdealItem.IsExcluded == true) ? 0 : IdealItem.Qty,
                                    MissedPrice = IdealItem.MissedPrice,
                                    OriginalCurrencyPrice = IdealItem.OriginalCurrencyPrice,
                                    Qty = (IdealItem.IsExcluded == true) ? 0 : IdealItem.Qty,
                                    UnitPrice = IdealItem.UnitPrice,
                                    TotalPrice = IdealItem.Qty * IdealItem.UPriceAfterDiscount * IdealItem.ExchRateNow,
                                    BoqItemO = IdealItem.BoqItemO,
                                    OriginalCurrency = IdealItem.OriginalCurrency,
                                    ExchRate = IdealItem.ExchRate,
                                    ExchRateNow = IdealItem.ExchRateNow,
                                    Discount = IdealItem.Discount,
                                    UPriceAfterDiscount = (IdealItem.IsExcluded == true) ? 0 : Math.Round((double)(IdealItem.UPriceAfterDiscount), 2),//Math.Round((double)(IdealItem.OriginalCurrencyPrice - (IdealItem.OriginalCurrencyPrice * ((IdealItem.Discount == null) ? 0 : IdealItem.Discount) / 100)), 2)
                                    IsAlternative = IdealItem.IsAlternative,
                                    IsNewItem = IdealItem.IsNewItem,
                                    NewItemId = IdealItem.NewItemId,
                                    NewItemResourceId = IdealItem.NewItemResourceId,
                                    ParentItemO = IdealItem.ParentItemO,
                                    ParentResourceId = IdealItem.ParentResourceId
                                });
                            }
                        }
                    }
                }
            }

            return levels;
        }

        public List<GroupingBoqGroupModel> GetComparisonSheetBoqByGroup(int packageId, SearchInput input)
        {
            IEnumerable<BoqRessourcesList> condQuery = (from o in _dbContext.TblOriginalBoqVds
                                                        join b in _dbContext.TblBoqVds on o.ItemO equals b.BoqItem
                                                        join r in _dbContext.TblResources on b.BoqResSeq equals r.ResSeq
                                                        join g in _dbContext.ComparisonPackageGroups on o.GroupId equals g.Id
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
                                                            BoqUprice = o.UnitRate,
                                                            BoqDiv = b.BoqDiv,
                                                            BoqPackage = b.BoqPackage,
                                                            BoqScope = b.BoqScope,
                                                            ResSeq = r.ResSeq,
                                                            ResDescription = r.ResDescription,
                                                            GroupName = g.Name,
                                                            GroupId = g.Id
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


            var groups = condQuery
                .GroupBy(x => new { x.GroupId, x.GroupName })
                //.Select(p => p.Gr)
                .Select(p => new GroupingBoqGroupModel
                {
                    Id = p.First().GroupId.HasValue ? p.First().GroupId.Value : 0,
                    Name = p.First().GroupName,
                    ValidPerc = true,
                    TotalQty = p.Sum(c => c.QtyO),
                    TotalPrice = p.Sum(c => c.QtyO * c.UnitRateO)
                }).ToList();

            var curList = (from b in _mdbContext.TblCurrencies
                           select b).ToList();

            var usedCur = from cur in curList
                          join b in _dbContext.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                          join a in _dbContext.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                          where (a.SpPackageId == packageId && b.PrRevNo == 0)
                          group cur by cur.CurCode into g
                          select new LiveExchange
                          {
                              fromCurrency = g.Key
                          };

            var ExchNowList = (from cur in usedCur
                               select new LiveExchange
                               {
                                   fromCurrency = cur.fromCurrency,
                                   ExchRateNow = GetExchange(cur.fromCurrency)
                               }).ToList();
            var supList = (from b in _mdbContext.TblSuppliers
                           select b).ToList();

            var querySupp = (from cur in curList
                             join b in _dbContext.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                             join a in _dbContext.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                             join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                             join sup in supList on a.SpSupplierId equals sup.SupCode
                             join boq in _dbContext.TblOriginalBoqVds on c.RdBoqItem equals boq.ItemO
                             join g in _dbContext.ComparisonPackageGroups on boq.GroupId equals g.Id
                             where (a.SpPackageId == packageId && boq.Scope == packageId && b.PrRevNo == 0)
                             select new GroupingPackageSupplierPriceModel
                             {
                                 SupplierId = sup.SupCode,
                                 SupplierName = sup.SupName,
                                 LastRevisionDate = b.PrRevDate,
                                 AssignedPercentage = c.RdAssignedPerc,
                                 AssignedQty = c.RdAssignedQty,
                                 MissedPrice = c.RdMissedPrice,
                                 TotalPrice = (c.RdAssignedQty * c.RdPriceOrigCurrency),
                                 GroupId = g.Id,
                                 OriginalCurrency = cur.CurCode,
                                 ExchRate = b.PrExchRate,
                                 ExchRateNow = ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow
                             }).ToList();

            foreach (var group in groups)
            {
                var lsst = querySupp.Where(x => x.GroupId == group.Id).ToList();
                group.GroupingPackageSuppliersPrices = lsst
                    .GroupBy(x => new {
                        x.SupplierId,
                        x.SupplierName
                    })
                    .Select(p => new GroupingPackageSupplierPriceModel
                    {
                        SupplierId = p.First().SupplierId,
                        SupplierName = p.First().SupplierName,
                        LastRevisionDate = p.First().LastRevisionDate,
                        AssignedPercentage = p.First().AssignedPercentage,
                        MissedPrice = p.First().MissedPrice,
                        TotalPrice = p.Sum(c => c.TotalPrice) * p.First().ExchRateNow,
                        OriginalCurrency = p.First().OriginalCurrency,
                        ExchRate = p.First().ExchRate,
                        ExchRateNow = p.First().ExchRateNow
                    }).OrderBy(x => x.SupplierName).ToList();
            }
            return groups;
        }

        public List<GroupingBoqGroupModel> GetComparisonSheetResourcesByGroup(int packageId, SearchInput input)
        {
            IEnumerable<BoqRessourcesList> condQuery = (from o in _dbContext.TblOriginalBoqVds
                                                        join b in _dbContext.TblBoqVds on o.ItemO equals b.BoqItem
                                                        join r in _dbContext.TblResources on b.BoqResSeq equals r.ResSeq
                                                        join g in _dbContext.ComparisonPackageGroups on b.GroupId equals g.Id
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
                                                            ResDescription = r.ResDescription,
                                                            GroupName = g.Name,
                                                            GroupId = g.Id
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


            var groups = condQuery
                .GroupBy(x => new { x.GroupId, x.GroupName })
                //.Select(p => p.Gr)
                .Select(p => new GroupingBoqGroupModel
                {
                    Id = p.First().GroupId.HasValue ? p.First().GroupId.Value : 0,
                    Name = p.First().GroupName,
                    ValidPerc = true,
                    TotalPrice = p.Sum(c => c.BoqQty * c.BoqUprice)
                }).ToList();

            var curList = (from b in _mdbContext.TblCurrencies
                           select b).ToList();

            var usedCur = from cur in curList
                          join b in _dbContext.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                          join a in _dbContext.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                          where (a.SpPackageId == packageId && b.PrRevNo == 0)
                          group cur by cur.CurCode into g
                          select new LiveExchange
                          {
                              fromCurrency = g.Key
                          };

            var ExchNowList = (from cur in usedCur
                               select new LiveExchange
                               {
                                   fromCurrency = cur.fromCurrency,
                                   ExchRateNow = GetExchange(cur.fromCurrency)
                               }).ToList();
            var supList = (from b in _mdbContext.TblSuppliers
                           select b).ToList();


            var querySupp = (from cur in curList
                             join b in _dbContext.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                             join a in _dbContext.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                             join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                             join sup in supList on a.SpSupplierId equals sup.SupCode
                             join boq in _dbContext.TblBoqVds on c.RdResourceSeq equals boq.BoqSeq
                             join g in _dbContext.ComparisonPackageGroups on boq.GroupId equals g.Id
                             where (a.SpPackageId == packageId && boq.BoqScope == packageId && b.PrRevNo == 0)
                             select new GroupingPackageSupplierPriceModel
                             {
                                 SupplierId = sup.SupCode,
                                 SupplierName = sup.SupName,
                                 LastRevisionDate = b.PrRevDate,
                                 AssignedPercentage = c.RdAssignedPerc,
                                 AssignedQty = c.RdAssignedQty,
                                 MissedPrice = c.RdMissedPrice,
                                 TotalPrice = (c.RdAssignedQty * c.RdPriceOrigCurrency),
                                 GroupId = g.Id,
                                 OriginalCurrency = cur.CurCode,
                                 ExchRate = b.PrExchRate,
                                 ExchRateNow = ExchNowList.Find(x=> x.fromCurrency ==cur.CurCode).ExchRateNow
                             }).ToList();

            foreach (var group in groups)
            {
                var lsst = querySupp.Where(x => x.GroupId == group.Id).ToList();
                group.GroupingPackageSuppliersPrices = lsst
                    .GroupBy(x => new { x.SupplierId, 
                        x.SupplierName
                        })
                    .Select(p => new GroupingPackageSupplierPriceModel
                    {
                        SupplierId = p.First().SupplierId,
                        SupplierName = p.First().SupplierName,
                        LastRevisionDate = p.First().LastRevisionDate,
                        AssignedPercentage = p.First().AssignedPercentage,
                        MissedPrice = p.First().MissedPrice,
                        TotalPrice = p.Sum(c => c.TotalPrice) * p.First().ExchRateNow,
                        OriginalCurrency = p.First().OriginalCurrency,
                        ExchRate = p.First().ExchRate,
                        ExchRateNow =p.First().ExchRateNow
                    }).OrderBy(x => x.SupplierName).ToList();
            }
            return groups;
        }

        public string GetComparisonSheet_Excel(int packageId, SearchInput input, List<boqPackageList> boqPackageList, List<TmpComparisonConditionsReply> comcondRepLst, List<TmpComparisonConditionsReply> techcondRepLst)
        {
            List<GroupingLevelModel> levels = GetComparisonSheet(packageId, input,0);

            var package = _mdbContext.TblPackages.Where(x => x.PkgeId == packageId).FirstOrDefault();
            string PackageName = package.PkgeName;

            var p = _dbContext.TblParameters.FirstOrDefault();
            //var proj = _pdbContext.Tblprojects.Where(x => x.Seq == p.TsProjId).FirstOrDefault();
            //string ProjectName = proj.PrjName;
            string ProjectName = p.Project;

            List<string> suppliers = new List<string>();

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("BOQ Comparison");
                worksheet.Columns.AutoFit();
                worksheet.Protection.IsProtected = false;

                int row=0, j, c;

                worksheet.Cells["A1:C1"].Merge = true;
                worksheet.Cells["A2:C2"].Merge = true;
                worksheet.Cells["A3:C3"].Merge = true;
                worksheet.Cells["A4:C4"].Merge = true;
                worksheet.Cells["A5:C5"].Merge = true;

                worksheet.Cells[2, 1].Value = "Résumé des Offres/Feuille de Comparaison";
                worksheet.Cells[2, 1].Style.Font.Bold = true;
                worksheet.Cells[2, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[3, 1].Value = "Project:" + ProjectName;
                worksheet.Cells[3, 1].Style.Font.Bold = true;
                worksheet.Cells[3, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[4, 1].Value = "Department :";
                worksheet.Cells[4, 1].Style.Font.Bold = true;
                worksheet.Cells[4, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[5, 1].Value = PackageName;
                worksheet.Cells[5, 1].Style.Font.Bold = true;
                worksheet.Cells[5, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.SelectedRange[5, 50].Style.Font.Bold = true;
                worksheet.SelectedRange[5, 50].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.SelectedRange[7, 50].Style.Font.Bold = true;
                worksheet.SelectedRange[7, 50].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells["E6:F6"].Merge = true;
                worksheet.Cells[6, 5].Value = "ACC budget";
                worksheet.Cells[6, 5].Style.Font.Bold = true;
                worksheet.Cells[6, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Column(5).Width = 20;

                foreach (var level in levels)
                {
                    if (level.Items.Count > 0)
                    {
                        GroupingBoqModel item1 = level.Items.First();
                        GroupingResourceModel sup = item1.GroupingResources.First();
                        string boq = item1.ItemO;

                        //var lst = item1.GroupingPackageSuppliersPrices.Where(x => x.BoqItemO == boq).OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();
                        //var lst1 = lst.OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();

                        var SupList = sup.GroupingPackageSuppliersPrices.OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();

                        int col = 7;
                        int m = 7;
                        foreach (var l in SupList)
                        {
                            worksheet.Cells[6, m].Value = l.SupplierName == "Ideal" ? l.SupplierName : l.SupplierName + " " + DateTime.Parse(l.LastRevisionDate.ToString()).ToString("dd/MM/yyyy");
                            worksheet.Cells[6, m].Style.Font.Bold = true;
                            worksheet.Columns[m].Style.WrapText = true;
                            worksheet.Column(m).AutoFit();
                            worksheet.Cells[6, m].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells[6, m, 6, m + 2].Merge = true;
                            m = m + 3;
                            if (!suppliers.Contains(l.SupplierName))
                                suppliers.Add(l.SupplierName.ToString());

                            col++;
                        }
                    }

                    row = 7;
                    worksheet.Cells[row, 1].Value = "No";
                    worksheet.Cells[row, 2].Value = "Description";
                    worksheet.Column(2).Width = 70;
                    worksheet.Columns[2].Style.WrapText = true;
                    worksheet.Column(2).AutoFit();
                    worksheet.Cells[row, 3].Value = "U.";
                    worksheet.Cells[row, 4].Value = "Qty Total";
                    worksheet.Cells[row, 5].Value = "P.U.";
                    worksheet.Cells[row, 6].Value = "P.T.";

                    worksheet.Cells[row, 1].EntireRow.Style.Font.Bold = true;
                    worksheet.Cells[row, 1].EntireRow.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    row = 9;
                    j = 0;
                    foreach (var item in level.Items)
                    {
                        //var lst = item.GroupingPackageSuppliersPrices.OrderByDescending(s => s.GroupId).OrderByDescending(s => s.BoqItemO).OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();
                        //foreach (var sup in item.GroupingPackageSuppliersPrices)
                        //{
                        worksheet.Cells[row, 1].Value = j++;
                        worksheet.Column(2).Width = 70;
                        worksheet.Cells[row, 1].Value = (item.ItemO) == null ? "" : item.ItemO;
                        worksheet.Cells[row, 2].Value = (item.DescriptionO) == null ? "" : item.DescriptionO;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        worksheet.Columns[2].Style.WrapText = true;

                        row++;

                        foreach (var res in item.GroupingResources)
                        {
                            worksheet.Cells[row, 2].Value = (res.ResourceDescription) == null ? "" : res.ResourceDescription;
                            worksheet.Cells[row, 3].Value = (res.Unit) == null ? "" : res.Unit;
                            worksheet.Cells[row, 4].Value = (res.Qty) == null ? "" : res.Qty;
                            worksheet.Cells[row, 5].Value = (res.UnitPrice) == null ? "" : res.UnitPrice;
                            worksheet.Cells[row, 6].Value = (res.TotalPrice) == null ? "" : res.TotalPrice;

                            int col = 0;
                            foreach (var suplier in suppliers)
                            {
                                var v = worksheet.Cells[7, 7 + col].Value;
                                if (v == null)
                                {
                                    worksheet.Cells[7, 7 + col].Value = "Assigned Qty";
                                    worksheet.Cells[7, 8 + col].Value = "P.U.";
                                    worksheet.Cells[7, 9 + col].Value = "P.T.";
                                }

                                var supReply = res.GroupingPackageSuppliersPrices.Where(x => x.BoqResourceId == res.BoqSeq && x.SupplierName == suplier.ToString()).OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).FirstOrDefault();
                                if (supReply != null)
                                {
                                    worksheet.Cells[row, 7 + col].Value = (supReply.AssignedQty) == null ? "" : supReply.AssignedQty;
                                    worksheet.Cells[row, 8 + col].Value = (supReply.UnitPrice) == null ? "" : supReply.UPriceAfterDiscount * supReply.ExchRateNow;
                                    worksheet.Cells[row, 9 + col].Value = (supReply.TotalPrice) == null ? "" : supReply.AssignedQty * supReply.UPriceAfterDiscount * supReply.ExchRateNow;
                                }
                                col = col + 3;
                            }
                            row++;
                        }
                        //}
                        row++;
                    }

                    row++;
                }

                row++;

                //Commercial Conditions
                var comcondRep = comcondRepLst.OrderBy(r => r.CondDesc).ToList();

                var replies = comcondRep.GroupBy(x => new { x.CondDesc,x.AccCond })
                .Select(p => p.FirstOrDefault())
                .Select(p => new TmpComparisonConditionsReply
                {
                    CondId = p.CondId,
                    CondDesc = p.CondDesc,
                    AccCond = p.AccCond
                })
                .ToList();

                if (replies.Count > 0)
                {
                    worksheet.SelectedRange[row, 3].Merge = true;
                    worksheet.Cells[row, 1].Value = "Commercial Conditions";
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;

                    worksheet.Cells[row, 3, row, 3 + 4].Merge = true;
                    worksheet.Cells[row, 3].Value = "ACC Conditions";
                    worksheet.Cells[row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 3].Style.Font.Bold = true;

                    row++;
                    foreach (var reply in replies)
                    {
                        worksheet.Cells[row, 2].Value = reply.CondDesc;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        worksheet.Cells[row, 3, row, 3 + 4].Merge = true;
                        worksheet.Cells[row, 3].Value = reply.AccCond;
                        worksheet.Cells[row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        int colsup = 7;
                        foreach (var sup in suppliers)
                        {
                            var supReply = comcondRepLst.Where(x => x.SupName == sup.ToString() && x.CondId == reply.CondId).FirstOrDefault();
                            if (supReply != null)
                                worksheet.Cells[row, colsup].Value = (supReply.CondReply) == null ? "" : supReply.CondReply;

                            colsup = colsup + 3;
                        }
                        row++;
                    }
                }

                row++;

                //Technical Conditions
                var techcondRep = techcondRepLst.OrderBy(r => r.CondDesc).ToList();

                var treplies = techcondRep.GroupBy(x => new { x.CondDesc, x.AccCond })
                .Select(p => p.FirstOrDefault())
                .Select(p => new TmpComparisonConditionsReply
                {
                    CondId = p.CondId,
                    CondDesc = p.CondDesc,
                    AccCond=p.AccCond
                })
                .ToList();

                if (treplies.Count > 0)
                {
                    worksheet.SelectedRange[row, 3].Merge = true;
                    worksheet.Cells[row, 1].Value = "Technical Conditions";
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;

                    worksheet.Cells[row, 3, row, 3 + 4].Merge = true;
                    worksheet.Cells[row, 3].Value = "ACC Conditions";
                    worksheet.Cells[row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 3].Style.Font.Bold = true;

                    row++;
                    foreach (var reply in treplies)
                    {
                        worksheet.Cells[row, 2].Value = reply.CondDesc;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        worksheet.Cells[row, 3, row, 3 + 4].Merge = true;
                        worksheet.Cells[row, 3].Value = reply.AccCond;
                        worksheet.Cells[row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        int colsup = 7;
                        foreach (var sup in suppliers)
                        {
                            var supReply = techcondRepLst.Where(x => x.SupName == sup.ToString() && x.CondId == reply.CondId).FirstOrDefault();
                            if (supReply != null)
                                worksheet.Cells[row, colsup].Value = (supReply.CondReply) == null ? "" : supReply.CondReply;

                            colsup = colsup + 3;
                        }
                        row++;
                    }
                }


                xlPackage.Save();
                stream.Position = 0;
                string excelName = $"{ProjectName}-{PackageName}-Comparison-{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx";

                //string path = @"C:\App\";

                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}
                //string FullPath = path + excelName;

                if (File.Exists(excelName))
                    File.Delete(excelName);

                excelName = excelName.Replace("/", "-");
                xlPackage.SaveAs(excelName);

                return excelName;
            }
        }

        public string GetComparisonSheetByBoq_Excel(int packageId, SearchInput input, List<boqPackageList> boqPackageList, List<TmpComparisonConditionsReply> comcondRepLst, List<TmpComparisonConditionsReply> techcondRepLst)
        {
            List<GroupingLevelModel> levels = GetComparisonSheetByBoq(packageId, input,0);

            var package = _mdbContext.TblPackages.Where(x => x.PkgeId == packageId).FirstOrDefault();
            string PackageName = package.PkgeName;

            var p = _dbContext.TblParameters.FirstOrDefault();
            //var proj = _pdbContext.Tblprojects.Where(x => x.Seq == p.TsProjId).FirstOrDefault();
            //string ProjectName = proj.PrjName;
            string ProjectName = p.Project;

            List<string> suppliers = new List<string>(); 

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("BOQ Comparison");
                worksheet.Columns.AutoFit();
                worksheet.Protection.IsProtected = false;

                int row=0, j, c;

                worksheet.Cells["A1:C1"].Merge = true;
                worksheet.Cells["A2:C2"].Merge = true;
                worksheet.Cells["A3:C3"].Merge = true;
                worksheet.Cells["A4:C4"].Merge = true;
                worksheet.Cells["A5:C5"].Merge = true;

                worksheet.Cells[2, 1].Value = "Résumé des Offres/Feuille de Comparaison";
                worksheet.Cells[2, 1].Style.Font.Bold = true;
                worksheet.Cells[2, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[3, 1].Value = "Project:" + ProjectName;
                worksheet.Cells[3, 1].Style.Font.Bold = true;
                worksheet.Cells[3, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[4, 1].Value = "Department :";
                worksheet.Cells[4, 1].Style.Font.Bold = true;
                worksheet.Cells[4, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[5, 1].Value = PackageName;
                worksheet.Cells[5, 1].Style.Font.Bold = true;
                worksheet.Cells[5, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.SelectedRange[5, 50].Style.Font.Bold = true;
                worksheet.SelectedRange[5, 50].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.SelectedRange[7, 50].Style.Font.Bold = true;
                worksheet.SelectedRange[7, 50].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells["D6:F6"].Merge = true;
                worksheet.Cells[6, 4].Value = "ACC budget";
                worksheet.Cells[6, 4].Style.Font.Bold = true;
                worksheet.Cells[6, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells["G6:H6"].Merge = true;
                worksheet.Cells[6, 7].Value = "Quotation";
                worksheet.Cells[6, 7].Style.Font.Bold = true;
                worksheet.Cells[6, 7].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                //worksheet.Column(4).Width = 30;

                row = 9;
                foreach (var level in levels)
                {
                    if (level.Items.Count > 0)
                    {
                        GroupingBoqModel item1 = level.Items.First();

                        GroupingPackageSupplierPriceModel supItem = item1.GroupingPackageSuppliersPrices.First();
                        //string boq = sup.BoqItemO;

                        var SupList = item1.GroupingPackageSuppliersPrices.OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();
                        //var SupList = lst.OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();

                        int col = 9;
                        int m = 9;
                        foreach (var l in SupList)
                        {
                            worksheet.Cells[6, m].Value = l.SupplierName == "Ideal" ? l.SupplierName : l.SupplierName + " " + DateTime.Parse(l.LastRevisionDate.ToString()).ToString("dd/MM/yyyy");
                            worksheet.Cells[6, m].Style.Font.Bold = true;
                            worksheet.Columns[m].Style.WrapText = true;
                            worksheet.Column(m).AutoFit();
                            worksheet.Cells[6, m].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells[6, m, 6, m + 4].Merge = true;
                            m = m + 5;
                            if (!suppliers.Contains(l.SupplierName))
                                suppliers.Add(l.SupplierName.ToString());

                            col++;
                        }
                    }

                    //row = 7;
                    worksheet.Cells[7, 1].Value = "No";
                    worksheet.Cells[7, 2].Value = "Description";
                    worksheet.Column(2).Width = 70;
                    worksheet.Columns[2].Style.WrapText = true;
                    worksheet.Column(2).AutoFit();
                    worksheet.Cells[7, 3].Value = "Unit";
                    worksheet.Cells[7, 4].Value = "Final Qty";
                    worksheet.Cells[7, 5].Value = "Price U.";
                    worksheet.Cells[7, 6].Value = "Price T.";

                    worksheet.Cells[7, 7].Value = "Q. Qty";
                    worksheet.Cells[7, 8].Value = "Price T.";

                    worksheet.Cells[7, 1].EntireRow.Style.Font.Bold = true;
                    worksheet.Cells[7, 1].EntireRow.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    //row = 9;
                    j = 0;
                    foreach (var item in level.Items)
                    {
                        var lst = item.GroupingPackageSuppliersPrices.OrderByDescending(s => s.GroupId).OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();
                        foreach (var sup in item.GroupingPackageSuppliersPrices)
                        {
                            worksheet.Cells[row, 1].Value = j++;
                            worksheet.Column(2).Width = 70;
                            worksheet.Cells[row, 1].Value = (item.ItemO) == null ? "" : item.ItemO;
                            worksheet.Cells[row, 2].Value = (item.DescriptionO) == null ? "" : item.DescriptionO;
                            worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                            worksheet.Columns[2].Style.WrapText = true;
                            worksheet.Cells[row, 3].Value = (item.Unit) == null ? "" : item.Unit;
                            worksheet.Cells[row, 4].Value = (item.Qty) == null ? "" : item.Qty;
                            worksheet.Cells[row, 5].Value = (item.UnitPrice) == null ? "" : item.UnitPrice;
                            worksheet.Cells[row, 5].Style.Numberformat.Format = "#,##0.0";
                            worksheet.Cells[row, 6].Value = (item.TotalPrice) == null ? "" : item.TotalPrice;
                            worksheet.Cells[row, 6].Style.Numberformat.Format = "#,##0.0";
                            worksheet.Cells[row, 7].Value = (item.QuotationQty) == null ? "" : item.QuotationQty;
                            worksheet.Cells[row, 7].Style.Numberformat.Format = "#,##0.0";
                            worksheet.Cells[row, 8].Value = (item.QuotationAmt) == null ? "" : item.QuotationAmt;
                            worksheet.Cells[row, 8].Style.Numberformat.Format = "#,##0.0";

                            var quotQty = item.QuotationQty;

                            int col = 0;
                            foreach (var suplier in suppliers)
                            {
                                var v = worksheet.Cells[7, 9 + col].Value;
                                if (v == null)
                                {
                                    worksheet.Cells[7, 9 + col].Value = "Assigned Qty";
                                    worksheet.Cells[7, 10 + col].Value = "Price U.";
                                    worksheet.Cells[7, 11 + col].Value = "Disc %";
                                    worksheet.Cells[7, 12 + col].Value = "Final P.U.";
                                    worksheet.Cells[7, 13 + col].Value = "Price T.";
                                }

                                var supReply = item.GroupingPackageSuppliersPrices.Where(x => x.BoqItemO == sup.BoqItemO && x.SupplierName == suplier.ToString()).OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).FirstOrDefault();
                                if (supReply != null)
                                {
                                    worksheet.Cells[row, 9 + col].Value = (supReply.AssignedQty) == null ? "" : supReply.AssignedQty;
                                    worksheet.Cells[row, 9 + col].Style.Numberformat.Format = "#,##0.0";
                                    worksheet.Cells[row, 10 + col].Value = (supReply.UnitPrice) == null ? "" : supReply.OriginalCurrencyPrice * supReply.ExchRateNow;
                                    worksheet.Cells[row, 10 + col].Style.Numberformat.Format = "#,##0.0";
                                    worksheet.Cells[row, 11 + col].Value = (supReply.Discount) == null ? "" : supReply.Discount;
                                    //worksheet.Cells[row, 12 + col].Style.Numberformat.Format = "#,##0.0";
                                    worksheet.Cells[row, 12 + col].Value = (supReply.UPriceAfterDiscount) == null ? "" : (double)(supReply.UPriceAfterDiscount * supReply.ExchRateNow);
                                    worksheet.Cells[row, 13 + col].Value = quotQty * supReply.UPriceAfterDiscount * supReply.ExchRateNow; //(supReply.TotalPrice) == null ? "" : supReply.TotalPrice; 
                                    worksheet.Cells[row, 13 + col].Style.Numberformat.Format = "#,##0.0";
                                }
                                col = col + 5;
                            }                           
                        }
                        row++;
                    }
                    //row++;
                }
  

                //Commercial Conditions
                var comcondRep = comcondRepLst.OrderBy(r => r.CondDesc).ToList();

                var replies = comcondRep.GroupBy(x => new { x.CondDesc ,x.AccCond})
                .Select(p => p.FirstOrDefault())
                .Select(p => new TmpComparisonConditionsReply
                {
                    CondId = p.CondId,
                    CondDesc = p.CondDesc,
                    AccCond=p.AccCond
                })
                .ToList();

                if (replies.Count > 0)
                {
                    worksheet.SelectedRange[row, 3].Merge = true;
                    worksheet.Cells[row, 1].Value = "Commercial Conditions";
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;

                    worksheet.Cells[row, 3, row, 3 + 4].Merge = true;
                    worksheet.Cells[row, 3].Value = "ACC Conditions";
                    worksheet.Cells[row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 3].Style.Font.Bold = true;

                    row++;
                    foreach (var reply in replies)
                    {
                        worksheet.Cells[row, 2].Value = reply.CondDesc;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        worksheet.Cells[row, 3, row, 3 + 4].Merge = true;
                        worksheet.Cells[row, 3].Value = reply.AccCond;
                        worksheet.Cells[row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        int colsup = 9;
                        foreach (var sup in suppliers)
                        {
                            var supReply = comcondRepLst.Where(x => x.SupName == sup.ToString() && x.CondId == reply.CondId).FirstOrDefault();
                            if (supReply != null)
                                worksheet.Cells[row, colsup].Value = (supReply.CondReply) == null ? "" : supReply.CondReply;

                            colsup=colsup+5;
                        }
                        row++;
                    }
                }

                row++;

                //Technical Conditions
                var techcondRep = techcondRepLst.OrderBy(r => r.CondDesc).ToList();

                var treplies = techcondRep.GroupBy(x => new { x.CondDesc,x.AccCond })
                .Select(p => p.FirstOrDefault())
                .Select(p => new TmpComparisonConditionsReply
                {
                    CondId = p.CondId,
                    CondDesc = p.CondDesc,
                    AccCond=p.AccCond
                })
                .ToList();

                if (treplies.Count > 0)
                {
                    worksheet.SelectedRange[row, 3].Merge = true;
                    worksheet.Cells[row, 1].Value = "Technical Conditions";
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;

                    worksheet.Cells[row, 3, row, 3 + 4].Merge = true;
                    worksheet.Cells[row, 3].Value = "ACC Conditions";
                    worksheet.Cells[row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 3].Style.Font.Bold = true;

                    row++;
                    foreach (var reply in treplies)
                    {
                        worksheet.Cells[row, 2].Value = reply.CondDesc;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        worksheet.Cells[row, 3, row, 3 + 4].Merge = true;
                        worksheet.Cells[row, 3].Value = reply.AccCond;
                        worksheet.Cells[row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        int colsup = 9;
                        foreach (var sup in suppliers)
                        {
                            var supReply = techcondRepLst.Where(x => x.SupName == sup.ToString() && x.CondId == reply.CondId).FirstOrDefault();
                            if (supReply != null)
                                worksheet.Cells[row, colsup].Value = (supReply.CondReply) == null ? "" : supReply.CondReply;

                            colsup = colsup + 5;
                        }
                        row++;
                    }
                }


                xlPackage.Save();
                stream.Position = 0;
                string excelName = $"{ProjectName}-{PackageName}-Comparison-{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx";

                //string path = @"C:\App\";

                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}
                //string FullPath = path + excelName;

                if (File.Exists(excelName))
                    File.Delete(excelName);

                excelName = excelName.Replace("/", "-");
                xlPackage.SaveAs(excelName);

                return excelName;
            }
        }
         
        public string GetComparisonSheetResourcesByGroup_Excel(int packageId, SearchInput input, List<TmpComparisonConditionsReply> comcondRepLst, List<TmpComparisonConditionsReply> techcondRepLst)
        {
            List<GroupingBoqGroupModel> items = GetComparisonSheetBoqByGroup(packageId, input);

            var package = _mdbContext.TblPackages.Where(x => x.PkgeId == packageId).FirstOrDefault();
            string PackageName = package.PkgeName;

            var p = _dbContext.TblParameters.FirstOrDefault();
            //var proj = _pdbContext.Tblprojects.Where(x => x.Seq == p.TsProjId).FirstOrDefault();
            //string ProjectName = proj.PrjName;
            string ProjectName = p.Project;

            List<string> suppliers = new List<string>(); ;

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("Recap");
                worksheet.Columns.AutoFit();
                worksheet.Protection.IsProtected = false;

                int row, j, c;

                worksheet.Cells["A1:C1"].Merge = true;
                worksheet.Cells["A2:C2"].Merge = true;
                worksheet.Cells["A3:C3"].Merge = true;
                worksheet.Cells["A4:C4"].Merge = true;
                worksheet.Cells["A5:C5"].Merge = true;

                worksheet.Cells[2, 1].Value = "Résumé des Offres/Feuille de Comparaison";
                worksheet.Cells[2, 1].Style.Font.Bold = true;
                worksheet.Cells[2, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[3, 1].Value = "Project:" + ProjectName;
                worksheet.Cells[3, 1].Style.Font.Bold = true;
                worksheet.Cells[3, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[4, 1].Value = "Department :";
                worksheet.Cells[4, 1].Style.Font.Bold = true;
                worksheet.Cells[4, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[5, 1].Value = PackageName;
                worksheet.Cells[5, 1].Style.Font.Bold = true;
                worksheet.Cells[5, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.SelectedRange[6, 50].Style.Font.Bold = true;
                worksheet.SelectedRange[6, 50].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.SelectedRange[7, 50].Style.Font.Bold = true;
                worksheet.SelectedRange[7, 50].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells[6, 4].Value = "ACC budget";
                worksheet.Cells[6, 4].Style.Font.Bold = true;
                worksheet.Cells[6, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells["E6:F6"].Merge = true;
                worksheet.Cells[6, 5].Value = "ACC budget breakdown";
                worksheet.Cells[6, 5].Style.Font.Bold = true;
                worksheet.Cells[6, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Column(5).Width = 20;

                if (items.Count > 0)
                {
                    GroupingBoqGroupModel item1 = items.First();
                    GroupingPackageSupplierPriceModel sup = item1.GroupingPackageSuppliersPrices.First();
                    string boq = sup.BoqItemO;

                    var lst = item1.GroupingPackageSuppliersPrices.Where(x => x.BoqItemO == boq).OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();
                    var lst1 = lst.OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();

                    int col = 7;
                    foreach (var l in lst1)
                    {
                        worksheet.Cells[6, col].Value = l.SupplierName == "Ideal" ? l.SupplierName : l.SupplierName + " " + DateTime.Parse(l.LastRevisionDate.ToString()).ToString("dd/MM/yyyy");
                        worksheet.Cells[6, col].Style.Font.Bold = true;
                        worksheet.Columns[col].Style.WrapText = true;
                        worksheet.Column(col).AutoFit();

                        if (!suppliers.Contains(l.SupplierName))
                            suppliers.Add(l.SupplierName.ToString());

                        col++;
                    }
                }

                row = 7;
                worksheet.Cells[row, 1].Value = "No";
                worksheet.Cells[row, 2].Value = "Description";
                worksheet.Column(2).Width = 70;
                worksheet.Columns[2].Style.WrapText = true;
                worksheet.Column(2).AutoFit();
                worksheet.Cells[row, 3].Value = "Qty";
                worksheet.Cells[row, 4].Value = "Total";
                worksheet.Cells[row, 5].Value = "Material";
                worksheet.Cells[row, 6].Value = "Workmanship";

                worksheet.Cells[row, 1].EntireRow.Style.Font.Bold = true;
                worksheet.Cells[row, 1].EntireRow.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                row = 8;
                j = 0;
                foreach (var item in items)
                {
                    var lst = item.GroupingPackageSuppliersPrices.OrderByDescending(s => s.GroupId).OrderByDescending(s => s.BoqItemO).OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();
                    foreach (var sup in item.GroupingPackageSuppliersPrices)
                    {
                        worksheet.Cells[row, 1].Value = j++;
                        worksheet.Column(2).Width = 70;
                        worksheet.Cells[row, 2].Value = (item.Name) == null ? "" : item.Name;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        worksheet.Columns[2].Style.WrapText = true;
                        worksheet.Cells[row, 3].Value = (sup.Qty) == null ? "" : sup.Qty;
                        worksheet.Cells[row, 4].Value = (item.TotalPrice) == null ? "" : item.TotalPrice;

                        var supList = item.GroupingPackageSuppliersPrices.Where(x => x.BoqItemO == sup.BoqItemO).OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();
                        c = 0;
                        foreach (var s in supList)
                        {
                            worksheet.Cells[row, 7 + c++].Value = (s.TotalPrice) == null ? "" : s.TotalPrice;
                        }
                    }
                    row++;
                }

                row++;

                //Commercial Conditions
                var comcondRep = comcondRepLst.OrderBy(r => r.CondDesc).ToList();

                var replies = comcondRep.GroupBy(x => new { x.CondDesc })
                .Select(p => p.FirstOrDefault())
                .Select(p => new TmpConditionsReply
                {
                    CondId = p.CondId,
                    CondDesc = p.CondDesc
                })
                .ToList();

                if (replies.Count > 0)
                {
                    worksheet.SelectedRange[row, 3].Merge = true;
                    worksheet.Cells[row, 1].Value = "Commercial Conditions";
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;

                    row++;
                    foreach (var reply in replies)
                    {
                        worksheet.Cells[row, 2].Value = reply.CondDesc;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        int colsup = 7;
                        foreach (var sup in suppliers)
                        {
                            var supReply = comcondRepLst.Where(x => x.SupName == sup.ToString() && x.CondId == reply.CondId).FirstOrDefault();
                            if (supReply != null)
                                worksheet.Cells[row, colsup].Value = (supReply.CondReply) == null ? "" : supReply.CondReply;

                            colsup = colsup + 1;
                        }
                        row++;
                    }
                }

                row++;

                //Technical Conditions
                var techcondRep = techcondRepLst.OrderBy(r => r.CondDesc).ToList();

                var treplies = techcondRep.GroupBy(x => new { x.CondDesc })
                .Select(p => p.FirstOrDefault())
                .Select(p => new TmpConditionsReply
                {
                    CondId = p.CondId,
                    CondDesc = p.CondDesc
                })
                .ToList();

                if (treplies.Count > 0)
                {
                    worksheet.SelectedRange[row, 3].Merge = true;
                    worksheet.Cells[row, 1].Value = "Technical Conditions";
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;

                    row++;
                    foreach (var reply in treplies)
                    {
                        worksheet.Cells[row, 2].Value = reply.CondDesc;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        int colsup = 7;
                        foreach (var sup in suppliers)
                        {
                            var supReply = techcondRepLst.Where(x => x.SupName == sup.ToString() && x.CondId == reply.CondId).FirstOrDefault();
                            if (supReply != null)
                                worksheet.Cells[row, colsup].Value = (supReply.CondReply) == null ? "" : supReply.CondReply;

                            colsup = colsup + 1;
                        }
                        row++;
                    }
                }


                xlPackage.Save();
                stream.Position = 0;
                string excelName = $"{ProjectName}-{PackageName}-Comparison-{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx";

                //string path = @"C:\App\";

                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}
                //string FullPath = path + excelName;

                if (File.Exists(excelName))
                    File.Delete(excelName);

                excelName = excelName.Replace("/", "-");
                xlPackage.SaveAs(excelName);

                return excelName;
            }
        }
  
        public string GetComparisonSheetBoqByGroup_Excel(int packageId, SearchInput input, List<boqPackageList> boqPackageList, List<TmpComparisonConditionsReply> comcondRepLst, List<TmpComparisonConditionsReply> techcondRepLst)
        {
            List<GroupingBoqGroupModel> items = GetComparisonSheetBoqByGroup(packageId, input);

            var package = _mdbContext.TblPackages.Where(x => x.PkgeId == packageId).FirstOrDefault();
            string PackageName = package.PkgeName;

            var p = _dbContext.TblParameters.FirstOrDefault();
            //var proj = _pdbContext.Tblprojects.Where(x => x.Seq == p.TsProjId).FirstOrDefault();
            //string ProjectName = proj.PrjName;
            string ProjectName = p.Project;

            List<string> suppliers = new List<string>(); ;

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("Recap");
                worksheet.Columns.AutoFit();
                worksheet.Protection.IsProtected = false;

                int row, j,c;
   
                worksheet.Cells["A1:C1"].Merge = true;
                worksheet.Cells["A2:C2"].Merge = true;
                worksheet.Cells["A3:C3"].Merge = true;
                worksheet.Cells["A4:C4"].Merge = true;
                worksheet.Cells["A5:C5"].Merge = true;

                worksheet.Cells[2, 1].Value = "Résumé des Offres/Feuille de Comparaison";
                worksheet.Cells[2, 1].Style.Font.Bold = true;
                worksheet.Cells[2, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[3, 1].Value = "Project:" + ProjectName;
                worksheet.Cells[3, 1].Style.Font.Bold = true;
                worksheet.Cells[3, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[4, 1].Value = "Department :";
                worksheet.Cells[4, 1].Style.Font.Bold = true;
                worksheet.Cells[4, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[5, 1].Value = PackageName;
                worksheet.Cells[5, 1].Style.Font.Bold = true;
                worksheet.Cells[5, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.SelectedRange[6, 50].Style.Font.Bold = true;
                worksheet.SelectedRange[6, 50].Style.HorizontalAlignment =OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.SelectedRange[7, 50].Style.Font.Bold = true;
                worksheet.SelectedRange[7, 50].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells[6, 4].Value = "ACC budget";
                worksheet.Cells[6, 4].Style.Font.Bold = true;
                worksheet.Cells[6, 4].Style.HorizontalAlignment=OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells["E6:F6"].Merge = true;
                worksheet.Cells[6, 5].Value = "ACC budget breakdown";
                worksheet.Cells[6, 5].Style.Font.Bold = true;
                worksheet.Cells[6, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Column(5).Width = 20;

                if (items.Count > 0)
                {
                    GroupingBoqGroupModel item1 = items.First();                  
                    GroupingPackageSupplierPriceModel sup = item1.GroupingPackageSuppliersPrices.First();
                    //string boq = sup.BoqItemO;

                    var Suplist = item1.GroupingPackageSuppliersPrices.OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();
                    //var lst1 =lst.OrderByDescending(s => s.SupplierName).OrderByDescending(s=>s.LastRevisionDate).ToList();

                    int col = 7;
                    foreach (var l in Suplist)
                    {
                        worksheet.Cells[6, col].Value = l.SupplierName == "Ideal" ? l.SupplierName : l.SupplierName + " " + DateTime.Parse(l.LastRevisionDate.ToString()).ToString("dd/MM/yyyy");
                        worksheet.Cells[6, col].Style.Font.Bold = true;
                        worksheet.Columns[col].Style.WrapText = true;
                        worksheet.Column(col).AutoFit();

                        if (!suppliers.Contains(l.SupplierName))    
                          suppliers.Add(l.SupplierName.ToString());

                        col++;
                    }
                }

                row = 7;
                worksheet.Cells[row, 1].Value = "No";
                worksheet.Cells[row, 2].Value = "Description";
                worksheet.Column(2).Width = 70;
                worksheet.Columns[2].Style.WrapText = true;
                worksheet.Column(2).AutoFit();
                worksheet.Cells[row, 3].Value = "Qty";
                worksheet.Cells[row, 4].Value = "Total";
                worksheet.Cells[row, 5].Value = "Material";
                worksheet.Cells[row, 6].Value = "Workmanship";

                worksheet.Cells[row, 1].EntireRow.Style.Font.Bold = true;
                worksheet.Cells[row, 1].EntireRow.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                row = 8;
                j = 1;
                foreach(var item in items)
                {
                    worksheet.Cells[row, 1].Value = j++;
                    worksheet.Cells[row, 2].Value = (item.Name) == null ? "" : item.Name;
                    worksheet.Column(2).Width = 70;
                    worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Columns[2].Style.WrapText = true;

                    worksheet.Cells[row, 3].Value = (item.TotalQty) == null ? "" : item.TotalQty;
                    worksheet.Cells[row, 4].Value = (item.TotalPrice) == null ? "" : item.TotalPrice;

                    var lst = item.GroupingPackageSuppliersPrices.OrderByDescending(s => s.GroupId).OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();
                    foreach (var sup in item.GroupingPackageSuppliersPrices)
                    {                          
                        var supList = item.GroupingPackageSuppliersPrices.OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();
                        c = 0;
                        foreach (var s in supList)
                        {
                            worksheet.Cells[row, 7+c++].Value = (s.TotalPrice) == null ? "" : s.TotalPrice;
                        }                     
                    }
                    row++;
                }

                row++;

                //Commercial Conditions
                var comcondRep = comcondRepLst.OrderBy(r => r.CondDesc).ToList();

                var replies = comcondRep.GroupBy(x =>new {x.CondDesc})
                .Select(p => p.FirstOrDefault())
                .Select(p => new TmpConditionsReply
                {
                    CondId=p.CondId,
                    CondDesc = p.CondDesc             
                })
                .ToList();

                if (replies.Count > 0)
                {
                    worksheet.SelectedRange[row, 3].Merge = true;
                    worksheet.Cells[row, 1].Value = "Commercial Conditions";
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;

                    row++;
                    foreach (var reply in replies)
                    {
                        worksheet.Cells[row, 2].Value = reply.CondDesc;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        int colsup = 7;
                        foreach (var sup in suppliers)
                        {
                            var supReply = comcondRepLst.Where(x => x.SupName == sup.ToString() && x.CondId==reply.CondId).FirstOrDefault();
                            if (supReply!= null) 
                              worksheet.Cells[row, colsup].Value = (supReply.CondReply) == null ? "" : supReply.CondReply;

                            colsup = colsup + 1;
                        }
                        row++;
                    }
                }

                row++;

                //Technical Conditions
                var techcondRep = techcondRepLst.OrderBy(r => r.CondDesc).ToList();

                var treplies = techcondRep.GroupBy(x => new { x.CondDesc })
                .Select(p => p.FirstOrDefault())
                .Select(p => new TmpConditionsReply
                {
                    CondId = p.CondId,
                    CondDesc = p.CondDesc
                })
                .ToList();

                if (treplies.Count > 0)
                {
                    worksheet.SelectedRange[row, 3].Merge = true;
                    worksheet.Cells[row, 1].Value = "Technical Conditions";
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;

                    row++;
                    foreach (var reply in treplies)
                    {
                        worksheet.Cells[row, 2].Value = reply.CondDesc;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        int colsup = 7;
                        foreach (var sup in suppliers)
                        {
                            var supReply = techcondRepLst.Where(x => x.SupName == sup.ToString() && x.CondId == reply.CondId).FirstOrDefault();
                            if (supReply != null)
                                worksheet.Cells[row, colsup].Value = (supReply.CondReply) == null ? "" : supReply.CondReply;

                            colsup = colsup + 1;
                        }
                        row++;
                    }
                }


                xlPackage.Save();
                stream.Position = 0;
                string excelName = $"{ProjectName}-{PackageName}-Comparison-{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx";

                //string path = @"C:\App\";

                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}
                //string FullPath = path + excelName;

                if (File.Exists(excelName))
                    File.Delete(excelName);

                excelName = excelName.Replace("/", "-");
                xlPackage.SaveAs(excelName);

                return excelName;
            }
        }
        public byte checkByBoq(int packageId)
        {
            var packageSupp = _dbContext.TblSupplierPackages.Where(x => x.SpPackageId == packageId).FirstOrDefault();
            return (byte)((packageSupp.SpByBoq == null) ? 0 : packageSupp.SpByBoq);            
        }
        public List<string> GenerateSuppliersContracts_Excel(int packageId, SearchInput input, List<TmpComparisonConditionsReply> comcondRepLst, List<TmpComparisonConditionsReply> techcondRepLst)
        {
            List<GroupingBoqModel> items;
            List<string> excelList = new List<string>();

            byte byBoq = checkByBoq(packageId);

            var supList = (from b in _mdbContext.TblSuppliers
                           select b).ToList();

            var querySupp = (from sup in supList
                             join a in _dbContext.TblSupplierPackages on sup.SupCode equals  a.SpSupplierId
                             join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                             join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                             where (a.SpPackageId == packageId && b.PrRevNo == 0 && c.RdPriceOrigCurrency > 0)
                             group sup by sup.SupCode into s
                             select new GroupingPackageSupplierPriceModel
                             {
                                 SupplierId = s.Key
                             }).ToList();

            foreach (var sup in querySupp)
            {
                if (byBoq == 1)
                    excelList.Add(GenerateSupplierContract_BOQ_Excel(packageId, sup.SupplierId, input, comcondRepLst, techcondRepLst));
                else
                    excelList.Add(GenerateSupplierContract_Excel(packageId, sup.SupplierId, input, comcondRepLst, techcondRepLst));
            }

            return excelList;
        }

        public string GenerateSupplierContract_BOQ_Excel(int packageId,int supId, SearchInput input, List<TmpComparisonConditionsReply> comcondRepLst, List<TmpComparisonConditionsReply> techcondRepLst)
        {
            List<GroupingLevelModel> levels = GetComparisonSheetByBoq(packageId, input, supId);

            var package = _mdbContext.TblPackages.Where(x => x.PkgeId == packageId).FirstOrDefault();
            string PackageName = package.PkgeName;

            var p = _dbContext.TblParameters.FirstOrDefault();
            //var proj = _pdbContext.Tblprojects.Where(x => x.Seq == p.TsProjId).FirstOrDefault();
            //string ProjectName = proj.PrjName;
            string ProjectName = p.Project;

            List<string> suppliers = new List<string>();

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("Sheet1");
                worksheet.Columns.AutoFit();
                worksheet.Protection.IsProtected = false;

                int row=0, j, c;

                worksheet.Cells["A1:C1"].Merge = true;
                worksheet.Cells["A2:C2"].Merge = true;
                worksheet.Cells["A3:C3"].Merge = true;
                worksheet.Cells["A4:C4"].Merge = true;
                worksheet.Cells["A5:C5"].Merge = true;

                worksheet.Cells[2, 1].Value = "Résumé de Offre";
                worksheet.Cells[2, 1].Style.Font.Bold = true;
                worksheet.Cells[2, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[3, 1].Value = "Project:" + ProjectName;
                worksheet.Cells[3, 1].Style.Font.Bold = true;
                worksheet.Cells[3, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[4, 1].Value = "Department :";
                worksheet.Cells[4, 1].Style.Font.Bold = true;
                worksheet.Cells[4, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[5, 1].Value = PackageName;
                worksheet.Cells[5, 1].Style.Font.Bold = true;
                worksheet.Cells[5, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.SelectedRange[5, 50].Style.Font.Bold = true;
                worksheet.SelectedRange[5, 50].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.SelectedRange[7, 50].Style.Font.Bold = true;
                worksheet.SelectedRange[7, 50].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells["E6:F6"].Merge = true;
                worksheet.Cells[6, 5].Value = "ACC budget";
                worksheet.Cells[6, 5].Style.Font.Bold = true;
                worksheet.Cells[6, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                //worksheet.Column(5).Width = 20;

                worksheet.Cells["G6:H6"].Merge = true;
                worksheet.Cells[6, 7].Value = "Quotation";
                worksheet.Cells[6, 7].Style.Font.Bold = true;
                worksheet.Cells[6, 7].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                row = 9;
                foreach (var level in levels)
                {
                    if (level.Items.Count > 0)
                    {
                        GroupingBoqModel item1 = level.Items.First();
                        GroupingPackageSupplierPriceModel supItem = item1.GroupingPackageSuppliersPrices.First();
                        //string boq = sup.BoqItemO;

                        var SupList = item1.GroupingPackageSuppliersPrices.OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();
                        //var SupList = lst.OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();

                        int col = 9;
                        int m = 9;
                        foreach (var supplier in SupList)
                        {
                            worksheet.Cells[6, m].Value = supplier.SupplierName == "Ideal" ? supplier.SupplierName : supplier.SupplierName + " " + DateTime.Parse(supplier.LastRevisionDate.ToString()).ToString("dd/MM/yyyy");
                            worksheet.Cells[6, m].Style.Font.Bold = true;
                            worksheet.Columns[m].Style.WrapText = true;
                            worksheet.Column(m).AutoFit();
                            worksheet.Cells[6, m].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells[6, m, 6, m + 4].Merge = true;
                            m = m + 5;
                            if (!suppliers.Contains(supplier.SupplierName))
                                suppliers.Add(supplier.SupplierName.ToString());

                            col++;
                        }
                    }

                    //row = 7;
                    worksheet.Cells[7, 1].Value = "No";
                    worksheet.Cells[7, 2].Value = "Description";
                    worksheet.Column(2).Width = 70;
                    worksheet.Columns[2].Style.WrapText = true;
                    worksheet.Column(2).AutoFit();
                    worksheet.Cells[7, 3].Value = "U.";
                    worksheet.Cells[7, 4].Value = "Final Qty";
                    worksheet.Cells[7, 5].Value = "P.U.";
                    worksheet.Cells[7, 6].Value = "P.T.";

                    worksheet.Cells[7, 7].Value = "Q. Qty";
                    worksheet.Cells[7, 8].Value = "Price T.";

                    worksheet.Cells[7, 1].EntireRow.Style.Font.Bold = true;
                    worksheet.Cells[7, 1].EntireRow.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    //row = 9;
                    //row++;
                    j = 0;

                    foreach (var item in level.Items)
                    {
                        var lst = item.GroupingPackageSuppliersPrices.OrderByDescending(s => s.GroupId).OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();
                        foreach (var sup in item.GroupingPackageSuppliersPrices)
                        {
                            worksheet.Cells[row, 1].Value = j++;
                            worksheet.Column(2).Width = 70;
                            worksheet.Cells[row, 1].Value = (item.ItemO) == null ? "" : item.ItemO;
                            worksheet.Cells[row, 2].Value = (item.DescriptionO) == null ? "" : item.DescriptionO;
                            worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                            worksheet.Columns[2].Style.WrapText = true;
                            worksheet.Cells[row, 3].Value = (item.Unit) == null ? "" : item.Unit;
                            worksheet.Cells[row, 4].Value = (item.Qty) == null ? "" : item.Qty;
                            worksheet.Cells[row, 4].Style.Numberformat.Format = "#,##0.0";
                            worksheet.Cells[row, 5].Value = (item.UnitPrice) == null ? "" : item.UnitPrice;
                            worksheet.Cells[row, 5].Style.Numberformat.Format = "#,##0.0";
                            worksheet.Cells[row, 6].Value = (item.TotalPrice) == null ? "" : item.TotalPrice;
                            worksheet.Cells[row, 6].Style.Numberformat.Format = "#,##0.0";
                            worksheet.Cells[row, 7].Value = (item.QuotationQty) == null ? "" : item.QuotationQty;
                            worksheet.Cells[row, 7].Style.Numberformat.Format = "#,##0.0";
                            worksheet.Cells[row, 8].Value = (item.QuotationAmt) == null ? "" : item.QuotationAmt;
                            worksheet.Cells[row, 8].Style.Numberformat.Format = "#,##0.0";

                            int col = 0;
                            foreach (var suplier in suppliers)
                            {
                                var v = worksheet.Cells[7, 9 + col].Value;
                                if (v == null)
                                {
                                    worksheet.Cells[7, 9 + col].Value = "Assigned Qty";
                                    worksheet.Cells[7, 10 + col].Value = "P.U.";
                                    worksheet.Cells[7, 11 + col].Value = "Disc %";
                                    worksheet.Cells[7, 12 + col].Value = "Final P.U.";
                                    worksheet.Cells[7, 13 + col].Value = "P.T.";
                                }

                                var supReply = item.GroupingPackageSuppliersPrices.Where(x => x.BoqItemO == sup.BoqItemO && x.SupplierName == suplier.ToString()).OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).FirstOrDefault();
                                if (supReply != null)
                                {
                                    worksheet.Cells[row, 9 + col].Value = (supReply.AssignedQty) == null ? "" : supReply.AssignedQty;
                                    worksheet.Cells[row, 9 + col].Style.Numberformat.Format = "#,##0.0";
                                    worksheet.Cells[row, 10 + col].Value = (supReply.UnitPrice) == null ? "" : supReply.OriginalCurrencyPrice * supReply.ExchRateNow;
                                    worksheet.Cells[row, 10 + col].Style.Numberformat.Format = "#,##0.0";
                                    worksheet.Cells[row, 11 + col].Value = (supReply.Discount) == null ? "" : supReply.Discount;
                                    //worksheet.Cells[row, 12 + col].Style.Numberformat.Format = "#,##0.0";
                                    worksheet.Cells[row, 12 + col].Value = (supReply.UPriceAfterDiscount) == null ? "" : supReply.UPriceAfterDiscount * supReply.ExchRateNow;             
                                    worksheet.Cells[row, 13 + col].Value = item.QuotationQty * supReply.UPriceAfterDiscount * supReply.ExchRateNow; //(supReply.TotalPrice) == null ? "" : supReply.TotalPrice;
                                    worksheet.Cells[row, 13 + col].Style.Numberformat.Format = "#,##0.0";
                                }
                                col = col + 5;
                            }
                        }
                        row++;
                    }
                    //row++;
                }

                //Commercial Conditions
                var comcondRep = comcondRepLst.OrderBy(r => r.CondDesc).ToList();

                var replies = comcondRep.GroupBy(x => new { x.CondDesc,x.AccCond })
                .Select(p => p.FirstOrDefault())
                .Select(p => new TmpComparisonConditionsReply
                {
                    CondId = p.CondId,
                    CondDesc = p.CondDesc,
                    AccCond = p.AccCond
                })
                .ToList();

                if (replies.Count > 0)
                {
                    worksheet.SelectedRange[row, 3].Merge = true;
                    worksheet.Cells[row, 1].Value = "Commercial Conditions";
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;

                    worksheet.Cells[row, 3, row, 3 + 4].Merge = true;
                    worksheet.Cells[row, 3].Value = "ACC Conditions";
                    worksheet.Cells[row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 3].Style.Font.Bold = true;

                    row++;
                    foreach (var reply in replies)
                    {
                        worksheet.Cells[row, 2].Value = reply.CondDesc;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        worksheet.Cells[row, 3, row, 3 + 4].Merge = true;
                        worksheet.Cells[row, 3].Value = reply.AccCond;
                        worksheet.Cells[row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        int colsup = 7;
                        foreach (var sup in suppliers)
                        {
                            var supReply = comcondRepLst.Where(x => x.SupName == sup.ToString() && x.CondId == reply.CondId).FirstOrDefault();
                            if (supReply != null)
                                worksheet.Cells[row, colsup].Value = (supReply.CondReply) == null ? "" : supReply.CondReply;

                            colsup = colsup + 3;
                        }
                        row++;
                    }
                }

                row++;

                //Technical Conditions
                var techcondRep = techcondRepLst.OrderBy(r => r.CondDesc).ToList();

                var treplies = techcondRep.GroupBy(x => new { x.CondDesc,x.AccCond })
                .Select(p => p.FirstOrDefault())
                .Select(p => new TmpComparisonConditionsReply
                {
                    CondId = p.CondId,
                    CondDesc = p.CondDesc,
                    AccCond=p.AccCond
                })
                .ToList();

                if (treplies.Count > 0)
                {
                    worksheet.SelectedRange[row, 3].Merge = true;
                    worksheet.Cells[row, 1].Value = "Technical Conditions";
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;

                    worksheet.Cells[row, 3, row, 3 + 4].Merge = true;
                    worksheet.Cells[row, 3].Value = "ACC Conditions";
                    worksheet.Cells[row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 3].Style.Font.Bold = true;

                    row++;
                    foreach (var reply in treplies)
                    {
                        worksheet.Cells[row, 2].Value = reply.CondDesc;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        worksheet.Cells[row, 3, row, 3 + 4].Merge = true;
                        worksheet.Cells[row, 3].Value = reply.AccCond;
                        worksheet.Cells[row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        int colsup = 7;
                        foreach (var sup in suppliers)
                        {
                            var supReply = techcondRepLst.Where(x => x.SupName == sup.ToString() && x.CondId == reply.CondId).FirstOrDefault();
                            if (supReply != null)
                                worksheet.Cells[row, colsup].Value = (supReply.CondReply) == null ? "" : supReply.CondReply;

                            colsup = colsup + 3;
                        }
                        row++;
                    }
                }

                xlPackage.Save();
                stream.Position = 0;

                string suplierName =  suppliers.FirstOrDefault().Trim();
                string excelName = $"{ProjectName}-{suplierName}-{PackageName}-{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx";

                if (File.Exists(excelName))
                    File.Delete(excelName);

                excelName = excelName.Replace("/", "-");
                xlPackage.SaveAs(excelName);

                return excelName;
            }
        }

        public string GenerateSupplierContract_Excel(int packageId, int supId, SearchInput input, List<TmpComparisonConditionsReply> comcondRepLst, List<TmpComparisonConditionsReply> techcondRepLst)
        {
            List<GroupingLevelModel> levels = GetComparisonSheet(packageId, input, supId);

            var package = _mdbContext.TblPackages.Where(x => x.PkgeId == packageId).FirstOrDefault();
            string PackageName = package.PkgeName;

            var p = _dbContext.TblParameters.FirstOrDefault();
            //var proj = _pdbContext.Tblprojects.Where(x => x.Seq == p.TsProjId).FirstOrDefault();
            //string ProjectName = proj.PrjName;
            string ProjectName = p.Project;

            List<string> suppliers = new List<string>();

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("BOQ Comparison");
                worksheet.Columns.AutoFit();
                worksheet.Protection.IsProtected = false;

                int row=0, j, c;

                worksheet.Cells["A1:C1"].Merge = true;
                worksheet.Cells["A2:C2"].Merge = true;
                worksheet.Cells["A3:C3"].Merge = true;
                worksheet.Cells["A4:C4"].Merge = true;
                worksheet.Cells["A5:C5"].Merge = true;

                worksheet.Cells[2, 1].Value = "Résumé de Offre";
                worksheet.Cells[2, 1].Style.Font.Bold = true;
                worksheet.Cells[2, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[3, 1].Value = "Project:" + ProjectName;
                worksheet.Cells[3, 1].Style.Font.Bold = true;
                worksheet.Cells[3, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[4, 1].Value = "Department :";
                worksheet.Cells[4, 1].Style.Font.Bold = true;
                worksheet.Cells[4, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[5, 1].Value = PackageName;
                worksheet.Cells[5, 1].Style.Font.Bold = true;
                worksheet.Cells[5, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.SelectedRange[5, 50].Style.Font.Bold = true;
                worksheet.SelectedRange[5, 50].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.SelectedRange[7, 50].Style.Font.Bold = true;
                worksheet.SelectedRange[7, 50].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells["E6:F6"].Merge = true;
                worksheet.Cells[6, 5].Value = "ACC budget";
                worksheet.Cells[6, 5].Style.Font.Bold = true;
                worksheet.Cells[6, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Column(5).Width = 20;

                foreach (var level in levels)
                {
                    if (level.Items.Count > 0)
                    {
                        GroupingBoqModel item1 = level.Items.First();
                        GroupingResourceModel sup = item1.GroupingResources.First();
                        string boq = item1.ItemO;

                        var SupList = sup.GroupingPackageSuppliersPrices.OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();

                        int col = 7;
                        int m = 7;
                        foreach (var l in SupList)
                        {
                            worksheet.Cells[6, m].Value = l.SupplierName == "Ideal" ? l.SupplierName : l.SupplierName + " " + DateTime.Parse(l.LastRevisionDate.ToString()).ToString("dd/MM/yyyy");
                            worksheet.Cells[6, m].Style.Font.Bold = true;
                            worksheet.Columns[m].Style.WrapText = true;
                            worksheet.Column(m).AutoFit();
                            worksheet.Cells[6, m].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells[6, m, 6, m + 2].Merge = true;
                            m = m + 3;
                            if (!suppliers.Contains(l.SupplierName))
                                suppliers.Add(l.SupplierName.ToString());

                            col++;
                        }
                    }

                    row = 7;
                    worksheet.Cells[row, 1].Value = "No";
                    worksheet.Cells[row, 2].Value = "Description";
                    worksheet.Column(2).Width = 70;
                    worksheet.Columns[2].Style.WrapText = true;
                    worksheet.Column(2).AutoFit();
                    worksheet.Cells[row, 3].Value = "U.";
                    worksheet.Cells[row, 4].Value = "Qty Total";
                    worksheet.Cells[row, 5].Value = "P.U.";
                    worksheet.Cells[row, 6].Value = "P.T.";

                    worksheet.Cells[row, 1].EntireRow.Style.Font.Bold = true;
                    worksheet.Cells[row, 1].EntireRow.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    row = 9;
                    j = 0;
                    foreach (var item in level.Items)
                    {
                        worksheet.Cells[row, 1].Value = j++;
                        worksheet.Column(2).Width = 70;
                        worksheet.Cells[row, 1].Value = (item.ItemO) == null ? "" : item.ItemO;
                        worksheet.Cells[row, 2].Value = (item.DescriptionO) == null ? "" : item.DescriptionO;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        worksheet.Columns[2].Style.WrapText = true;

                        row++;

                        foreach (var res in item.GroupingResources)
                        {
                            worksheet.Cells[row, 2].Value = (res.ResourceDescription) == null ? "" : res.ResourceDescription;
                            worksheet.Cells[row, 3].Value = (res.Unit) == null ? "" : res.Unit;
                            worksheet.Cells[row, 4].Value = (res.Qty) == null ? "" : res.Qty;
                            worksheet.Cells[row, 5].Value = (res.UnitPrice) == null ? "" : res.UnitPrice;
                            worksheet.Cells[row, 6].Value = (res.TotalPrice) == null ? "" : res.TotalPrice;

                            int col = 0;
                            foreach (var suplier in suppliers)
                            {
                                var v = worksheet.Cells[7, 7 + col].Value;
                                if (v == null)
                                {
                                    worksheet.Cells[7, 7 + col].Value = "Assigned Qty";
                                    worksheet.Cells[7, 8 + col].Value = "P.U.";
                                    worksheet.Cells[7, 9 + col].Value = "P.T.";
                                }

                                var supReply = res.GroupingPackageSuppliersPrices.Where(x => x.BoqResourceId == res.BoqSeq && x.SupplierName == suplier.ToString()).OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).FirstOrDefault();
                                if (supReply != null)
                                {
                                    worksheet.Cells[row, 7 + col].Value = (supReply.AssignedQty) == null ? "" : supReply.AssignedQty;
                                    worksheet.Cells[row, 8 + col].Value = (supReply.UnitPrice) == null ? "" : supReply.OriginalCurrencyPrice * supReply.ExchRateNow;
                                    worksheet.Cells[row, 9 + col].Value = (supReply.TotalPrice) == null ? "" : supReply.AssignedQty * supReply.OriginalCurrencyPrice * supReply.ExchRateNow;
                                }
                                col = col + 3;
                            }
                            row++;
                        }
                        row++;
                    }
                    row++;
                }

                row++;

                //Commercial Conditions
                var comcondRep = comcondRepLst.OrderBy(r => r.CondDesc).ToList();

                var replies = comcondRep.GroupBy(x => new { x.CondDesc,x.AccCond })
                .Select(p => p.FirstOrDefault())
                .Select(p => new TmpComparisonConditionsReply
                {
                    CondId = p.CondId,
                    CondDesc = p.CondDesc,
                    AccCond=p.AccCond
                })
                .ToList();

                if (replies.Count > 0)
                {
                    worksheet.SelectedRange[row, 3].Merge = true;
                    worksheet.Cells[row, 1].Value = "Commercial Conditions";
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;

                    worksheet.Cells[row, 3, row, 3 + 4].Merge = true;
                    worksheet.Cells[row, 3].Value = "ACC Conditions";
                    worksheet.Cells[row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 3].Style.Font.Bold = true;

                    row++;
                    foreach (var reply in replies)
                    {
                        worksheet.Cells[row, 2].Value = reply.CondDesc;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        worksheet.Cells[row, 3, row, 3 + 4].Merge = true;
                        worksheet.Cells[row, 3].Value = reply.AccCond;
                        worksheet.Cells[row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        int colsup = 7;
                        foreach (var sup in suppliers)
                        {
                            var supReply = comcondRepLst.Where(x => x.SupName == sup.ToString() && x.CondId == reply.CondId).FirstOrDefault();
                            if (supReply != null)
                                worksheet.Cells[row, colsup].Value = (supReply.CondReply) == null ? "" : supReply.CondReply;

                            colsup = colsup + 3;
                        }
                        row++;
                    }
                }

                row++;

                //Technical Conditions
                var techcondRep = techcondRepLst.OrderBy(r => r.CondDesc).ToList();

                var treplies = techcondRep.GroupBy(x => new { x.CondDesc, x.AccCond })
                .Select(p => p.FirstOrDefault())
                .Select(p => new TmpComparisonConditionsReply
                {
                    CondId = p.CondId,
                    CondDesc = p.CondDesc,
                    AccCond=p.AccCond
                })
                .ToList();

                if (treplies.Count > 0)
                {
                    worksheet.SelectedRange[row, 3].Merge = true;
                    worksheet.Cells[row, 1].Value = "Technical Conditions";
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;

                    worksheet.Cells[row, 3, row, 3 + 4].Merge = true;
                    worksheet.Cells[row, 3].Value = "ACC Conditions";
                    worksheet.Cells[row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 3].Style.Font.Bold = true;

                    row++;
                    foreach (var reply in treplies)
                    {
                        worksheet.Cells[row, 2].Value = reply.CondDesc;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        worksheet.Cells[row, 3, row, 3 + 4].Merge = true;
                        worksheet.Cells[row, 3].Value = reply.AccCond;
                        worksheet.Cells[row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        int colsup = 7;
                        foreach (var sup in suppliers)
                        {
                            var supReply = techcondRepLst.Where(x => x.SupName == sup.ToString() && x.CondId == reply.CondId).FirstOrDefault();
                            if (supReply != null)
                                worksheet.Cells[row, colsup].Value = (supReply.CondReply) == null ? "" : supReply.CondReply;

                            colsup = colsup + 3;
                        }
                        row++;
                    }
                }

                xlPackage.Save();
                stream.Position = 0;

                string suplierName = suppliers.FirstOrDefault();
                string excelName = $"{ProjectName}-{suplierName}-{PackageName}-{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx";

                if (File.Exists(excelName))
                    File.Delete(excelName);

                excelName = excelName.Replace("/", "-");
                xlPackage.SaveAs(excelName);

                return excelName;
            }
        }

        private double GetExchange(string foreignCurrency)
        {
            var curList = (from b in _mdbContext.TblCurrencies
                           select b).ToList();

            var result = from a in curList 
                         join b in _dbContext.TblParameters
                         on a.CurId equals b.EstimatedCur
                         select new ProjectCurrency
                         {
                             curId = (int)b.EstimatedCur,
                             curCode = a.CurCode
                         };


            string localCurrency = result.FirstOrDefault().curCode;

            CurrencyConverterRepository currencyConverterRepository = new CurrencyConverterRepository();
            return currencyConverterRepository.GetCurrencyExchange(localCurrency, foreignCurrency);
        }

        public List<AcceptComment> GetRevisionAcceptance(int revId)
        {
            var acceptanceComments = _dbContext.AcceptanceComments.Where(x=>x.Enabled == true).ToList();
            var revAcceptanceComments = _dbContext.RevisionAcceptanceComments.Where(x => x.RevisionId == revId).ToList();
            //var result = (from  a in _dbContext.AcceptanceComments
            //join b in _dbContext.RevisionAcceptanceComments on a.Id equals b.AcceptanceCommentId into gr
            //from subpet in gr.DefaultIfEmpty()

            //select new AcceptComment
            //{
            //  acRevId = revId,
            //  acId =  a.Id,         
            //  acCaption=a.Caption,
            //  acChecked= (subpet.AcceptanceCommentId == null ) ? false : true
            //}).ToList();

            var result = acceptanceComments.Select(x => new AcceptComment { 
                acCaption = x.Caption,
                acId = x.Id,
                acRevId = revId,
                acChecked = revAcceptanceComments.FirstOrDefault(y=>y.AcceptanceCommentId == x.Id) != null? true : false,
            }).ToList();

            return result;
        }

        public bool ExcludBoq(int packId, string Item, bool isNewItem,bool exclud) {

            var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                   join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                   join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                   where (a.SpPackageId == packId && b.PrRevNo == 0 && ((isNewItem==true && c.NewItemId == int.Parse(Item)) || (isNewItem == false && c.RdBoqItem==Item)))
                                   select new AssignRevisionDetails
                                   {
                                       resourceID = c.RdResourceSeq,
                                       boqItem=c.RdBoqItem,
                                       revisionId = c.RdRevisionId
                                   }).ToList();

            foreach (var rev in revisionDetails)
            {
                var result = _dbContext.TblRevisionDetails.SingleOrDefault(b => b.RdRevisionId == rev.revisionId && b.RdBoqItem == rev.boqItem);
                if (result != null)
                {
                    result.IsExcluded = exclud;
                }
            }
            _dbContext.SaveChanges();
            return true;
        }

        public bool ExcludRessource(int packId, int boqSeq, bool isNewItem, bool isAlternative, bool exclud)
        {
            var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                   join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                   join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                   where (a.SpPackageId == packId && b.PrRevNo == 0 && 
                                   ((isNewItem == true && c.NewItemResourceId == boqSeq && c.IsNew == isNewItem) || (isAlternative == true && c.ParentResourceId == boqSeq && c.IsAlternative == isAlternative)
                                   || (isNewItem == false && isAlternative==false && c.RdResourceSeq == boqSeq) ))
                                   select new GroupingPackageSupplierPriceModel
                                   {
                                       BoqItemO = c.RdBoqItem,
                                       BoqResourceId = c.RdResourceSeq,
                                       RevisionId = c.RdRevisionId,
                                       IsAlternative = c.IsAlternative,
                                       IsNewItem = c.IsNew,
                                       NewItemId = c.NewItemId,
                                       NewItemResourceId = c.NewItemResourceId,
                                       ParentItemO = c.ParentItemO,
                                       ParentResourceId = c.ParentResourceId

                                   }).ToList();

            foreach (var rev in revisionDetails)
            {
                var result = _dbContext.TblRevisionDetails.SingleOrDefault(c => c.RdRevisionId == rev.RevisionId &&
                ((isNewItem == true && c.NewItemResourceId == boqSeq && c.IsNew == isNewItem) || (isAlternative == true && c.ParentResourceId == boqSeq && c.IsAlternative == isAlternative)
                || (isNewItem == false && isAlternative == false && c.RdResourceSeq == boqSeq && c.RdBoqItem == rev.BoqItemO)));
                
                if (result != null)
                {
                    result.IsExcluded = exclud;
                }
            }
            _dbContext.SaveChanges();
            return true;
        }
    }
}
