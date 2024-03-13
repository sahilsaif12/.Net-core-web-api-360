using System.ComponentModel.DataAnnotations;

namespace asp.net_core_web_api_proect_1.Models
{
    public class Address
    {
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        //[Required(ErrorMessage = "country is required")]
        public string? Country { get; set; }
    }
}
