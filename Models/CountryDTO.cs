using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models
{
    public class CreateCountryDTO
    {

        [Required(ErrorMessage = "Country Name is required.")]
        [StringLength(maximumLength: 50, ErrorMessage = "Country Name is too long.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Short Country Name is required.")]
        [StringLength(maximumLength: 2, ErrorMessage = "Short Country Name is too long.")]
        public string ShortName { get; set; }
    }

    public class UpdateCountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }
    }

    public class CountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }

        public IList<HotelDTO> Hotels { get; set; }
    }
}
