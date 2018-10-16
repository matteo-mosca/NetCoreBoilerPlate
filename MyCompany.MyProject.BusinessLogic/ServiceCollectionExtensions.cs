namespace MyCompany.MyProject.BusinessLogic
{
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;
    using MyCompany.MyProject.BusinessLogic.Customers;
    using MyCompany.MyProject.Common.Customers;
    using MyCompany.MyProject.Common.Validation;

    public static class ServiceCollectionExtensions
    {
        public static void RegisterBusinessContexts(this IServiceCollection services)
        {
            services.AddTransient<ICustomerBusinessContext, CustomerBusinessContext>();
        }

        public static void RegisterValidators(this IServiceCollection services)
        {
            services.AddSingleton<IValidator<Customer>, CustomerValidator>();
            services.AddSingleton<ICustomValidator<Customer>, CustomerValidator>();
        }
    }
}
