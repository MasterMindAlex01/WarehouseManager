using MediatR;
using WarehouseManager.Application.Auditing;
using WarehouseManager.Application.Common.Interfaces;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Auditing;

public class GetMyAuditLogsRequestHandler : IRequestHandler<GetMyAuditLogsRequest, IResult>
{
    private readonly ICurrentUser _currentUser;
    private readonly IAuditService _auditService;

    public GetMyAuditLogsRequestHandler(ICurrentUser currentUser, IAuditService auditService) =>
        (_currentUser, _auditService) = (currentUser, auditService);

    public async Task<IResult> Handle(GetMyAuditLogsRequest request, CancellationToken cancellationToken)
    {
        var auditLogs = await _auditService.GetUserTrailsAsync(_currentUser.GetUserId());
        return Results.Ok(Result<List<AuditDto>>.Success(auditLogs));
    }
}