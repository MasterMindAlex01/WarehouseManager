using Carter;
using MediatR;
using System.Security.Claims;
using WarehouseManager.Api.Features.Identity.Users;
using WarehouseManager.Application.Common.Exceptions;
using WarehouseManager.Application.Identity.Users;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Personal
{
    public class GetProfileEndpoint : ICarterModule     
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/personal/profile", 
            async (IMediator mediator, HttpContext HttpContext) =>
            {
                if (HttpContext.User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId))
                {
                    throw new UnauthorizedException("User is not authorized.");
                }

                return await mediator.Send(new GetUserByIdRequest(userId));
            })            
            .RequireAuthorization()
            .WithName("GetProfile")
            .WithSummary("Get profile of currently logged in user.")
            .WithTags("Personal")
            .Produces<ErrorResult>(StatusCodes.Status404NotFound)
            .Produces<Result<UserDetailsDto>>(StatusCodes.Status200OK);
        }
    }
}
