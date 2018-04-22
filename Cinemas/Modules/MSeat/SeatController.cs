using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Cinemas.Modules.MSeat
{
    [RoutePrefix("api/Seats")]
    public class SeatController : CommonController
    {
        private ISeatService SeatService;
        public SeatController(ISeatService SeatService)
        {
            this.SeatService = SeatService;
        }
        [HttpGet, Route("Count")]
        public int Count([FromUri] SearchSeatEntity SearchSeatEntity)
        {
            return SeatService.Count(UserEntity, SearchSeatEntity);
        }

        [HttpGet, Route("")]
        public List<SeatEntity> Gets([FromUri] SearchSeatEntity SearchSeatEntity)
        {
            return SeatService.Gets(UserEntity, SearchSeatEntity);
        }

        [HttpGet, Route("SeatId")]
        public SeatEntity GetId([FromBody] int SeatId)
        {
            return SeatService.GetId(UserEntity, SeatId);
        }

        [HttpPost, Route("")]
        public SeatEntity Create([FromBody] SeatEntity SeatEntity)
        {
            return SeatService.Create(UserEntity, SeatEntity);
        }

        [HttpPut, Route("{SeatId}")]
        public SeatEntity Update([FromUri] int OrderId, [FromBody] SeatEntity OrderEntity)
        {
            return SeatService.Update(UserEntity, OrderId, OrderEntity);
        }

        [HttpDelete, Route("{OrderId}")]
        public bool Delete([FromUri] int OrderId)
        {
            return SeatService.Delete(UserEntity, OrderId);
        }
    }
}