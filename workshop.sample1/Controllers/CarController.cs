using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
                _cars.Add(new Car { Id = 1, Name = "VW Beetle", CountPeopleInCar=1 });
                _cars.Add(new Car { Id = 2, Name = "VW Golf", CountPeopleInCar = 5 });
                _cars.Add(new Car { Id = 3, Name = "VW Tiguan", CountPeopleInCar = 10 });
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
        [Route("increasebyone/{id}")]
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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("decreasebyone/{id}")]
        public async Task<IResult> Decrease(int id)
        {
            var car = _cars.Where(car => car.Id == id).FirstOrDefault();
            if (car != null)
            {
                car.CountPeopleInCar--;
                return Results.Ok(car);
            }
            return Results.NotFound();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("greaterthan/{count}")]
        public async Task<IResult> GetGreater(int count)
        {
            var results = _cars.Where(car => car.CountPeopleInCar >= count).ToList();
            
            return results.Count>0 ? Results.Ok(results) : Results.NotFound();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("lessthan/{count}")]
        public async Task<IResult> GetLess(int count)
        {
            var results = _cars.Where(car => car.CountPeopleInCar < count).ToList();
           
            return results.Count > 0 ? Results.Ok(results) : Results.NotFound();
        }

    }
}
