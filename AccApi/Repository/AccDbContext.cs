using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AccApi.Repository.Models;

#nullable disable

namespace AccApi.Repository
{
    public partial class AccDbContext : DbContext
    {
        public AccDbContext()
        {
        }

        public AccDbContext(DbContextOptions<AccDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AaaBoqDiv03> AaaBoqDiv03s { get; set; }
        public virtual DbSet<AccountingCostCode> AccountingCostCodes { get; set; }
        public virtual DbSet<Boq> Boqs { get; set; }
        public virtual DbSet<ConcreteCost> ConcreteCosts { get; set; }
        public virtual DbSet<ConcreteCostTotal> ConcreteCostTotals { get; set; }
        public virtual DbSet<Div03> Div03s { get; set; }
        public virtual DbSet<EqpCostCode> EqpCostCodes { get; set; }
        public virtual DbSet<LabourCostcenter> LabourCostcenters { get; set; }
        public virtual DbSet<Mark> Marks { get; set; }
        public virtual DbSet<MaterialCostCenter> MaterialCostCenters { get; set; }
        public virtual DbSet<Missing> Missings { get; set; }
        public virtual DbSet<PackagesNetwork> PackagesNetworks { get; set; }
        public virtual DbSet<Parameter> Parameters { get; set; }
        public virtual DbSet<SendToExcel> SendToExcels { get; set; }
        public virtual DbSet<TargetRatio> TargetRatios { get; set; }
        public virtual DbSet<TblAcmClass> TblAcmClasses { get; set; }
        public virtual DbSet<TblActivitiesGroup> TblActivitiesGroups { get; set; }
        public virtual DbSet<TblActivitiesSubWb> TblActivitiesSubWbs { get; set; }
        public virtual DbSet<TblActivity> TblActivities { get; set; }
        public virtual DbSet<TblAssignWbsActivitySubWb> TblAssignWbsActivitySubWbs { get; set; }
        public virtual DbSet<TblAvgManhourCost> TblAvgManhourCosts { get; set; }
        public virtual DbSet<TblBcwpWbsProg> TblBcwpWbsProgs { get; set; }
        public virtual DbSet<TblBoq> TblBoqs { get; set; }
        public virtual DbSet<TblBoqWbsL> TblBoqWbsLs { get; set; }
        public virtual DbSet<TblBoqbackUp> TblBoqbackUps { get; set; }
        public virtual DbSet<TblBoqtemp> TblBoqtemps { get; set; }
        public virtual DbSet<TblBoqunitRate> TblBoqunitRates { get; set; }
        public virtual DbSet<TblBoqwb> TblBoqwbs { get; set; }
        public virtual DbSet<TblBoqwbstemp> TblBoqwbstemps { get; set; }
        public virtual DbSet<TblBudgetVolume> TblBudgetVolumes { get; set; }
        public virtual DbSet<TblCandyRessource> TblCandyRessources { get; set; }
        public virtual DbSet<TblCategory> TblCategories { get; set; }
        public virtual DbSet<TblCode> TblCodes { get; set; }
        public virtual DbSet<TblContract> TblContracts { get; set; }
        public virtual DbSet<TblCurrency> TblCurrencies { get; set; }
        public virtual DbSet<TblDiscount> TblDiscounts { get; set; }
        public virtual DbSet<TblDivisionPercent> TblDivisionPercents { get; set; }
        public virtual DbSet<TblDryCostFactor> TblDryCostFactors { get; set; }
        public virtual DbSet<TblEngReport> TblEngReports { get; set; }
        public virtual DbSet<TblExceptionEva> TblExceptionEvas { get; set; }
        public virtual DbSet<TblField> TblFields { get; set; }
        public virtual DbSet<TblFieldsTable> TblFieldsTables { get; set; }
        public virtual DbSet<TblFormwork> TblFormworks { get; set; }
        public virtual DbSet<TblGlsubtrade> TblGlsubtrades { get; set; }
        public virtual DbSet<TblHistogram> TblHistograms { get; set; }
        public virtual DbSet<TblImportLogFile> TblImportLogFiles { get; set; }
        public virtual DbSet<TblIndirectCostHistogram> TblIndirectCostHistograms { get; set; }
        public virtual DbSet<TblLastLogon> TblLastLogons { get; set; }
        public virtual DbSet<TblLogInDateChange> TblLogInDateChanges { get; set; }
        public virtual DbSet<TblLookAheadQty> TblLookAheadQties { get; set; }
        public virtual DbSet<TblMailTocc> TblMailToccs { get; set; }
        public virtual DbSet<TblMissingCc> TblMissingCcs { get; set; }
        public virtual DbSet<TblMissingItemsList> TblMissingItemsLists { get; set; }
        public virtual DbSet<TblMissingPrice> TblMissingPrices { get; set; }
        public virtual DbSet<TblOriginalBoq> TblOriginalBoqs { get; set; }
        public virtual DbSet<TblOriginalBoqtemp> TblOriginalBoqtemps { get; set; }
        public virtual DbSet<TblParameter> TblParameters { get; set; }
        public virtual DbSet<TblPayment> TblPayments { get; set; }
        public virtual DbSet<TblPrelimsCategory> TblPrelimsCategories { get; set; }
        public virtual DbSet<TblPrelimsGroup> TblPrelimsGroups { get; set; }
        public virtual DbSet<TblPrelimsHistogram> TblPrelimsHistograms { get; set; }
        public virtual DbSet<TblQuotation> TblQuotations { get; set; }
        public virtual DbSet<TblReadyMixHdr> TblReadyMixHdrs { get; set; }
        public virtual DbSet<TblResource> TblResources { get; set; }
        public virtual DbSet<TblResourcesInDirect> TblResourcesInDirects { get; set; }
        public virtual DbSet<TblResourcesInDirectBoq> TblResourcesInDirectBoqs { get; set; }
        public virtual DbSet<TblResourcesInDirectBoqitem> TblResourcesInDirectBoqitems { get; set; }
        public virtual DbSet<TblResourcesInDirectGroup> TblResourcesInDirectGroups { get; set; }
        public virtual DbSet<TblResourcesInDirectIndex> TblResourcesInDirectIndices { get; set; }
        public virtual DbSet<TblResourcesInDirectSub> TblResourcesInDirectSubs { get; set; }
        public virtual DbSet<TblResourcesIndirectCategory> TblResourcesIndirectCategories { get; set; }
        public virtual DbSet<TblRevisionDetail> TblRevisionDetails { get; set; }
        public virtual DbSet<TblRevisionField> TblRevisionFields { get; set; }
        public virtual DbSet<TblRivision> TblRivisions { get; set; }
        public virtual DbSet<TblRndSel> TblRndSels { get; set; }
        public virtual DbSet<TblSubContEarnDed> TblSubContEarnDeds { get; set; }
        public virtual DbSet<TblSubContractorDiscount> TblSubContractorDiscounts { get; set; }
        public virtual DbSet<TblSubcProdBudget> TblSubcProdBudgets { get; set; }
        public virtual DbSet<TblSubcontractor> TblSubcontractors { get; set; }
        public virtual DbSet<TblSubcontractorOffer> TblSubcontractorOffers { get; set; }
        public virtual DbSet<TblSubcontractorPricesTradeTest> TblSubcontractorPricesTradeTests { get; set; }
        public virtual DbSet<TblSummaryException> TblSummaryExceptions { get; set; }
        public virtual DbSet<TblSupplier> TblSuppliers { get; set; }
        public virtual DbSet<TblSupplierDiv> TblSupplierDivs { get; set; }
        public virtual DbSet<TblSupplierPackage> TblSupplierPackages { get; set; }
        public virtual DbSet<TblSupplierPackageRevision> TblSupplierPackageRevisions { get; set; }
        public virtual DbSet<TblSystemRevLog> TblSystemRevLogs { get; set; }
        public virtual DbSet<TblTargetQty> TblTargetQties { get; set; }
        public virtual DbSet<TblTotal> TblTotals { get; set; }
        public virtual DbSet<TblTotalAdditional> TblTotalAdditionals { get; set; }
        public virtual DbSet<TblTotalAdditionalTemp> TblTotalAdditionalTemps { get; set; }
        public virtual DbSet<TblTotalTemp> TblTotalTemps { get; set; }
        public virtual DbSet<TblUserCounter> TblUserCounters { get; set; }
        public virtual DbSet<TblWbsMap> TblWbsMaps { get; set; }
        public virtual DbSet<TblWeeklyFormanByArea> TblWeeklyFormanByAreas { get; set; }
        public virtual DbSet<Tblproject> Tblprojects { get; set; }
        public virtual DbSet<Temp> Temps { get; set; }
        public virtual DbSet<TempImportAcc> TempImportAccs { get; set; }
        public virtual DbSet<TempLabourCost> TempLabourCosts { get; set; }
        public virtual DbSet<TmpDivCostCode> TmpDivCostCodes { get; set; }
        public virtual DbSet<TmpEarnedValue> TmpEarnedValues { get; set; }
        public virtual DbSet<TmpEngReport> TmpEngReports { get; set; }
        public virtual DbSet<TmpStaffHistogram> TmpStaffHistograms { get; set; }
        public virtual DbSet<TmpWbsProgQty> TmpWbsProgQties { get; set; }
        public virtual DbSet<Uuuuuuuuuu> Uuuuuuuuuus { get; set; }
        public virtual DbSet<View1> View1s { get; set; }
        public virtual DbSet<View2> View2s { get; set; }
        public virtual DbSet<View3> View3s { get; set; }
        public virtual DbSet<ViewBoq> ViewBoqs { get; set; }
        public virtual DbSet<ViewBoqitemLastRevision> ViewBoqitemLastRevisions { get; set; }
        public virtual DbSet<ViewBoqitemLastRevisionActive> ViewBoqitemLastRevisionActives { get; set; }
        public virtual DbSet<ViewBoqitemLastRevisionAll> ViewBoqitemLastRevisionAlls { get; set; }
        public virtual DbSet<ViewBoqunitPrice> ViewBoqunitPrices { get; set; }
        public virtual DbSet<ViewBudgetByCc> ViewBudgetByCcs { get; set; }
        public virtual DbSet<ViewOriginalBoqactive> ViewOriginalBoqactives { get; set; }
        public virtual DbSet<ViewOriginalBoqall> ViewOriginalBoqalls { get; set; }
        public virtual DbSet<ViewOtherAmount> ViewOtherAmounts { get; set; }
        public virtual DbSet<ViewOtherAmountsByCc> ViewOtherAmountsByCcs { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                optionsBuilder.UseSqlServer("Data Source=10.10.2.123;Initial Catalog=NewProject_CostData;Persist Security Info=True;User ID=accdb;Password=db@TSs15;Integrated Security=False");
        //            }
        //        }

        public AccDbContext CreateConnectionFromOut(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AccDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            var context = new AccDbContext(optionsBuilder.Options);
            return context;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1256_CI_AS");

            modelBuilder.Entity<AaaBoqDiv03>(entity =>
            {
                entity.ToView("aaaBOQ_Div03");

                entity.Property(e => e.BoqUnitMesure).IsUnicode(false);

                entity.Property(e => e.BoqWbs).IsUnicode(false);
            });

            modelBuilder.Entity<AccountingCostCode>(entity =>
            {
                entity.HasKey(e => new { e.AcProject, e.AcUpto, e.AcCc, e.AcGlaccount });

                entity.Property(e => e.AcProject).IsUnicode(false);

                entity.Property(e => e.AcCc).IsUnicode(false);

                entity.Property(e => e.AcGlaccount).IsUnicode(false);

                entity.Property(e => e.AcDiv).IsUnicode(false);

                entity.Property(e => e.AcSubDiv).IsUnicode(false);

                entity.Property(e => e.AcSubTrade).IsUnicode(false);

                entity.Property(e => e.AcTrade).IsUnicode(false);

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUserUpdate).IsUnicode(false);

                entity.Property(e => e.Wbs).IsUnicode(false);
            });

            modelBuilder.Entity<ConcreteCost>(entity =>
            {
                entity.Property(e => e.Area).IsUnicode(false);

                entity.Property(e => e.DivSubDiv).IsUnicode(false);

                entity.Property(e => e.SubDiv).IsUnicode(false);
            });

            modelBuilder.Entity<ConcreteCostTotal>(entity =>
            {
                entity.Property(e => e.DivSubDiv).IsUnicode(false);

                entity.Property(e => e.SubDiv).IsUnicode(false);
            });

            modelBuilder.Entity<Div03>(entity =>
            {
                entity.ToView("Div03");

                entity.Property(e => e.DescriptionO).IsUnicode(false);

                entity.Property(e => e.ObSeq).ValueGeneratedOnAdd();

                entity.Property(e => e.Zone).IsUnicode(false);
            });

            modelBuilder.Entity<EqpCostCode>(entity =>
            {
                entity.Property(e => e.EcSeq).IsUnicode(false);

                entity.Property(e => e.EcCc).IsUnicode(false);

                entity.Property(e => e.EcCostcenterDescription).IsUnicode(false);

                entity.Property(e => e.EcDescription).IsUnicode(false);

                entity.Property(e => e.EcDiv).IsUnicode(false);

                entity.Property(e => e.EcProj).IsUnicode(false);

                entity.Property(e => e.EcSubDiv).IsUnicode(false);

                entity.Property(e => e.EcSubTrade).IsUnicode(false);

                entity.Property(e => e.EcTrade).IsUnicode(false);
            });

            modelBuilder.Entity<LabourCostcenter>(entity =>
            {
                entity.HasKey(e => e.LcSeq)
                    .HasName("PK_LabourCostcenter_1");

                entity.Property(e => e.LcCampTranspAuxCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.LcCarpenter).HasDefaultValueSql("((0))");

                entity.Property(e => e.LcCumulative).HasDefaultValueSql("((0))");

                entity.Property(e => e.LcDivSubDiv).IsUnicode(false);

                entity.Property(e => e.LcDivTrade).IsUnicode(false);

                entity.Property(e => e.LcGenForman).IsUnicode(false);

                entity.Property(e => e.LcLabour).HasDefaultValueSql("((0))");

                entity.Property(e => e.LcMason).HasDefaultValueSql("((0))");

                entity.Property(e => e.LcNote).IsUnicode(false);

                entity.Property(e => e.LcOcc).IsUnicode(false);

                entity.Property(e => e.LcOtherLabors).HasDefaultValueSql("((0))");

                entity.Property(e => e.LcPainter).HasDefaultValueSql("((0))");

                entity.Property(e => e.LcPlasterer).HasDefaultValueSql("((0))");

                entity.Property(e => e.LcProj).HasDefaultValueSql("((0))");

                entity.Property(e => e.LcSecEng).IsUnicode(false);

                entity.Property(e => e.LcSiteEng).HasDefaultValueSql("((0))");

                entity.Property(e => e.LcSteelFixer).HasDefaultValueSql("((0))");

                entity.Property(e => e.LcSubCon).HasDefaultValueSql("((0))");

                entity.Property(e => e.LcTiler).HasDefaultValueSql("((0))");

                entity.Property(e => e.LcWbs).IsUnicode(false);

                entity.Property(e => e.Ldate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Luser).IsUnicode(false);
            });

            modelBuilder.Entity<MaterialCostCenter>(entity =>
            {
                entity.HasKey(e => new { e.McProj, e.McWeek, e.McCc, e.McArea });
            });

            modelBuilder.Entity<Missing>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisLab).IsUnicode(false);
            });

            modelBuilder.Entity<PackagesNetwork>(entity =>
            {
                entity.Property(e => e.FilePath).IsUnicode(false);
            });

            modelBuilder.Entity<SendToExcel>(entity =>
            {
                entity.Property(e => e.LcArea).IsUnicode(false);

                entity.Property(e => e.LcCc).IsUnicode(false);

                entity.Property(e => e.LcDiv).IsUnicode(false);

                entity.Property(e => e.LcForman).IsUnicode(false);

                entity.Property(e => e.LcSubDiv).IsUnicode(false);

                entity.Property(e => e.LcSubTrade).IsUnicode(false);

                entity.Property(e => e.LcTotalCost).IsUnicode(false);

                entity.Property(e => e.LcTotalHours).IsUnicode(false);

                entity.Property(e => e.LcTotalIndirectCost).IsUnicode(false);

                entity.Property(e => e.LcTrade).IsUnicode(false);

                entity.Property(e => e.LcWeek).IsUnicode(false);
            });

            modelBuilder.Entity<TargetRatio>(entity =>
            {
                entity.HasKey(e => new { e.Trade, e.TrProject });

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUserUpdate).IsUnicode(false);

                entity.Property(e => e.TrDiv).IsUnicode(false);

                entity.Property(e => e.TrQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.TrSubDiv).IsUnicode(false);

                entity.Property(e => e.TrSubTrade).IsUnicode(false);

                entity.Property(e => e.TrTrade).IsUnicode(false);

                entity.Property(e => e.Wbs).IsUnicode(false);
            });

            modelBuilder.Entity<TblAcmClass>(entity =>
            {
                entity.Property(e => e.AcClass).IsUnicode(false);
            });

            modelBuilder.Entity<TblActivitiesGroup>(entity =>
            {
                entity.Property(e => e.GrpDesc).IsUnicode(false);

                entity.Property(e => e.GrpProjectCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblActivitiesSubWb>(entity =>
            {
                entity.Property(e => e.SwDesc).IsUnicode(false);

                entity.Property(e => e.SwProjectCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblActivity>(entity =>
            {
                entity.Property(e => e.ActDesc).IsUnicode(false);
            });

            modelBuilder.Entity<TblAssignWbsActivitySubWb>(entity =>
            {
                entity.HasKey(e => new { e.AsTrade, e.AsActSubWbs });

                entity.Property(e => e.AsTrade).IsUnicode(false);
            });

            modelBuilder.Entity<TblAvgManhourCost>(entity =>
            {
                entity.HasKey(e => new { e.AmhProject, e.AmhWeek, e.AmhArea })
                    .HasName("PK_AvgManhourCost");

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUserUpdate).IsUnicode(false);
            });

            modelBuilder.Entity<TblBcwpWbsProg>(entity =>
            {
                entity.HasKey(e => new { e.BwpProject, e.BwpWeek, e.BwpWbs, e.BwpLevel, e.BwpUnit, e.Boq })
                    .HasName("PK_tblBcwpWbsProg_1");

                entity.Property(e => e.BwpWbs).IsUnicode(false);

                entity.Property(e => e.BwpLevel).IsUnicode(false);

                entity.Property(e => e.BwpUnit)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Boq)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.BwpCnt).HasDefaultValueSql("((0))");

                entity.Property(e => e.BwpExecQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.BwpPrelim).HasDefaultValueSql("((0))");

                entity.Property(e => e.BwpQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.BwpTotAmnt).HasDefaultValueSql("((0))");

                entity.Property(e => e.BwpType).IsUnicode(false);

                entity.Property(e => e.BwpUnitPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.BwpWbsdesc).IsUnicode(false);
            });

            modelBuilder.Entity<TblBoq>(entity =>
            {
                entity.HasKey(e => e.BoqSeq)
                    .HasName("PK__tblBOQ__D0171FA9E99FF85F");

                entity.Property(e => e.BoqSeq).ValueGeneratedNever();

                entity.Property(e => e.BoqPackage).IsUnicode(false);

                entity.Property(e => e.BoqProcPackage).HasDefaultValueSql("((0))");

                entity.Property(e => e.BoqQtyScope).HasDefaultValueSql("((0))");

                entity.Property(e => e.BoqScope).HasDefaultValueSql("((0))");

                entity.Property(e => e.BoqSelected).HasDefaultValueSql("((0))");

                entity.Property(e => e.BoqUnitMesure).IsUnicode(false);

                entity.Property(e => e.BoqWbs).IsUnicode(false);

                entity.Property(e => e.Ldate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.QtyScope).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblBoqWbsL>(entity =>
            {
                entity.Property(e => e.LsWbs).IsFixedLength(true);
            });

            modelBuilder.Entity<TblBoqbackUp>(entity =>
            {
                entity.HasKey(e => e.BbuDate)
                    .HasName("PK__tblBOQBa__D652AF752A53ED59");

                entity.Property(e => e.BbuProj).IsUnicode(false);
            });

            modelBuilder.Entity<TblBoqtemp>(entity =>
            {
                entity.HasKey(e => e.BoqSeq)
                    .HasName("PK__tblBOQTe__D0171FA925413C03");

                entity.Property(e => e.BoqLevel).IsUnicode(false);

                entity.Property(e => e.BoqPackage).IsUnicode(false);

                entity.Property(e => e.BoqUnitMesure).IsUnicode(false);

                entity.Property(e => e.BoqWbs).IsUnicode(false);

                entity.HasOne(d => d.BoqItemNavigation)
                    .WithMany(p => p.TblBoqtemps)
                    .HasForeignKey(d => d.BoqItem)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tblBOQTemp_tblOriginalBOQTemp");
            });

            modelBuilder.Entity<TblBoqunitRate>(entity =>
            {
                entity.HasKey(e => new { e.BurItem, e.BurRev, e.BurBackUpDate })
                    .HasName("PK__tblBOQUn__6C87B9584726ED86");

                entity.Property(e => e.BurItem).IsUnicode(false);

                entity.Property(e => e.BurProject).IsUnicode(false);

                entity.HasOne(d => d.BurBackUpDateNavigation)
                    .WithMany(p => p.TblBoqunitRates)
                    .HasForeignKey(d => d.BurBackUpDate)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBOQUnitRate_tblBOQBackUp");
            });

            modelBuilder.Entity<TblBoqwb>(entity =>
            {
                entity.HasKey(e => new { e.BoqwRivision, e.BoqwItem, e.BoqwWbs, e.BoqwCtg, e.BoqwLevel, e.BoqWbackUpDate })
                    .HasName("PK_tblBOQWBS_1");

                entity.Property(e => e.BoqwWbs).IsUnicode(false);

                entity.Property(e => e.BoqwLevel).IsUnicode(false);

                entity.Property(e => e.BoqWproj).IsUnicode(false);

                entity.Property(e => e.BoqwNetPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.BoqwQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.BoqwUprice).HasDefaultValueSql("((0))");

                entity.Property(e => e.RowNumber).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblBoqwbstemp>(entity =>
            {
                entity.HasKey(e => new { e.BoqwRivision, e.BoqwItem, e.BoqwWbs, e.BoqwCtg, e.BoqwLevel })
                    .HasName("PK_tblBOQWBSTemp_11");

                entity.Property(e => e.BoqwWbs).IsUnicode(false);

                entity.Property(e => e.BoqwLevel).IsUnicode(false);

                entity.Property(e => e.BoqWproj)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.BoqwNetPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.BoqwQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.BoqwUprice).HasDefaultValueSql("((0))");

                entity.Property(e => e.RowNumber).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblBudgetVolume>(entity =>
            {
                entity.HasKey(e => new { e.BvDiv, e.BvSubDiv, e.BvTrade });
            });

            modelBuilder.Entity<TblCandyRessource>(entity =>
            {
                entity.Property(e => e.CrCode).IsUnicode(false);

                entity.Property(e => e.CrDescription).IsUnicode(false);

                entity.Property(e => e.CrType).IsUnicode(false);
            });

            modelBuilder.Entity<TblCategory>(entity =>
            {
                entity.Property(e => e.CtgAbv).IsUnicode(false);

                entity.Property(e => e.CtgDesc).IsUnicode(false);
            });

            modelBuilder.Entity<TblCode>(entity =>
            {
                entity.Property(e => e.CodAuxiliaryCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.Tsdb).IsUnicode(false);
            });

            modelBuilder.Entity<TblContract>(entity =>
            {
                entity.HasKey(e => new { e.CntVendor, e.CntPackage, e.CntProject });

                entity.Property(e => e.CntProject).IsUnicode(false);

                entity.Property(e => e.CntRef).IsUnicode(false);

                entity.Property(e => e.CntSeq).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TblDivisionPercent>(entity =>
            {
                entity.HasKey(e => new { e.DpDiv, e.DpFromLine, e.DpToLine });
            });

            modelBuilder.Entity<TblDryCostFactor>(entity =>
            {
                entity.HasKey(e => new { e.DcfDiv, e.DcfPhase });
            });

            modelBuilder.Entity<TblEngReport>(entity =>
            {
                entity.Property(e => e.Block).IsUnicode(false);

                entity.Property(e => e.Boq).IsUnicode(false);

                entity.Property(e => e.Casting).HasDefaultValueSql("((0))");

                entity.Property(e => e.ErConManConf).HasDefaultValueSql("((0))");

                entity.Property(e => e.ErCounter).HasDefaultValueSql("((0))");

                entity.Property(e => e.ErDiv).IsUnicode(false);

                entity.Property(e => e.ErDwgNb).IsUnicode(false);

                entity.Property(e => e.ErPlannedCarp).HasDefaultValueSql("((0))");

                entity.Property(e => e.ErPlannedCast).HasDefaultValueSql("((0))");

                entity.Property(e => e.ErPlannedLabor).HasDefaultValueSql("((0))");

                entity.Property(e => e.ErPlannedMc).HasDefaultValueSql("((0))");

                entity.Property(e => e.ErPlannedOtherLabors).HasDefaultValueSql("((0))");

                entity.Property(e => e.ErPlannedPainter).HasDefaultValueSql("((0))");

                entity.Property(e => e.ErPlannedQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.ErPlannedSteelfix).HasDefaultValueSql("((0))");

                entity.Property(e => e.ErPlannedTiler).HasDefaultValueSql("((0))");

                entity.Property(e => e.ErReadyMixHdr).IsUnicode(false);

                entity.Property(e => e.ErSecEng).IsUnicode(false);

                entity.Property(e => e.ErSelectUser).IsUnicode(false);

                entity.Property(e => e.ErSiteEng).HasDefaultValueSql("((0))");

                entity.Property(e => e.ErSubDiv).IsUnicode(false);

                entity.Property(e => e.ErTrade).IsUnicode(false);

                entity.Property(e => e.Forman).IsUnicode(false);

                entity.Property(e => e.IsTemplate).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastUserUpdate).IsUnicode(false);

                entity.Property(e => e.Ldate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.Painter).HasDefaultValueSql("((0))");

                entity.Property(e => e.PlannedInsertedBy).IsUnicode(false);

                entity.Property(e => e.Project).IsUnicode(false);

                entity.Property(e => e.SubDivision).IsUnicode(false);

                entity.Property(e => e.SubId).IsUnicode(false);

                entity.Property(e => e.Trade).IsUnicode(false);

                entity.Property(e => e.Wbs).IsUnicode(false);
            });

            modelBuilder.Entity<TblExceptionEva>(entity =>
            {
                entity.Property(e => e.EvaSeq).IsUnicode(false);
            });

            modelBuilder.Entity<TblField>(entity =>
            {
                entity.HasKey(e => new { e.FldTableName, e.FldFieldName })
                    .HasName("PK__tblField__C81C0D2487DAC64F");

                entity.Property(e => e.FldTableName).IsUnicode(false);

                entity.Property(e => e.FldFieldName).IsUnicode(false);

                entity.Property(e => e.FldAbv).IsUnicode(false);

                entity.Property(e => e.FldDesc).IsUnicode(false);

                entity.Property(e => e.FldFieldNameSub).IsUnicode(false);

                entity.Property(e => e.FldSrcField).IsUnicode(false);

                entity.Property(e => e.FldSrcKey).IsUnicode(false);

                entity.Property(e => e.FldSrcTable).IsUnicode(false);
            });

            modelBuilder.Entity<TblFieldsTable>(entity =>
            {
                entity.HasKey(e => new { e.FldTableName, e.FldFieldName });
            });

            modelBuilder.Entity<TblFormwork>(entity =>
            {
                entity.Property(e => e.FwSeq).IsUnicode(false);

                entity.Property(e => e.FwDiv).IsUnicode(false);

                entity.Property(e => e.FwSubDiv).IsUnicode(false);

                entity.Property(e => e.FwSubTrade).IsUnicode(false);

                entity.Property(e => e.FwTrade).IsUnicode(false);

                entity.Property(e => e.Wbslevel).IsUnicode(false);
            });

            modelBuilder.Entity<TblGlsubtrade>(entity =>
            {
                entity.Property(e => e.GlWbs).IsUnicode(false);

                entity.Property(e => e.Glacct).IsUnicode(false);

                entity.Property(e => e.Gldesc).IsUnicode(false);

                entity.Property(e => e.GlsubTrade).IsUnicode(false);
            });

            modelBuilder.Entity<TblHistogram>(entity =>
            {
                entity.HasKey(e => new { e.PrResCode, e.Revision });

                entity.Property(e => e.PrResCode).IsUnicode(false);

                entity.Property(e => e.CostMonth).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty1).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty10).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty11).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty12).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty13).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty14).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty15).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty16).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty17).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty18).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty19).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty2).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty20).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty21).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty22).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty23).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty24).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty25).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty26).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty27).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty28).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty29).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty3).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty30).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty31).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty32).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty33).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty34).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty35).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty36).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty37).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty38).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty39).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty4).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty40).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty41).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty42).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty43).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty44).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty45).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty5).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty6).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty7).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty8).HasDefaultValueSql("((0))");

                entity.Property(e => e.Qty9).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblImportLogFile>(entity =>
            {
                entity.Property(e => e.IlfType).IsUnicode(false);

                entity.Property(e => e.IlfUser).IsUnicode(false);
            });

            modelBuilder.Entity<TblIndirectCostHistogram>(entity =>
            {
                entity.Property(e => e.Grp).IsUnicode(false);

                entity.Property(e => e.Indexx).IsUnicode(false);

                entity.Property(e => e.Project).IsUnicode(false);
            });

            modelBuilder.Entity<TblLastLogon>(entity =>
            {
                entity.HasKey(e => e.LlLogonDate)
                    .HasName("PK__tblLastL__6EBFCEA1AE20EA38");
            });

            modelBuilder.Entity<TblLogInDateChange>(entity =>
            {
                entity.HasKey(e => e.LdcSeq)
                    .HasName("PK__tblLogIn__9BB4F434836B7358");
            });

            modelBuilder.Entity<TblLookAheadQty>(entity =>
            {
                entity.Property(e => e.LaActSubWbs).HasDefaultValueSql("((0))");

                entity.Property(e => e.LaArea).HasDefaultValueSql("((0))");

                entity.Property(e => e.LaDayNight).HasDefaultValueSql("((0))");

                entity.Property(e => e.LaDiv).IsUnicode(false);

                entity.Property(e => e.LaForman).HasDefaultValueSql("((0))");

                entity.Property(e => e.LaPlannedQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.LaProject).IsUnicode(false);

                entity.Property(e => e.LaSecEng).HasDefaultValueSql("((0))");

                entity.Property(e => e.LaSiteEng).HasDefaultValueSql("((0))");

                entity.Property(e => e.LaSubContractor).IsUnicode(false);

                entity.Property(e => e.LaSubDiv).IsUnicode(false);

                entity.Property(e => e.LaSubId).IsUnicode(false);

                entity.Property(e => e.LaTrade).IsUnicode(false);

                entity.Property(e => e.LaWeek).HasDefaultValueSql("((0))");

                entity.Property(e => e.LaZone).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastUserUpdate).IsUnicode(false);

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.Trade).IsUnicode(false);
            });

            modelBuilder.Entity<TblMailTocc>(entity =>
            {
                entity.HasKey(e => new { e.MldSeq, e.MldCountry })
                    .HasName("PK_tblMailTOCC_1");

                entity.Property(e => e.MldSeq).ValueGeneratedOnAdd();

                entity.Property(e => e.MldContact).IsUnicode(false);

                entity.Property(e => e.MldMail).IsUnicode(false);

                entity.Property(e => e.MldMailDisabled).HasDefaultValueSql("((0))");

                entity.Property(e => e.MldPosition).IsUnicode(false);

                entity.Property(e => e.MldToCc).IsUnicode(false);
            });

            modelBuilder.Entity<TblMissingCc>(entity =>
            {
                entity.Property(e => e.MccCc).IsUnicode(false);

                entity.Property(e => e.MccDesc).IsUnicode(false);
            });

            modelBuilder.Entity<TblMissingItemsList>(entity =>
            {
                entity.HasKey(e => new { e.MilWrongItem, e.MilCorrectItem, e.MilUnitRate });

                entity.Property(e => e.MilWrongItem).IsUnicode(false);

                entity.Property(e => e.MilCorrectItem).IsUnicode(false);
            });

            modelBuilder.Entity<TblMissingPrice>(entity =>
            {
                entity.HasKey(e => new { e.RevisionId, e.BoqResourceSeq });
            });

            modelBuilder.Entity<TblOriginalBoq>(entity =>
            {
                entity.Property(e => e.C1).IsUnicode(false);

                entity.Property(e => e.C2).IsUnicode(false);

                entity.Property(e => e.C3).IsUnicode(false);

                entity.Property(e => e.C4).IsUnicode(false);

                entity.Property(e => e.C5).IsUnicode(false);

                entity.Property(e => e.C6).IsUnicode(false);

                entity.Property(e => e.CandyTemplate).HasDefaultValueSql("((0))");

                entity.Property(e => e.DescriptionO).IsUnicode(false);

                entity.Property(e => e.L1).IsUnicode(false);

                entity.Property(e => e.L2).IsUnicode(false);

                entity.Property(e => e.L3).IsUnicode(false);

                entity.Property(e => e.L4).IsUnicode(false);

                entity.Property(e => e.L5).IsUnicode(false);

                entity.Property(e => e.L6).IsUnicode(false);

                entity.Property(e => e.Ldate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.ObBillPage).IsUnicode(false);

                entity.Property(e => e.ObBoqsellTotPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.ObLevel).IsUnicode(false);

                entity.Property(e => e.ObPriceCode).IsUnicode(false);

                entity.Property(e => e.ObSkipWbsqty).HasDefaultValueSql("((0))");

                entity.Property(e => e.Prefix).IsUnicode(false);

                entity.Property(e => e.QtyScope).HasDefaultValueSql("((0))");

                entity.Property(e => e.RefNumber).IsUnicode(false);

                entity.Property(e => e.Zone).IsUnicode(false);
            });

            modelBuilder.Entity<TblOriginalBoqtemp>(entity =>
            {
                entity.Property(e => e.C1).IsUnicode(false);

                entity.Property(e => e.C2).IsUnicode(false);

                entity.Property(e => e.C3).IsUnicode(false);

                entity.Property(e => e.C4).IsUnicode(false);

                entity.Property(e => e.C5).IsUnicode(false);

                entity.Property(e => e.C6).IsUnicode(false);

                entity.Property(e => e.CandyTemplate).HasDefaultValueSql("((0))");

                entity.Property(e => e.DescriptionO).IsUnicode(false);

                entity.Property(e => e.L1).IsUnicode(false);

                entity.Property(e => e.L2).IsUnicode(false);

                entity.Property(e => e.L3).IsUnicode(false);

                entity.Property(e => e.L4).IsUnicode(false);

                entity.Property(e => e.L5).IsUnicode(false);

                entity.Property(e => e.L6).IsUnicode(false);

                entity.Property(e => e.ObBillPage).IsUnicode(false);

                entity.Property(e => e.ObBoqsellTotPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.ObLevel).IsUnicode(false);

                entity.Property(e => e.ObPriceCode).IsUnicode(false);

                entity.Property(e => e.ObSeq).ValueGeneratedOnAdd();

                entity.Property(e => e.RefNumber).IsUnicode(false);

                entity.Property(e => e.Zone).IsUnicode(false);
            });

            modelBuilder.Entity<TblParameter>(entity =>
            {
                entity.HasKey(e => e.Project)
                    .HasName("PK__tblParameters__31A25463");

                entity.Property(e => e.AyappProjId).HasDefaultValueSql("((0))");

                entity.Property(e => e.CarPriceA).HasDefaultValueSql("((0))");

                entity.Property(e => e.CarPriceB).HasDefaultValueSql("((0))");

                entity.Property(e => e.CarPriceC).HasDefaultValueSql("((0))");

                entity.Property(e => e.EstimBoqFromCc).HasDefaultValueSql("((0))");

                entity.Property(e => e.EstimMethode).HasDefaultValueSql("((0))");

                entity.Property(e => e.ExchLoanToUsd).HasDefaultValueSql("((0))");

                entity.Property(e => e.ExchLoanToUsdFrac).IsUnicode(false);

                entity.Property(e => e.ExchToUsd).HasDefaultValueSql("((0))");

                entity.Property(e => e.ExchToUsdFrac).IsUnicode(false);

                entity.Property(e => e.ImportTkByDate).HasDefaultValueSql("((0))");

                entity.Property(e => e.ImportTkExported).HasDefaultValueSql("((0))");

                entity.Property(e => e.ImportTkFromServer).IsUnicode(false);

                entity.Property(e => e.ImportTkJv).HasDefaultValueSql("((0))");

                entity.Property(e => e.InsurancePriceA).HasDefaultValueSql("((0))");

                entity.Property(e => e.InsurancePriceB).HasDefaultValueSql("((0))");

                entity.Property(e => e.InsurancePriceC).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsSilverCoast).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsUsedNewEstimDb).HasDefaultValueSql("((0))");

                entity.Property(e => e.MobileAllowA).HasDefaultValueSql("((0))");

                entity.Property(e => e.MobileAllowB).HasDefaultValueSql("((0))");

                entity.Property(e => e.MobileAllowC).HasDefaultValueSql("((0))");

                entity.Property(e => e.PolicySource).IsUnicode(false);

                entity.Property(e => e.ProjAyappId).HasDefaultValueSql("((0))");

                entity.Property(e => e.ProjectDefinition).IsUnicode(false);

                entity.Property(e => e.StaffActAllow).HasDefaultValueSql("((0))");

                entity.Property(e => e.StaffActMed).HasDefaultValueSql("((0))");

                entity.Property(e => e.StaffActSal).HasDefaultValueSql("((0))");

                entity.Property(e => e.StaffActVisa).HasDefaultValueSql("((0))");

                entity.Property(e => e.StaffBudAir).HasDefaultValueSql("((0))");

                entity.Property(e => e.StaffBudAllow).HasDefaultValueSql("((0))");

                entity.Property(e => e.StaffBudMed).HasDefaultValueSql("((0))");

                entity.Property(e => e.StaffBudVisa).HasDefaultValueSql("((0))");

                entity.Property(e => e.StafffBudSal).HasDefaultValueSql("((0))");

                entity.Property(e => e.TicketBusPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.TicketEcoPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.TotBudDeprecRent).HasDefaultValueSql("((0))");

                entity.Property(e => e.TotBudFuel).HasDefaultValueSql("((0))");

                entity.Property(e => e.TotBudOd).HasDefaultValueSql("((0))");

                entity.Property(e => e.TotBudOperator).HasDefaultValueSql("((0))");

                entity.Property(e => e.TsPolicy).IsUnicode(false);

                entity.Property(e => e.UseMapWbs).HasDefaultValueSql("((0))");

                entity.Property(e => e.WbsToCcMethode).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblPayment>(entity =>
            {
                entity.Property(e => e.PayNb).ValueGeneratedNever();

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUserUpdate).IsUnicode(false);

                entity.Property(e => e.PayCertified).HasDefaultValueSql("((0))");

                entity.Property(e => e.PayOverHead).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblPrelimsCategory>(entity =>
            {
                entity.HasKey(e => new { e.PrcGroup, e.PrcCode });

                entity.Property(e => e.PrcDescription).IsUnicode(false);
            });

            modelBuilder.Entity<TblPrelimsGroup>(entity =>
            {
                entity.Property(e => e.PrgDescription).IsUnicode(false);
            });

            modelBuilder.Entity<TblPrelimsHistogram>(entity =>
            {
                entity.Property(e => e.PrCateg).ValueGeneratedNever();
            });

            modelBuilder.Entity<TblReadyMixHdr>(entity =>
            {
                entity.HasKey(e => e.RmHdrSeq)
                    .HasName("PK__tblReady__03AF559581941B41");

                entity.Property(e => e.RmHdrSeq).IsUnicode(false);

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.RmRequestBy).IsUnicode(false);

                entity.Property(e => e.RmRequestByName).IsUnicode(false);
            });

            modelBuilder.Entity<TblResource>(entity =>
            {
                entity.HasKey(e => e.ResSeq)
                    .HasName("PK__tblResources1__347EC10E");

                entity.Property(e => e.ResSeq).IsUnicode(false);

                entity.Property(e => e.ResDescription).IsUnicode(false);

                entity.Property(e => e.ResDiv).IsUnicode(false);

                entity.Property(e => e.ResNotes).IsUnicode(false);

                entity.Property(e => e.ResSection).IsUnicode(false);

                entity.Property(e => e.ResSubDiv).IsUnicode(false);

                entity.Property(e => e.ResSubTrade).IsUnicode(false);

                entity.Property(e => e.ResSupplier).IsUnicode(false);

                entity.Property(e => e.ResTrade).IsUnicode(false);
            });

            modelBuilder.Entity<TblResourcesInDirect>(entity =>
            {
                entity.HasKey(e => new { e.RinHdrSeq, e.RinProject, e.RinRivision, e.RinGrp });

                entity.Property(e => e.RinHdrSeq).IsUnicode(false);

                entity.Property(e => e.RinProject).IsUnicode(false);

                entity.Property(e => e.RinGrp).IsUnicode(false);

                entity.Property(e => e.RinAcmA).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinArea).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinBasicMthOperatorCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinByOthers).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinCarsClass).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinConstFinishAc).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinDesc).IsUnicode(false);

                entity.Property(e => e.RinDiv).IsUnicode(false);

                entity.Property(e => e.RinFracCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinFuelQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinInvest).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinIsLabors).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinLocal).IsUnicode(false);

                entity.Property(e => e.RinMaxQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinMedicalClass).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinMinQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinMobileClass).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinMthOperatorCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinMthlyMedicalCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinMthlyOtherAuxCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinMthlyResidCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinNote).IsUnicode(false);

                entity.Property(e => e.RinOperatorFactor).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinRent).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinSpareQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinSubDiv).IsUnicode(false);

                entity.Property(e => e.RinSubTrade).IsUnicode(false);

                entity.Property(e => e.RinToDiv01).HasDefaultValueSql("((1))");

                entity.Property(e => e.RinTotalCostLabor).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinTotalCostNonLabor).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinTrade).IsUnicode(false);

                entity.Property(e => e.RinWbs).IsUnicode(false);

                entity.Property(e => e.RinYearly).HasDefaultValueSql("((0))");

                entity.Property(e => e.RinYearlyIncCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.Updated).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.RinHdrSeqNavigation)
                    .WithMany(p => p.TblResourcesInDirects)
                    .HasForeignKey(d => d.RinHdrSeq)
                    .HasConstraintName("FK_tblResourcesInDirect_tblResourcesInDirectIndex");
            });

            modelBuilder.Entity<TblResourcesInDirectBoq>(entity =>
            {
                entity.HasKey(e => new { e.RibHdrSeq, e.RibBoq })
                    .HasName("PK__tblResourcesInDi__07420643");

                entity.Property(e => e.RibHdrSeq).IsUnicode(false);

                entity.Property(e => e.RibBoq).IsUnicode(false);
            });

            modelBuilder.Entity<TblResourcesInDirectBoqitem>(entity =>
            {
                entity.HasKey(e => new { e.RibHdrSeq, e.RibCtg, e.RibGrp, e.RibBoq })
                    .HasName("PK__tblResou__0863AD60F243059B");

                entity.Property(e => e.RibHdrSeq).IsUnicode(false);

                entity.Property(e => e.RibGrp).IsUnicode(false);

                entity.Property(e => e.RibBoq).IsUnicode(false);
            });

            modelBuilder.Entity<TblResourcesInDirectGroup>(entity =>
            {
                entity.HasKey(e => e.RigSeq)
                    .HasName("PK__tblResourcesInDi__384F51F2");

                entity.Property(e => e.RigSeq).IsUnicode(false);

                entity.Property(e => e.RigAbv).IsUnicode(false);

                entity.Property(e => e.RigDesc).IsUnicode(false);
            });

            modelBuilder.Entity<TblResourcesInDirectIndex>(entity =>
            {
                entity.HasKey(e => e.RiiSeq)
                    .HasName("PK__tblResourcesInDi__3A379A64");

                entity.Property(e => e.RiiSeq).IsUnicode(false);

                entity.Property(e => e.RiiCode).IsUnicode(false);

                entity.Property(e => e.RiiDesc).IsUnicode(false);

                entity.Property(e => e.RiiDiv).IsUnicode(false);

                entity.Property(e => e.RiiFieldName).IsUnicode(false);

                entity.Property(e => e.RiiGroup).IsUnicode(false);

                entity.Property(e => e.RiiGrp).IsUnicode(false);

                entity.Property(e => e.RiiProductiveLab).HasDefaultValueSql("((0))");

                entity.Property(e => e.RiiSort).HasDefaultValueSql("((0))");

                entity.Property(e => e.RiiSubDiv).IsUnicode(false);

                entity.Property(e => e.RiiSubDivCode).IsUnicode(false);

                entity.Property(e => e.RiiSubTrade).IsUnicode(false);

                entity.Property(e => e.RiiTrade).IsUnicode(false);

                entity.Property(e => e.RiiTradeCode).IsUnicode(false);

                entity.Property(e => e.RiisBudgetE).HasDefaultValueSql("((0))");

                entity.Property(e => e.RiisBudgetL).HasDefaultValueSql("((0))");

                entity.Property(e => e.RiisBudgetM).HasDefaultValueSql("((0))");

                entity.Property(e => e.RiisBudgetS).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblResourcesInDirectSub>(entity =>
            {
                entity.HasKey(e => new { e.RisHdrSeq, e.RisProject, e.RisRivision, e.RisGrp, e.RisMonthFrom, e.RisMonthTo })
                    .HasName("PK__tblResourcesInDi__3C74E891");

                entity.Property(e => e.RisHdrSeq).IsUnicode(false);

                entity.Property(e => e.RisProject).IsUnicode(false);

                entity.Property(e => e.RisGrp).IsUnicode(false);

                entity.Property(e => e.RisAcmA).HasDefaultValueSql("((0))");

                entity.Property(e => e.RisApplicableVal).HasDefaultValueSql("((0))");

                entity.Property(e => e.RisCarsClass).HasDefaultValueSql("((0))");

                entity.Property(e => e.RisFracCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.RisFuel).HasDefaultValueSql("((0))");

                entity.Property(e => e.RisFuelTotalCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.RisMedicalClass).HasDefaultValueSql("((0))");

                entity.Property(e => e.RisMobileClass).HasDefaultValueSql("((0))");

                entity.Property(e => e.RisMthOperatorCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.RisMthlyMedicalCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.RisMthlyOtherAuxCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.RisMthlyResidCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.RisSparePartsMthlyCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.RisSparePartsTotCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.RisSpareQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.RisTotOperCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.RisYearly).HasDefaultValueSql("((0))");

                entity.Property(e => e.RisYearlyIncCost).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblResourcesIndirectCategory>(entity =>
            {
                entity.HasKey(e => e.RicSeq)
                    .HasName("PK_tblGroups");

                entity.Property(e => e.RicSeq).IsUnicode(false);

                entity.Property(e => e.RicDesc).IsUnicode(false);

                entity.Property(e => e.RicSort).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblRevisionDetail>(entity =>
            {
                entity.HasKey(e => new { e.RdRevisionId, e.RdResourceSeq });

                entity.Property(e => e.RdAssignedPerc).HasDefaultValueSql("((0))");

                entity.Property(e => e.RdAssignedPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.RdAssignedQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.RdPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.RdPriceOrigCurrency).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblRivision>(entity =>
            {
                entity.HasKey(e => e.RivSeq)
                    .HasName("PK__tblRivis__17571E75215BEB13");

                entity.Property(e => e.RivSeq).ValueGeneratedNever();
            });

            modelBuilder.Entity<TblRndSel>(entity =>
            {
                entity.Property(e => e.RnsCod).IsUnicode(false);

                entity.Property(e => e.RnsDsc).IsUnicode(false);

                entity.Property(e => e.RnsRnd).IsUnicode(false);
            });

            modelBuilder.Entity<TblSubContEarnDed>(entity =>
            {
                entity.Property(e => e.SdSeq).IsUnicode(false);

                entity.Property(e => e.SdAccPc).IsUnicode(false);

                entity.Property(e => e.SdBoq).IsUnicode(false);

                entity.Property(e => e.SdDiv).IsUnicode(false);

                entity.Property(e => e.SdDivision).IsUnicode(false);

                entity.Property(e => e.SdNote).IsUnicode(false);

                entity.Property(e => e.SdPc).IsUnicode(false);

                entity.Property(e => e.SdSubDiv).IsUnicode(false);

                entity.Property(e => e.SdSubTrade).IsUnicode(false);

                entity.Property(e => e.SdTradeCode).IsUnicode(false);
            });

            modelBuilder.Entity<TblSubContractorDiscount>(entity =>
            {
                entity.HasKey(e => new { e.SdSubId, e.SdDiv })
                    .HasName("PK__tblSubCo__4BE3F96F485300E1");

                entity.Property(e => e.SdSubId).IsUnicode(false);

                entity.Property(e => e.SdDiv).IsUnicode(false);
            });

            modelBuilder.Entity<TblSubcProdBudget>(entity =>
            {
                entity.Property(e => e.DivSubDiv).IsUnicode(false);

                entity.Property(e => e.Unit).IsUnicode(false);
            });

            modelBuilder.Entity<TblSubcontractorOffer>(entity =>
            {
                entity.HasKey(e => new { e.SoSubcontractor, e.SoItem, e.SoDate })
                    .HasName("PK__tblSubco__3A7BDE529FA28286");

                entity.Property(e => e.SoSubcontractor).IsUnicode(false);

                entity.Property(e => e.SoItem).IsUnicode(false);

                entity.Property(e => e.SoCurr).IsUnicode(false);

                entity.Property(e => e.SoNote).IsUnicode(false);

                entity.Property(e => e.SoOurRef).IsUnicode(false);

                entity.Property(e => e.SoRef).IsUnicode(false);
            });

            modelBuilder.Entity<TblSubcontractorPricesTradeTest>(entity =>
            {
                entity.Property(e => e.Boq).IsUnicode(false);

                entity.Property(e => e.IdSub).IsUnicode(false);

                entity.Property(e => e.Project).IsUnicode(false);

                entity.Property(e => e.SubDivision).IsUnicode(false);

                entity.Property(e => e.Trade).IsUnicode(false);
            });

            modelBuilder.Entity<TblSummaryException>(entity =>
            {
                entity.HasKey(e => new { e.SeProject, e.SeCc });

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUserUpdate).IsUnicode(false);
            });

            modelBuilder.Entity<TblSupplier>(entity =>
            {
                entity.HasKey(e => e.SupCode)
                    .HasName("PK__tblSuppl__8599381D70BBA62A");

                entity.Property(e => e.SupCode).ValueGeneratedNever();
            });

            modelBuilder.Entity<TblSupplierDiv>(entity =>
            {
                entity.HasKey(e => new { e.SupCode, e.SupDiv });

                entity.Property(e => e.SupDiv).IsUnicode(false);
            });

            modelBuilder.Entity<TblSupplierPackage>(entity =>
            {
                entity.HasKey(e => e.SpPackSuppId)
                    .HasName("PK_tbSupplierPackages");
            });

            modelBuilder.Entity<TblSupplierPackageRevision>(entity =>
            {
                entity.Property(e => e.PrExchRate).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblSystemRevLog>(entity =>
            {
                entity.HasKey(e => e.SrlSeq)
                    .HasName("PK__tblSystemRevLog1__4E1E9780");

                entity.Property(e => e.SrlLocation).IsUnicode(false);

                entity.Property(e => e.SrlNote).IsUnicode(false);

                entity.Property(e => e.SrlRequestedBy).IsUnicode(false);

                entity.Property(e => e.SrlRev).IsUnicode(false);

                entity.Property(e => e.SrlUser).IsUnicode(false);
            });

            modelBuilder.Entity<TblTargetQty>(entity =>
            {
                entity.Property(e => e.LastUserUpdate).IsUnicode(false);

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.TaMaxExecQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.TqActSubWbs).HasDefaultValueSql("((0))");

                entity.Property(e => e.TqArea)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TqProj)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TqTrade)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblTotal>(entity =>
            {
                entity.HasKey(e => new { e.Project, e.Week, e.Item })
                    .HasName("PK__tblTotal__E5E5085A7B8CBFE3");

                entity.Property(e => e.BudQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.BudUnitRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.CostPlusAmount).HasDefaultValueSql("((0))");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Div).IsUnicode(false);

                entity.Property(e => e.Ldate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Luser).IsUnicode(false);
            });

            modelBuilder.Entity<TblTotalAdditional>(entity =>
            {
                entity.Property(e => e.TaSeq).IsUnicode(false);

                entity.Property(e => e.Cc).IsUnicode(false);

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.LastUserUpdate).IsUnicode(false);

                entity.Property(e => e.Project).IsUnicode(false);

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.TaAbv).IsUnicode(false);

                entity.Property(e => e.TaDiv).IsUnicode(false);

                entity.Property(e => e.TaSubDiv).IsUnicode(false);

                entity.Property(e => e.TaTrade).IsUnicode(false);

                entity.Property(e => e.TaType).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TblTotalAdditionalTemp>(entity =>
            {
                entity.Property(e => e.TaSeq).IsUnicode(false);

                entity.Property(e => e.Cc).IsUnicode(false);

                entity.Property(e => e.Project).IsUnicode(false);

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.TaAbv).IsUnicode(false);

                entity.Property(e => e.TaDiv).IsUnicode(false);

                entity.Property(e => e.TaSubDiv).IsUnicode(false);

                entity.Property(e => e.TaTrade).IsUnicode(false);
            });

            modelBuilder.Entity<TblTotalTemp>(entity =>
            {
                entity.HasKey(e => e.Seq)
                    .HasName("PK__tblTotal__CA1E3C88F96F3DBD");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.PtPrefix).IsUnicode(false);
            });

            modelBuilder.Entity<TblUserCounter>(entity =>
            {
                entity.HasKey(e => new { e.UcProj, e.UcStId, e.UcUserKey, e.UcType });
            });

            modelBuilder.Entity<TblWbsMap>(entity =>
            {
                entity.HasKey(e => new { e.WbsCost, e.WbsTk });

                entity.Property(e => e.WbsCost).IsUnicode(false);

                entity.Property(e => e.WbsTk).IsUnicode(false);

                entity.Property(e => e.Used).HasDefaultValueSql("((0))");

                entity.Property(e => e.WbsTkDesc).IsUnicode(false);
            });

            modelBuilder.Entity<TblWeeklyFormanByArea>(entity =>
            {
                entity.HasKey(e => new { e.WfaWeek, e.WfaForman, e.WfaArea, e.WfaTrade });

                entity.Property(e => e.WfaForman).IsUnicode(false);

                entity.Property(e => e.WfaArea).IsUnicode(false);

                entity.Property(e => e.WfaTrade).IsUnicode(false);

                entity.Property(e => e.WfaDiv).IsUnicode(false);

                entity.Property(e => e.WfaGenForman).IsUnicode(false);

                entity.Property(e => e.WfaSubDiv).IsUnicode(false);

                entity.Property(e => e.WfaTradeCode).IsUnicode(false);
            });

            modelBuilder.Entity<Tblproject>(entity =>
            {
                entity.HasKey(e => new { e.Seq, e.PrjTsseq });

                entity.Property(e => e.Seq).ValueGeneratedOnAdd();

                entity.Property(e => e.BuildingNo).HasDefaultValueSql("((0))");

                entity.Property(e => e.Client).IsUnicode(false);

                entity.Property(e => e.Concrete).HasDefaultValueSql("((0))");

                entity.Property(e => e.ConcreteRebars).HasDefaultValueSql("((0))");

                entity.Property(e => e.ConstFinishAc).HasDefaultValueSql("((0))");

                entity.Property(e => e.ConstPeriod).HasDefaultValueSql("((0))");

                entity.Property(e => e.Country).IsUnicode(false);

                entity.Property(e => e.CoveredArea).HasDefaultValueSql("((0))");

                entity.Property(e => e.DieselGalon).HasDefaultValueSql("((0))");

                entity.Property(e => e.DirectCostValue).HasDefaultValueSql("((0))");

                entity.Property(e => e.Dlpduration).HasDefaultValueSql("((0))");

                entity.Property(e => e.Engineer).IsUnicode(false);

                entity.Property(e => e.FloorsSubStruct).HasDefaultValueSql("((0))");

                entity.Property(e => e.FloorsSuperStruct).HasDefaultValueSql("((0))");

                entity.Property(e => e.Location).IsUnicode(false);

                entity.Property(e => e.ManhoursAddPercent).HasDefaultValueSql("((0))");

                entity.Property(e => e.MthlyAvglabNonProd).HasDefaultValueSql("((0))");

                entity.Property(e => e.MthlyAvglabProd).HasDefaultValueSql("((0))");

                entity.Property(e => e.NoFloorEq).HasDefaultValueSql("((0))");

                entity.Property(e => e.NoFloorEqHeightMtr).HasDefaultValueSql("((0))");

                entity.Property(e => e.Operatormh).HasDefaultValueSql("((0))");

                entity.Property(e => e.OperatorsFactor).HasDefaultValueSql("((0))");

                entity.Property(e => e.PrelimsValue).HasDefaultValueSql("((0))");

                entity.Property(e => e.SubTotalDry).HasDefaultValueSql("((0))");

                entity.Property(e => e.TsprojId).HasDefaultValueSql("((0))");

                entity.Property(e => e.TypeOfConst).IsUnicode(false);

                entity.Property(e => e.Vat).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TempImportAcc>(entity =>
            {
                entity.Property(e => e.Adjustment).HasDefaultValueSql("((0))");

                entity.Property(e => e.Sap).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TempLabourCost>(entity =>
            {
                entity.Property(e => e.DivSubDiv).IsUnicode(false);

                entity.Property(e => e.DivTrade).IsUnicode(false);

                entity.Property(e => e.GenForman).IsUnicode(false);

                entity.Property(e => e.Occ).IsUnicode(false);

                entity.Property(e => e.SecEng).IsUnicode(false);

                entity.Property(e => e.SiteEng).HasDefaultValueSql("((0))");

                entity.Property(e => e.TxtNote).IsUnicode(false);

                entity.Property(e => e.Wbs).IsUnicode(false);
            });

            modelBuilder.Entity<TmpDivCostCode>(entity =>
            {
                entity.Property(e => e.Div).ValueGeneratedNever();
            });

            modelBuilder.Entity<TmpEarnedValue>(entity =>
            {
                entity.Property(e => e.CcSubDiv).IsUnicode(false);

                entity.Property(e => e.Div).IsUnicode(false);

                entity.Property(e => e.Wbs).IsUnicode(false);
            });

            modelBuilder.Entity<TmpEngReport>(entity =>
            {
                entity.HasKey(e => e.Seq)
                    .HasName("PK_TmpEngReport_1");

                entity.Property(e => e.ActualQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.Area).IsUnicode(false);

                entity.Property(e => e.Block).IsUnicode(false);

                entity.Property(e => e.Boq).IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ErDiv).IsUnicode(false);

                entity.Property(e => e.ErSubDiv).IsUnicode(false);

                entity.Property(e => e.ErTrade).IsUnicode(false);

                entity.Property(e => e.Forman).IsUnicode(false);

                entity.Property(e => e.LannedPainter).HasDefaultValueSql("((0))");

                entity.Property(e => e.Night).HasDefaultValueSql("((0))");

                entity.Property(e => e.PlannedCarp).HasDefaultValueSql("((0))");

                entity.Property(e => e.PlannedCasting).HasDefaultValueSql("((0))");

                entity.Property(e => e.PlannedLabor).HasDefaultValueSql("((0))");

                entity.Property(e => e.PlannedMason).HasDefaultValueSql("((0))");

                entity.Property(e => e.PlannedPainter).HasDefaultValueSql("((0))");

                entity.Property(e => e.PlannedQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.PlannedSteelFix).HasDefaultValueSql("((0))");

                entity.Property(e => e.PlannedTiler).HasDefaultValueSql("((0))");

                entity.Property(e => e.Project).IsUnicode(false);

                entity.Property(e => e.SecEng).IsUnicode(false);

                entity.Property(e => e.SiteEngineer).IsUnicode(false);

                entity.Property(e => e.SubContractor).IsUnicode(false);

                entity.Property(e => e.SubDivision).IsUnicode(false);

                entity.Property(e => e.SubId).IsUnicode(false);

                entity.Property(e => e.SubWbs).IsUnicode(false);

                entity.Property(e => e.Trade).IsUnicode(false);

                entity.Property(e => e.Unit).IsUnicode(false);

                entity.Property(e => e.Wbs).IsUnicode(false);

                entity.Property(e => e.Week).HasDefaultValueSql("((0))");

                entity.Property(e => e.Zone).IsUnicode(false);
            });

            modelBuilder.Entity<TmpStaffHistogram>(entity =>
            {
                entity.HasKey(e => new { e.Seq, e.UserName });

                entity.Property(e => e.Seq).ValueGeneratedOnAdd();

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.Property(e => e.ActQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.Category).IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.GrpSort).HasDefaultValueSql("((0))");

                entity.Property(e => e.Id).IsUnicode(false);

                entity.Property(e => e.MonthlyCostAct).HasDefaultValueSql("((0))");

                entity.Property(e => e.MonthlyCostPlan).HasDefaultValueSql("((0))");

                entity.Property(e => e.MthNo).HasDefaultValueSql("((0))");

                entity.Property(e => e.NextPlanQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.OtherAllowCostAct).HasDefaultValueSql("((0))");

                entity.Property(e => e.OtherAllowCostPlan).HasDefaultValueSql("((0))");

                entity.Property(e => e.PayrollCostAct).HasDefaultValueSql("((0))");

                entity.Property(e => e.PayrollCostPlan).HasDefaultValueSql("((0))");

                entity.Property(e => e.PlanQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.Project).IsUnicode(false);

                entity.Property(e => e.Sort).HasDefaultValueSql("((0))");

                entity.Property(e => e.SubCategory).IsUnicode(false);

                entity.Property(e => e.TotalCostPlan).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TmpWbsProgQty>(entity =>
            {
                entity.HasKey(e => e.Seq)
                    .HasName("PK__tmpWbsPr__CA1E3C88C152185C");

                entity.Property(e => e.Level).IsUnicode(false);

                entity.Property(e => e.Unit).IsUnicode(false);

                entity.Property(e => e.Wbs).IsUnicode(false);
            });

            modelBuilder.Entity<Uuuuuuuuuu>(entity =>
            {
                entity.ToView("uuuuuuuuuu");

                entity.Property(e => e.RinHdrSeq).IsUnicode(false);
            });

            modelBuilder.Entity<View1>(entity =>
            {
                entity.ToView("View1");
            });

            modelBuilder.Entity<View2>(entity =>
            {
                entity.ToView("View2");
            });

            modelBuilder.Entity<View3>(entity =>
            {
                entity.ToView("View3");
            });

            modelBuilder.Entity<ViewBoq>(entity =>
            {
                entity.ToView("viewBOQ");
            });

            modelBuilder.Entity<ViewBoqitemLastRevision>(entity =>
            {
                entity.ToView("viewBOQItemLastRevision");

                entity.Property(e => e.BurItem).IsUnicode(false);
            });

            modelBuilder.Entity<ViewBoqitemLastRevisionActive>(entity =>
            {
                entity.ToView("viewBOQItemLastRevisionActive");

                entity.Property(e => e.BurItem).IsUnicode(false);
            });

            modelBuilder.Entity<ViewBoqitemLastRevisionAll>(entity =>
            {
                entity.ToView("viewBOQItemLastRevisionALL");

                entity.Property(e => e.BurItem).IsUnicode(false);
            });

            modelBuilder.Entity<ViewBoqunitPrice>(entity =>
            {
                entity.ToView("viewBOQUnitPrice");
            });

            modelBuilder.Entity<ViewBudgetByCc>(entity =>
            {
                entity.ToView("viewBudgetByCC");

                entity.Property(e => e.RiiDiv).IsUnicode(false);

                entity.Property(e => e.RiiSubDivCode).IsUnicode(false);
            });

            modelBuilder.Entity<ViewOriginalBoqactive>(entity =>
            {
                entity.ToView("viewOriginalBOQActive");

                entity.Property(e => e.DescriptionO).IsUnicode(false);

                entity.Property(e => e.Prefix).IsUnicode(false);

                entity.Property(e => e.Zone).IsUnicode(false);
            });

            modelBuilder.Entity<ViewOriginalBoqall>(entity =>
            {
                entity.ToView("viewOriginalBOQALL");

                entity.Property(e => e.DescriptionO).IsUnicode(false);

                entity.Property(e => e.Prefix).IsUnicode(false);

                entity.Property(e => e.Zone).IsUnicode(false);
            });

            modelBuilder.Entity<ViewOtherAmount>(entity =>
            {
                entity.ToView("viewOtherAmounts");
            });

            modelBuilder.Entity<ViewOtherAmountsByCc>(entity =>
            {
                entity.ToView("viewOtherAmountsByCC");

                entity.Property(e => e.Div).IsUnicode(false);

                entity.Property(e => e.SubDiv).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
