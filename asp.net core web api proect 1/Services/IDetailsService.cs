using asp.net_core_web_api_proect_1.Misc;
using asp.net_core_web_api_proect_1.Models;

namespace asp.net_core_web_api_proect_1.Services
{
    public interface IDetailsService
    {
        public List<Entity> GetDetails(int page,int size,string sortBy);
        public Entity GetDetailBYId(int id);

        public Entity PostDetail(Entity entity);

        public Entity UpdateDetail(int id, Entity entity);

        public string DeleteDetail(int id);
        public List<Entity> SearchQuery(string query);
        public List<Entity> FilterDetails(string gender, string[] countries, DateTime? startDate, DateTime? endDay);
    }
}
