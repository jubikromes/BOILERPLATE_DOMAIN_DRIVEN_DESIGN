using Domain.Entities.Aggregates.Recipe;
using Infrastructure.Configuration;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using SharedKernel.Interfaces;
using System.Data;

namespace Infrastructure.Context
{
    public class DDDContext : DbContext, IUnitOfWork
    {
        public DbSet<Recipe> Recipes { get; set; }


        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        public DDDContext(DbContextOptions<DDDContext> options) : base(options) { }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public DDDContext(DbContextOptions<DDDContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


            System.Diagnostics.Debug.WriteLine("OrderingContext::ctor ->" + this.GetHashCode());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RecipeConfiguration());

        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed

            var result = await base.SaveChangesAsync(cancellationToken);


            return true;
        }


        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            //_currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            _currentTransaction = await Database.BeginTransactionAsync(CancellationToken.None);


            return _currentTransaction;
        }
        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                await transaction.CommitAsync();
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
        public void RollbackTransaction()
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

    public class DDDContextFactory : IDesignTimeDbContextFactory<DDDContext>
    {
        public DDDContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DDDContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=DDDContext;Trusted_Connection=True;Encrypt=False");


            return new DDDContext(optionsBuilder.Options);
        }
    }
}
