using Carter;
using WarehouseManager.Api.Helpers;
using WarehouseManager.Application.Identity.Users;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users;

public class EmailVerificationEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/users/email-verification",
            async (
                ForgotPasswordRequest request,
                IUserService userService,
                IHttpExtensionHelper httpExtensionHelper) =>
            {
                var response = await userService
                    .GetEmailVerificationUriAsync(request.Email, httpExtensionHelper.GetOriginFromRequest());

                return Results.Ok(Result.Success());
            })
        .WithName("EmailVerification")
        .WithTags("Users")
        .ProducesValidationProblem()
        .Produces<ErrorResult>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResult>(StatusCodes.Status500InternalServerError)
        .Produces<Result>(StatusCodes.Status200OK);
    }
}
