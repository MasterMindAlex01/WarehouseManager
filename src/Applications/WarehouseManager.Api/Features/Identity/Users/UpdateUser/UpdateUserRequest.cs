using MediatR;
using WarehouseManager.Application.Common.FileStorage;

namespace WarehouseManager.Api.Features.Identity.Users;

public class UpdateUserRequest : IRequest<IResult>
{
    public string Id { get; set; } = default!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public FileUploadRequest? Image { get; set; }
    public bool DeleteCurrentImage { get; set; } = false;
}
