using AutoMapper;
using HotelListing.IRepository;
using HotelListing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<CountryController> logger;
        private readonly IMapper mapper;

        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries = await unitOfWork.Countries.GetAll();
                var results = mapper.Map<IList<CountryDTO>>(countries);
                return Ok(results);
            }
            catch (Exception excError)
            {
                logger.LogError(excError, string.Concat("Error: ", nameof(GetCountries)));
                return StatusCode(500, "Internal Server Error. PLease try again");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCountryById(int id)
        {
            try
            {
                var country = await unitOfWork.Countries.Get(expression: q => q.Id == id, includes: new List<string> { "Hotels" });
                var result = mapper.Map<CountryDTO>(country);
                return Ok(result);
            }
            catch (Exception excError)
            {
                logger.LogError(excError, string.Concat("Error: ", nameof(GetCountryById)));
                return StatusCode(500, "Internal Server Error. PLease try again");
            }
        }
    }
}
