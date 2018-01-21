namespace Sokudo.Api.Commands
{
    using Boilerplate.AspNetCore;
    using Sokudo.Api.ViewModels;

    public interface IPutCarCommand : IAsyncCommand<int, SaveCar>
    {
    }
}
