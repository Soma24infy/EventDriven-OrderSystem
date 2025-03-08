﻿using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints
{
    //accepts a order id as parameter
    //constructs the deleteordercommand
    //uses mediatr to send command to the corresponding handler
    //returns a response with success or not found
    //public record DeleteOrderRequest(Guid Id);
    public record DeleteOrderResponse(bool IsSuccess);
    public class DeleteOrder:ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/orders/{id}", async (Guid Id, ISender sender) =>
            {              
                var result = await sender.Send(new DeleteOrderCommand(Id));
                var response = result.Adapt<DeleteOrderResponse>();
                return Results.Ok(response);
            }).WithName("DeleteOrder")
            .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Order")
            .WithDescription("Delete Order");
        }
    }
}
