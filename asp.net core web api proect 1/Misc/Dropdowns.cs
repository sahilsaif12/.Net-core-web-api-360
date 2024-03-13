using System.Text.Json.Serialization;

namespace asp.net_core_web_api_proect_1.Misc
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        Male,
        Female,
        Others
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortBy
    {
        Name,
        City,
        Country,
        Date
    }
}
