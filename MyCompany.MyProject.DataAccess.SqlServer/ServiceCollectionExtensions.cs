namespace MyCompany.MyProject.DataAccess.SqlServer
{
    using Microsoft.Extensions.DependencyInjection;
    using MyCompany.MyProject.Common.Customers;
    using MyCompany.MyProject.DataAccess.SqlServer.Customers;

    public static class ServiceCollectionExtensions
    {
        public static void RegisterDatabaseContext(this IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>();
        }

        public static void RegisterDataContexts(this IServiceCollection services)
        {
            services.AddTransient<ICustomerDataContext, CustomerDataContext>();
        }
    }
}
