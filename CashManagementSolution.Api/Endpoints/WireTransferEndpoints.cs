using CashManagementSolution.Api.Models;
using CashManagementSolution.Api.Services;

namespace CashManagementSolution.Api.Endpoints;

public static class WireTransferEndpoints
{
    public static void MapWireTransferEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/wire-transfers");

        group.MapPost("/", async (SubmitWireTransferRequest request, WireTransferService service) =>
        {
            var result = await service.SubmitAsync(request.FromAccountId, request.ToAccountId, request.Amount);
            return Results.CreatedAtRoute("GetWireTransfer", new { id = result.Id }, result);
        });

        group.MapPost("/{id:guid}/validate", async (Guid id, WireTransferService service) =>
        {
            var result = await service.ValidateAsync(id);
            return result is null ? Results.NotFound() : Results.Ok(result);
        });

        group.MapGet("/{id:guid}", async (Guid id, WireTransferService service) =>
        {
            var result = await service.GetByIdAsync(id);
            return result is null ? Results.NotFound() : Results.Ok(result);
        })
        .WithName("GetWireTransfer");
    }
}
