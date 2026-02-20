using FluentValidation;
using Microsoft.Extensions.Localization;
using WarehouseManager.Application.Catalog.Brands;
using WarehouseManager.Application.Common.Persistence;
using WarehouseManager.Domain.Catalog;

namespace WarehouseManager.Api.Features.Catalog.Brands;

public class UpdateBrandRequestValidator : AbstractValidator<UpdateBrandRequest>
{
    public UpdateBrandRequestValidator(IRepository<Brand> repository, IStringLocalizer<UpdateBrandRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (brand, name, ct) =>
                    await repository.FirstOrDefaultAsync(new BrandByNameSpec(name), ct)
                        is not Brand existingBrand || existingBrand.Id == brand.Id)
                .WithMessage((_, name) => T["Brand {0} already Exists.", name]);
}
