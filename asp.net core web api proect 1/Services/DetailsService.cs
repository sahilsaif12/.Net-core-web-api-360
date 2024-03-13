using asp.net_core_web_api_proect_1.Models;

namespace asp.net_core_web_api_proect_1.Services
{
    public class DetailsService: IDetailsService
    {
        private List<Entity> _entityList;
        private int _id = 1;
        private int _failCount = 0;
        public DetailsService()
        {
            _entityList = new List<Entity>();
        }
        public List<Entity> GetDetails(int page,int size , string sortBy)
        {
            var paginated_details=_entityList.Skip(size*(page-1)).Take(size);
            switch (sortBy)
            {
                case "Name":
                    paginated_details = paginated_details.Where(e => e.Names != null && e.Names[0].FirstName != null)
                                                         .OrderBy(e => e.Names[0].FirstName);
                    break;
                case "City":
                    paginated_details = paginated_details.Where(e => e.Addresses != null && e.Addresses[0].City != null)
                                                         .OrderBy(e => e.Addresses[0].City);
                    break;
                case "Country":
                    paginated_details = paginated_details.Where(e => e.Addresses != null && e.Addresses[0].Country != null)
                                                         .OrderBy(e => e.Addresses[0].Country);
                    break;
                case "Date":
                    paginated_details = paginated_details.Where(e=> e.Dates != null)
                                                         .OrderBy(e => e.Dates[0]._Date);
                    break;
            }
            return paginated_details.ToList();
        }
        
        public Entity GetDetailBYId(int id)
        {
            //NOTE: for testing purpose of 'Retry and Backoff Mechanism' id=10000 is reserved (and intentionaly implemented this bug).
            // to check that mechanism id=10000 is needed and look itno debug window
            if (id == 10000)
            {
                if (_failCount==4)
                {
                    return null;
                }

                _failCount++;
                throw new Exception();
            }
            if (id==15000) throw new Exception();

            var entity = _entityList.FirstOrDefault(e => e.Id == id);
            return entity;
        }

        public Entity PostDetail(Entity entity)
        {
            entity.Id = _id++;
            /*var en=_entityList.FirstOrDefault(e => e.Id==entity.Id);
            if (en != null)
            {
                return null;
            }*/

            _entityList.Add(entity);
            return entity;
        }

        public Entity UpdateDetail(int id, Entity entity)
        {
            for (int i = 0; i < _entityList.Count; i++)
            {
                if (_entityList[i].Id == id)
                {
                    _entityList[i].Names = entity.Names!=null ? entity.Names : _entityList[i].Names;
                    _entityList[i].Addresses = entity.Addresses!=null ? entity.Addresses : _entityList[i].Addresses;
                    _entityList[i].Dates = entity.Dates!=null ? entity.Dates : _entityList[i].Dates;
                    _entityList[i].Gender = entity.Gender!=null ? entity.Gender : _entityList[i].Gender;
                    _entityList[i].Deceased = entity.Deceased!=null ? entity.Deceased : _entityList[i].Deceased;
                    return _entityList[i];
                    
                }
            }
            return null;
        }

        public string DeleteDetail(int id)
        {
            for (int i = 0; i < _entityList.Count; i++)
            {
                if (_entityList[i].Id == id)
                {
                    _entityList.RemoveAt(i);
                    return id.ToString();
                }
            }

            return null;

        }

        public List<Entity> SearchQuery(string query)
        {
            var filtered_details = _entityList.Where(entity =>
                entity.Names.Any(name =>
                    name.FirstName.Contains(query,StringComparison.OrdinalIgnoreCase) ||
                    name.MiddleName.Contains(query,StringComparison.OrdinalIgnoreCase) ||
                    name.SurName.Contains(query,StringComparison.OrdinalIgnoreCase) 
                ) ||
                entity.Addresses.Any(name =>
                    name.AddressLine.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    name.City.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    name.Country.Contains(query, StringComparison.OrdinalIgnoreCase)
                )

            );
            System.Diagnostics.Debug.WriteLine(filtered_details);
            
            return filtered_details.ToList();

        }

        public List<Entity> FilterDetails(string gender, string[] countries,DateTime? startDate,DateTime? endDate)
        {
            var filtered_details = _entityList.Where((entity) =>
                (gender == null || entity.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase)) &&
                (countries == null || countries.Length == 0 || entity.Addresses.Any(address => countries.Contains(address.Country,StringComparer.OrdinalIgnoreCase))) &&
                (startDate == null || entity.Dates.Any(date => date._Date.HasValue && date._Date>=startDate)) &&
                (endDate == null || entity.Dates.Any(date => date._Date.HasValue && date._Date<=endDate))
                );

            return filtered_details.ToList();
        }
    }
}
