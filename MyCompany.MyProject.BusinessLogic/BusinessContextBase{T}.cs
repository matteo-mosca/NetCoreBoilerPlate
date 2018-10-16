namespace MyCompany.MyProject.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using MyCompany.MyProject.Common;
    using MyCompany.MyProject.Common.Validation;

    public abstract class BusinessContextBase<T> : IBusinessContext<T>
        where T : class
    {
        private readonly IDataContext<T> dataContext;

        private readonly ICustomValidator<T> validator;

        private bool disposedValue = false;

        public BusinessContextBase(IDataContext<T> dataContext, ICustomValidator<T> validator)
        {
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<Result<T>> Add(T toAdd, CancellationToken cancellationToken = default)
        {
            var result = await this.validator.PerformValidation(toAdd);

            result = await this.ApplyAddRules(result, toAdd);

            if (!result.IsValid)
            {
                return result;
            }

            var added = await this.dataContext.Add(toAdd, cancellationToken);

            return new Result<T>(added, new List<Failure>());
        }

        public async Task<Result> Delete(T toDelete, CancellationToken cancellationToken = default)
        {
            var result = await this.ApplyDeleteRules(toDelete);

            if (!result.IsValid)
            {
                return result;
            }

            await this.dataContext.Delete(toDelete, cancellationToken);

            return new Result(new List<Failure>());
        }

        public async Task<Result<T>> Update(T toUpdate, CancellationToken cancellationToken = default)
        {
            var result = await this.validator.PerformValidation(toUpdate);

            result = await this.ApplyUpdateRules(result, toUpdate);

            if (!result.IsValid)
            {
                return result;
            }

            var updated = await this.dataContext.Update(toUpdate, cancellationToken);

            return new Result<T>(updated, new List<Failure>());
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected abstract Task<Result<T>> ApplyAddRules(Result<T> currentResult, T toAdd);

        protected abstract Task<Result> ApplyDeleteRules(T toDelete);

        protected abstract Task<Result<T>> ApplyUpdateRules(Result<T> currentResult, T toUpdate);

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.dataContext.Dispose();
                }

                this.disposedValue = true;
            }
        }
    }
}
