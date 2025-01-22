using cHub.Recipes.API.IntegrationEvents.Events;
using Rebus.Handlers;

namespace cHub.Recipes.API.IntegrationEvents.EventHandling;

public class CreateNewRecipeEventHandler : IHandleMessages<CreateNewRecipe>
{
    public async Task Handle(CreateNewRecipe currentDateTime)
    {
        Console.WriteLine("The time is {0}", currentDateTime);
    }
}