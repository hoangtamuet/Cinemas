using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Cinemas.Modules.MRoom
{
    [RoutePrefix("api/Rooms")]
    public class RoomController : CommonController
    {
        private IRoomService RoomService;
        public RoomController(IRoomService RoomService)
        {
            this.RoomService = RoomService;
        }

        [HttpGet, Route("Count")]
        public int Count([FromUri] SearchRoomEntity SearchRoomEntity)
        {
            return RoomService.Count(UserEntity, SearchRoomEntity);
        }
        [HttpGet, Route("")]
        public List<RoomEntity> Gets([FromUri] SearchRoomEntity SearchRoomEntity)
        {
            return RoomService.Gets(UserEntity, SearchRoomEntity);
        }
        [HttpGet, Route("RoomId")]
        public RoomEntity GetId([FromUri] int RoomId)
        {
            return RoomService.GetId(UserEntity, RoomId);
        }

        [HttpPost, Route("")]
        public RoomEntity Create([FromBody] RoomEntity RoomEntity)
        {
            return RoomService.Create(UserEntity, RoomEntity);
        }

        [HttpPut, Route("{RoomId}")]
        public RoomEntity Update([FromUri] int RoomId, [FromBody] RoomEntity RoomEntity)
        {
            return RoomService.Update(UserEntity, RoomId, RoomEntity);
        }

        [HttpDelete, Route("{RoomId}")]
        public bool Delete([FromBody] int RoomId)
        {
            return RoomService.Delete(UserEntity, RoomId);
        }
    }
}