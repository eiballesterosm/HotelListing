using AutoMapper;
using HotelListing.IRepository;
using HotelListing.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<HotelController> logger;
        private readonly IMapper mapper;

        public HotelController(IUnitOfWork unitOfWork, ILogger<HotelController> logger, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotels()
        {
            try
            {
                var hotels = await unitOfWork.Hotels.GetAll();
                var results = mapper.Map<IList<HotelDTO>>(hotels);
                return Ok(results);
            }
            catch (Exception excError)
            {
                logger.LogError(excError, string.Concat("Error: ", nameof(GetHotels)));
                return StatusCode(500, "Internal Server Error. Please try again");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotelById(int id)
        {
            try
            {
                var hotel = await unitOfWork.Hotels.Get(expression: q => q.Id == id, includes: new List<string> { "Country" });
                var result = mapper.Map<HotelDTO>(hotel);
                return Ok(result);
            }
            catch (Exception excError)
            {
                logger.LogError(excError, string.Concat("Error: ", nameof(GetHotelById)));
                return StatusCode(500, "Internal Server Error. Please try again");
            }
        }
    }
}
