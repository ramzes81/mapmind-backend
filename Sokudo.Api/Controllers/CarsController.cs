﻿namespace Sokudo.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Sokudo.Api.Constants;
    using Sokudo.Api.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Net.Http.Headers;

    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class CarsController : ControllerBase
    {
        public CarsController()
        {
        }

        /// <summary>
        /// Returns an Allow HTTP header with the allowed HTTP methods.
        /// </summary>
        /// <returns>A 200 OK response.</returns>
        /// <response code="200">The allowed HTTP methods.</response>
        [HttpOptions]
        public IActionResult Options()
        {
            this.HttpContext.Response.Headers.AppendCommaSeparatedValues(
                HeaderNames.Allow,
                HttpMethods.Get,
                HttpMethods.Head,
                HttpMethods.Options,
                HttpMethods.Post);
            return this.Ok();
        }

        /// <summary>
        /// Returns an Allow HTTP header with the allowed HTTP methods for a car with the specified unique identifier.
        /// </summary>
        /// <returns>A 200 OK response.</returns>
        /// <response code="200">The allowed HTTP methods.</response>
        [HttpOptions("{carId}")]
        public IActionResult Options(int carId)
        {
            this.HttpContext.Response.Headers.AppendCommaSeparatedValues(
                HeaderNames.Allow,
                HttpMethods.Delete,
                HttpMethods.Get,
                HttpMethods.Head,
                HttpMethods.Options,
                HttpMethods.Patch,
                HttpMethods.Post,
                HttpMethods.Put);
            return this.Ok();
        }

        /// <summary>
        /// Deletes the car with the specified ID.
        /// </summary>
        /// <param name="carId">The car ID.</param>
        /// <returns>A 204 No Content response if the car was deleted or a 404 Not Found if a car with the specified ID
        /// was not found.</returns>
        /// <response code="204">The car with the specified ID was deleted.</response>
        /// <response code="404">A car with the specified ID was not found.</response>
        [HttpDelete("{carId}", Name = CarsControllerRoute.DeleteCar)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public Task<IActionResult> Delete(int carId) => null;

        /// <summary>
        /// Gets the car with the specified ID.
        /// </summary>
        /// <param name="carId">The car ID.</param>
        /// <returns>A 200 OK response containing the car or a 404 Not Found if a car with the specified ID was not
        /// found.</returns>
        /// <response code="200">The car with the specified ID.</response>
        /// <response code="304">The car has not changed since the date given in the If-Modified-Since HTTP header.</response>
        /// <response code="404">A car with the specified ID could not be found.</response>
        [HttpGet("{carId}", Name = CarsControllerRoute.GetCar)]
        [HttpHead("{carId}")]
        [ProducesResponseType(typeof(Car), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status304NotModified)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public Task<IActionResult> Get(int carId) => null;

        /// <summary>
        /// Gets a collection of cars using the specified page number and number of items per page.
        /// </summary>
        /// <param name="pageOptions">The page options.</param>
        /// <returns>A 200 OK response containing a collection of cars, a 400 Bad Request if the page request
        /// parameters are invalid or a 404 Not Found if a page with the specified page number was not found.
        /// </returns>
        /// <response code="200">A collection of cars for the specified page.</response>
        /// <response code="400">The page request parameters are invalid.</response>
        /// <response code="404">A page with the specified page number was not found.</response>
        [HttpGet("", Name = CarsControllerRoute.GetCarPage)]
        [HttpHead("")]
        [ProducesResponseType(typeof(PageResult<Car>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public Task<IActionResult> GetPage([FromQuery] PageOptions pageOptions) => null;

        /// <summary>
        /// Patches the car with the specified ID.
        /// </summary>
        /// <param name="carId">The car ID.</param>
        /// <param name="patch">The patch document. See http://jsonpatch.com/.</param>
        /// <returns>A 200 OK if the car was patched, a 400 Bad Request if the patch was invalid or a 404 Not Found
        /// if a car with the specified ID was not found.</returns>
        /// <response code="200">The patched car with the specified ID.</response>
        /// <response code="400">The patch document is invalid.</response>
        /// <response code="404">A car with the specified ID could not be found.</response>
        [HttpPatch("{carId}", Name = CarsControllerRoute.PatchCar)]
        [ProducesResponseType(typeof(Car), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public Task<IActionResult> Patch(int carId, [FromBody] JsonPatchDocument<SaveCar> patch) => null;

        /// <summary>
        /// Creates a new car.
        /// </summary>
        /// <param name="car">The car to create.</param>
        /// <returns>A 201 Created response containing the newly created car or a 400 Bad Request if the car is
        /// invalid.</returns>
        /// <response code="201">The car was created.</response>
        /// <response code="400">The car is invalid.</response>
        [HttpPost("", Name = CarsControllerRoute.PostCar)]
        [ProducesResponseType(typeof(Car), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Post([FromBody] SaveCar car) => null;

        /// <summary>
        /// Updates an existing car with the specified ID.
        /// </summary>
        /// <param name="carId">The car identifier.</param>
        /// <param name="car">The car to update.</param>
        /// <returns>A 200 OK response containing the newly updated car, a 400 Bad Request if the car is invalid or a
        /// or a 404 Not Found if a car with the specified ID was not found.</returns>
        /// <response code="200">The car was updated.</response>
        /// <response code="400">The car is invalid.</response>
        /// <response code="404">A car with the specified ID could not be found.</response>
        [HttpPut("{carId}", Name = CarsControllerRoute.PutCar)]
        [ProducesResponseType(typeof(Car), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public Task<IActionResult> Put(int carId, [FromBody] SaveCar car) => null;
    }
}
