namespace MyCompany.MyProject.BusinessLogic.Customers
{
    using FluentValidation;
    using MyCompany.MyProject.BusinessLogic.Validation;
    using MyCompany.MyProject.Common.Customers;

    using Resource = Common.Customers.Resources;

    public class CustomerValidator : CustomValidator<Customer>
    {
        public CustomerValidator()
        {
            this.RuleFor(c => c.Name)
                .NotEmpty().WithMessage(Resource.NameMandatory);
        }
    }
}
