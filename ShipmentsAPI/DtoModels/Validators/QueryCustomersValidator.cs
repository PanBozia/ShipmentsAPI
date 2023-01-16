using FluentValidation;
using ShipmentsAPI.Entities;
using System.Linq;

namespace ShipmentsAPI.DtoModels.Validators
{
    public class QueryCustomersValidator:AbstractValidator<QueryCustomers>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 15, 20 };
        private string[] allowedColumnNames = new[]
        {
            nameof(Customer.Name),
            nameof(Customer.ShortName),
            nameof(Customer.ClientNumber),
            nameof(Customer.CityAddress),
            nameof(Customer.CountryAddress),
            nameof(Customer.ZipCodeAddress),
            nameof(Customer.StreetAddress)
        };

        public QueryCustomersValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"Page size must be in [{string.Join(",", allowedPageSizes)}]");
                }

            });

            RuleFor(x => x.SortBy).Must(value => string.IsNullOrEmpty(value) || allowedColumnNames.Contains(value))
                .WithMessage($"SortBy is optional, or must be one of the following: {string.Join(", ", allowedColumnNames)}");

        }
    }
}
