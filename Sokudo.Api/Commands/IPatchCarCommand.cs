namespace Sokudo.Api.Commands
{
    using Boilerplate.AspNetCore;
    using Microsoft.AspNetCore.JsonPatch;
    using Sokudo.Api.ViewModels;

    public interface IPatchCarCommand : IAsyncCommand<int, JsonPatchDocument<SaveCar>>
    {
    }
}
