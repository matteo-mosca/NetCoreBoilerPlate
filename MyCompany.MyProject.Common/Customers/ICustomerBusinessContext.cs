namespace MyCompany.MyProject.Common.Customers
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ICustomerBusinessContext : IBusinessContext<Customer>
    {
        Task<Customer[]> SearchByNames(IEnumerable<string> names, CancellationToken cancellationToken = default);
    }
}
