namespace MyCompany.MyProject.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using MyCompany.MyProject.Common.Validation;

    public interface IBusinessContext<T> : IDisposable
        where T : class
    {
        Task<Result<T>> Add(T toAdd, CancellationToken cancellationToken = default);

        Task<Result> Delete(T toDelete, CancellationToken cancellationToken = default);

        Task<Result<T>> Update(T toUpdate, CancellationToken cancellationToken = default);
    }
}
