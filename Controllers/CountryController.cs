using AutoMapper;
using HotelListing.Data;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                return StatusCode(500, "Internal Server Error. Please try again");
            }
        }

        [HttpGet("{id:int}", Name = "GetCountryById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                return StatusCode(500, "Internal Server Error. Please try again");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCountry([FromBody] CountryDTO countryDTO)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError(string.Concat(nameof(GetCountryById), ": Invalid model"));
                return StatusCode(500, "Internal Server Error. Please try again");
            }

            try
            {
                var country = mapper.Map<Country>(countryDTO);
                await unitOfWork.Countries.Insert(country);
                await unitOfWork.Save();
                return CreatedAtRoute("GetCountryById", new { id = country.Id }, country);
            }
            catch (Exception excError)
            {
                logger.LogError(excError, string.Concat("Error: ", nameof(GetCountryById)));
                return StatusCode(500, "Internal Server Error. Please try again");
            }
        }

    }
}
