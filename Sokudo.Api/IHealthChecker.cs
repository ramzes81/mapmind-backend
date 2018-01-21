namespace Sokudo.Api
{
    using System.Threading.Tasks;

    public interface IHealthChecker
    {
        Task CheckHealth();
    }
}
