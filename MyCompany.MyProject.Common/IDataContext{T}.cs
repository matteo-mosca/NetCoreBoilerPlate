namespace MyCompany.MyProject.Common
{
    using System;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDataContext<T> : IDisposable
        where T : class
    {
        Task<T> Add(T toAdd, CancellationToken cancellationToken = default);

        Task<bool> ExistsBy<TK>(Expression<Func<T, TK>> selector, TK value, CancellationToken cancellationToken = default);

        Task<T> GetBy<TK>(Expression<Func<T, TK>> selector, TK value, CancellationToken cancellationToken = default);

        Task Delete(T toDelete, CancellationToken cancellationToken = default);

        Task<T> Update(T toUpdate, CancellationToken cancellationToken = default);
    }
}
