using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AccApi.Repository.Models.PolicyModels;

#nullable disable

namespace AccApi.Repository
{
    public partial class PolicyDbContext : DbContext
    {
        private readonly string _connectionString;

        public PolicyDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PolicyDbContext(DbContextOptions<PolicyDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public PolicyDbContext CreateConnectionFromOut(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PolicyDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            var context = new PolicyDbContext(optionsBuilder.Options);
            return context;
        }

        public virtual DbSet<AddDed> AddDeds { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<DataIdleLostHour> DataIdleLostHours { get; set; }
        public virtual DbSet<Datamaster> Datamasters { get; set; }
        public virtual DbSet<Datum> Data { get; set; }
        public virtual DbSet<JobsList> JobsLists { get; set; }
        public virtual DbSet<Kadum> Kada { get; set; }
        public virtual DbSet<LinkArea> LinkAreas { get; set; }
        public virtual DbSet<LinkZone> LinkZones { get; set; }
        public virtual DbSet<Mouhafaza> Mouhafazas { get; set; }
        public virtual DbSet<Mstatus> Mstatuses { get; set; }
        public virtual DbSet<SapJob> SapJobs { get; set; }
        public virtual DbSet<SapLabor> SapLabors { get; set; }
        public virtual DbSet<SendToExcel> SendToExcels { get; set; }
        public virtual DbSet<TblAccomFloor> TblAccomFloors { get; set; }
        public virtual DbSet<TblAccomLocation> TblAccomLocations { get; set; }
        public virtual DbSet<TblAccomRoom> TblAccomRooms { get; set; }
        public virtual DbSet<TblArea> TblAreas { get; set; }
        public virtual DbSet<TblAssignFormanEng> TblAssignFormanEngs { get; set; }
        public virtual DbSet<TblAttinsuranceCommpany> TblAttinsuranceCommpanies { get; set; }
        public virtual DbSet<TblAuxiliary> TblAuxiliaries { get; set; }
        public virtual DbSet<TblAyappMapOccup> TblAyappMapOccups { get; set; }
        public virtual DbSet<TblAyappMapOccupDiv01> TblAyappMapOccupDiv01s { get; set; }
        public virtual DbSet<TblBankLab> TblBankLabs { get; set; }
        public virtual DbSet<TblBankLabNew> TblBankLabNews { get; set; }
        public virtual DbSet<TblBankSal> TblBankSals { get; set; }
        public virtual DbSet<TblBankSalNew> TblBankSalNews { get; set; }
        public virtual DbSet<TblBuilding> TblBuildings { get; set; }
        public virtual DbSet<TblClassSalaryMargin> TblClassSalaryMargins { get; set; }
        public virtual DbSet<TblCode> TblCodes { get; set; }
        public virtual DbSet<TblCombinationWb> TblCombinationWbs { get; set; }
        public virtual DbSet<TblCompany> TblCompanies { get; set; }
        public virtual DbSet<TblDailyAbsentForman> TblDailyAbsentFormen { get; set; }
        public virtual DbSet<TblDailyIdleHour> TblDailyIdleHours { get; set; }
        public virtual DbSet<TblDailyReportOccupGroup> TblDailyReportOccupGroups { get; set; }
        public virtual DbSet<TblDailySubConLabor> TblDailySubConLabors { get; set; }
        public virtual DbSet<TblDesignation> TblDesignations { get; set; }
        public virtual DbSet<TblDistribFormanWb> TblDistribFormanWbs { get; set; }
        public virtual DbSet<TblDistribHdr> TblDistribHdrs { get; set; }
        public virtual DbSet<TblDistribHdrDeleted> TblDistribHdrDeleteds { get; set; }
        public virtual DbSet<TblDistribHdrIdleLostHour> TblDistribHdrIdleLostHours { get; set; }
        public virtual DbSet<TblDistribHdrManPower> TblDistribHdrManPowers { get; set; }
        public virtual DbSet<TblDistribHdrMaster> TblDistribHdrMasters { get; set; }
        public virtual DbSet<TblDistribHdrMasterBackup15112016> TblDistribHdrMasterBackup15112016s { get; set; }
        public virtual DbSet<TblDistribHdrVisitor> TblDistribHdrVisitors { get; set; }
        public virtual DbSet<TblDivision> TblDivisions { get; set; }
        public virtual DbSet<TblEmpTimeSheet> TblEmpTimeSheets { get; set; }
        public virtual DbSet<TblEmpTransferHistory> TblEmpTransferHistories { get; set; }
        public virtual DbSet<TblEntryInsurance> TblEntryInsurances { get; set; }
        public virtual DbSet<TblEntryMedical> TblEntryMedicals { get; set; }
        public virtual DbSet<TblEntryPassport> TblEntryPassports { get; set; }
        public virtual DbSet<TblEntryPayment> TblEntryPayments { get; set; }
        public virtual DbSet<TblEntryResidence> TblEntryResidences { get; set; }
        public virtual DbSet<TblEntrySaudiCouncil> TblEntrySaudiCouncils { get; set; }
        public virtual DbSet<TblForemanProject> TblForemanProjects { get; set; }
        public virtual DbSet<TblForman> TblFormen { get; set; }
        public virtual DbSet<TblFunction> TblFunctions { get; set; }
        public virtual DbSet<TblGroup> TblGroups { get; set; }
        public virtual DbSet<TblGroupsUser> TblGroupsUsers { get; set; }
        public virtual DbSet<TblHoliday> TblHolidays { get; set; }
        public virtual DbSet<TblImportEntry> TblImportEntries { get; set; }
        public virtual DbSet<TblImportForman> TblImportFormen { get; set; }
        public virtual DbSet<TblImportGosi> TblImportGosis { get; set; }
        public virtual DbSet<TblImportWb> TblImportWbs { get; set; }
        public virtual DbSet<TblImportZoneArea> TblImportZoneAreas { get; set; }
        public virtual DbSet<TblIndirectCost> TblIndirectCosts { get; set; }
        public virtual DbSet<TblInsuranceCompany> TblInsuranceCompanies { get; set; }
        public virtual DbSet<TblInvDtl> TblInvDtls { get; set; }
        public virtual DbSet<TblInvHdr> TblInvHdrs { get; set; }
        public virtual DbSet<TblJobCode> TblJobCodes { get; set; }
        public virtual DbSet<TblJobSkill> TblJobSkills { get; set; }
        public virtual DbSet<TblLab> TblLabs { get; set; }
        public virtual DbSet<TblLabAccomodation> TblLabAccomodations { get; set; }
        public virtual DbSet<TblLabAddress> TblLabAddresses { get; set; }
        public virtual DbSet<TblLabBlockHr> TblLabBlockHrs { get; set; }
        public virtual DbSet<TblLabDocument> TblLabDocuments { get; set; }
        public virtual DbSet<TblLabDraft> TblLabDrafts { get; set; }
        public virtual DbSet<TblLabHiddenWeek> TblLabHiddenWeeks { get; set; }
        public virtual DbSet<TblLabSalHistory> TblLabSalHistories { get; set; }
        public virtual DbSet<TblLabSti> TblLabStis { get; set; }
        public virtual DbSet<TblLaborHistogram> TblLaborHistograms { get; set; }
        public virtual DbSet<TblLaborVacation> TblLaborVacations { get; set; }
        public virtual DbSet<TblLaborWarning> TblLaborWarnings { get; set; }
        public virtual DbSet<TblLastLogon> TblLastLogons { get; set; }
        public virtual DbSet<TblLocation> TblLocations { get; set; }
        public virtual DbSet<TblLog> TblLogs { get; set; }
        public virtual DbSet<TblLogChangeLaborsId> TblLogChangeLaborsIds { get; set; }
        public virtual DbSet<TblLogInDateChange> TblLogInDateChanges { get; set; }
        public virtual DbSet<TblManPowerSupp> TblManPowerSupps { get; set; }
        public virtual DbSet<TblManpowerSuppSalary> TblManpowerSuppSalaries { get; set; }
        public virtual DbSet<TblMapNewLabId> TblMapNewLabIds { get; set; }
        public virtual DbSet<TblMapOccupation> TblMapOccupations { get; set; }
        public virtual DbSet<TblMasterProject> TblMasterProjects { get; set; }
        public virtual DbSet<TblModify> TblModifies { get; set; }
        public virtual DbSet<TblMoneyClass> TblMoneyClasses { get; set; }
        public virtual DbSet<TblMoneyClassLab> TblMoneyClassLabs { get; set; }
        public virtual DbSet<TblMonthlyAddDed> TblMonthlyAddDeds { get; set; }
        public virtual DbSet<TblNationality> TblNationalities { get; set; }
        public virtual DbSet<TblNetSalary> TblNetSalaries { get; set; }
        public virtual DbSet<TblOccupGroup> TblOccupGroups { get; set; }
        public virtual DbSet<TblOccupSubGroup> TblOccupSubGroups { get; set; }
        public virtual DbSet<TblPayrollDate> TblPayrollDates { get; set; }
        public virtual DbSet<TblPermGrpUsr> TblPermGrpUsrs { get; set; }
        public virtual DbSet<TblPermission> TblPermissions { get; set; }
        public virtual DbSet<TblProjectWeek> TblProjectWeeks { get; set; }
        public virtual DbSet<TblRegroup> TblRegroups { get; set; }
        public virtual DbSet<TblReport> TblReports { get; set; }
        public virtual DbSet<TblReportsBy> TblReportsBies { get; set; }
        public virtual DbSet<TblReportsColumn> TblReportsColumns { get; set; }
        public virtual DbSet<TblRndSel> TblRndSels { get; set; }
        public virtual DbSet<TblRndSelCc> TblRndSelCcs { get; set; }
        public virtual DbSet<TblSerial> TblSerials { get; set; }
        public virtual DbSet<TblSickLeave> TblSickLeaves { get; set; }
        public virtual DbSet<TblSkillPrice> TblSkillPrices { get; set; }
        public virtual DbSet<TblStatus> TblStatuses { get; set; }
        public virtual DbSet<TblSubConstractorSalary> TblSubConstractorSalaries { get; set; }
        public virtual DbSet<TblSubcontractor> TblSubcontractors { get; set; }
        public virtual DbSet<TblSystemDef> TblSystemDefs { get; set; }
        public virtual DbSet<TblTaxMargin> TblTaxMargins { get; set; }
        public virtual DbSet<TblTaxRate> TblTaxRates { get; set; }
        public virtual DbSet<TblTemp> TblTemps { get; set; }
        public virtual DbSet<TblTempImportEntry> TblTempImportEntries { get; set; }
        public virtual DbSet<TblTempLaborContract> TblTempLaborContracts { get; set; }
        public virtual DbSet<TblTempReport> TblTempReports { get; set; }
        public virtual DbSet<TblTimeSchedule> TblTimeSchedules { get; set; }
        public virtual DbSet<TblTimeScheduleExpDtl> TblTimeScheduleExpDtls { get; set; }
        public virtual DbSet<TblTimeScheduleExpHdr> TblTimeScheduleExpHdrs { get; set; }
        public virtual DbSet<TblTimeScheduleHdr> TblTimeScheduleHdrs { get; set; }
        public virtual DbSet<TblTmpImportGosi> TblTmpImportGosis { get; set; }
        public virtual DbSet<TblTrade> TblTrades { get; set; }
        public virtual DbSet<TblTransfPassport> TblTransfPassports { get; set; }
        public virtual DbSet<TblTransfResidence> TblTransfResidences { get; set; }
        public virtual DbSet<TblUser> TblUsers { get; set; }
        public virtual DbSet<TblUserCounter> TblUserCounters { get; set; }
        public virtual DbSet<TblUsersProject> TblUsersProjects { get; set; }
        public virtual DbSet<TblUsersProjectsDef> TblUsersProjectsDefs { get; set; }
        public virtual DbSet<TblUsersZone> TblUsersZones { get; set; }
        public virtual DbSet<TblVisitor> TblVisitors { get; set; }
        public virtual DbSet<TblWb> TblWbs { get; set; }
        public virtual DbSet<TblWbsArea> TblWbsAreas { get; set; }
        public virtual DbSet<TblWbsMap> TblWbsMaps { get; set; }
        public virtual DbSet<TblZone> TblZones { get; set; }
        public virtual DbSet<Tblproject> Tblprojects { get; set; }
        public virtual DbSet<Tblquantitysubcontractor> Tblquantitysubcontractors { get; set; }
        public virtual DbSet<TempAyappforman> TempAyappformen { get; set; }
        public virtual DbSet<TempAyappreport> TempAyappreports { get; set; }
        public virtual DbSet<TempAyappreport1> TempAyappreports1 { get; set; }
        public virtual DbSet<TempAyappreportsDiv01> TempAyappreportsDiv01s { get; set; }
        public virtual DbSet<TempAyappreportsDiv011> TempAyappreportsDiv01s1 { get; set; }
        public virtual DbSet<TempCorrectWb> TempCorrectWbs { get; set; }
        public virtual DbSet<TempImport> TempImports { get; set; }
        public virtual DbSet<TempImportMonthlyAddDed> TempImportMonthlyAddDeds { get; set; }
        public virtual DbSet<Tempwb> Tempwbs { get; set; }
        public virtual DbSet<Tmp> Tmps { get; set; }
        public virtual DbSet<TmpChangeSalary> TmpChangeSalaries { get; set; }
        public virtual DbSet<TmpExportEntry> TmpExportEntries { get; set; }
        public virtual DbSet<TmpImportAttendance> TmpImportAttendances { get; set; }
        public virtual DbSet<TmpImportLabor> TmpImportLabors { get; set; }
        public virtual DbSet<TmpImportLaborHistogram> TmpImportLaborHistograms { get; set; }
        public virtual DbSet<TmpJoinTrsfStaffWorker> TmpJoinTrsfStaffWorkers { get; set; }
        public virtual DbSet<TmpLabAccomodation> TmpLabAccomodations { get; set; }
        public virtual DbSet<TmpMissingDatum> TmpMissingData { get; set; }
        public virtual DbSet<TmpReplaceWb> TmpReplaceWbs { get; set; }
        public virtual DbSet<TmpResignedLab> TmpResignedLabs { get; set; }
        public virtual DbSet<TmpStaff> TmpStaffs { get; set; }
        public virtual DbSet<TrasferEmp> TrasferEmps { get; set; }
        public virtual DbSet<ViewLaborDailyAttCount> ViewLaborDailyAttCounts { get; set; }
        public virtual DbSet<ViewLaborDailyAttCountHistory> ViewLaborDailyAttCountHistories { get; set; }
        public virtual DbSet<ViewLaborMaxDate> ViewLaborMaxDates { get; set; }
        public virtual DbSet<ViewLaborsAttDay> ViewLaborsAttDays { get; set; }
        public virtual DbSet<ViewServerDate> ViewServerDates { get; set; }
        public virtual DbSet<VwGetPrevHoursFirstAttendance> VwGetPrevHoursFirstAttendances { get; set; }
        public virtual DbSet<VwIsFirstAttendance> VwIsFirstAttendances { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1256_CI_AS");

            modelBuilder.Entity<AddDed>(entity =>
            {
                entity.Property(e => e.FileNo).IsUnicode(false);

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.Property(e => e.Type).IsUnicode(false);
            });

            modelBuilder.Entity<DataIdleLostHour>(entity =>
            {
                entity.Property(e => e.F1).IsUnicode(false);

                entity.Property(e => e.F2).IsUnicode(false);

                entity.Property(e => e.F5).IsUnicode(false);

                entity.Property(e => e.F6).IsUnicode(false);

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.Sts).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Datamaster>(entity =>
            {
                entity.Property(e => e.Seq).ValueGeneratedNever();

                entity.Property(e => e.F1).IsUnicode(false);

                entity.Property(e => e.F2).IsUnicode(false);

                entity.Property(e => e.F5).IsUnicode(false);

                entity.Property(e => e.F6).IsUnicode(false);

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.Sts).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Datum>(entity =>
            {
                entity.HasKey(e => e.Seq)
                    .HasName("PK_tblLabAttBarCode");

                entity.Property(e => e.F1).IsUnicode(false);

                entity.Property(e => e.F2).IsUnicode(false);

                entity.Property(e => e.F5).IsUnicode(false);

                entity.Property(e => e.F6).IsUnicode(false);

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.Sts).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<LinkArea>(entity =>
            {
                entity.HasKey(e => new { e.OldArea, e.NewArea });

                entity.Property(e => e.OldArea).IsUnicode(false);

                entity.Property(e => e.NewArea).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<LinkZone>(entity =>
            {
                entity.HasKey(e => new { e.OldZoneId, e.NewZoneId });

                entity.Property(e => e.OldZoneId).IsUnicode(false);

                entity.Property(e => e.NewZoneId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SapLabor>(entity =>
            {
                entity.Property(e => e.ManualLabId).ValueGeneratedNever();

                entity.Property(e => e.Manul).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SendToExcel>(entity =>
            {
                entity.Property(e => e.AcTyp).IsUnicode(false);

                entity.Property(e => e.AttDate).IsUnicode(false);

                entity.Property(e => e.Building).IsUnicode(false);

                entity.Property(e => e.Coar).IsUnicode(false);

                entity.Property(e => e.CostCenter).IsUnicode(false);

                entity.Property(e => e.FromTime).IsUnicode(false);

                entity.Property(e => e.Holiday).HasDefaultValueSql("((0))");

                entity.Property(e => e.InternalOrder).IsUnicode(false);

                entity.Property(e => e.NormalHours).HasDefaultValueSql("((0))");

                entity.Property(e => e.PersonnelNumber).IsUnicode(false);

                entity.Property(e => e.ProjDef).IsUnicode(false);

                entity.Property(e => e.Project).IsUnicode(false);

                entity.Property(e => e.SapWbs).IsUnicode(false);

                entity.Property(e => e.SendCctr).IsUnicode(false);

                entity.Property(e => e.ToTime).IsUnicode(false);

                entity.Property(e => e.Total).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.Property(e => e.Wbs).IsUnicode(false);
            });

            modelBuilder.Entity<TblAccomFloor>(entity =>
            {
                entity.HasKey(e => new { e.FlCampSeq, e.FlId })
                    .HasName("PK_tblFloor_1");

                entity.Property(e => e.FlId).IsUnicode(false);

                entity.Property(e => e.FlAbv).IsUnicode(false);

                entity.HasOne(d => d.FlCampSeqNavigation)
                    .WithMany(p => p.TblAccomFloors)
                    .HasForeignKey(d => d.FlCampSeq)
                    .HasConstraintName("FK_tblAccomFloor_tblAccomLocation");
            });

            modelBuilder.Entity<TblAccomLocation>(entity =>
            {
                entity.HasKey(e => e.AlId)
                    .HasName("PK_tblAccomLocation_1");

                entity.Property(e => e.AlDailyRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.AlProjectCode).IsUnicode(false);
            });

            modelBuilder.Entity<TblAccomRoom>(entity =>
            {
                entity.HasKey(e => new { e.RmId, e.RmCampSeq })
                    .HasName("PK_tblAccomRoom_1");

                entity.Property(e => e.RmId).ValueGeneratedOnAdd();

                entity.Property(e => e.RmAbv).IsUnicode(false);

                entity.Property(e => e.RmCapacity).HasDefaultValueSql("((0))");

                entity.Property(e => e.RmFloorId)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblArea>(entity =>
            {
                entity.HasKey(e => e.ArId)
                    .HasName("PK_tblAreas_1");

                entity.Property(e => e.ArLevel).IsUnicode(false);

                entity.Property(e => e.ArName).IsUnicode(false);

                entity.Property(e => e.ArProjDef)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUserUpdate).IsUnicode(false);
            });

            modelBuilder.Entity<TblAssignFormanEng>(entity =>
            {
                entity.HasKey(e => new { e.FeForman, e.FeFromDate })
                    .HasName("PK_tblAssignForSecSiteEng");

                entity.Property(e => e.FeArea).HasDefaultValueSql("((0))");

                entity.Property(e => e.FeCm).HasDefaultValueSql("((0))");

                entity.Property(e => e.FeGeneralForman).HasDefaultValueSql("((0))");

                entity.Property(e => e.FeProject).HasDefaultValueSql("((0))");

                entity.Property(e => e.FeSectionEng).HasDefaultValueSql("((0))");

                entity.Property(e => e.FeSeq).ValueGeneratedOnAdd();

                entity.Property(e => e.FeSiteEng).HasDefaultValueSql("((0))");

                entity.Property(e => e.FeWbs).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblAttinsuranceCommpany>(entity =>
            {
                entity.HasKey(e => e.Seq)
                    .HasName("PK_tblAttinsurancecommpany");

                entity.Property(e => e.InsInsertBy).IsUnicode(false);

                entity.Property(e => e.InsInsurancecompany).IsUnicode(false);

                entity.Property(e => e.InsPolicynumber).IsUnicode(false);

                entity.Property(e => e.InsProject).IsUnicode(false);

                entity.Property(e => e.InsTypeofinsurance).IsUnicode(false);
            });

            modelBuilder.Entity<TblAuxiliary>(entity =>
            {
                entity.HasKey(e => new { e.AxDiv, e.AxSubDiv, e.AxTrade, e.AxNatioId, e.AxJob, e.AxCampId, e.AxDateFrom, e.AxDateTo })
                    .HasName("PK_tblAuxiliary_Old_1");

                entity.Property(e => e.AxDiv)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.AxSubDiv)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.AxTrade)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.AxDateFrom).HasDefaultValueSql("('01-01-1900')");

                entity.Property(e => e.AxDateTo).HasDefaultValueSql("('01-01-9999')");

                entity.Property(e => e.AxCampAuxRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.AxContChochesion).HasDefaultValueSql("((0))");

                entity.Property(e => e.AxContGhs).HasDefaultValueSql("((0))");

                entity.Property(e => e.AxContIndustrial).HasDefaultValueSql("((0))");

                entity.Property(e => e.AxContLeave).HasDefaultValueSql("((0))");

                entity.Property(e => e.AxContMedicalFixed).HasDefaultValueSql("((0))");

                entity.Property(e => e.AxContMedicalUnion).HasDefaultValueSql("((0))");

                entity.Property(e => e.AxContProvidentFund).HasDefaultValueSql("((0))");

                entity.Property(e => e.AxContReduduncy).HasDefaultValueSql("((0))");

                entity.Property(e => e.AxContSocialIns).HasDefaultValueSql("((0))");

                entity.Property(e => e.AxContUnion).HasDefaultValueSql("((0))");

                entity.Property(e => e.AxContUnions).HasDefaultValueSql("((0))");

                entity.Property(e => e.AxDedIncomeTax).HasDefaultValueSql("((0))");

                entity.Property(e => e.AxDedMedicalFixed).HasDefaultValueSql("((0))");

                entity.Property(e => e.AxDedProvidentFund).HasDefaultValueSql("((0))");

                entity.Property(e => e.AxDedSocialIns).HasDefaultValueSql("((0))");

                entity.Property(e => e.AxDedUnionsFund).HasDefaultValueSql("((0))");

                entity.Property(e => e.AxNatioAuxRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.AxOtherAuxRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.AxProjCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.AxSponsor).HasDefaultValueSql("((0))");

                entity.Property(e => e.AxTransAuxRate).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblAyappMapOccup>(entity =>
            {
                entity.Property(e => e.AyappOccup).IsUnicode(false);

                entity.Property(e => e.Tsoccup).IsUnicode(false);

                entity.Property(e => e.Wbs).IsUnicode(false);
            });

            modelBuilder.Entity<TblAyappMapOccupDiv01>(entity =>
            {
                entity.Property(e => e.AyappOccup).IsUnicode(false);

                entity.Property(e => e.Tsoccup).IsUnicode(false);

                entity.Property(e => e.Wbs).IsUnicode(false);
            });

            modelBuilder.Entity<TblBankLab>(entity =>
            {
                entity.Property(e => e.LabFfname).IsUnicode(false);

                entity.Property(e => e.LabFname).IsUnicode(false);

                entity.Property(e => e.LabId).IsUnicode(false);

                entity.Property(e => e.LabLname).IsUnicode(false);

                entity.Property(e => e.LabMobile).IsUnicode(false);

                entity.Property(e => e.LabMstatus).IsUnicode(false);

                entity.Property(e => e.LabNbPass).IsUnicode(false);

                entity.Property(e => e.LabPhone).IsUnicode(false);

                entity.Property(e => e.LabSex).IsUnicode(false);

                entity.Property(e => e.LabTitle).IsUnicode(false);

                entity.Property(e => e.NameP).IsUnicode(false);

                entity.Property(e => e.Nat).IsUnicode(false);

                entity.Property(e => e.SecNat).IsUnicode(false);
            });

            modelBuilder.Entity<TblBankLabNew>(entity =>
            {
                entity.Property(e => e.Areaname).IsUnicode(false);

                entity.Property(e => e.Bldg).IsUnicode(false);

                entity.Property(e => e.CodDescE).IsUnicode(false);

                entity.Property(e => e.Country).IsUnicode(false);

                entity.Property(e => e.Cur).IsUnicode(false);

                entity.Property(e => e.Dob).IsUnicode(false);

                entity.Property(e => e.Filler).IsUnicode(false);

                entity.Property(e => e.Gender).IsUnicode(false);

                entity.Property(e => e.Id).IsUnicode(false);

                entity.Property(e => e.LabFfname).IsUnicode(false);

                entity.Property(e => e.LabFname).IsUnicode(false);

                entity.Property(e => e.LabLname).IsUnicode(false);

                entity.Property(e => e.LabMobile).IsUnicode(false);

                entity.Property(e => e.LabPhone).IsUnicode(false);

                entity.Property(e => e.LadEmail).IsUnicode(false);

                entity.Property(e => e.LadPostBox).IsUnicode(false);

                entity.Property(e => e.LadRegion).IsUnicode(false);

                entity.Property(e => e.LadStr).IsUnicode(false);

                entity.Property(e => e.NameP).IsUnicode(false);

                entity.Property(e => e.Title).IsUnicode(false);
            });

            modelBuilder.Entity<TblBankSal>(entity =>
            {
                entity.Property(e => e.CodAbrv).IsUnicode(false);

                entity.Property(e => e.LabFaccNew).IsUnicode(false);

                entity.Property(e => e.LabId).IsUnicode(false);

                entity.Property(e => e.Salary).IsUnicode(false);
            });

            modelBuilder.Entity<TblBankSalNew>(entity =>
            {
                entity.Property(e => e.Cur).IsUnicode(false);

                entity.Property(e => e.EmplyerData).IsUnicode(false);

                entity.Property(e => e.LabFaccNew).IsUnicode(false);

                entity.Property(e => e.Salary).IsUnicode(false);
            });

            modelBuilder.Entity<TblBuilding>(entity =>
            {
                entity.HasKey(e => new { e.BldgId, e.BldgProj });

                entity.Property(e => e.BldgId).ValueGeneratedOnAdd();

                entity.Property(e => e.BldgMain).IsUnicode(false);

                entity.Property(e => e.BldgName).IsUnicode(false);

                entity.Property(e => e.BldgProjDef).IsUnicode(false);

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUserUpdate).IsUnicode(false);
            });

            modelBuilder.Entity<TblClassSalaryMargin>(entity =>
            {
                entity.Property(e => e.CsmClass).IsUnicode(false);
            });

            modelBuilder.Entity<TblCode>(entity =>
            {
                entity.Property(e => e.CodAbrv).IsUnicode(false);

                entity.Property(e => e.CodAuxiliaryCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.CodAuxiliarybyCamp).HasDefaultValueSql("((0))");

                entity.Property(e => e.CodRep).IsUnicode(false);

                entity.Property(e => e.CodRt).IsUnicode(false);

                entity.Property(e => e.Code).IsUnicode(false);

                entity.Property(e => e.Luser).IsUnicode(false);
            });

            modelBuilder.Entity<TblCombinationWb>(entity =>
            {
                entity.HasKey(e => e.ComProjSapWbs)
                    .HasName("PK_tbCombinationWBS");

                entity.Property(e => e.ComProjSapWbs).IsUnicode(false);

                entity.Property(e => e.ComDiv)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ComProjDef).IsUnicode(false);

                entity.Property(e => e.ComProjSapname).IsUnicode(false);

                entity.Property(e => e.ComTax).HasDefaultValueSql("((0))");

                entity.Property(e => e.Loans).HasDefaultValueSql("((0))");

                entity.Property(e => e.NetSalaries).HasDefaultValueSql("((0))");

                entity.Property(e => e.Round).HasDefaultValueSql("((0))");

                entity.Property(e => e.Selected).HasDefaultValueSql("((0))");

                entity.Property(e => e.Tax).HasDefaultValueSql("((0))");

                entity.Property(e => e.TotalEarning).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblCompany>(entity =>
            {
                entity.HasKey(e => new { e.ComProjId, e.ComProjCode });

                entity.Property(e => e.ComProjCode).IsUnicode(false);

                entity.Property(e => e.ComCompany).IsUnicode(false);

                entity.Property(e => e.ComCostCenter).IsUnicode(false);
            });

            modelBuilder.Entity<TblDailyAbsentForman>(entity =>
            {
                entity.HasKey(e => new { e.DafProject, e.DafDate, e.DafForman });

                entity.Property(e => e.DafProject).IsUnicode(false);

                entity.Property(e => e.DafNote).IsUnicode(false);
            });

            modelBuilder.Entity<TblDailyIdleHour>(entity =>
            {
                entity.HasKey(e => new { e.IdlDate, e.IdlType, e.IdlForman, e.IdlProject, e.IdlZone, e.IdlArea })
                    .HasName("PK_tblIdleHours");

                entity.Property(e => e.IdlProject).IsUnicode(false);

                entity.Property(e => e.IdlHours).HasDefaultValueSql("((0))");

                entity.Property(e => e.IdlNotes).IsUnicode(false);

                entity.Property(e => e.IdlProjDef).IsUnicode(false);

                entity.Property(e => e.IdlWbs).IsUnicode(false);

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy).IsUnicode(false);
            });

            modelBuilder.Entity<TblDailyReportOccupGroup>(entity =>
            {
                entity.HasKey(e => new { e.DrProjId, e.DrJobId });

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Luser).IsUnicode(false);
            });

            modelBuilder.Entity<TblDailySubConLabor>(entity =>
            {
                entity.HasKey(e => new { e.DslProject, e.DslDate, e.DslSubId, e.DslJob })
                    .HasName("PK_tblDailySubConLabors_1");

                entity.Property(e => e.DslProject).IsUnicode(false);
            });

            modelBuilder.Entity<TblDesignation>(entity =>
            {
                entity.Property(e => e.Id).IsUnicode(false);
            });

            modelBuilder.Entity<TblDistribFormanWb>(entity =>
            {
                entity.HasKey(e => new { e.Disformanid, e.Disprojectdef, e.DisWbs });

                entity.Property(e => e.Disprojectdef).IsUnicode(false);

                entity.Property(e => e.DisWbs)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.DisinsertDate).IsUnicode(false);

                entity.Property(e => e.Disinsertby).IsUnicode(false);
            });

            modelBuilder.Entity<TblDistribHdr>(entity =>
            {
                entity.HasKey(e => e.Seq)
                    .HasName("PK__tblDistribHdr1__7A672E12");

                entity.Property(e => e.Confirmed).HasDefaultValueSql("((0))");

                entity.Property(e => e.ConfirmedBy).IsUnicode(false);

                entity.Property(e => e.DisArea).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisBldg).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisContraHrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisDailyHours).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisDailyPayTax).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisDayFee).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisDayFeeCs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisDeductionHrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisDeletedBy).IsUnicode(false);

                entity.Property(e => e.DisDesig).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisEarlyHrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisExchRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisFood).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisHol).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisHolHrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisHolOthrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisHolOtpay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisHolPay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisHours).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisHousingPay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisHrsDay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisHrsDaySchedule).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisIdleHrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisJob).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisLab).IsUnicode(false);

                entity.Property(e => e.DisLocation).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisLunchBreak).HasDefaultValueSql("((1))");

                entity.Property(e => e.DisLunchBreakHrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisLunchBreakWrkHrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisNh).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisNight).IsUnicode(false);

                entity.Property(e => e.DisNonProdHrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisNonProdPay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisNonTaxable).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisNorHrsday).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisOccupGrp).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisOthrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisOtvalidation).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisOtvalidationBy).IsUnicode(false);

                entity.Property(e => e.DisPayAcc).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisPayNh).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisPayOver).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisPrevHrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisProdHrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisProdPay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisProject).IsUnicode(false);

                entity.Property(e => e.DisProjectDef).IsUnicode(false);

                entity.Property(e => e.DisRecalc).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisRecalcBy).IsUnicode(false);

                entity.Property(e => e.DisSickRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisSponsor).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisSummerHrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisTotalOthrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisTotalPay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisTotalPayOver).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisTransportPay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisVacHrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisVacPay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisWbs).IsUnicode(false);

                entity.Property(e => e.DisWe).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisWeek).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisWehrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisWeothrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisWeotpay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisWepay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisfirstAtt).HasDefaultValueSql("((0))");

                entity.Property(e => e.Disforman).HasDefaultValueSql("((0))");

                entity.Property(e => e.Exported).HasDefaultValueSql("((0))");

                entity.Property(e => e.ExportedBy).IsUnicode(false);

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy).IsUnicode(false);

                entity.Property(e => e.UserOpenExport).IsUnicode(false);

                entity.HasOne(d => d.DisLabNavigation)
                    .WithMany(p => p.TblDistribHdrs)
                    .HasForeignKey(d => d.DisLab)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tblDistribHdr_tblLab");
            });

            modelBuilder.Entity<TblDistribHdrDeleted>(entity =>
            {
                entity.Property(e => e.Seq).ValueGeneratedNever();

                entity.Property(e => e.ConfirmedBy).IsUnicode(false);

                entity.Property(e => e.DisBldg).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisDailyPayTax).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisDayFee).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisDeletedBy).IsUnicode(false);

                entity.Property(e => e.DisDesig).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisExchRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisHolHrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisHolOtpay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisHolPay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisHousingPay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisJob).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisLab).IsUnicode(false);

                entity.Property(e => e.DisLunchBreakWrkHrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisNight).IsUnicode(false);

                entity.Property(e => e.DisNonTaxable).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisNorHrsday).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisOccupGrp).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisPayAcc).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisPayOver).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisPrevHrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisProject).IsUnicode(false);

                entity.Property(e => e.DisProjectDef).IsUnicode(false);

                entity.Property(e => e.DisRecalc).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisRecalcBy).IsUnicode(false);

                entity.Property(e => e.DisSponsor).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisTransportPay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisWbs).IsUnicode(false);

                entity.Property(e => e.DisWeek).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisWehrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisWepay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisfirstAtt).HasDefaultValueSql("((0))");

                entity.Property(e => e.DteInsFromHdr).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExportedBy).IsUnicode(false);

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.UpdatedBy).IsUnicode(false);

                entity.Property(e => e.UserOpenExport).IsUnicode(false);
            });

            modelBuilder.Entity<TblDistribHdrIdleLostHour>(entity =>
            {
                entity.HasKey(e => e.Seq)
                    .HasName("PK_tblDistribHdrsubcontractor_1");

                entity.Property(e => e.DisDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisLab).IsUnicode(false);
            });

            modelBuilder.Entity<TblDistribHdrManPower>(entity =>
            {
                entity.HasKey(e => e.Seq)
                    .HasName("PK__tblDistribHdrManPower1__7A672E12");

                entity.Property(e => e.Confirmed).HasDefaultValueSql("((0))");

                entity.Property(e => e.ConfirmedBy).IsUnicode(false);

                entity.Property(e => e.DisClass).IsUnicode(false);

                entity.Property(e => e.DisDeductionHrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisDeletedBy).IsUnicode(false);

                entity.Property(e => e.DisDesig).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisLabCount).HasDefaultValueSql("((1))");

                entity.Property(e => e.DisLocation).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisNight).IsUnicode(false);

                entity.Property(e => e.DisNorHrsday).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisOthrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisProject).IsUnicode(false);

                entity.Property(e => e.DisProjectDef).IsUnicode(false);

                entity.Property(e => e.DisWbs).IsUnicode(false);

                entity.Property(e => e.Exported).HasDefaultValueSql("((0))");

                entity.Property(e => e.ExportedBy).IsUnicode(false);

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy).IsUnicode(false);

                entity.Property(e => e.UserOpenExport).IsUnicode(false);

                entity.HasOne(d => d.DisLabNavigation)
                    .WithMany(p => p.TblDistribHdrManPowers)
                    .HasForeignKey(d => d.DisLab)
                    .HasConstraintName("FK_tblDistribHdrManPower_tblManPowerSupp");
            });

            modelBuilder.Entity<TblDistribHdrMaster>(entity =>
            {
                entity.Property(e => e.Seq).ValueGeneratedNever();

                entity.Property(e => e.ConfirmedBy).IsUnicode(false);

                entity.Property(e => e.DisBldg).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisDailyPayTax).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisDayFee).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisDeletedBy).IsUnicode(false);

                entity.Property(e => e.DisExchRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisHolHrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisHolOtpay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisHolPay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisHousingPay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisJob).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisLab).IsUnicode(false);

                entity.Property(e => e.DisNight).IsUnicode(false);

                entity.Property(e => e.DisNonTaxable).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisNorHrsday).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisOccupGrp).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisOtvalidation).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisOtvalidationBy).IsUnicode(false);

                entity.Property(e => e.DisPayAcc).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisPayOver).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisPrevHrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisProject).IsUnicode(false);

                entity.Property(e => e.DisProjectDef).IsUnicode(false);

                entity.Property(e => e.DisRecalc).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisRecalcBy).IsUnicode(false);

                entity.Property(e => e.DisSponsor).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisTransportPay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisWbs).IsUnicode(false);

                entity.Property(e => e.DisWeek).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisWehrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisWepay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisfirstAtt).HasDefaultValueSql("((0))");

                entity.Property(e => e.DteInsFromHdr).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExportedBy).IsUnicode(false);

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.UpdatedBy).IsUnicode(false);

                entity.Property(e => e.UserOpenExport).IsUnicode(false);
            });

            modelBuilder.Entity<TblDistribHdrMasterBackup15112016>(entity =>
            {
                entity.Property(e => e.ConfirmedBy).IsUnicode(false);

                entity.Property(e => e.DisBldg).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisDailyPayTax).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisDayFee).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisDeletedBy).IsUnicode(false);

                entity.Property(e => e.DisExchRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisHolHrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisHolOtpay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisHolPay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisHousingPay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisJob).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisLab).IsUnicode(false);

                entity.Property(e => e.DisNight).IsUnicode(false);

                entity.Property(e => e.DisNonTaxable).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisNorHrsday).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisPayAcc).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisPayOver).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisPrevHrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisProject).IsUnicode(false);

                entity.Property(e => e.DisProjectDef).IsUnicode(false);

                entity.Property(e => e.DisRecalc).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisRecalcBy).IsUnicode(false);

                entity.Property(e => e.DisSponsor).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisTransportPay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisWbs).IsUnicode(false);

                entity.Property(e => e.DisWeek).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisWehrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisWepay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisfirstAtt).HasDefaultValueSql("((0))");

                entity.Property(e => e.DteInsFromHdr).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExportedBy).IsUnicode(false);

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.UpdatedBy).IsUnicode(false);

                entity.Property(e => e.UserOpenExport).IsUnicode(false);
            });

            modelBuilder.Entity<TblDistribHdrVisitor>(entity =>
            {
                entity.HasKey(e => e.Seq)
                    .HasName("PK__tblDistribHdrVisitor1__7A672E12");

                entity.Property(e => e.Confirmed).HasDefaultValueSql("((0))");

                entity.Property(e => e.ConfirmedBy).IsUnicode(false);

                entity.Property(e => e.DisCompanyVisited).IsUnicode(false);

                entity.Property(e => e.DisDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisDeletedBy).IsUnicode(false);

                entity.Property(e => e.DisLab).IsUnicode(false);

                entity.Property(e => e.DisPersonVisited).IsUnicode(false);

                entity.Property(e => e.DisProject).IsUnicode(false);

                entity.Property(e => e.DisProjectDef).IsUnicode(false);

                entity.Property(e => e.DisVisitorCompany).IsUnicode(false);

                entity.Property(e => e.DisVisitorName).IsUnicode(false);

                entity.Property(e => e.DisVisitorOccup).IsUnicode(false);

                entity.Property(e => e.Exported).HasDefaultValueSql("((0))");

                entity.Property(e => e.ExportedBy).IsUnicode(false);

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy).IsUnicode(false);

                entity.Property(e => e.UserOpenExport).IsUnicode(false);

                entity.HasOne(d => d.DisLabNavigation)
                    .WithMany(p => p.TblDistribHdrVisitors)
                    .HasForeignKey(d => d.DisLab)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tblDistribHdrVisitor_tblLab");
            });

            modelBuilder.Entity<TblDivision>(entity =>
            {
                entity.HasKey(e => new { e.Proj, e.Seq });
            });

            modelBuilder.Entity<TblEmpTimeSheet>(entity =>
            {
                entity.HasKey(e => new { e.EtsProject, e.EtsProjectDef, e.EtsDate, e.EtsEmpPsc });

                entity.Property(e => e.EtsProject).IsUnicode(false);

                entity.Property(e => e.EtsProjectDef).IsUnicode(false);

                entity.Property(e => e.EtsEmpPsc).IsUnicode(false);

                entity.Property(e => e.EtsEmpName).IsUnicode(false);

                entity.Property(e => e.EtsExport).HasDefaultValueSql("((0))");

                entity.Property(e => e.EtsProjectId).HasDefaultValueSql("((0))");

                entity.Property(e => e.EtsRemark).IsUnicode(false);

                entity.Property(e => e.EtsSent).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblEmpTransferHistory>(entity =>
            {
                entity.HasKey(e => new { e.EthEmpId, e.EthDate, e.EthProjIdfrom });

                entity.Property(e => e.EthEmpId).IsUnicode(false);

                entity.Property(e => e.EthProjectDefFrom).IsUnicode(false);

                entity.Property(e => e.EthProjectDefTo).IsUnicode(false);
            });

            modelBuilder.Entity<TblEntryInsurance>(entity =>
            {
                entity.HasKey(e => new { e.Seq, e.EntryNoHdr });

                entity.Property(e => e.Seq).ValueGeneratedOnAdd();

                entity.Property(e => e.EntryNoHdr).IsUnicode(false);

                entity.Property(e => e.FileNo).IsUnicode(false);

                entity.Property(e => e.InsAttach).IsUnicode(false);

                entity.Property(e => e.InsId).IsUnicode(false);

                entity.Property(e => e.InsIssued).HasDefaultValueSql("((0))");

                entity.Property(e => e.InsNote).IsUnicode(false);

                entity.Property(e => e.Ldate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.LuserUpdate).IsUnicode(false);
            });

            modelBuilder.Entity<TblEntryMedical>(entity =>
            {
                entity.HasKey(e => new { e.Seq, e.EntryNoHdr });

                entity.Property(e => e.Seq).ValueGeneratedOnAdd();

                entity.Property(e => e.EntryNoHdr).IsUnicode(false);

                entity.Property(e => e.EntryNoHdr1).IsUnicode(false);

                entity.Property(e => e.FileNo).IsUnicode(false);

                entity.Property(e => e.Ldate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.LuserUpdate).IsUnicode(false);

                entity.Property(e => e.MedAttach).IsUnicode(false);

                entity.Property(e => e.MedIssued).HasDefaultValueSql("((0))");

                entity.Property(e => e.MedNote).IsUnicode(false);
            });

            modelBuilder.Entity<TblEntryPassport>(entity =>
            {
                entity.HasKey(e => new { e.EntryNoHdr, e.PassportId })
                    .HasName("PK_tblEntryPassport_1");

                entity.Property(e => e.EntryNoHdr).IsUnicode(false);

                entity.Property(e => e.PassportId).IsUnicode(false);

                entity.Property(e => e.FileNo).IsUnicode(false);

                entity.Property(e => e.Ldate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.LuserUpdate).IsUnicode(false);

                entity.Property(e => e.PasAttach).IsUnicode(false);

                entity.Property(e => e.PasNote).IsUnicode(false);

                entity.Property(e => e.PasReceivBy).IsUnicode(false);
            });

            modelBuilder.Entity<TblEntryPayment>(entity =>
            {
                entity.Property(e => e.EntryNoHdr).IsUnicode(false);

                entity.Property(e => e.FileNo).IsUnicode(false);

                entity.Property(e => e.Ldate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.LuserUpdate).IsUnicode(false);

                entity.Property(e => e.PayAttach).IsUnicode(false);

                entity.Property(e => e.PayBalanceAmt).HasDefaultValueSql("((0))");

                entity.Property(e => e.PayBalanceIssued).HasDefaultValueSql("((0))");

                entity.Property(e => e.PayFineAmt).HasDefaultValueSql("((0))");

                entity.Property(e => e.PayNote).IsUnicode(false);

                entity.Property(e => e.PayType).HasDefaultValueSql("((0))");

                entity.Property(e => e.PayWrkLicenseAmt).HasDefaultValueSql("((0))");

                entity.Property(e => e.PayWrkLicenseIssued).HasDefaultValueSql("((0))");

                entity.Property(e => e.PayWrkLicenseNb).IsUnicode(false);
            });

            modelBuilder.Entity<TblEntryResidence>(entity =>
            {
                entity.HasKey(e => new { e.EntryNoHdr, e.ResId });

                entity.Property(e => e.EntryNoHdr).IsUnicode(false);

                entity.Property(e => e.ResId).IsUnicode(false);

                entity.Property(e => e.FileNo).IsUnicode(false);

                entity.Property(e => e.Ldate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.LuserUpdate).IsUnicode(false);

                entity.Property(e => e.ResAttach).IsUnicode(false);

                entity.Property(e => e.ResNote).IsUnicode(false);

                entity.Property(e => e.ResReceivBy).IsUnicode(false);
            });

            modelBuilder.Entity<TblEntrySaudiCouncil>(entity =>
            {
                entity.HasKey(e => new { e.Seq, e.EntryNoHdr });

                entity.Property(e => e.Seq).ValueGeneratedOnAdd();

                entity.Property(e => e.EntryNoHdr).IsUnicode(false);

                entity.Property(e => e.FileNo).IsUnicode(false);

                entity.Property(e => e.Ldate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.LuserUpdate).IsUnicode(false);

                entity.Property(e => e.ScAttach).IsUnicode(false);

                entity.Property(e => e.ScId).IsUnicode(false);

                entity.Property(e => e.ScIssued).HasDefaultValueSql("((0))");

                entity.Property(e => e.ScNote).IsUnicode(false);
            });

            modelBuilder.Entity<TblForemanProject>(entity =>
            {
                entity.HasKey(e => new { e.ForemanId, e.ProjectId });

                entity.Property(e => e.OccupationGroup).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblForman>(entity =>
            {
                entity.HasKey(e => new { e.ForId, e.ForFileNumber });

                entity.Property(e => e.ForId).ValueGeneratedOnAdd();

                entity.Property(e => e.ForFileNumber).IsUnicode(false);

                entity.Property(e => e.ForCm).HasDefaultValueSql("((0))");

                entity.Property(e => e.ForCompany).HasDefaultValueSql("((0))");

                entity.Property(e => e.ForSection).HasDefaultValueSql("((0))");

                entity.Property(e => e.ForSiteEng).HasDefaultValueSql("((0))");

                entity.Property(e => e.Forusername).IsUnicode(false);

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUserUpdate).IsUnicode(false);
            });

            modelBuilder.Entity<TblGroup>(entity =>
            {
                entity.HasKey(e => e.GrpId)
                    .HasName("PK__tblGroups__1920BF5C");

                entity.Property(e => e.GrpId).IsUnicode(false);

                entity.Property(e => e.GrpDesc).IsUnicode(false);

                entity.Property(e => e.GrpPwd).IsUnicode(false);
            });

            modelBuilder.Entity<TblGroupsUser>(entity =>
            {
                entity.HasKey(e => new { e.GuUser, e.GuGroup })
                    .HasName("PK__tblGroupsUsers__1B0907CE");

                entity.Property(e => e.GuUser).IsUnicode(false);

                entity.Property(e => e.GuGroup).IsUnicode(false);
            });

            modelBuilder.Entity<TblHoliday>(entity =>
            {
                entity.HasKey(e => new { e.HolProjId, e.HolDate });

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblImportEntry>(entity =>
            {
                entity.HasKey(e => new { e.Seq, e.EntryNo })
                    .HasName("PK_tblImportEntry_1");

                entity.Property(e => e.Seq).ValueGeneratedOnAdd();

                entity.Property(e => e.EntryNo).IsUnicode(false);

                entity.Property(e => e.DayFee).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisDeletedBy).IsUnicode(false);

                entity.Property(e => e.Engineer).HasDefaultValueSql("((0))");

                entity.Property(e => e.EntryDateHijri).IsUnicode(false);

                entity.Property(e => e.Exported).HasDefaultValueSql("((0))");

                entity.Property(e => e.ExportedBy).IsUnicode(false);

                entity.Property(e => e.FamilyName).IsUnicode(false);

                entity.Property(e => e.FatherName).IsUnicode(false);

                entity.Property(e => e.FileNo).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.FoodAllow).HasDefaultValueSql("((0))");

                entity.Property(e => e.IqamaEndDateHijri).IsUnicode(false);

                entity.Property(e => e.Job).IsUnicode(false);

                entity.Property(e => e.Ldate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.Natio).IsUnicode(false);

                entity.Property(e => e.OtherAllow).HasDefaultValueSql("((0))");

                entity.Property(e => e.Progress).HasDefaultValueSql("((0))");

                entity.Property(e => e.ProjectCode).IsUnicode(false);

                entity.Property(e => e.Sponsor).IsUnicode(false);

                entity.Property(e => e.Staff).HasDefaultValueSql("((0))");

                entity.Property(e => e.VisaEndDateHijri).IsUnicode(false);
            });

            modelBuilder.Entity<TblImportForman>(entity =>
            {
                entity.Property(e => e.Area).IsUnicode(false);

                entity.Property(e => e.Cm).IsUnicode(false);

                entity.Property(e => e.FileName).IsUnicode(false);

                entity.Property(e => e.FileNumber).IsUnicode(false);

                entity.Property(e => e.GeneralForeman).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.ProjectDef).IsUnicode(false);

                entity.Property(e => e.ProjectId).HasDefaultValueSql("((0))");

                entity.Property(e => e.SectionEng).IsUnicode(false);

                entity.Property(e => e.SiteEng).IsUnicode(false);

                entity.Property(e => e.Type).IsUnicode(false);

                entity.Property(e => e.Wbs).IsUnicode(false);
            });

            modelBuilder.Entity<TblImportGosi>(entity =>
            {
                entity.Property(e => e.BirthDate).IsUnicode(false);

                entity.Property(e => e.FileNo)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Gosi).IsUnicode(false);

                entity.Property(e => e.Gosisponsor).IsUnicode(false);

                entity.Property(e => e.HafizaNo).IsUnicode(false);

                entity.Property(e => e.HiringDate).IsUnicode(false);

                entity.Property(e => e.IkamaNo).IsUnicode(false);

                entity.Property(e => e.NationalId).IsUnicode(false);

                entity.Property(e => e.PassportId).IsUnicode(false);

                entity.Property(e => e.ProjectCode).IsUnicode(false);

                entity.Property(e => e.Sponsor).IsUnicode(false);

                entity.Property(e => e.Staff).HasDefaultValueSql("((0))");

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblImportWb>(entity =>
            {
                entity.Property(e => e.IwDescription).IsUnicode(false);

                entity.Property(e => e.IwDivision).IsUnicode(false);

                entity.Property(e => e.IwProjDef).IsUnicode(false);

                entity.Property(e => e.IwSubDiv).IsUnicode(false);

                entity.Property(e => e.IwSubTrade).IsUnicode(false);

                entity.Property(e => e.IwTrade).IsUnicode(false);

                entity.Property(e => e.IwWbsCode).IsUnicode(false);

                entity.Property(e => e.IwWbsMap).IsUnicode(false);
            });

            modelBuilder.Entity<TblImportZoneArea>(entity =>
            {
                entity.Property(e => e.AreaName).IsUnicode(false);

                entity.Property(e => e.LocationSubArea).IsUnicode(false);

                entity.Property(e => e.ProjectDef).IsUnicode(false);

                entity.Property(e => e.ZoneName).IsUnicode(false);
            });

            modelBuilder.Entity<TblIndirectCost>(entity =>
            {
                entity.HasKey(e => new { e.IcArea, e.IcType });
            });

            modelBuilder.Entity<TblInsuranceCompany>(entity =>
            {
                entity.HasKey(e => e.InsurancecomId)
                    .HasName("PK_tblInsurancecompany");

                entity.Property(e => e.InsurancecomName).IsUnicode(false);
            });

            modelBuilder.Entity<TblInvDtl>(entity =>
            {
                entity.HasKey(e => new { e.IndHdrCode, e.IndHdrType, e.IndItem, e.IndItemDesc, e.IndUnitCost });

                entity.Property(e => e.IndItem).IsUnicode(false);

                entity.Property(e => e.IndItemDesc).IsUnicode(false);

                entity.Property(e => e.Authorization).HasDefaultValueSql("((0))");

                entity.Property(e => e.Balance).HasDefaultValueSql("((0))");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.DeletedBy).IsUnicode(false);

                entity.Property(e => e.EntryNo).IsUnicode(false);

                entity.Property(e => e.Gosi).HasDefaultValueSql("((0))");

                entity.Property(e => e.IkamaNo).IsUnicode(false);

                entity.Property(e => e.IndAddAmount).HasDefaultValueSql("((0))");

                entity.Property(e => e.IndAddHours).HasDefaultValueSql("((0))");

                entity.Property(e => e.IndDayFee).HasDefaultValueSql("((0))");

                entity.Property(e => e.IndHolOthrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.IndHolOtpay).HasDefaultValueSql("((0))");

                entity.Property(e => e.IndItemName).IsUnicode(false);

                entity.Property(e => e.IndNet).HasDefaultValueSql("((0))");

                entity.Property(e => e.IndNhours).HasDefaultValueSql("((0))");

                entity.Property(e => e.IndNhpay).HasDefaultValueSql("((0))");

                entity.Property(e => e.IndNothrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.IndNotpay).HasDefaultValueSql("((0))");

                entity.Property(e => e.IndOthours).HasDefaultValueSql("((0))");

                entity.Property(e => e.IndOthrRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.IndTaxValue).HasDefaultValueSql("((0))");

                entity.Property(e => e.IndTotAmount).HasDefaultValueSql("((0))");

                entity.Property(e => e.IndTotHours).HasDefaultValueSql("((0))");

                entity.Property(e => e.IndTotPresDays).HasDefaultValueSql("((0))");

                entity.Property(e => e.IndTotWorkers).HasDefaultValueSql("((0))");

                entity.Property(e => e.IndWeothrs).HasDefaultValueSql("((0))");

                entity.Property(e => e.IndWeotpay).HasDefaultValueSql("((0))");

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Insurance).HasDefaultValueSql("((0))");

                entity.Property(e => e.InvNhpay).HasDefaultValueSql("((0))");

                entity.Property(e => e.InvTotPresDays).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastUserUpdate).IsUnicode(false);

                entity.Property(e => e.Ldate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.MedicalClass).IsUnicode(false);

                entity.Property(e => e.PassportNo).IsUnicode(false);

                entity.Property(e => e.Ratification).HasDefaultValueSql("((0))");

                entity.Property(e => e.Saudization).HasDefaultValueSql("((0))");

                entity.Property(e => e.VisaFees).HasDefaultValueSql("((0))");

                entity.Property(e => e.WorkLicense).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IndHdr)
                    .WithMany(p => p.TblInvDtls)
                    .HasForeignKey(d => new { d.IndHdrCode, d.IndHdrType })
                    .HasConstraintName("FK_tblInvDtl_tblInvHdr");
            });

            modelBuilder.Entity<TblInvHdr>(entity =>
            {
                entity.HasKey(e => new { e.InvSeq, e.InvType })
                    .HasName("PK_tblInvoices");

                entity.Property(e => e.InvSeq).ValueGeneratedOnAdd();

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.InvApproved).HasDefaultValueSql("((0))");

                entity.Property(e => e.InvByOccupDesig).HasDefaultValueSql("((1))");

                entity.Property(e => e.InvCode).IsUnicode(false);

                entity.Property(e => e.InvDiscount).HasDefaultValueSql("((0))");

                entity.Property(e => e.InvDone).HasDefaultValueSql("((0))");

                entity.Property(e => e.InvDtlType).HasDefaultValueSql("((0))");

                entity.Property(e => e.InvOthrRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.InvOtweholHrRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.InvProjectCode).IsUnicode(false);

                entity.Property(e => e.InvTax).HasDefaultValueSql("((0))");

                entity.Property(e => e.InvconfBy).IsUnicode(false);

                entity.Property(e => e.Luser).IsUnicode(false);
            });

            modelBuilder.Entity<TblJobCode>(entity =>
            {
                entity.Property(e => e.JcSeq).ValueGeneratedNever();

                entity.Property(e => e.CodShowOnAbsent).HasDefaultValueSql("((1))");

                entity.Property(e => e.Grade).IsUnicode(false);

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.JcShowDailyRpt).IsUnicode(false);

                entity.Property(e => e.LastUserUpdate).IsUnicode(false);
            });

            modelBuilder.Entity<TblJobSkill>(entity =>
            {
                entity.HasKey(e => new { e.JsJobSeq, e.JsProjId, e.JsSponsor, e.JsSkill });

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.JsBasicRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.JsFdate).HasDefaultValueSql("('01/01/1900')");

                entity.Property(e => e.JsSkillPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.JsSponsorType).HasDefaultValueSql("((0))");

                entity.Property(e => e.JsToDate).HasDefaultValueSql("('01/01/9999')");

                entity.Property(e => e.LastUserUpdate).IsUnicode(false);
            });

            modelBuilder.Entity<TblLab>(entity =>
            {
                entity.Property(e => e.LabId).IsUnicode(false);

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LabBankAcc).IsUnicode(false);

                entity.Property(e => e.LabBb).IsUnicode(false);

                entity.Property(e => e.LabClassSalary).IsUnicode(false);

                entity.Property(e => e.LabDayFee).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabDepartment).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabEntryNo).IsUnicode(false);

                entity.Property(e => e.LabExceptionAttendance).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabFileNo).IsUnicode(false);

                entity.Property(e => e.LabFixedMonthly).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabFood).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabGosi).IsUnicode(false);

                entity.Property(e => e.LabGovernorate).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabHasAccommodation).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabHasIdcard).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabHasPhoto).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabHasTransportation).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabHidden).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabHolHrate).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabHousing).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabHrsDay).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabIdNo).IsUnicode(false);

                entity.Property(e => e.LabInActive).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabInsurancePin).IsUnicode(false);

                entity.Property(e => e.LabLegacyNo)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LabManualEntryPerm).HasDefaultValueSql("((1))");

                entity.Property(e => e.LabMealType).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabMuslim).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabNat).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabNationalNo).IsUnicode(false);

                entity.Property(e => e.LabNbPass).IsFixedLength(true);

                entity.Property(e => e.LabNew).HasDefaultValueSql("((1))");

                entity.Property(e => e.LabNotes).IsUnicode(false);

                entity.Property(e => e.LabNssfNb).IsUnicode(false);

                entity.Property(e => e.LabOthoursPermit).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabOtrate).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabPhoneAllow).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabProject).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabReportToPolice).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabResidPlace).IsUnicode(false);

                entity.Property(e => e.LabSafetyInd).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabSalType).HasDefaultValueSql("((1))");

                entity.Property(e => e.LabSemiEmp).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabShowSalRpt).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabSkilled).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabSponsor).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabTeam).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabTitle).HasDefaultValueSql("((202))");

                entity.Property(e => e.LabTransport).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabType).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabVisaLocation).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabWehrate).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabWepayType).HasDefaultValueSql("((1))");

                entity.Property(e => e.LabWithTs).HasDefaultValueSql("((1))");

                entity.Property(e => e.LabWithoutTax).HasDefaultValueSql("((0))");

                entity.Property(e => e.Labbank).IsUnicode(false);

                entity.Property(e => e.Labjob).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabprojectCode).IsUnicode(false);

                entity.Property(e => e.Labsex).HasDefaultValueSql("((196))");

                entity.Property(e => e.UpdatedBy).IsUnicode(false);
            });

            modelBuilder.Entity<TblLabAccomodation>(entity =>
            {
                entity.HasKey(e => new { e.Seq, e.AcclabId, e.AccDate })
                    .HasName("PK_tblLabAccomodation_1");

                entity.Property(e => e.Seq).ValueGeneratedOnAdd();

                entity.Property(e => e.AcclabId).IsUnicode(false);

                entity.Property(e => e.AccCamp).HasDefaultValueSql("((0))");

                entity.Property(e => e.AccDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.AccLabCompany).HasDefaultValueSql("((0))");

                entity.Property(e => e.AccLabName).IsUnicode(false);

                entity.Property(e => e.AccRoom).HasDefaultValueSql("((0))");

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblLabBlockHr>(entity =>
            {
                entity.HasKey(e => new { e.LabId, e.DateF });

                entity.Property(e => e.LabId).IsUnicode(false);

                entity.Property(e => e.BlockContra).HasDefaultValueSql("((0))");

                entity.Property(e => e.BlockOt).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblLabDocument>(entity =>
            {
                entity.HasKey(e => new { e.DocLabId, e.DocType })
                    .HasName("PK_tblLabDocuments_1");

                entity.Property(e => e.DocLabId).IsUnicode(false);

                entity.HasOne(d => d.DocLab)
                    .WithMany(p => p.TblLabDocuments)
                    .HasForeignKey(d => d.DocLabId)
                    .HasConstraintName("FK_tblLabDocuments_tblLab");
            });

            modelBuilder.Entity<TblLabDraft>(entity =>
            {
                entity.Property(e => e.LabId).IsUnicode(false);

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.LabApproved).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabBankAcc).IsUnicode(false);

                entity.Property(e => e.LabIdNo).IsUnicode(false);

                entity.Property(e => e.LabLegacyNo).IsUnicode(false);

                entity.Property(e => e.LabPhone).IsUnicode(false);
            });

            modelBuilder.Entity<TblLabHiddenWeek>(entity =>
            {
                entity.Property(e => e.LhwLabSeq).IsUnicode(false);
            });

            modelBuilder.Entity<TblLabSalHistory>(entity =>
            {
                entity.HasKey(e => new { e.LshLabSeq, e.LshDate });

                entity.Property(e => e.LshLabSeq).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LshDayFeeNew).HasDefaultValueSql("((0))");

                entity.Property(e => e.LshExceptionAttendanceNew).HasDefaultValueSql("((0))");

                entity.Property(e => e.LshExceptionAttendanceold).HasDefaultValueSql("((0))");

                entity.Property(e => e.LshFixedMonthlyNew).HasDefaultValueSql("((0))");

                entity.Property(e => e.LshFoodNew).HasDefaultValueSql("((0))");

                entity.Property(e => e.LshHardship).HasDefaultValueSql("((0))");

                entity.Property(e => e.LshHolHrateNew).HasDefaultValueSql("((0))");

                entity.Property(e => e.LshHolHrateold).HasDefaultValueSql("((0))");

                entity.Property(e => e.LshHousingNew).HasDefaultValueSql("((0))");

                entity.Property(e => e.LshLabSponsorNew).HasDefaultValueSql("((0))");

                entity.Property(e => e.LshLabSponsorOld).HasDefaultValueSql("((0))");

                entity.Property(e => e.LshOtrateNew).HasDefaultValueSql("((0))");

                entity.Property(e => e.LshOtrateold).HasDefaultValueSql("((0))");

                entity.Property(e => e.LshPhoneAllowNew).HasDefaultValueSql("((0))");

                entity.Property(e => e.LshSeq).ValueGeneratedOnAdd();

                entity.Property(e => e.LshTransportNew).HasDefaultValueSql("((0))");

                entity.Property(e => e.LshTransportOld).HasDefaultValueSql("((0))");

                entity.Property(e => e.LshVisitVisa).HasDefaultValueSql("((0))");

                entity.Property(e => e.LshWehrateNew).HasDefaultValueSql("((0))");

                entity.Property(e => e.LshWehrateold).HasDefaultValueSql("((0))");

                entity.Property(e => e.LshWepayTypeNew).HasDefaultValueSql("((1))");

                entity.Property(e => e.LshWepayTypeOld).HasDefaultValueSql("((0))");

                entity.Property(e => e.LshbPhoneAllowNew).HasDefaultValueSql("((0))");

                entity.Property(e => e.LshlabHrsDay).HasDefaultValueSql("((0))");

                entity.Property(e => e.LshlabjobNew).HasDefaultValueSql("((0))");

                entity.Property(e => e.LshlabjobOld).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.LshLabSeqNavigation)
                    .WithMany(p => p.TblLabSalHistories)
                    .HasForeignKey(d => d.LshLabSeq)
                    .HasConstraintName("FK_tblLabSalHistory_tblLab");
            });

            modelBuilder.Entity<TblLabSti>(entity =>
            {
                entity.Property(e => e.LabId).IsUnicode(false);

                entity.Property(e => e.LabSort).IsUnicode(false);
            });

            modelBuilder.Entity<TblLaborHistogram>(entity =>
            {
                entity.HasKey(e => e.Lhseq)
                    .HasName("PK__tblLabor__057A622DD3B0516D");

                entity.Property(e => e.LhDiv).IsUnicode(false);
            });

            modelBuilder.Entity<TblLaborVacation>(entity =>
            {
                entity.HasKey(e => new { e.LvLabor, e.LvFromDate, e.LvToDate });

                entity.Property(e => e.LvLabor).IsUnicode(false);
            });

            modelBuilder.Entity<TblLaborWarning>(entity =>
            {
                entity.HasKey(e => new { e.LwSeq, e.LwLabId })
                    .HasName("PK__tblLabor__0A381EA95F74D762");

                entity.Property(e => e.LwSeq).ValueGeneratedOnAdd();

                entity.Property(e => e.LwLabId).IsUnicode(false);

                entity.Property(e => e.LwProjectCode).IsUnicode(false);
            });

            modelBuilder.Entity<TblLastLogon>(entity =>
            {
                entity.HasKey(e => e.LlLogonDate)
                    .HasName("PK__tblLastL__6EBFCEA1B74C8E7D");

                entity.Property(e => e.ConCatalog).IsUnicode(false);

                entity.Property(e => e.ConPwd).IsUnicode(false);

                entity.Property(e => e.ConServer).IsUnicode(false);

                entity.Property(e => e.ConUserId).IsUnicode(false);
            });

            modelBuilder.Entity<TblLocation>(entity =>
            {
                entity.HasKey(e => new { e.LocId, e.LocProj })
                    .HasName("PK_tblLocation_1");

                entity.Property(e => e.LocId).ValueGeneratedOnAdd();

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUserUpdate).IsUnicode(false);

                entity.Property(e => e.LocArea).HasDefaultValueSql("((0))");

                entity.Property(e => e.LocBldg).HasDefaultValueSql("((0))");

                entity.Property(e => e.LocName).IsUnicode(false);

                entity.Property(e => e.LocProjDef)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LocZone).HasDefaultValueSql("((0))");

                entity.Property(e => e.Locfirst).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblLog>(entity =>
            {
                entity.Property(e => e.Descrip).IsUnicode(false);

                entity.Property(e => e.Ldate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Luser).IsFixedLength(true);
            });

            modelBuilder.Entity<TblLogChangeLaborsId>(entity =>
            {
                entity.Property(e => e.Descrip).IsUnicode(false);

                entity.Property(e => e.Ldate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Luser).IsFixedLength(true);

                entity.Property(e => e.NewFileNo).IsUnicode(false);

                entity.Property(e => e.OldFileNo).IsUnicode(false);
            });

            modelBuilder.Entity<TblLogInDateChange>(entity =>
            {
                entity.HasKey(e => e.LdcSeq)
                    .HasName("PK__tblLogIn__9BB4F434D4C9D1B8");
            });

            modelBuilder.Entity<TblManPowerSupp>(entity =>
            {
                entity.Property(e => e.InsertBy).IsUnicode(false);

                entity.Property(e => e.LastUpdateBy).IsUnicode(false);

                entity.Property(e => e.MpAbv).IsUnicode(false);

                entity.Property(e => e.MpAddress).IsUnicode(false);

                entity.Property(e => e.MpAllowContraHrs).HasDefaultValueSql("((1))");

                entity.Property(e => e.MpCo).IsUnicode(false);

                entity.Property(e => e.MpEmail).IsUnicode(false);

                entity.Property(e => e.MpFax).IsUnicode(false);

                entity.Property(e => e.MpFileMaxRange).HasDefaultValueSql("((0))");

                entity.Property(e => e.MpGosi).HasDefaultValueSql("((0))");

                entity.Property(e => e.MpLabFileAutoNo).HasDefaultValueSql("((0))");

                entity.Property(e => e.MpLogo).IsUnicode(false);

                entity.Property(e => e.MpMainCompany).IsUnicode(false);

                entity.Property(e => e.MpName).IsUnicode(false);

                entity.Property(e => e.MpNote).IsUnicode(false);

                entity.Property(e => e.MpOthrate).HasDefaultValueSql("((0))");

                entity.Property(e => e.MpPhone).IsUnicode(false);

                entity.Property(e => e.MpProject).IsUnicode(false);

                entity.Property(e => e.MpStock).HasDefaultValueSql("((0))");

                entity.Property(e => e.PermissionExcept).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblManpowerSuppSalary>(entity =>
            {
                entity.HasKey(e => new { e.MphId, e.MpClass });

                entity.Property(e => e.MpClass).IsUnicode(false);

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MpClassSalary).HasDefaultValueSql("((0))");

                entity.Property(e => e.MpClassSalaryCostSystem).HasDefaultValueSql("((0))");

                entity.Property(e => e.MpOtherAllowance).HasDefaultValueSql("((0))");

                entity.Property(e => e.MpOtherAllowanceCostSystem).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedBy).IsUnicode(false);

                entity.HasOne(d => d.Mph)
                    .WithMany(p => p.TblManpowerSuppSalaries)
                    .HasForeignKey(d => d.MphId)
                    .HasConstraintName("FK_tblManpowerSuppSalary_tblManPowerSupp");
            });

            modelBuilder.Entity<TblMapNewLabId>(entity =>
            {
                entity.Property(e => e.NewLabId).IsUnicode(false);

                entity.Property(e => e.OldLabId).IsUnicode(false);
            });

            modelBuilder.Entity<TblMapOccupation>(entity =>
            {
                entity.HasKey(e => new { e.MapTscode, e.MapPayCode, e.MapProjId });

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.MapPayDesc).IsUnicode(false);

                entity.Property(e => e.MapTsdesc).IsUnicode(false);
            });

            modelBuilder.Entity<TblMasterProject>(entity =>
            {
                entity.HasKey(e => e.MsSeq)
                    .HasName("PK__tblMasterProject__20C1E124");

                entity.Property(e => e.MsSeq).ValueGeneratedNever();

                entity.Property(e => e.MsConnection).IsUnicode(false);

                entity.Property(e => e.MsDesc).IsUnicode(false);

                entity.Property(e => e.MsServer).IsUnicode(false);
            });

            modelBuilder.Entity<TblModify>(entity =>
            {
                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.ModId).ValueGeneratedOnAdd();

                entity.Property(e => e.ModProj).IsUnicode(false);

                entity.Property(e => e.ModUsrId).IsUnicode(false);
            });

            modelBuilder.Entity<TblMonthlyAddDed>(entity =>
            {
                entity.HasKey(e => e.MadSeq)
                    .HasName("PK_tblMontlhlyAddDed");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.DeletedBy).IsUnicode(false);

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MadLabId).IsUnicode(false);

                entity.Property(e => e.MadPayrollNo).HasDefaultValueSql("((0))");

                entity.Property(e => e.MadProject).IsUnicode(false);

                entity.Property(e => e.MadProjectDef).IsUnicode(false);

                entity.Property(e => e.MadRemark).IsUnicode(false);

                entity.Property(e => e.MadType).IsUnicode(false);

                entity.Property(e => e.UpdatedBy).IsUnicode(false);

                entity.HasOne(d => d.MadLab)
                    .WithMany(p => p.TblMonthlyAddDeds)
                    .HasForeignKey(d => d.MadLabId)
                    .HasConstraintName("FK_tblMonthlyAddDed_tblLab1");
            });

            modelBuilder.Entity<TblNationality>(entity =>
            {
                entity.Property(e => e.NatSeq).ValueGeneratedNever();

                entity.Property(e => e.NatDescA).IsUnicode(false);
            });

            modelBuilder.Entity<TblNetSalary>(entity =>
            {
                entity.HasKey(e => e.Seq)
                    .HasName("PK__tblNetSa__CA1E3C880EE85AD1");

                entity.Property(e => e.Seq).IsUnicode(false);

                entity.Property(e => e.CodDescE).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LabFileNo).IsUnicode(false);

                entity.Property(e => e.LabId).IsUnicode(false);

                entity.Property(e => e.LabIdNo).IsUnicode(false);

                entity.Property(e => e.LabPhoto).IsUnicode(false);

                entity.Property(e => e.LastProjectDef).IsUnicode(false);

                entity.Property(e => e.ProjectName).IsUnicode(false);

                entity.Property(e => e.SkillGroupDesc).IsUnicode(false);

                entity.Property(e => e.Snref).ValueGeneratedOnAdd();

                entity.Property(e => e.TaxFixDyn).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);
            });

            modelBuilder.Entity<TblOccupGroup>(entity =>
            {
                entity.Property(e => e.GrpSort).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblOccupSubGroup>(entity =>
            {
                entity.HasKey(e => new { e.SgGrpSeq, e.SgSeq });

                entity.Property(e => e.SgSeq).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TblPayrollDate>(entity =>
            {
                entity.HasKey(e => e.PdPayrollNum)
                    .HasName("PK__tblPayro__A5104533F0D84501");

                entity.Property(e => e.CalcLaborDailyWithAllowances).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TblPermGrpUsr>(entity =>
            {
                entity.Property(e => e.PrmFuncId).IsUnicode(false);

                entity.Property(e => e.PrmUser).IsUnicode(false);
            });

            modelBuilder.Entity<TblPermission>(entity =>
            {
                entity.HasKey(e => new { e.PrmFuncId, e.PrmGrpUsrId, e.PrmGrpUsr })
                    .HasName("PK__tblPermissions__22AA2996");

                entity.Property(e => e.PrmFuncId).IsUnicode(false);

                entity.Property(e => e.PrmGrpUsrId).IsUnicode(false);

                entity.Property(e => e.PrmGrpUsr).IsUnicode(false);
            });

            modelBuilder.Entity<TblProjectWeek>(entity =>
            {
                entity.HasKey(e => new { e.PwkProject, e.PwkWeek })
                    .HasName("PK__tblProjectWeeks__300424B4");

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.PwkMonth).IsUnicode(false);

                entity.HasOne(d => d.PwkProjectNavigation)
                    .WithMany(p => p.TblProjectWeeks)
                    .HasForeignKey(d => d.PwkProject)
                    .HasConstraintName("FK_tblProjectWeeks_tblproject");
            });

            modelBuilder.Entity<TblReport>(entity =>
            {
                entity.Property(e => e.PermissionExcept).HasDefaultValueSql("((0))");

                entity.Property(e => e.RptDesc).IsUnicode(false);

                entity.Property(e => e.RptFixSpace).HasDefaultValueSql("((0))");

                entity.Property(e => e.RptHasColumns).HasDefaultValueSql("((0))");

                entity.Property(e => e.RptIsDynamicCol).HasDefaultValueSql("((0))");

                entity.Property(e => e.RptObject).IsUnicode(false);

                entity.Property(e => e.RptRunSsrs).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblReportsBy>(entity =>
            {
                entity.HasKey(e => new { e.ByRptSeq, e.BySeq });

                entity.Property(e => e.ByDesc).IsUnicode(false);

                entity.Property(e => e.ByRptObject).IsUnicode(false);
            });

            modelBuilder.Entity<TblReportsColumn>(entity =>
            {
                entity.HasKey(e => new { e.RcRpt, e.RcCol, e.RcJob });
            });

            modelBuilder.Entity<TblRndSel>(entity =>
            {
                entity.Property(e => e.RnsClass).IsFixedLength(true);

                entity.Property(e => e.RnsProject).IsUnicode(false);

                entity.Property(e => e.RnsSel).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblRndSelCc>(entity =>
            {
                entity.Property(e => e.RnsWbs).IsUnicode(false);

                entity.Property(e => e.RnsWbscode).IsUnicode(false);
            });

            modelBuilder.Entity<TblSerial>(entity =>
            {
                entity.HasKey(e => e.SerSeq)
                    .HasName("PK__tblSerial__24927208");

                entity.Property(e => e.SerSeq).ValueGeneratedNever();
            });

            modelBuilder.Entity<TblSickLeave>(entity =>
            {
                entity.Property(e => e.SlStatusId).ValueGeneratedNever();
            });

            modelBuilder.Entity<TblSkillPrice>(entity =>
            {
                entity.HasKey(e => new { e.SpSkillId, e.SpProjectId, e.SpFdate })
                    .HasName("PK_Table_1_1");

                entity.Property(e => e.SpFdate).HasDefaultValueSql("('01/01/1900')");

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.SpSkillPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpToDate).HasDefaultValueSql("('01/01/9999')");
            });

            modelBuilder.Entity<TblStatus>(entity =>
            {
                entity.HasKey(e => e.AsSeq)
                    .HasName("PK__tblStatus__267ABA7A");

                entity.Property(e => e.AsSeq).ValueGeneratedNever();

                entity.Property(e => e.AsAbv).IsUnicode(false);

                entity.Property(e => e.AsDesc).IsUnicode(false);

                entity.Property(e => e.AsGroup).HasDefaultValueSql("((0))");

                entity.Property(e => e.AsHasCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.AsSort).HasDefaultValueSql("((0))");

                entity.Property(e => e.AsTkuse).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TblSubConstractorSalary>(entity =>
            {
                entity.HasKey(e => new { e.SubConId, e.SubConClass });
            });

            modelBuilder.Entity<TblSubcontractor>(entity =>
            {
                entity.Property(e => e.ScSubId).ValueGeneratedNever();

                entity.Property(e => e.InsertBy).IsUnicode(false);

                entity.Property(e => e.LastUpdateBy).IsUnicode(false);

                entity.Property(e => e.ScAbv).IsUnicode(false);

                entity.Property(e => e.ScAddress).IsUnicode(false);

                entity.Property(e => e.ScCo).IsUnicode(false);

                entity.Property(e => e.ScEmail).IsUnicode(false);

                entity.Property(e => e.ScFax).IsUnicode(false);

                entity.Property(e => e.ScName).IsUnicode(false);

                entity.Property(e => e.ScNote).IsUnicode(false);

                entity.Property(e => e.ScPhone).IsUnicode(false);
            });

            modelBuilder.Entity<TblSystemDef>(entity =>
            {
                entity.Property(e => e.SdSeq).ValueGeneratedNever();

                entity.Property(e => e.SdApplyExcepOt).HasDefaultValueSql("((0))");

                entity.Property(e => e.SdApplyFridayFrac).HasComment("0: No Fraction , 1 : Apply Friday Fraction Days");

                entity.Property(e => e.SdApplyTax).HasDefaultValueSql("((0))");

                entity.Property(e => e.SdApplyWedays).HasComment("0:No ; 1: Yes");

                entity.Property(e => e.SdCompanyTitle).IsUnicode(false);

                entity.Property(e => e.SdCurrency).IsUnicode(false);

                entity.Property(e => e.SdDailyMonthly).HasDefaultValueSql("((0))");

                entity.Property(e => e.SdFixedTax).HasDefaultValueSql("((0))");

                entity.Property(e => e.SdLabSkillForce).HasDefaultValueSql("((0))");

                entity.Property(e => e.SdMultipleAttendance).HasDefaultValueSql("((0))");

                entity.Property(e => e.SdPayData).IsUnicode(false);

                entity.Property(e => e.SdPayrollCurrency).IsUnicode(false);

                entity.Property(e => e.SdPayrollMonAllow).HasDefaultValueSql("((0))");

                entity.Property(e => e.SdPhotoLocation).IsUnicode(false);

                entity.Property(e => e.SdProjectLogo).IsUnicode(false);

                entity.Property(e => e.SdUnlockPass).IsUnicode(false);

                entity.Property(e => e.SdbankName).IsUnicode(false);
            });

            modelBuilder.Entity<TblTaxMargin>(entity =>
            {
                entity.HasKey(e => new { e.TmStatus, e.TmFrom });
            });

            modelBuilder.Entity<TblTaxRate>(entity =>
            {
                entity.Property(e => e.TaxProj).ValueGeneratedNever();

                entity.Property(e => e.TaxBarCodeFile).IsUnicode(false);

                entity.Property(e => e.TaxContChochesion).HasDefaultValueSql("((0))");

                entity.Property(e => e.TaxContIndustrial).HasDefaultValueSql("((0))");

                entity.Property(e => e.TaxContLeave).HasDefaultValueSql("((0))");

                entity.Property(e => e.TaxContMedicalFixed).HasDefaultValueSql("((0))");

                entity.Property(e => e.TaxContMedicalUnion).HasDefaultValueSql("((0))");

                entity.Property(e => e.TaxContProvidentFund).HasDefaultValueSql("((0))");

                entity.Property(e => e.TaxContReduduncy).HasDefaultValueSql("((0))");

                entity.Property(e => e.TaxContSocialIns).HasDefaultValueSql("((0))");

                entity.Property(e => e.TaxContUnion).HasDefaultValueSql("((0))");

                entity.Property(e => e.TaxContUnions).HasDefaultValueSql("((0))");

                entity.Property(e => e.TaxContUnionsFund).HasDefaultValueSql("((0))");

                entity.Property(e => e.TaxDedMedicalFixed).HasDefaultValueSql("((0))");

                entity.Property(e => e.TaxDedProvidentFund).HasDefaultValueSql("((0))");

                entity.Property(e => e.TaxDedSocialIns).HasDefaultValueSql("((0))");

                entity.Property(e => e.TaxDedUnionsFund).HasDefaultValueSql("((0))");

                entity.Property(e => e.TaxRndCeil).HasDefaultValueSql("((1))");

                entity.Property(e => e.TaxServerPath).IsUnicode(false);
            });

            modelBuilder.Entity<TblTemp>(entity =>
            {
                entity.Property(e => e.Area).IsUnicode(false);

                entity.Property(e => e.Camp).IsUnicode(false);

                entity.Property(e => e.DayNo).HasDefaultValueSql("((0))");

                entity.Property(e => e.DirectCost).IsUnicode(false);

                entity.Property(e => e.DisArea).IsUnicode(false);

                entity.Property(e => e.DisHol).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisWe).HasDefaultValueSql("((0))");

                entity.Property(e => e.Div).IsUnicode(false);

                entity.Property(e => e.DivDesc).IsUnicode(false);

                entity.Property(e => e.Foreman).IsUnicode(false);

                entity.Property(e => e.HistogramJobDesc).IsUnicode(false);

                entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsWehol).HasDefaultValueSql("((0))");

                entity.Property(e => e.Job).IsUnicode(false);

                entity.Property(e => e.LabId).IsUnicode(false);

                entity.Property(e => e.LabName).IsUnicode(false);

                entity.Property(e => e.MthName).IsUnicode(false);

                entity.Property(e => e.Nationality).IsUnicode(false);

                entity.Property(e => e.ProjDef).IsUnicode(false);

                entity.Property(e => e.Sponser).IsUnicode(false);

                entity.Property(e => e.Status).IsUnicode(false);

                entity.Property(e => e.SubDiv).IsUnicode(false);

                entity.Property(e => e.SubDivDesc).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.Property(e => e.Wbs).IsUnicode(false);

                entity.Property(e => e.WbsDesc).IsUnicode(false);

                entity.Property(e => e.Zone).IsUnicode(false);
            });

            modelBuilder.Entity<TblTempImportEntry>(entity =>
            {
                entity.Property(e => e.DayFee).HasDefaultValueSql("((0))");

                entity.Property(e => e.FamilyName).IsUnicode(false);

                entity.Property(e => e.FatherName).IsUnicode(false);

                entity.Property(e => e.FileNo).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.FoodAllow).HasDefaultValueSql("((0))");

                entity.Property(e => e.Ldate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.Natio).IsUnicode(false);

                entity.Property(e => e.Occupation).IsUnicode(false);

                entity.Property(e => e.OtherAllow).HasDefaultValueSql("((0))");

                entity.Property(e => e.ProjectCode).IsUnicode(false);

                entity.Property(e => e.Sponsor).IsUnicode(false);

                entity.Property(e => e.Staff).HasDefaultValueSql("((0))");

                entity.Property(e => e.تاريخإنتهاءالإقامة).IsUnicode(false);

                entity.Property(e => e.تاريخإنتهاءالتأشيرة).IsUnicode(false);

                entity.Property(e => e.تاريخالدخول).IsUnicode(false);

                entity.Property(e => e.رقمدخولالحدود).IsUnicode(false);
            });

            modelBuilder.Entity<TblTempLaborContract>(entity =>
            {
                entity.Property(e => e.LabId).IsUnicode(false);

                entity.Property(e => e.LabContact).IsUnicode(false);

                entity.Property(e => e.LabDeg).IsUnicode(false);

                entity.Property(e => e.LabEndRef).IsUnicode(false);

                entity.Property(e => e.LabIdNo).IsUnicode(false);

                entity.Property(e => e.LabJobE).IsUnicode(false);

                entity.Property(e => e.LabNatE).IsUnicode(false);

                entity.Property(e => e.LabNbPass).IsFixedLength(true);

                entity.Property(e => e.LabWrkRef).IsUnicode(false);

                entity.Property(e => e.PrjName).IsUnicode(false);
            });

            modelBuilder.Entity<TblTempReport>(entity =>
            {
                entity.Property(e => e.AbsentFrom).IsUnicode(false);

                entity.Property(e => e.Area).IsUnicode(false);

                entity.Property(e => e.ClassSalary).IsUnicode(false);

                entity.Property(e => e.DisLab).IsUnicode(false);

                entity.Property(e => e.DisNight).IsUnicode(false);

                entity.Property(e => e.DisOccupGrp).HasDefaultValueSql("((0))");

                entity.Property(e => e.DisProj).IsUnicode(false);

                entity.Property(e => e.DisProject).IsUnicode(false);

                entity.Property(e => e.DisProjectDef).IsUnicode(false);

                entity.Property(e => e.DsgDesc).IsUnicode(false);

                entity.Property(e => e.Forman).IsUnicode(false);

                entity.Property(e => e.Grp).IsUnicode(false);

                entity.Property(e => e.GrpSort).HasDefaultValueSql("((0))");

                entity.Property(e => e.HistogramCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.HistogramJobDesc).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Job).IsUnicode(false);

                entity.Property(e => e.LabId).IsUnicode(false);

                entity.Property(e => e.LaborsCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.MainSponsor).IsUnicode(false);

                entity.Property(e => e.Monthly).HasDefaultValueSql("((0))");

                entity.Property(e => e.Nationality).IsUnicode(false);

                entity.Property(e => e.ProjectCountry).IsUnicode(false);

                entity.Property(e => e.ProjectName).IsUnicode(false);

                entity.Property(e => e.Sponsor).IsUnicode(false);

                entity.Property(e => e.StatusDesc).IsUnicode(false);

                entity.Property(e => e.SubGrp).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.Property(e => e.WorkTypeDesc).IsUnicode(false);
            });

            modelBuilder.Entity<TblTimeSchedule>(entity =>
            {
                entity.HasKey(e => new { e.TsdProjId, e.TsdProjectDef, e.Tsdnight, e.TsdDayNumber });

                entity.Property(e => e.TsdProjectDef).IsUnicode(false);

                entity.Property(e => e.Tsdnight).IsUnicode(false);

                entity.Property(e => e.TsdWeekEnd).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblTimeScheduleExpDtl>(entity =>
            {
                entity.HasKey(e => new { e.TsedHdrSeq, e.Tsednight, e.TsedDayNumber });

                entity.Property(e => e.Tsednight).IsUnicode(false);

                entity.HasOne(d => d.TsedHdrSeqNavigation)
                    .WithMany(p => p.TblTimeScheduleExpDtls)
                    .HasForeignKey(d => d.TsedHdrSeq)
                    .HasConstraintName("FK_tblTimeScheduleExpDtl_tblTimeScheduleExpHdr");
            });

            modelBuilder.Entity<TblTimeScheduleExpHdr>(entity =>
            {
                entity.Property(e => e.TsehDesc).IsUnicode(false);

                entity.Property(e => e.TsehProjectDef).IsUnicode(false);
            });

            modelBuilder.Entity<TblTimeScheduleHdr>(entity =>
            {
                entity.HasKey(e => new { e.TshProjId, e.TshProjectDef });

                entity.Property(e => e.TshProjectDef).IsUnicode(false);
            });

            modelBuilder.Entity<TblTmpImportGosi>(entity =>
            {
                entity.Property(e => e.FileNo)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ProjectCode).IsUnicode(false);

                entity.Property(e => e.Sponsor).IsUnicode(false);

                entity.Property(e => e.Staff).HasDefaultValueSql("((0))");

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");

                entity.Property(e => e.تاريحالإلتحاق).IsUnicode(false);

                entity.Property(e => e.تاريخالميلاد).IsUnicode(false);

                entity.Property(e => e.رقمالاقامة).IsUnicode(false);

                entity.Property(e => e.رقمالجواز).IsUnicode(false);

                entity.Property(e => e.رقمالحفيظة).IsUnicode(false);

                entity.Property(e => e.رقمالمشترك).IsUnicode(false);

                entity.Property(e => e.رقمالهويةالوطنية).IsUnicode(false);
            });

            modelBuilder.Entity<TblTrade>(entity =>
            {
                entity.Property(e => e.TrSeq).ValueGeneratedNever();

                entity.Property(e => e.TrDesc).IsUnicode(false);

                entity.Property(e => e.TrProject).IsUnicode(false);
            });

            modelBuilder.Entity<TblTransfPassport>(entity =>
            {
                entity.HasKey(e => new { e.PassportId, e.PasReceivBy, e.PasReceivDate })
                    .HasName("PK_tblTransfPassport_1");

                entity.Property(e => e.PassportId).IsUnicode(false);

                entity.Property(e => e.PasReceivBy).IsUnicode(false);

                entity.Property(e => e.Ldate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.LuserUpdate).IsUnicode(false);

                entity.Property(e => e.PasNote).IsUnicode(false);

                entity.Property(e => e.PasTransfTo).IsUnicode(false);
            });

            modelBuilder.Entity<TblTransfResidence>(entity =>
            {
                entity.HasKey(e => new { e.ResId, e.ResReceivBy, e.ResReceivDate })
                    .HasName("PK_Table_1");

                entity.Property(e => e.ResId).IsUnicode(false);

                entity.Property(e => e.ResReceivBy).IsUnicode(false);

                entity.Property(e => e.Ldate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.LuserUpdate).IsUnicode(false);

                entity.Property(e => e.ResNote).IsUnicode(false);

                entity.Property(e => e.ResTransfTo).IsUnicode(false);
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UsrId)
                    .HasName("PK__tblUsers__286302EC");

                entity.Property(e => e.UsrId).IsUnicode(false);

                entity.Property(e => e.AllowAccess).HasDefaultValueSql("((0))");

                entity.Property(e => e.UsrDesc).IsUnicode(false);

                entity.Property(e => e.UsrEmail).IsUnicode(false);

                entity.Property(e => e.UsrPwd).IsUnicode(false);

                entity.Property(e => e.UsrSignature).IsUnicode(false);
            });

            modelBuilder.Entity<TblUserCounter>(entity =>
            {
                entity.HasKey(e => new { e.UcProj, e.UcStId, e.UcUserKey, e.UcType });
            });

            modelBuilder.Entity<TblUsersProject>(entity =>
            {
                entity.HasKey(e => new { e.UpUserId, e.UpProject })
                    .HasName("PK__tblUsers__4DCF3A536C5F01FE");

                entity.Property(e => e.UpUserId).IsUnicode(false);
            });

            modelBuilder.Entity<TblUsersProjectsDef>(entity =>
            {
                entity.HasKey(e => new { e.UpdUserId, e.UpdProject, e.UpdProjCode });

                entity.Property(e => e.UpdUserId).IsUnicode(false);

                entity.Property(e => e.UpdProjCode).IsUnicode(false);

                entity.HasOne(d => d.Upd)
                    .WithMany(p => p.TblUsersProjectsDefs)
                    .HasForeignKey(d => new { d.UpdUserId, d.UpdProject })
                    .HasConstraintName("FK_tblUsersProjectsDef_tblUsersProjects");
            });

            modelBuilder.Entity<TblUsersZone>(entity =>
            {
                entity.HasKey(e => new { e.UzUserId, e.UzZoneId, e.UzProjId });

                entity.Property(e => e.UzZoneId).IsUnicode(false);
            });

            modelBuilder.Entity<TblVisitor>(entity =>
            {
                entity.HasKey(e => e.LabId)
                    .HasName("PK_Visitor");

                entity.Property(e => e.LabId).IsUnicode(false);

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LabCompany).IsUnicode(false);

                entity.Property(e => e.LabIdNo).IsUnicode(false);

                entity.Property(e => e.LabInActive).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabNbPass).IsFixedLength(true);

                entity.Property(e => e.LabOccupation).IsUnicode(false);

                entity.Property(e => e.LabPhone).IsUnicode(false);
            });

            modelBuilder.Entity<TblWb>(entity =>
            {
                entity.HasKey(e => new { e.ProjId, e.WbsCode, e.WbsProject, e.WbsLevel });

                entity.Property(e => e.WbsCode).IsUnicode(false);

                entity.Property(e => e.WbsProject).IsUnicode(false);

                entity.Property(e => e.Div).IsUnicode(false);

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUserUpdate).IsUnicode(false);

                entity.Property(e => e.RelatedArea).IsUnicode(false);

                entity.Property(e => e.SubDiv).IsUnicode(false);

                entity.Property(e => e.SubTrade).IsUnicode(false);

                entity.Property(e => e.Trade).IsUnicode(false);

                entity.Property(e => e.Unit).IsUnicode(false);

                entity.Property(e => e.Wbs).IsUnicode(false);

                entity.Property(e => e.WbsDesc).IsUnicode(false);

                entity.Property(e => e.WbsHidden).HasDefaultValueSql("((0))");

                entity.Property(e => e.WbsType)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('W')");

                entity.Property(e => e.Wbsmap).IsUnicode(false);

                entity.Property(e => e.Wbsusage).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblWbsArea>(entity =>
            {
                entity.HasKey(e => new { e.WaWbs, e.WaArea, e.WaProj });

                entity.Property(e => e.WaWbs).IsUnicode(false);

                entity.Property(e => e.WaArea).IsUnicode(false);
            });

            modelBuilder.Entity<TblWbsMap>(entity =>
            {
                entity.HasKey(e => new { e.WbsTk, e.WbsSap });

                entity.Property(e => e.WbsTk).IsUnicode(false);

                entity.Property(e => e.WbsSap).IsUnicode(false);
            });

            modelBuilder.Entity<TblZone>(entity =>
            {
                entity.HasKey(e => new { e.ZonId, e.ZonProj });

                entity.Property(e => e.ZonId).ValueGeneratedOnAdd();

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.ZonName).IsUnicode(false);
            });

            modelBuilder.Entity<Tblproject>(entity =>
            {
                entity.HasKey(e => e.Seq)
                    .HasName("PK__tblproject__2E1BDC42");

                entity.Property(e => e.Seq).ValueGeneratedNever();

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUserUpdate).IsUnicode(false);

                entity.Property(e => e.Luser).IsUnicode(false);

                entity.Property(e => e.PrTimeShedNightFrom).IsUnicode(false);

                entity.Property(e => e.PrjAbv).IsUnicode(false);

                entity.Property(e => e.PrjAllowContra).HasDefaultValueSql("((1))");

                entity.Property(e => e.PrjAllowOt).HasDefaultValueSql("((1))");

                entity.Property(e => e.PrjAllowOtHol).HasDefaultValueSql("((1))");

                entity.Property(e => e.PrjAllowOtWe).HasDefaultValueSql("((0))");

                entity.Property(e => e.PrjCode).IsUnicode(false);

                entity.Property(e => e.PrjCostDatabase).IsUnicode(false);

                entity.Property(e => e.PrjCostDbEmailTemplate).IsUnicode(false);

                entity.Property(e => e.PrjCountry).IsUnicode(false);

                entity.Property(e => e.PrjDataCollectorSaveFile).IsUnicode(false);

                entity.Property(e => e.PrjFile).IsUnicode(false);

                entity.Property(e => e.PrjHasBldgs).HasDefaultValueSql("((0))");

                entity.Property(e => e.PrjHasDesig).HasDefaultValueSql("((0))");

                entity.Property(e => e.PrjHasLocation).HasDefaultValueSql("((0))");

                entity.Property(e => e.PrjLogoPath).IsUnicode(false);

                entity.Property(e => e.PrjName).IsUnicode(false);

                entity.Property(e => e.PrjNewTsVers).HasDefaultValueSql("((1))");

                entity.Property(e => e.PrjPath).IsUnicode(false);

                entity.Property(e => e.PrjProjectType).HasDefaultValueSql("((0))");

                entity.Property(e => e.PrjSapcode).IsUnicode(false);

                entity.Property(e => e.PrjTitle).IsUnicode(false);
            });

            modelBuilder.Entity<Tblquantitysubcontractor>(entity =>
            {
                entity.HasKey(e => new { e.SubconId, e.Qtydate, e.Qtywbs });

                entity.Property(e => e.Qtywbs).IsUnicode(false);

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.Qtyproject).IsUnicode(false);
            });

            modelBuilder.Entity<TempAyappreport>(entity =>
            {
                entity.Property(e => e.Area).IsUnicode(false);

                entity.Property(e => e.AyappDesignation).IsUnicode(false);

                entity.Property(e => e.DayNight).IsUnicode(false);

                entity.Property(e => e.Descrip).IsUnicode(false);

                entity.Property(e => e.DisForman).IsUnicode(false);

                entity.Property(e => e.DisLab).IsUnicode(false);

                entity.Property(e => e.DisProj).IsUnicode(false);

                entity.Property(e => e.Div).IsUnicode(false);

                entity.Property(e => e.DivSubDiv).IsUnicode(false);

                entity.Property(e => e.Forman).IsUnicode(false);

                entity.Property(e => e.ProjectDef).IsUnicode(false);

                entity.Property(e => e.SectionEng).IsUnicode(false);

                entity.Property(e => e.SiteEng).IsUnicode(false);

                entity.Property(e => e.Sponsor).IsUnicode(false);

                entity.Property(e => e.TsDesignation).IsUnicode(false);

                entity.Property(e => e.Username).IsUnicode(false);

                entity.Property(e => e.Wbs).IsUnicode(false);

                entity.Property(e => e.Zone).IsUnicode(false);
            });

            modelBuilder.Entity<TempAyappreport1>(entity =>
            {
                entity.Property(e => e.Area).IsUnicode(false);

                entity.Property(e => e.AyappDesignation).IsUnicode(false);

                entity.Property(e => e.DayNight).IsUnicode(false);

                entity.Property(e => e.Descrip).IsUnicode(false);

                entity.Property(e => e.DisForman).IsUnicode(false);

                entity.Property(e => e.DisLab).IsUnicode(false);

                entity.Property(e => e.DisProj).IsUnicode(false);

                entity.Property(e => e.Div).IsUnicode(false);

                entity.Property(e => e.DivSubDiv).IsUnicode(false);

                entity.Property(e => e.Forman).IsUnicode(false);

                entity.Property(e => e.ProjectDef).IsUnicode(false);

                entity.Property(e => e.SectionEng).IsUnicode(false);

                entity.Property(e => e.SiteEng).IsUnicode(false);

                entity.Property(e => e.Sponsor).IsUnicode(false);

                entity.Property(e => e.TsDesignation).IsUnicode(false);

                entity.Property(e => e.Wbs).IsUnicode(false);

                entity.Property(e => e.Zone).IsUnicode(false);
            });

            modelBuilder.Entity<TempAyappreportsDiv01>(entity =>
            {
                entity.Property(e => e.Area).IsUnicode(false);

                entity.Property(e => e.AyappDesignation).IsUnicode(false);

                entity.Property(e => e.DayNight).IsUnicode(false);

                entity.Property(e => e.Descrip).IsUnicode(false);

                entity.Property(e => e.DisForman).IsUnicode(false);

                entity.Property(e => e.DisLab).IsUnicode(false);

                entity.Property(e => e.DisProj).IsUnicode(false);

                entity.Property(e => e.Div).IsUnicode(false);

                entity.Property(e => e.DivSubDiv).IsUnicode(false);

                entity.Property(e => e.Forman).IsUnicode(false);

                entity.Property(e => e.ProjectDef).IsUnicode(false);

                entity.Property(e => e.SectionEng).IsUnicode(false);

                entity.Property(e => e.SiteEng).IsUnicode(false);

                entity.Property(e => e.Sponsor).IsUnicode(false);

                entity.Property(e => e.TsDesignation).IsUnicode(false);

                entity.Property(e => e.Username).IsUnicode(false);

                entity.Property(e => e.Wbs).IsUnicode(false);

                entity.Property(e => e.Zone).IsUnicode(false);
            });

            modelBuilder.Entity<TempAyappreportsDiv011>(entity =>
            {
                entity.Property(e => e.Area).IsUnicode(false);

                entity.Property(e => e.AyappDesignation).IsUnicode(false);

                entity.Property(e => e.DayNight).IsUnicode(false);

                entity.Property(e => e.Descrip).IsUnicode(false);

                entity.Property(e => e.DisForman).IsUnicode(false);

                entity.Property(e => e.DisLab).IsUnicode(false);

                entity.Property(e => e.DisProj).IsUnicode(false);

                entity.Property(e => e.Div).IsUnicode(false);

                entity.Property(e => e.DivSubDiv).IsUnicode(false);

                entity.Property(e => e.Forman).IsUnicode(false);

                entity.Property(e => e.ProjectDef).IsUnicode(false);

                entity.Property(e => e.SectionEng).IsUnicode(false);

                entity.Property(e => e.SiteEng).IsUnicode(false);

                entity.Property(e => e.Sponsor).IsUnicode(false);

                entity.Property(e => e.TsDesignation).IsUnicode(false);

                entity.Property(e => e.Wbs).IsUnicode(false);

                entity.Property(e => e.Zone).IsUnicode(false);
            });

            modelBuilder.Entity<TempCorrectWb>(entity =>
            {
                entity.Property(e => e.Area).IsUnicode(false);

                entity.Property(e => e.CorrectWbs).IsUnicode(false);

                entity.Property(e => e.FileNo).IsUnicode(false);

                entity.Property(e => e.Foreman).IsUnicode(false);

                entity.Property(e => e.InsertedBy).IsUnicode(false);

                entity.Property(e => e.NewArea).IsUnicode(false);

                entity.Property(e => e.NewProjectDef).IsUnicode(false);

                entity.Property(e => e.ProjectDef).IsUnicode(false);

                entity.Property(e => e.WrongWbs).IsUnicode(false);
            });

            modelBuilder.Entity<TempImport>(entity =>
            {
                entity.Property(e => e.LabAccbranch).IsUnicode(false);

                entity.Property(e => e.LabAccnew).IsUnicode(false);

                entity.Property(e => e.Labbank).IsUnicode(false);

                entity.Property(e => e.Labid).IsUnicode(false);
            });

            modelBuilder.Entity<TempImportMonthlyAddDed>(entity =>
            {
                entity.Property(e => e.AddDedType).IsUnicode(false);

                entity.Property(e => e.LaborId).IsUnicode(false);

                entity.Property(e => e.Project).IsUnicode(false);

                entity.Property(e => e.ProjectDef).IsUnicode(false);

                entity.Property(e => e.Remark).IsUnicode(false);
            });

            modelBuilder.Entity<Tempwb>(entity =>
            {
                entity.Property(e => e.Levelsss).IsUnicode(false);

                entity.Property(e => e.Wbs).IsUnicode(false);

                entity.Property(e => e.Wbsdesc).IsUnicode(false);
            });

            modelBuilder.Entity<Tmp>(entity =>
            {
                entity.Property(e => e.DescripId).IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Dte).IsUnicode(false);

                entity.Property(e => e.Id).IsUnicode(false);

                entity.Property(e => e.Value)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TmpChangeSalary>(entity =>
            {
                entity.Property(e => e.ExcepDailyWorkingHrs).IsUnicode(false);

                entity.Property(e => e.ExcepOtHrRate).IsUnicode(false);

                entity.Property(e => e.HolHrRate).IsUnicode(false);

                entity.Property(e => e.LaborId).IsUnicode(false);

                entity.Property(e => e.Occupation).IsUnicode(false);

                entity.Property(e => e.OtHrRate).IsUnicode(false);

                entity.Property(e => e.Sponsor).IsUnicode(false);

                entity.Property(e => e.WeHrRate).IsUnicode(false);

                entity.Property(e => e.WePayType).IsUnicode(false);
            });

            modelBuilder.Entity<TmpExportEntry>(entity =>
            {
                entity.Property(e => e.EntryNo).IsUnicode(false);

                entity.Property(e => e.FamilyName).IsUnicode(false);

                entity.Property(e => e.FatherName).IsUnicode(false);

                entity.Property(e => e.FileNo).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.IqamaNo).IsUnicode(false);

                entity.Property(e => e.Location).IsUnicode(false);

                entity.Property(e => e.Nationality).IsUnicode(false);

                entity.Property(e => e.PassportNumber).IsUnicode(false);

                entity.Property(e => e.Sponsor).IsUnicode(false);
            });

            modelBuilder.Entity<TmpImportAttendance>(entity =>
            {
                entity.Property(e => e.Area).IsUnicode(false);

                entity.Property(e => e.Id).IsUnicode(false);

                entity.Property(e => e.ProjectCode).IsUnicode(false);

                entity.Property(e => e.Team).IsUnicode(false);

                entity.Property(e => e.Wbs).IsUnicode(false);
            });

            modelBuilder.Entity<TmpImportLabor>(entity =>
            {
                entity.Property(e => e.FileNo).IsUnicode(false);

                entity.Property(e => e.Governorate).IsUnicode(false);

                entity.Property(e => e.LabResidPlace).IsUnicode(false);

                entity.Property(e => e.PlaceOfBirth).IsUnicode(false);

                entity.Property(e => e.PlaceOfResidence).IsUnicode(false);
            });

            modelBuilder.Entity<TmpImportLaborHistogram>(entity =>
            {
                entity.HasKey(e => e.Seq)
                    .HasName("PK__tmpImpor__DDDFBCBE69FB02A5");

                entity.Property(e => e.Div).IsUnicode(false);

                entity.Property(e => e.Occupation).IsUnicode(false);
            });

            modelBuilder.Entity<TmpJoinTrsfStaffWorker>(entity =>
            {
                entity.Property(e => e.Id).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.ProjCode).IsUnicode(false);

                entity.Property(e => e.ProjectDef).IsUnicode(false);

                entity.Property(e => e.Psc).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);
            });

            modelBuilder.Entity<TmpLabAccomodation>(entity =>
            {
                entity.Property(e => e.Camp).IsUnicode(false);

                entity.Property(e => e.LabCompany).IsUnicode(false);

                entity.Property(e => e.LabIdSubc).IsUnicode(false);

                entity.Property(e => e.LabName).IsUnicode(false);

                entity.Property(e => e.Room).IsUnicode(false);
            });

            modelBuilder.Entity<TmpMissingDatum>(entity =>
            {
                entity.Property(e => e.CodDescE).IsUnicode(false);

                entity.Property(e => e.LabId).IsUnicode(false);

                entity.Property(e => e.Labname).IsUnicode(false);

                entity.Property(e => e.ProjectName).IsUnicode(false);

                entity.Property(e => e.Type).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.Property(e => e.Wbs).IsUnicode(false);
            });

            modelBuilder.Entity<TmpReplaceWb>(entity =>
            {
                entity.Property(e => e.Area).IsUnicode(false);

                entity.Property(e => e.FileNo).IsUnicode(false);

                entity.Property(e => e.Foreman).IsUnicode(false);

                entity.Property(e => e.NewArea).IsUnicode(false);

                entity.Property(e => e.NewFileNo).IsUnicode(false);

                entity.Property(e => e.NewProjectCode).IsUnicode(false);

                entity.Property(e => e.NewSponsor).IsUnicode(false);

                entity.Property(e => e.OldSponsor).IsUnicode(false);
            });

            modelBuilder.Entity<TmpResignedLab>(entity =>
            {
                entity.Property(e => e.FileNo).IsUnicode(false);
            });

            modelBuilder.Entity<TmpStaff>(entity =>
            {
                entity.Property(e => e.AbsentFrom).IsUnicode(false);

                entity.Property(e => e.Daily).HasDefaultValueSql("((0))");

                entity.Property(e => e.Dn).IsUnicode(false);

                entity.Property(e => e.EmpId).IsUnicode(false);

                entity.Property(e => e.EmpName).IsUnicode(false);

                entity.Property(e => e.Grp).IsUnicode(false);

                entity.Property(e => e.GrpSort).HasDefaultValueSql("((0))");

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProjectDef).IsUnicode(false);

                entity.Property(e => e.Psc).IsUnicode(false);

                entity.Property(e => e.Sponsor).IsUnicode(false);

                entity.Property(e => e.StatusDesc).IsUnicode(false);

                entity.Property(e => e.SubGrp).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.Property(e => e.WorkTypeDesc).IsUnicode(false);

                entity.Property(e => e.Worktype).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TrasferEmp>(entity =>
            {
                entity.HasKey(e => new { e.EmpId, e.TransDate, e.FromProj, e.ToProj });

                entity.Property(e => e.EmpId).IsUnicode(false);

                entity.Property(e => e.FromProj).IsUnicode(false);

                entity.Property(e => e.ToProj).IsUnicode(false);
            });

            modelBuilder.Entity<ViewLaborDailyAttCount>(entity =>
            {
                entity.ToView("viewLaborDailyAttCount");

                entity.Property(e => e.DisLab).IsUnicode(false);
            });

            modelBuilder.Entity<ViewLaborDailyAttCountHistory>(entity =>
            {
                entity.ToView("viewLaborDailyAttCountHistory");

                entity.Property(e => e.DisLab).IsUnicode(false);
            });

            modelBuilder.Entity<ViewLaborMaxDate>(entity =>
            {
                entity.ToView("viewLaborMaxDate");

                entity.Property(e => e.LabSeq).IsUnicode(false);
            });

            modelBuilder.Entity<ViewLaborsAttDay>(entity =>
            {
                entity.ToView("ViewLaborsAttDays");

                entity.Property(e => e.DisLab).IsUnicode(false);

                entity.Property(e => e.DisProject).IsUnicode(false);

                entity.Property(e => e.DisProjectDef).IsUnicode(false);
            });

            modelBuilder.Entity<ViewServerDate>(entity =>
            {
                entity.ToView("viewServerDate");

                entity.Property(e => e.Today).IsUnicode(false);
            });

            modelBuilder.Entity<VwGetPrevHoursFirstAttendance>(entity =>
            {
                entity.ToView("vwGetPrevHoursFirstAttendance");

                entity.Property(e => e.DisProjectDef).IsUnicode(false);

                entity.Property(e => e.PrevLab).IsUnicode(false);
            });

            modelBuilder.Entity<VwIsFirstAttendance>(entity =>
            {
                entity.ToView("vwIsFirstAttendance");

                entity.Property(e => e.DisLab).IsUnicode(false);

                entity.Property(e => e.DisProjectDef).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
