using System.Threading.Tasks;

namespace MapMind.Api
{
    public interface IHealthChecker
    {
        Task CheckHealth();
    }
}