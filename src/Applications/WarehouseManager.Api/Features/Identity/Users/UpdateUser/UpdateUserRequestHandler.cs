using MediatR;
using WarehouseManager.Application.Common.Exceptions;
using WarehouseManager.Application.Identity.Users;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users;

public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, IResult>
{
    private readonly IUserService _userService;

    public UpdateUserRequestHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IResult> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.Email)
            && !string.IsNullOrWhiteSpace(request.Email) 
            && await _userService.ExistsWithEmailAsync(request.Email, request.Id))
            throw new ConflictException($"Email {request.Email} is already registered.");
        
        if (!string.IsNullOrEmpty(request.PhoneNumber)
            && !string.IsNullOrWhiteSpace(request.PhoneNumber)
            && await _userService.ExistsWithPhoneNumberAsync(request.PhoneNumber,request.Id))
            throw new ConflictException($"Phone number {request.PhoneNumber} is already registered.");
        
        var updateUserRequestDto = new UpdateUserRequestDto
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Image = request.Image,
            DeleteCurrentImage = request.DeleteCurrentImage,
        };
        await _userService.UpdateAsync(updateUserRequestDto, request.Id);

        return Results.Ok(Result.Success("Update user successful"));
    }
}