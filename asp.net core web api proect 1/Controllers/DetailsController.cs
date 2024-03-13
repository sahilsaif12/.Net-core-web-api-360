using asp.net_core_web_api_proect_1.Misc;
using asp.net_core_web_api_proect_1.Models;
using asp.net_core_web_api_proect_1.Services;

//using asp.net_core_web_api_proect_1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asp.net_core_web_api_proect_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        public class DetailsController : ControllerBase
    {
        private IDetailsService _detailService;

        [ActivatorUtilitiesConstructor]
        public DetailsController(IDetailsService services) {
            _detailService= services;

        }


        ///<remarks>Get all entity details</remarks>
        /// <param name="page" >Page number (default is 1)</param>
        /// <param name="size"  >Number of entity in each page (default is 10) </param>
        /// <param name="sortBy"  >Sort by these fields </param>

        [HttpGet]
        public ActionResult<List<Entity>> getDetails(int page=1,int size=10,SortBy? sortBy=null)
        {
            return Ok(_detailService.GetDetails(page, size, sortBy.ToString()));
        }

        ///<summary>Get single entity detail by id</summary>
        ///<remarks>NOTE: for testing purpose of 'Retry and Backoff Mechanism' id=10000 is reserved (and intentionaly implemented this bug). to check that mechanism type 10000 to id and look itno debug window </remarks>

        [HttpGet("{id}")]
        public ActionResult<Entity> getDetailsById(int id) 
        {
            var entity = _detailService.GetDetailBYId(id);
            if (entity==null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        ///<remarks>No need to assign any value to id , we will handle it. Also You can remove those fields from body which you don't want to fill any data</remarks>
        [HttpPost]
        public ActionResult<Entity>  postDetails([FromBody] Entity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var detail = _detailService.PostDetail(entity);
            if (detail==null)
            {
                return StatusCode(StatusCodes.Status403Forbidden,"Id already exist");
            }
            return Ok(detail);
        }

        ///<remarks>For updating details via swagger please keep only those fileds you want to update , otherwise the default swagger body text will override the the data which is unwanted</remarks>
        [HttpPut("{id}")]
        public ActionResult<Entity> updateDetail(int id, [FromBody] Entity entity)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var detail = _detailService.UpdateDetail(id,entity);
            if (detail == null)
            {
                return NotFound();
            }
            return Ok(detail);
        }

        ///<remarks>Delete an entity by providing the entity id</remarks>
        [HttpDelete("{id}")]
        public ActionResult deleteDetail(int id)
        {
            var detail = _detailService.DeleteDetail(id);
            if (detail == null)
            {
                return NotFound();
            }
            return Ok(id + " deleted");
        }

        ///<remarks>Search entity data by firstname, middle name ,surname, address line , country ,city.</remarks>
        [HttpGet("search")]
        public ActionResult<List<Entity>> searchQuery(string query)
        {
            return Ok(_detailService.SearchQuery(query));
        }


        ///<remarks>Get users details with optional filters.</remarks>
        /// <param name="startDate"> Format: yyyy-mm-dd </param> 
        /// <param name="endDate" > Format: yyyy-mm-dd </param>
        ///<param name="gender" > select your gender</param>
        /// <param name="countries" >can add multiple countries </param>
        [HttpGet("filter")]
        public ActionResult<List<Entity>> filterDetails(Gender? gender=null, [FromQuery] string[]? countries=null, DateTime? startDate=null,DateTime? endDate= null)
        {
            return Ok(_detailService.FilterDetails(gender.ToString(),countries,startDate,endDate));
        }
        
    }
}

