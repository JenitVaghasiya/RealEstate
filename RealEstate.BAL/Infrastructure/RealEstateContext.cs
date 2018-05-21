namespace RealEstate.BAL.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using NJsonSchema.Annotations;

    public class RealEstateContext : TransactionalDbContext<RealEstateContext>
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IUserContext _userContext;

        public RealEstateContext(
            DbContextOptions<ProactContext> options,
            IDomainEventDispatcher domainEventDispatcher,
            IUserContext userContext)
            : base(options)
        {
            _domainEventDispatcher = domainEventDispatcher;
            _userContext = userContext;
        }

        public enum FilterKeys
        {
            ExcludeRejectedTimesheetEntries,
            ExcludeNonRejectedTimesheetEntries
        }

        public DbSet<AdjustedTimesheetEntry> AdjustedTimesheetEntries { get; set; }

        public DbSet<AllocationChangeRequest> AllocationChangeRequests { get; set; }

        public IQueryable<TimesheetEntry> AllTimesheetEntries
        {
            get
            {
                return TimesheetEntries.IgnoreQueryFilters();
            }
        }

        public DbSet<BusinessUnit> BusinessUnits { get; set; }

        public DbSet<ContractorCostRate> ContractorCostRates { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<CustomChargeRate> CustomChargeRates { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<DefaultChargeRate> DefaultChargeRates { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<ExpenseType> ExpenseTypes { get; set; }

        public DbSet<InvoiceTemplateRule> InvoiceTemplateRules { get; set; }

        public DbSet<OrderLine> OrderLines { get; set; }

        public DbSet<ResourceLineManager> LineManagers { get; set; }

        public DbSet<Office> Offices { get; set; }

        public DbSet<OrderSummary> OrderSummaries { get; set; }

        public DbSet<ParentProject> ParentProjects { get; set; }

        public DbSet<ProjectActivity> ProjectActivities { get; set; }

        public DbSet<ActivitySummaryCode> ActivitySummaryCodes { get; set; }

        public DbSet<ProjectAllocation> ProjectAllocations { get; set; }

        public DbSet<ProjectAllocationSkill> ProjectAllocationSkills { get; set; }

        public DbSet<ProjectForecastHistory> ProjectForecastHistorys { get; set; }

        public DbSet<ProjectInvoiceTemplate> ProjectInvoiceTemplates { get; set; }

        public DbSet<ProjectManager> ProjectManagers { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectType> ProjectTypes { get; set; }

        public DbSet<CostRateHeader> CostRateHeaders { get; set; }

        public DbSet<CostRate> CostRates { get; set; }

        public IQueryable<TimesheetEntry> RejectedTimesheetEntries
        {
            get
            {
                return TimesheetEntries
                    .IgnoreQueryFilters()
                    .Where(te => te.IsRejected);
            }
        }

        public DbSet<ResourceQualification> ResourceQualifications { get; set; }

        public DbSet<ResourceRequestPartA> ResourceRequestPartAs { get; set; }

        public DbSet<ResourceRequestPartBRequiredSkill> ResourceRequestPartBRequiredSkills { get; set; }

        public DbSet<ResourceRequestPartB> ResourceRequestPartBs { get; set; }

        public DbSet<ResourceRole> ResourceRoles { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<ResourceSecurityClearance> ResourceSecurityClearances { get; set; }

        public DbSet<ResourceSkillDraft> ResourceSkillDrafts { get; set; }

        public DbSet<ResourceSkill> ResourceSkills { get; set; }

        public DbSet<RoleCategory> RoleCategories { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<SalesPerson> SalesPersons { get; set; }

        public DbSet<SkillCategory> SkillCategories { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<TimesheetEntry> TimesheetEntries { get; set; }

        public DbSet<TimesheetSubmission> TimesheetSubmissions { get; set; }

        public DbSet<TimesheetPeriod> TimesheetPeriods { get; set; }

        public DbSet<TimesheetActivityBookmark> TimesheetActivityBookmarks { get; set; }

        public DbSet<TimesheetEntryInvoiceStatusHistory> TimesheetEntryInvoiceStatusHistories { get; set; }

        public DbSet<UnsuccessfulResourceRequest> UnsuccessfulResourceRequests { get; set; }

        public DbSet<UserResource> UserResources { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<WorkingLocation> WorkingLocations { get; set; }

        public DbSet<InvoiceStatement> InvoiceStatements { get; set; }

        public DbSet<MonthlyRevenueForecast> MonthlyRevenueForecasts { get; set; }

        public DbSet<YearlyRevenueForecast> YearlyRevenueForecasts { get; set; }

        public DbSet<FinancialYear> FinancialYears { get; set; }

        public DbSet<ResourceAbsence> ResourceAbsences { get; set; }

        public DbSet<InvoiceStatementHistory> InvoiceStatementHistorys { get; set; }

        public DbSet<InvoiceEntryFinanceComment> InvoiceEntryFinanceComments { get; set; }

        public DbSet<ExpenseStatusHistory> ExpenseStatusHistories { get; set; }

        public DbSet<ProactSyncEvent> SyncEvents { get; set; }

        public DbSet<SyncRun> SyncRuns { get; set; }

        public DbSet<SyncService> SyncServices { get; set; }

        public DbSet<FairsailAdditionalEmployee> FairsailAdditionalEmployees { get; set; }

        public DbSet<TimesheetEntryCorrection> TimesheetEntryCorrections { get; set; }

        public DbSet<FinanceNote> FinanceNotes { get; set; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ValidateCreatedAndUpdatedEntities();
            var updates = base.SaveChanges(acceptAllChangesOnSuccess);
            this.DispatchDomainEvents(_domainEventDispatcher).Wait();
            return updates;
        }

        public override int SaveChanges()
        {
            this.ValidateCreatedAndUpdatedEntities();
            var updates = base.SaveChanges();
            this.DispatchDomainEvents(_domainEventDispatcher).Wait();
            return updates;
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            this.ValidateCreatedAndUpdatedEntities();
            var updates = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            await this.DispatchDomainEvents(_domainEventDispatcher);
            return updates;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            this.ValidateCreatedAndUpdatedEntities();
            var updates = await base.SaveChangesAsync(cancellationToken);
            await this.DispatchDomainEvents(_domainEventDispatcher);
            return updates;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectAllocationSkill>()
                .HasKey(pas => new { pas.ProjectAllocationId, pas.SkillId });

            modelBuilder.Entity<ResourceSkillDraft>()
                .HasKey(rsd => new { rsd.ResourceId, rsd.SkillId });

            modelBuilder.Entity<ResourceSkill>()
                .HasKey(rs => new { rs.ResourceId, rs.SkillId });

            modelBuilder.Entity<ResourceLineManager>()
                .HasKey(frlm => new { frlm.ResourceId, frlm.LineManagerId });

            modelBuilder.Entity<ProjectManager>()
                .HasKey(pm => new { pm.ProjectId, pm.ManagerId });

            modelBuilder.Entity<ParentProject>()
                .HasKey(pm => new { pm.ProjectId, pm.ParentProjectId });

            modelBuilder.Entity<UserResource>()
                .HasKey(ur => new { ur.ObjectId, ur.ResourceId });

            modelBuilder.Entity<SalesPerson>()
                .HasKey(ur => new { ur.ProjectId, ur.SalesPersonId });

            modelBuilder.Entity<OrderLine>()
                .HasKey(ol => new { ol.ProjectId, ol.LineId });

            modelBuilder.Entity<TimesheetEntry>().HasQueryFilter(e => !e.IsRejected && !e.IsDeleted);
            modelBuilder.Entity<ProjectActivity>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<ProjectType>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<ProjectManager>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Project>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Expense>().HasQueryFilter(e => !e.IsDeleted);
        }

        public override EntityEntry<TEntity> Remove<TEntity>([NotNull] TEntity entity)
        {
            if (entity is TimesheetEntry timesheetEntry)
            {
                var user = GetCurrentUserFromClaimsPrincipal().Result;

                CreateTimesheetEntryDeletedSyncEvent(timesheetEntry as TimesheetEntry, user);
            }

            if (entity is TimesheetEntryCorrection timesheetEntryCorrection)
            {
                var user = GetCurrentUserFromClaimsPrincipal().Result;

                CreateTimesheetCorrectionDeletedSyncEvent(timesheetEntryCorrection, user);
            }

            return base.Remove(entity);
        }

        public override void RemoveRange([NotNull] IEnumerable<object> entities)
        {
            var user = GetCurrentUserFromClaimsPrincipal().Result;

            foreach (var entity in entities)
            {
                if (entity is TimesheetEntry timesheetEntry)
                {
                    CreateTimesheetEntryDeletedSyncEvent(timesheetEntry as TimesheetEntry, user);
                }
            }

            base.RemoveRange(entities);
        }

        private void CreateTimesheetEntryDeletedSyncEvent(TimesheetEntry timesheetEntry, User user)
        {
            int? syncSystemId = this.QueryFirstOrDefaultAsync<int?>(
            @"SELECT SitelogId FROM [Timesheets].[TimesheetEntry] TE
                    WHERE TE.Id = @entryId",
            new { entryId = timesheetEntry.Id }).Result;

            _domainEventDispatcher.Dispatch(new CreateProactSyncEvent
            {
                ProactSyncEventTypeId = ProactSyncEventType.TimesheetEntryDeleted,
                SyncEntity = timesheetEntry,
                EventDate = DateTime.UtcNow,
                EventUserId = user.ObjectId,
                SyncSystemEntityId = syncSystemId
            }).Wait();
        }

        private void CreateTimesheetCorrectionDeletedSyncEvent(TimesheetEntryCorrection timesheetEntryCorrection, User user)
        {
            int? syncSystemId = this.QueryFirstOrDefaultAsync<int?>(
                @"SELECT SitelogId FROM [Timesheets].[TimesheetEntryCorrection] TC
                  WHERE TC.Id = @entryId",
                new { entryId = timesheetEntryCorrection.Id }).Result;

            _domainEventDispatcher.Dispatch(new CreateProactSyncEvent
            {
                ProactSyncEventTypeId = ProactSyncEventType.TimesheetCorrectionDeleted,
                SyncEntity = timesheetEntryCorrection,
                EventDate = DateTime.UtcNow,
                EventUserId = user.ObjectId,
                SyncSystemEntityId = syncSystemId
            }).Wait();
        }

        public void DispatchDomainEvents()
        {
            this.DispatchDomainEvents(_domainEventDispatcher).Wait();
        }

        private Task<User> GetCurrentUserFromClaimsPrincipal()
        {
            var principal = _userContext.Principal;

            Ensure.This(principal)
                .IsNotDefaultValue<SecurityException>("Cannot get current user without a principal");

            var objectId = principal.GetUserObjectId();

            if (objectId == Guid.Empty)
            {
                throw new InvalidOperationException("Principal has no object identifier claim");
            }

            var user = FindAsync<User>(objectId);

            Ensure.This(user)
                .IsNotDefaultValue<InvalidOperationException>($"Cannot find user with id {objectId}");

            return user;
        }
    }
}
