using System;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models
{
    public class CreateHotelDTO
    {
        [Required(ErrorMessage = "Hotel Name is required")]
        [StringLength(maximumLength: 150, ErrorMessage = "Hotel Name is too long")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Hotel Address is required")]
        [StringLength(maximumLength: 250, ErrorMessage = "Hotel Address is too long")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Hotel Rating is required")]
        [Range(1, 5, ErrorMessage = "Hotel Rating is between 1 and 5")]
        public double Rating { get; set; }

        [Required(ErrorMessage = "Hotel Country is required")]
        public int CountryId { get; set; }
    }

    public class UpdateHotelDTO : CreateHotelDTO
    {
        public int Id { get; set; }
    }

    public class HotelDTO : CreateHotelDTO
    {
        public int Id { get; set; }

        public CountryDTO Country { get; set; }
    }
}
