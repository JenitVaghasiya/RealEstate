namespace RealEstate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Storage;
    using NJsonSchema.Annotations;
    using RealEstate.DAL.Domain;

    public class RealEstateContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private IDbContextTransaction _currentTransaction;

        public RealEstateContext(
            DbContextOptions<RealEstateContext> options,
            IDomainEventDispatcher domainEventDispatcher)
            : base(options)
        {
            _domainEventDispatcher = domainEventDispatcher;
        }

        public enum FilterKeys
        {
            ExcludeRejectedTimesheetEntries,
            ExcludeNonRejectedTimesheetEntries
        }

        public DbSet<Apartment> Apartments { get; set; }

        public DbSet<AppartmentComplex> AppartmentComplexs { get; set; }

        public DbSet<Item> Items { get; set; }

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
            base.OnModelCreating(modelBuilder);
        }

        public void DispatchDomainEvents()
        {
            this.DispatchDomainEvents(_domainEventDispatcher).Wait();
        }

        public virtual void BeginTransaction()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = Database.BeginTransaction();
        }

        public virtual async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();

                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public virtual void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
