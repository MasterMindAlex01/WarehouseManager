using MediatR;
using WarehouseManager.Api.Helpers;
using WarehouseManager.Application.Common.Exceptions;
using WarehouseManager.Application.Identity.Users;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users;

public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, IResult>
{
    private readonly IUserService _userService;
    private readonly IHttpExtensionHelper _httpExtensionHelper;

    public CreateUserRequestHandler(
        IUserService userService,
        IHttpExtensionHelper httpExtensionHelper)
    {
        _userService = userService;
        _httpExtensionHelper = httpExtensionHelper;
    }

    public async Task<IResult> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        if (await _userService.ExistsWithEmailAsync(request.Email))
            throw new ConflictException($"Email {request.Email} is already registered.");
        if (await _userService.ExistsWithNameAsync(request.UserName))
            throw new ConflictException($"Username {request.UserName} is already taken.");
        if (!string.IsNullOrEmpty(request.PhoneNumber) 
            && !string.IsNullOrWhiteSpace(request.PhoneNumber)
            && await _userService.ExistsWithPhoneNumberAsync(request.PhoneNumber))
            throw new ConflictException($"Phone number {request.PhoneNumber} is already registered.");
        
        var createUserRequestDto = new CreateUserRequestDto
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.UserName,
            Password = request.Password,
            ConfirmPassword = request.ConfirmPassword,
            PhoneNumber = request.PhoneNumber
        };

        var response = await _userService.CreateAsync(
                createUserRequestDto,
                _httpExtensionHelper.GetOriginFromRequest()
            );
        return Results.Ok(Result<string>.Success(response));
    }
}