namespace asp.net_core_web_api_proect_1.Models
{
    public class Entity : IEntity
    {
        
        public int? Id { get; set; }
        public List<Name>? Names { get; set; }
        public List<Address>? Addresses { get; set; }
        public List<Date>? Dates { get; set; }
        public string? Gender { get; set; }
        public bool? Deceased { get; set; }

    }
}
