using FluentValidation;
using ShipmentsAPI.Entities;
using System.Linq;

namespace ShipmentsAPI.DtoModels.Validators
{
    public class QueryShipmentsValidator : AbstractValidator<QueryShipments>
    {
        private int[] allowedPageSizes = new[] {5,10,15,20 };
        private string[] allowedColumnNames = new[]
        {
            nameof(Shipment.TimeOfDeparture),
            nameof(Shipment.ContainerType),
            nameof(Shipment.ContainerNumber),
            nameof(Shipment.ETD),
            nameof(Shipment.PalletQty),
            nameof(Shipment.Status.Name)
        };
        public QueryShipmentsValidator()
        {
            RuleFor(x => x.PalletQty).GreaterThanOrEqualTo(0);
            
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
