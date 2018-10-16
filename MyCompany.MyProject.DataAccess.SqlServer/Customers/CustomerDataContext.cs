namespace MyCompany.MyProject.DataAccess.SqlServer.Customers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyCompany.MyProject.Common.Customers;

    public class CustomerDataContext : DataContextBase<Customer>, ICustomerDataContext
    {
        public CustomerDataContext(DbContext dbContext)
            : base(dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
        }

        public Task<Customer[]> SearchByNames(IEnumerable<string> names, CancellationToken cancellationToken = default)
        {
            return this.Query().Where(c => names.Contains(c.Name)).ToArrayAsync();
        }
    }
}
