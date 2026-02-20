using MediatR;
using WarehouseManager.Application.Common.Exceptions;
using WarehouseManager.Application.Identity.Roles;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Roles;

public class CreateOrUpdateRoleRequestHandler : IRequestHandler<CreateOrUpdateRoleRequest, IResult>
{
    private readonly IRoleService _roleService;
    public CreateOrUpdateRoleRequestHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<IResult> Handle(CreateOrUpdateRoleRequest request, CancellationToken cancellationToken)
    {
        if (!await _roleService.ExistsAsync(request.Name, request.Id))
        {
            var createOrUpdateRoleDto = new CreateOrUpdateRoleRequestDto
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description
            };
            var response = await _roleService.CreateOrUpdateAsync(createOrUpdateRoleDto);
            return Results.Ok(Result<string>.Success(response, "create or update role"));
        }
        throw new ConflictException("Role already exists");
    }
}
