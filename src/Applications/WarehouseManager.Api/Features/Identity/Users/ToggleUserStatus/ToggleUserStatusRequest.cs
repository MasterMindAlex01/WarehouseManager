using MediatR;

namespace WarehouseManager.Api.Features.Identity.Users;

public class ToggleUserStatusRequest : IRequest<IResult>
{
    public bool? ActivateUser { get; set; }
    public string? UserId { get; set; }
}
