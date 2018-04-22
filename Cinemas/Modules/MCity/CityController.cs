using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cinemas.Modules.MCity
{
    [RoutePrefix("api/Cities")]
    public class CityController : CommonController
    {
        private ICityService CityService;
        public CityController(ICityService CityService)
        {
            this.CityService = CityService;
        }

        [HttpGet, Route("Count")]
        public int Count(SearchCityEntity SearchCityEntity)
        {
            return CityService.Count(UserEntity, SearchCityEntity);
        }

        [HttpGet, Route("")]
        public List<CityEntity> Gets(SearchCityEntity SearchCityEntity)
        {
            return CityService.Gets(UserEntity, SearchCityEntity);
        }


        [HttpGet, Route("{CityId}")]
        public CityEntity GetId([FromUri] int CityId)
        {
            return CityService.GetId(UserEntity, CityId);
        }

        [HttpPost, Route("")]
        public CityEntity Create([FromBody] CityEntity CityEntity)
        {
            return CityService.Create(UserEntity, CityEntity);
        }

        [HttpPut, Route("{CityId}")]
        public CityEntity Update([FromUri] int CityId, [FromBody] CityEntity CityEntity)
        {
            return CityService.Update(UserEntity, CityId, CityEntity);
        }

        [HttpDelete, Route("{CityId}")]
        public bool Delete([FromUri] int CityId)
        {
            return CityService.Delete(UserEntity, CityId);
        }
    }
}
