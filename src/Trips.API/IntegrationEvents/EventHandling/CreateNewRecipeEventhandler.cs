using cHub.Recipes.API.IntegrationEvents.Events;
using RabbitMQ.Client;
using Rebus.Handlers;
using Rebus.Pipeline;

namespace cHub.Recipes.API.IntegrationEvents.EventHandling;

public class CreateNewRecipeEventHandler(
    IMessageContext messageContext
) : IHandleMessages<CreateNewRecipe>
{
    public async Task Handle(CreateNewRecipe currentDateTime)
    {

        Console.WriteLine("The time is {0}", currentDateTime);
    }
}