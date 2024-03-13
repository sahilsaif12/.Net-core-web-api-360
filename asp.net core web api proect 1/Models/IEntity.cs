using System.Text.Json.Serialization;

namespace asp.net_core_web_api_proect_1.Models
{
    public interface IEntity
    {
        [JsonIgnore]
        int? Id { get; set; }
        List<Address>? Addresses { get; set; }
        bool? Deceased { get; set; }
        string? Gender { get; set; }
        List<Name>? Names { get; set; }
        List<Date>? Dates { get; set; }
    }
}
