namespace MyCompany.MyProject.DataAccess.SqlServer
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyCompany.MyProject.Common;
    using MyCompany.MyProject.Common.Extensions;

    public abstract class DataContextBase<T> : IDataContext<T>, IDisposable
        where T : class
    {
        private readonly DbContext context;

        private bool disposedValue = false;

        public DataContextBase(DbContext dbContext)
        {
            this.context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<T> Add(T toAdd, CancellationToken cancellationToken = default)
        {
            var entry = await this.context.AddAsync(toAdd, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);

            return entry.Entity;
        }

        public Task Delete(T toDelete, CancellationToken cancellationToken = default)
        {
            this.context.Remove(toDelete);
            return Task.CompletedTask;
        }

        public Task<bool> ExistsBy<TK>(System.Linq.Expressions.Expression<Func<T, TK>> selector, TK value, CancellationToken cancellationToken = default)
        {
            return this.context.Query<T>().AnyAsync(selector.GetBinaryLambdaExpression(value), cancellationToken);
        }

        public Task<T> GetBy<TK>(System.Linq.Expressions.Expression<Func<T, TK>> selector, TK value, CancellationToken cancellationToken = default)
        {
            return this.context.Query<T>().SingleOrDefaultAsync(selector.GetBinaryLambdaExpression(value), cancellationToken);
        }

        public async Task<T> Update(T toUpdate, CancellationToken cancellationToken = default)
        {
            var entry = this.context.Update(toUpdate);
            await this.context.SaveChangesAsync(cancellationToken);
            return entry.Entity;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected IQueryable<T> Query()
        {
            return this.context.Query<T>();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }

                this.disposedValue = true;
            }
        }
    }
}
