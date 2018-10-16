namespace MyCompany.MyProject.BusinessLogic.Customers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using MyCompany.MyProject.Common.Customers;
    using MyCompany.MyProject.Common.Validation;

    using Resource = Common.Customers.Resources;

    public class CustomerBusinessContext : BusinessContextBase<Customer>, ICustomerBusinessContext
    {
        private readonly ICustomerDataContext customerDataContext;

        public CustomerBusinessContext(ICustomerDataContext customerDataContext, ICustomValidator<Customer> customerValidator)
            : base(customerDataContext, customerValidator)
        {
            this.customerDataContext = customerDataContext ?? throw new ArgumentNullException(nameof(customerDataContext));
        }

        public Task<Customer[]> SearchByNames(IEnumerable<string> names, CancellationToken cancellationToken = default)
        {
            return this.customerDataContext.SearchByNames(names, cancellationToken);
        }

        protected override async Task<Result<Customer>> ApplyAddRules(Result<Customer> currentResult, Customer toAdd)
        {
            var exists = await this.customerDataContext.ExistsBy(c => c.Name, toAdd.Name);

            if (exists)
            {
                return new Result<Customer>(toAdd, new List<Failure> { new Failure(nameof(Customer.Name), Resource.DuplicateEntry) });
            }

            return new Result<Customer>(toAdd, new List<Failure>());
        }

        protected override Task<Result> ApplyDeleteRules(Customer toDelete)
        {
            return Task.FromResult(new Result(new List<Failure>()));
        }

        protected override async Task<Result<Customer>> ApplyUpdateRules(Result<Customer> currentResult, Customer toUpdate)
        {
            var current = await this.customerDataContext.GetBy(c => c.Name, toUpdate.Name);

            if (current != null && current.Id != toUpdate.Id)
            {
                return new Result<Customer>(toUpdate, new List<Failure> { new Failure(nameof(Customer.Name), Resource.DuplicateEntry) });
            }

            return new Result<Customer>(toUpdate, new List<Failure>());
        }
    }
}
