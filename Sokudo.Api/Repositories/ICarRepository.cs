namespace Sokudo.Api.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Sokudo.Api.Models;

    public interface ICarRepository
    {
        Task<Car> Add(Car car);

        Task Delete(Car car);

        Task<Car> Get(int carId);

        Task<ICollection<Car>> GetPage(int page, int count);

        Task<int> GetTotalPages(int count);

        Task<Car> Update(Car car);
    }
}
