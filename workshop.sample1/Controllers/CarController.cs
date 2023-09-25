using Microsoft.AspNetCore.Mvc;
using workshop.sample1.Models;

namespace workshop.sample1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private static List<Car> _cars = new List<Car>();

        public CarController()
        {
            if(_cars.Count==0)
            {
                _cars.Add(new Car { Id = 1, Name = "VW Beetle", CountPeopleInCar=0 });
                _cars.Add(new Car { Id = 2, Name = "VW Golf", CountPeopleInCar = 0 });
                _cars.Add(new Car { Id = 3, Name = "VW Tiguan", CountPeopleInCar = 0 });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IResult> GetCars()
        {
            return Results.Ok(_cars);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("{id}")]
        public async Task<IResult> GetCar(int id)
        {
            var car = _cars.Where(c => c.Id == id).FirstOrDefault();
            return car != null ? Results.Ok(car) : Results.NotFound();

            //if(car!=null)
            //{
            //    return Results.Ok(car);
            //}
            //return Results.NotFound();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("increasebyone")]
        public async Task<IResult> Increase(int id)
        {
            var car = _cars.Where(car => car.Id == id).FirstOrDefault();
            if(car!=null)
            {
                car.CountPeopleInCar++;
                return Results.Ok(car);
            }
            return Results.NotFound();            
        }
    }
}
