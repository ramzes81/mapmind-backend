namespace Sokudo.Api.Commands
{
    using System.Threading.Tasks;
    using Boilerplate;
    using Microsoft.AspNetCore.Mvc;
    using Sokudo.Api.Constants;
    using Sokudo.Api.Repositories;
    using Sokudo.Api.ViewModels;

    public class PutCarCommand : IPutCarCommand
    {
        private readonly ICarRepository carRepository;
        private readonly ITranslator<Models.Car, Car> carToCarTranslator;
        private readonly ITranslator<SaveCar, Models.Car> saveCarToCarTranslator;

        public PutCarCommand(
            ICarRepository carRepository,
            ITranslator<Models.Car, Car> carToCarTranslator,
            ITranslator<SaveCar, Models.Car> saveCarToCarTranslator)
        {
            this.carRepository = carRepository;
            this.carToCarTranslator = carToCarTranslator;
            this.saveCarToCarTranslator = saveCarToCarTranslator;
        }

        public async Task<IActionResult> ExecuteAsync(int carId, SaveCar saveCar)
        {
            var car = await this.carRepository.Get(carId);
            if (car == null)
            {
                return new NotFoundResult();
            }

            this.saveCarToCarTranslator.Translate(saveCar, car);
            car = await this.carRepository.Update(car);
            var carViewModel = this.carToCarTranslator.Translate(car);

            return new OkObjectResult(carViewModel);
        }
    }
}
