using Cinemas.Modules.MUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Cinemas.Modules.MCineplex
{
    [RoutePrefix("api/Cineplexes")]
    public class CineplexController: CommonController
    {
        private ICineplexService CineplexService;
        public CineplexController(ICineplexService CineplexService)
        {
            this.CineplexService = CineplexService;
        }

        [HttpGet, Route("Count")]
        public int Count([FromUri] SearchCineplexEntity SearchCineplexEntity)
        {
            return CineplexService.Count(UserEntity, SearchCineplexEntity);
        }

        [HttpGet, Route("")]
        public List<CineplexEntity> Gets([FromUri] SearchCineplexEntity SearchCineplexEntity)
        {
            return CineplexService.Gets(UserEntity, SearchCineplexEntity);
        }

        [HttpGet, Route("{CineplexId}")]
        public CineplexEntity GetId([FromUri] int CineplexId)
        {
            return CineplexService.GetId(UserEntity, CineplexId);
        }

        [HttpPost, Route("")]
        public CineplexEntity Create([FromBody] CineplexEntity CineplexEntity)
        {
            return CineplexService.Create(UserEntity, CineplexEntity);
        }

        [HttpPut, Route("{CineplexId}")]
        public CineplexEntity Update([FromUri] int CineplexId, [FromBody] CineplexEntity CineplexEntity)
        {
            return CineplexService.Update(UserEntity, CineplexId, CineplexEntity);
        }

        [HttpDelete, Route("{CineplexId}")]
        public bool Delete([FromUri] int CineplexId)
        {
            return CineplexService.Delete(UserEntity, CineplexId);
        }
    }
}