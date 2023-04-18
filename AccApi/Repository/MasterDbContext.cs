using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AccApi.Repository.Models.MasterModels;

#nullable disable

namespace AccApi.Repository
{
    public partial class MasterDbContext : DbContext
    {
        public MasterDbContext()
        {
        }

        public MasterDbContext(DbContextOptions<MasterDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AtsArea> AtsAreas { get; set; }
        public virtual DbSet<BemArea> BemAreas { get; set; }
        public virtual DbSet<TblComCond> TblComConds { get; set; }
        public virtual DbSet<TblCompany> TblCompanies { get; set; }
        public virtual DbSet<TblCompanyCode> TblCompanyCodes { get; set; }
        public virtual DbSet<TblCurrency> TblCurrencies { get; set; }
        public virtual DbSet<TblDailyStat> TblDailyStats { get; set; }
        public virtual DbSet<TblDatabase> TblDataBases { get; set; }
        public virtual DbSet<TblDistributionGrp> TblDistributionGrps { get; set; }
        public virtual DbSet<TblEmailTemplate> TblEmailTemplates { get; set; }
        public virtual DbSet<TblEmployee> TblEmployees { get; set; }
        public virtual DbSet<TblExchange> TblExchanges { get; set; }
        public virtual DbSet<TblFindLabor> TblFindLabors { get; set; }
        public virtual DbSet<TblMailHdr> TblMailHdrs { get; set; }
        public virtual DbSet<TblMailRequest> TblMailRequests { get; set; }
        public virtual DbSet<TblMailTocc> TblMailToccs { get; set; }
        public virtual DbSet<TblManagementUser> TblManagementUsers { get; set; }
        public virtual DbSet<TblMapOccup> TblMapOccups { get; set; }
        public virtual DbSet<TblMasterProject> TblMasterProjects { get; set; }
        public virtual DbSet<TblMasterProjectsOld> TblMasterProjectsOlds { get; set; }
        public virtual DbSet<TblNetSalary> TblNetSalaries { get; set; }
        public virtual DbSet<TblOffice> TblOffices { get; set; }
        public virtual DbSet<TblPackage> TblPackages { get; set; }
        public virtual DbSet<TblPaycheck> TblPaychecks { get; set; }
        public virtual DbSet<TblPayrollDatabase> TblPayrollDataBases { get; set; }
        public virtual DbSet<TblProjWb> TblProjWbs { get; set; }
        public virtual DbSet<TblReport> TblReports { get; set; }
        public virtual DbSet<TblRndSel> TblRndSels { get; set; }
        public virtual DbSet<TblRndSelProj> TblRndSelProjs { get; set; }
        public virtual DbSet<TblSource> TblSources { get; set; }
        public virtual DbSet<TblStaffCost> TblStaffCosts { get; set; }
        public virtual DbSet<TblStaffCostElementUnit> TblStaffCostElementUnits { get; set; }
        public virtual DbSet<TblStaffCostExcludedJob> TblStaffCostExcludedJobs { get; set; }
        public virtual DbSet<TblStaffCostUnit> TblStaffCostUnits { get; set; }
        public virtual DbSet<TblSubTrade> TblSubTrades { get; set; }
        public virtual DbSet<TblSupplier> TblSuppliers { get; set; }
        public virtual DbSet<TblSupplierDiv> TblSupplierDivs { get; set; }
        public virtual DbSet<TblTechCond> TblTechConds { get; set; }
        public virtual DbSet<TblTempCount> TblTempCounts { get; set; }
        public virtual DbSet<TblTempReportsAdmin> TblTempReportsAdmins { get; set; }
        public virtual DbSet<TblTempReportsDm> TblTempReportsDms { get; set; }
        public virtual DbSet<TblTempReportsDmsPbi> TblTempReportsDmsPbis { get; set; }
        public virtual DbSet<TblTrade> TblTrades { get; set; }
        public virtual DbSet<TblUsersProject> TblUsersProjects { get; set; }
        public virtual DbSet<TempLabor> TempLabors { get; set; }
        public virtual DbSet<Tmp> Tmps { get; set; }
        public virtual DbSet<TmpStaffCost> TmpStaffCosts { get; set; }
        public virtual DbSet<TmpUnitCost> TmpUnitCosts { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=ABEDHIJAZI;Initial Catalog=MasterProjects;Persist Security Info=True;User ID=accdb;Password=db@TSs15;Integrated Security=False");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1256_CI_AS");

            modelBuilder.Entity<AtsArea>(entity =>
            {
                entity.Property(e => e.New).IsUnicode(false);

                entity.Property(e => e.Old).IsUnicode(false);
            });

            modelBuilder.Entity<BemArea>(entity =>
            {
                entity.Property(e => e.New).IsUnicode(false);

                entity.Property(e => e.Old).IsUnicode(false);
            });

            modelBuilder.Entity<TblComCond>(entity =>
            {
                entity.Property(e => e.CmSelected).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblCompany>(entity =>
            {
                entity.Property(e => e.ComActive).HasComment("1");

                entity.Property(e => e.ComConnection).IsUnicode(false);

                entity.Property(e => e.ComName).IsUnicode(false);

                entity.Property(e => e.ComServer).IsUnicode(false);
            });

            modelBuilder.Entity<TblCompanyCode>(entity =>
            {
                entity.Property(e => e.CompanyCode).IsUnicode(false);

                entity.Property(e => e.CompanyLocation).IsUnicode(false);

                entity.Property(e => e.CompanyName).IsUnicode(false);

                entity.Property(e => e.CompanyPdefLen).HasDefaultValueSql("((0))");

                entity.Property(e => e.CompanyPdefStart).IsFixedLength(true);
            });

            modelBuilder.Entity<TblCurrency>(entity =>
            {
                entity.Property(e => e.CurCode).IsUnicode(false);

                entity.Property(e => e.CurDesc).IsUnicode(false);
            });

            modelBuilder.Entity<TblDailyStat>(entity =>
            {
                entity.Property(e => e.LabFileNo).IsUnicode(false);

                entity.Property(e => e.LabId).IsUnicode(false);

                entity.Property(e => e.Seq).IsUnicode(false);
            });

            modelBuilder.Entity<TblDatabase>(entity =>
            {
                entity.HasKey(e => e.DbSeq)
                    .HasName("PK_tblDataBases_1");

                entity.Property(e => e.DbSeq).ValueGeneratedNever();

                entity.Property(e => e.DbActive).HasDefaultValueSql("((0))");

                entity.Property(e => e.DbConnection).IsUnicode(false);

                entity.Property(e => e.DbDescription).IsUnicode(false);

                entity.Property(e => e.DbLocation).IsUnicode(false);

                entity.Property(e => e.DbName).IsUnicode(false);

                entity.Property(e => e.DbServer).IsUnicode(false);

                entity.Property(e => e.DbServerName).IsUnicode(false);

                entity.Property(e => e.DbUserId).IsUnicode(false);

                entity.Property(e => e.DbWebPortal).IsUnicode(false);

                entity.Property(e => e.SsrsDomain).IsUnicode(false);

                entity.Property(e => e.SsrsPass).IsUnicode(false);

                entity.Property(e => e.SsrsServer).IsUnicode(false);

                entity.Property(e => e.SsrsUser).IsUnicode(false);
            });

            modelBuilder.Entity<TblDistributionGrp>(entity =>
            {
                entity.Property(e => e.GdDesc).IsUnicode(false);

                entity.Property(e => e.GdEmail).IsUnicode(false);
            });

            modelBuilder.Entity<TblEmailTemplate>(entity =>
            {
                entity.Property(e => e.EtLang)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TblEmployee>(entity =>
            {
                entity.Property(e => e.EmpName).IsUnicode(false);

                entity.Property(e => e.Empjob).IsUnicode(false);
            });

            modelBuilder.Entity<TblExchange>(entity =>
            {
                entity.Property(e => e.CurId).IsUnicode(false);
            });

            modelBuilder.Entity<TblFindLabor>(entity =>
            {
                entity.Property(e => e.CodDescE).IsUnicode(false);

                entity.Property(e => e.LabFileNo).IsUnicode(false);

                entity.Property(e => e.LabId).IsUnicode(false);

                entity.Property(e => e.PrjName).IsUnicode(false);

                entity.Property(e => e.Seq).IsUnicode(false);
            });

            modelBuilder.Entity<TblMailHdr>(entity =>
            {
                entity.HasKey(e => new { e.MlhSeq, e.MlhProjCode, e.MlhMailType });

                entity.Property(e => e.MlhSeq).ValueGeneratedOnAdd();

                entity.Property(e => e.MlhProjCode).IsUnicode(false);

                entity.Property(e => e.MlhMailType).IsUnicode(false);

                entity.Property(e => e.MlhMailTypeDesc).IsUnicode(false);
            });

            modelBuilder.Entity<TblMailRequest>(entity =>
            {
                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MlrAttachment)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MlrCode).IsUnicode(false);

                entity.Property(e => e.MlrMailTo).IsUnicode(false);

                entity.Property(e => e.MlrMailType).IsUnicode(false);

                entity.Property(e => e.MlrProjCode).IsUnicode(false);

                entity.Property(e => e.MlrReqBy).IsUnicode(false);

                entity.Property(e => e.MlrRequesterMail).IsUnicode(false);

                entity.Property(e => e.MlrSent).HasDefaultValueSql("((0))");

                entity.Property(e => e.MlrSubject).IsUnicode(false);

                entity.Property(e => e.MlrWithTable).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblMailTocc>(entity =>
            {
                entity.HasKey(e => new { e.MldHdrSeq, e.MldSeq });

                entity.Property(e => e.MldSeq).ValueGeneratedOnAdd();

                entity.Property(e => e.MldMail).IsUnicode(false);

                entity.Property(e => e.MldMailDisabled).HasDefaultValueSql("((0))");

                entity.Property(e => e.MldToCc).IsUnicode(false);
            });

            modelBuilder.Entity<TblManagementUser>(entity =>
            {
                entity.Property(e => e.Mail).IsUnicode(false);

                entity.Property(e => e.Occupation).IsUnicode(false);

                entity.Property(e => e.UserName).IsFixedLength(true);
            });

            modelBuilder.Entity<TblMapOccup>(entity =>
            {
                entity.Property(e => e.EstOccup).IsUnicode(false);

                entity.Property(e => e.EstOccupId).IsUnicode(false);

                entity.Property(e => e.InsrertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MapOccup).IsUnicode(false);

                entity.Property(e => e.MapOccupId).IsUnicode(false);

                entity.Property(e => e.Staff).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblMasterProject>(entity =>
            {
                entity.Property(e => e.MsSeq).ValueGeneratedNever();

                entity.Property(e => e.MsActive).HasComment("1");

                entity.Property(e => e.MsConnection).IsUnicode(false);

                entity.Property(e => e.MsDesc).IsUnicode(false);

                entity.Property(e => e.MsDescSap).IsUnicode(false);

                entity.Property(e => e.MsEmasPm).HasDefaultValueSql("((0))");

                entity.Property(e => e.MsIsEms).HasDefaultValueSql("((0))");

                entity.Property(e => e.MsIsFreeze).HasDefaultValueSql("((0))");

                entity.Property(e => e.MsPbiReport).IsUnicode(false);

                entity.Property(e => e.MsReportsPortal).IsUnicode(false);

                entity.Property(e => e.MsServer).IsUnicode(false);

                entity.Property(e => e.MsSsrsServer).IsUnicode(false);

                entity.Property(e => e.NewProject).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TblMasterProjectsOld>(entity =>
            {
                entity.Property(e => e.MsConnection).IsUnicode(false);

                entity.Property(e => e.MsDesc).IsUnicode(false);

                entity.Property(e => e.MsDescSap).IsUnicode(false);

                entity.Property(e => e.MsServer).IsUnicode(false);
            });

            modelBuilder.Entity<TblNetSalary>(entity =>
            {
                entity.Property(e => e.CodDescE).IsUnicode(false);

                entity.Property(e => e.HolDays).HasDefaultValueSql("(0)");

                entity.Property(e => e.HolHrs).HasDefaultValueSql("(0)");

                entity.Property(e => e.HolOthrs).HasDefaultValueSql("(0)");

                entity.Property(e => e.HolOtpay).HasDefaultValueSql("(0)");

                entity.Property(e => e.HolPay).HasDefaultValueSql("(0)");

                entity.Property(e => e.IdleDays).HasDefaultValueSql("(0)");

                entity.Property(e => e.IdleHrs).HasDefaultValueSql("(0)");

                entity.Property(e => e.IdlePay).HasDefaultValueSql("(0)");

                entity.Property(e => e.LabDownPay).HasDefaultValueSql("(0)");

                entity.Property(e => e.LabFileNo).IsUnicode(false);

                entity.Property(e => e.LabId).IsUnicode(false);

                entity.Property(e => e.LabPhoto).IsUnicode(false);

                entity.Property(e => e.LabProject).IsUnicode(false);

                entity.Property(e => e.Ndhrs).HasDefaultValueSql("(0)");

                entity.Property(e => e.NdhrsAll).HasDefaultValueSql("(0)");

                entity.Property(e => e.Othrs).HasDefaultValueSql("(0)");

                entity.Property(e => e.PayAcc).HasDefaultValueSql("(0)");

                entity.Property(e => e.PayOver).HasDefaultValueSql("(0)");

                entity.Property(e => e.Seq).IsUnicode(false);

                entity.Property(e => e.SumOfdisFood).HasDefaultValueSql("(0)");

                entity.Property(e => e.TaxFixDyn).IsUnicode(false);

                entity.Property(e => e.TotalDays).HasDefaultValueSql("(0)");

                entity.Property(e => e.VacHrs).HasDefaultValueSql("(0)");

                entity.Property(e => e.VacPay).HasDefaultValueSql("(0)");

                entity.Property(e => e.Wedays).HasDefaultValueSql("(0)");

                entity.Property(e => e.Wehrs).HasDefaultValueSql("(0)");

                entity.Property(e => e.Weothrs).HasDefaultValueSql("(0)");

                entity.Property(e => e.Weotpay).HasDefaultValueSql("(0)");

                entity.Property(e => e.Wepay).HasDefaultValueSql("(0)");
            });

            modelBuilder.Entity<TblPackage>(entity =>
            {
                entity.HasKey(e => e.PkgeId)
                    .HasName("PK_Packages-Network");

                entity.Property(e => e.FilePath).IsUnicode(false);
            });

            modelBuilder.Entity<TblPaycheck>(entity =>
            {
                entity.HasKey(e => new { e.ScpYear, e.ScpMonth, e.ScpName });

                entity.Property(e => e.ScpYear).IsUnicode(false);

                entity.Property(e => e.ScpMonth).IsUnicode(false);

                entity.Property(e => e.ScpName).IsUnicode(false);

                entity.Property(e => e.Ldate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ScpJob).IsUnicode(false);

                entity.Property(e => e.ScpSheetType).IsUnicode(false);

                entity.Property(e => e.Seq).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TblPayrollDatabase>(entity =>
            {
                entity.HasKey(e => new { e.DbName, e.DbDescription });

                entity.Property(e => e.DbName).IsUnicode(false);

                entity.Property(e => e.DbDescription).IsUnicode(false);

                entity.Property(e => e.DbActive).HasDefaultValueSql("((0))");

                entity.Property(e => e.DbConnection).IsUnicode(false);

                entity.Property(e => e.DbLocation).IsUnicode(false);

                entity.Property(e => e.DbServer).IsUnicode(false);

                entity.Property(e => e.DbUserId).IsUnicode(false);
            });

            modelBuilder.Entity<TblProjWb>(entity =>
            {
                entity.Property(e => e.PsProjDesc).IsUnicode(false);

                entity.Property(e => e.PsProjWbs).IsUnicode(false);
            });

            modelBuilder.Entity<TblReport>(entity =>
            {
                entity.Property(e => e.RptDesc).IsUnicode(false);

                entity.Property(e => e.RptObject).IsUnicode(false);
            });

            modelBuilder.Entity<TblRndSel>(entity =>
            {
                entity.Property(e => e.RnsCod).IsUnicode(false);

                entity.Property(e => e.RnsDsc).IsUnicode(false);

                entity.Property(e => e.RnsRnd).IsUnicode(false);
            });

            modelBuilder.Entity<TblRndSelProj>(entity =>
            {
                entity.Property(e => e.RspCod).IsUnicode(false);

                entity.Property(e => e.RspDsc).IsUnicode(false);
            });

            modelBuilder.Entity<TblSource>(entity =>
            {
                entity.Property(e => e.ServerName).IsUnicode(false);

                entity.Property(e => e.Source).IsUnicode(false);
            });

            modelBuilder.Entity<TblStaffCost>(entity =>
            {
                entity.Property(e => e.ScpCoAccount).IsUnicode(false);

                entity.Property(e => e.ScpCompany).IsUnicode(false);

                entity.Property(e => e.ScpElementDesc).IsUnicode(false);

                entity.Property(e => e.ScpJob).IsUnicode(false);

                entity.Property(e => e.ScpName).IsUnicode(false);

                entity.Property(e => e.ScpProjOffice).IsUnicode(false);

                entity.Property(e => e.ScpSheetType).IsUnicode(false);

                entity.Property(e => e.ScpType).IsUnicode(false);

                entity.Property(e => e.ScpWbse).IsUnicode(false);
            });

            modelBuilder.Entity<TblStaffCostExcludedJob>(entity =>
            {
                entity.Property(e => e.EjJob).IsUnicode(false);

                entity.Property(e => e.EjSeq).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TblStaffCostUnit>(entity =>
            {
                entity.Property(e => e.ScUnit).IsUnicode(false);
            });

            modelBuilder.Entity<TblSubTrade>(entity =>
            {
                entity.HasKey(e => e.StSeq)
                    .HasName("PK_tblSubTrade_1");

                entity.Property(e => e.StDivCode).IsUnicode(false);

                entity.Property(e => e.StDivTradeCode).IsFixedLength(true);

                entity.Property(e => e.StSubDivCode).IsUnicode(false);

                entity.Property(e => e.StSubTradeCode).IsUnicode(false);

                entity.Property(e => e.StSubTradeDesc).IsUnicode(false);

                entity.Property(e => e.StTradeCode).IsUnicode(false);
            });

            modelBuilder.Entity<TblSupplier>(entity =>
            {
                entity.HasKey(e => e.SupCode)
                    .HasName("PK__tblSuppl__8599381D70BBA62A");
            });

            modelBuilder.Entity<TblSupplierDiv>(entity =>
            {
                entity.HasKey(e => new { e.SupCode, e.SupDiv });

                entity.Property(e => e.SupDiv).IsUnicode(false);
            });

            modelBuilder.Entity<TblTechCond>(entity =>
            {
                entity.Property(e => e.TcSelected).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblTempCount>(entity =>
            {
                entity.Property(e => e.LabFileNo).IsUnicode(false);

                entity.Property(e => e.LabId).IsUnicode(false);

                entity.Property(e => e.TSeq).IsUnicode(false);
            });

            modelBuilder.Entity<TblTempReportsAdmin>(entity =>
            {
                entity.Property(e => e.CoddescE).IsUnicode(false);

                entity.Property(e => e.Project).IsUnicode(false);
            });

            modelBuilder.Entity<TblTempReportsDm>(entity =>
            {
                entity.HasKey(e => e.Seq)
                    .HasName("PK__tblTempR__CA1938C0395884C4");

                entity.Property(e => e.Abv).IsUnicode(false);

                entity.Property(e => e.AdLoc1).IsUnicode(false);

                entity.Property(e => e.AdLoc2).IsUnicode(false);

                entity.Property(e => e.AdLoc3).IsUnicode(false);

                entity.Property(e => e.AdLoc4).IsUnicode(false);

                entity.Property(e => e.Agent).IsUnicode(false);

                entity.Property(e => e.AllUsers).HasDefaultValueSql("((0))");

                entity.Property(e => e.Area).IsUnicode(false);

                entity.Property(e => e.Attachment).IsUnicode(false);

                entity.Property(e => e.Discipline).IsUnicode(false);

                entity.Property(e => e.DisciplineGrp).IsUnicode(false);

                entity.Property(e => e.DisciplineSubGrp).IsUnicode(false);

                entity.Property(e => e.Div).IsUnicode(false);

                entity.Property(e => e.DocumentNo).IsUnicode(false);

                entity.Property(e => e.DwgNo).IsUnicode(false);

                entity.Property(e => e.Floor).IsUnicode(false);

                entity.Property(e => e.ForDwgDesc).IsUnicode(false);

                entity.Property(e => e.ForSeq).IsUnicode(false);

                entity.Property(e => e.Foreman).IsUnicode(false);

                entity.Property(e => e.FormDesc).IsUnicode(false);

                entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Location).IsUnicode(false);

                entity.Property(e => e.Manufacturer).IsUnicode(false);

                entity.Property(e => e.Originator).IsUnicode(false);

                entity.Property(e => e.ProjectDb).IsUnicode(false);

                entity.Property(e => e.Redesign).IsUnicode(false);

                entity.Property(e => e.ReplyStatus).IsUnicode(false);

                entity.Property(e => e.RevNo).IsUnicode(false);

                entity.Property(e => e.Revision).IsUnicode(false);

                entity.Property(e => e.RowNo).HasDefaultValueSql("((0))");

                entity.Property(e => e.SdhBldgs).IsUnicode(false);

                entity.Property(e => e.SdhSeq).IsUnicode(false);

                entity.Property(e => e.SdhTradeCode).IsUnicode(false);

                entity.Property(e => e.SecEng).IsUnicode(false);

                entity.Property(e => e.SiteEng).IsUnicode(false);

                entity.Property(e => e.Srt).HasDefaultValueSql("((0))");

                entity.Property(e => e.SsdEcc).IsUnicode(false);

                entity.Property(e => e.SsdReplyStatusCpa).IsUnicode(false);

                entity.Property(e => e.SsdRev).IsUnicode(false);

                entity.Property(e => e.SsdSeq).IsUnicode(false);

                entity.Property(e => e.Submittal).IsUnicode(false);

                entity.Property(e => e.Supplier).IsUnicode(false);

                entity.Property(e => e.Tittle).IsUnicode(false);

                entity.Property(e => e.TradeDesc).IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Validity).IsUnicode(false);

                entity.Property(e => e.VillaType).IsUnicode(false);

                entity.Property(e => e.Zone).IsUnicode(false);
            });

            modelBuilder.Entity<TblTempReportsDmsPbi>(entity =>
            {
                entity.HasKey(e => e.Seq)
                    .HasName("PK__tblTempR__PBI");

                entity.Property(e => e.Abv).IsUnicode(false);

                entity.Property(e => e.AdLoc1).IsUnicode(false);

                entity.Property(e => e.AdLoc2).IsUnicode(false);

                entity.Property(e => e.AdLoc3).IsUnicode(false);

                entity.Property(e => e.AdLoc4).IsUnicode(false);

                entity.Property(e => e.Agent).IsUnicode(false);

                entity.Property(e => e.Area).IsUnicode(false);

                entity.Property(e => e.Attachment).IsUnicode(false);

                entity.Property(e => e.Discipline).IsUnicode(false);

                entity.Property(e => e.DisciplineGrp).IsUnicode(false);

                entity.Property(e => e.DisciplineSubGrp).IsUnicode(false);

                entity.Property(e => e.Div).IsUnicode(false);

                entity.Property(e => e.DocumentNo).IsUnicode(false);

                entity.Property(e => e.DwgNo).IsUnicode(false);

                entity.Property(e => e.Floor).IsUnicode(false);

                entity.Property(e => e.ForSeq).IsUnicode(false);

                entity.Property(e => e.FormDesc).IsUnicode(false);

                entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Location).IsUnicode(false);

                entity.Property(e => e.LocationDetails).IsUnicode(false);

                entity.Property(e => e.Manufacturer).IsUnicode(false);

                entity.Property(e => e.MonthName).IsUnicode(false);

                entity.Property(e => e.Originator).IsUnicode(false);

                entity.Property(e => e.Project).IsUnicode(false);

                entity.Property(e => e.ProjectDb).IsUnicode(false);

                entity.Property(e => e.Redesign).IsUnicode(false);

                entity.Property(e => e.ReplyStatus).IsUnicode(false);

                entity.Property(e => e.ReplyStatusCpa).IsUnicode(false);

                entity.Property(e => e.RevNo).IsUnicode(false);

                entity.Property(e => e.Revision).IsUnicode(false);

                entity.Property(e => e.RowNo).HasDefaultValueSql("((0))");

                entity.Property(e => e.SdhBldgs).IsUnicode(false);

                entity.Property(e => e.SdhSeq).IsUnicode(false);

                entity.Property(e => e.SdhTradeCode).IsUnicode(false);

                entity.Property(e => e.SecEng).IsUnicode(false);

                entity.Property(e => e.SiteEng).IsUnicode(false);

                entity.Property(e => e.Srt).HasDefaultValueSql("((0))");

                entity.Property(e => e.SsdEcc).IsUnicode(false);

                entity.Property(e => e.SsdReplyStatusCpa).IsUnicode(false);

                entity.Property(e => e.SsdRev).IsUnicode(false);

                entity.Property(e => e.SsdSeq).IsUnicode(false);

                entity.Property(e => e.Submittal).IsUnicode(false);

                entity.Property(e => e.Supplier).IsUnicode(false);

                entity.Property(e => e.Tittle).IsUnicode(false);

                entity.Property(e => e.TradeDesc).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.Property(e => e.Validity).IsUnicode(false);

                entity.Property(e => e.VillaType).IsUnicode(false);

                entity.Property(e => e.VillaType1).IsUnicode(false);

                entity.Property(e => e.Zone).IsUnicode(false);
            });

            modelBuilder.Entity<TblTrade>(entity =>
            {
                entity.Property(e => e.TrSeq).ValueGeneratedNever();

                entity.Property(e => e.InsertBy).IsUnicode(false);

                entity.Property(e => e.LastUpdateBy).IsUnicode(false);

                entity.Property(e => e.TrAbv).IsUnicode(false);

                entity.Property(e => e.TrAbvTitle).IsUnicode(false);

                entity.Property(e => e.TrDisciplineGrp).IsUnicode(false);

                entity.Property(e => e.TrDisciplineSubGrp).IsUnicode(false);

                entity.Property(e => e.TrDivCode).IsUnicode(false);

                entity.Property(e => e.TrDivSubDivTrade).IsUnicode(false);

                entity.Property(e => e.TrSelected).HasDefaultValueSql("((0))");

                entity.Property(e => e.TrSubDivCode).IsUnicode(false);

                entity.Property(e => e.TrTrade).IsUnicode(false);

                entity.Property(e => e.TrTradeCode).IsUnicode(false);
            });

            modelBuilder.Entity<TblUsersProject>(entity =>
            {
                entity.HasKey(e => new { e.UsrProjId, e.UsrId });
            });

            modelBuilder.Entity<TempLabor>(entity =>
            {
                entity.HasKey(e => new { e.LabFileNo, e.LegacyNo });

                entity.Property(e => e.LabFileNo).IsUnicode(false);

                entity.Property(e => e.LegacyNo).IsUnicode(false);
            });

            modelBuilder.Entity<Tmp>(entity =>
            {
                entity.Property(e => e.Id3).IsUnicode(false);

                entity.Property(e => e.Id4).IsUnicode(false);

                entity.Property(e => e.TkId).IsUnicode(false);

                entity.Property(e => e.TsId).IsUnicode(false);

                entity.Property(e => e.VillaTypeId).IsUnicode(false);

                entity.Property(e => e.ZoneId).IsUnicode(false);
            });

            modelBuilder.Entity<TmpStaffCost>(entity =>
            {
                entity.Property(e => e.ScpCoAccount).IsUnicode(false);

                entity.Property(e => e.ScpCompany).IsUnicode(false);

                entity.Property(e => e.ScpElementDesc).IsUnicode(false);

                entity.Property(e => e.ScpJob).IsUnicode(false);

                entity.Property(e => e.ScpName).IsUnicode(false);

                entity.Property(e => e.ScpProjOffice).IsUnicode(false);

                entity.Property(e => e.ScpSheetType).IsUnicode(false);

                entity.Property(e => e.ScpType).IsUnicode(false);

                entity.Property(e => e.ScpUnit).IsUnicode(false);

                entity.Property(e => e.ScpWbse).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);
            });

            modelBuilder.Entity<TmpUnitCost>(entity =>
            {
                entity.Property(e => e.Office).IsUnicode(false);

                entity.Property(e => e.Unit).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
