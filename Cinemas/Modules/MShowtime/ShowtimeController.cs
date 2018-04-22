using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Cinemas.Modules.MShowtime
{
    [RoutePrefix("api/Showtimes")]
    public class ShowtimeController : CommonController
    {
        private IShowtimeService ShowtimeService;
        public ShowtimeController(IShowtimeService ShowtimeService)
        {
            this.ShowtimeService = ShowtimeService;
        }
        [HttpGet, Route("Count")]
        public int Count([FromUri] SearchShowtimeEntity SearchShowtimeEntity)
        {
            return ShowtimeService.Count(UserEntity, SearchShowtimeEntity);
        }

        [HttpGet, Route("")]
        public List<ShowtimeEntity> Gets([FromUri] SearchShowtimeEntity SearchShowtimerEntity)
        {
            return ShowtimeService.Gets(UserEntity, SearchShowtimerEntity);
        }

        [HttpGet, Route("ShowtimeId")]
        public ShowtimeEntity GetId([FromBody] int ShowtimeId)
        {
            return ShowtimeService.GetId(UserEntity, ShowtimeId);
        }

        [HttpPost, Route("")]
        public ShowtimeEntity Create([FromBody] ShowtimeEntity ShowtimeEntity)
        {
            return ShowtimeService.Create(UserEntity, ShowtimeEntity);
        }

        [HttpPut, Route("{ShowtimeId}")]
        public ShowtimeEntity Update([FromUri] int ShowtimeId, [FromBody] ShowtimeEntity ShowtimeEntity)
        {
            return ShowtimeService.Update(UserEntity, ShowtimeId, ShowtimeEntity);
        }

        [HttpDelete, Route("{ShowtimeId}")]
        public bool Delete([FromUri] int ShowtimeId)
        {
            return ShowtimeService.Delete(UserEntity, ShowtimeId);
        }
    }
}