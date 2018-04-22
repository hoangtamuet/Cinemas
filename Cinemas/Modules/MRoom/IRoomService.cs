using Cinemas.Modules.MUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MRoom
{
    public interface IRoomService
    {
        /// <summary>
        ///  Đếm số lượng Room
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SearchRoomEntity"></param>
        /// <returns></returns>
        int Count(UserEntity UserEntity, SearchRoomEntity SearchRoomEntity);
        /// <summary>
        /// Lọc thông tin Room dựa vào điều kiện search
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SearchRoomEntity"></param>
        /// <returns></returns>
        List<RoomEntity> Gets(UserEntity UserEntity, SearchRoomEntity SearchRoomEntity);
        /// <summary>
        /// Lọc thông tin Room dựa theo Id
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="RoomId"></param>
        /// <returns></returns>
        RoomEntity GetId(UserEntity UserEntity, int RoomId);
        /// <summary>
        /// Tạo Room mới 
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="RoomEntityy"></param>
        /// <returns></returns>
        RoomEntity Create(UserEntity UserEntity, RoomEntity RoomEntityy);
        /// <summary>
        /// Cập nhật Room theo Id
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="RoomId"></param>
        /// <param name="RoomEntity"></param>
        /// <returns></returns>
        RoomEntity Update(UserEntity UserEntity, int RoomId, RoomEntity RoomEntity);
        /// <summary>
        /// Xóa Room theo Id
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="RoomId"></param>
        /// <returns></returns>
        bool Delete(UserEntity UserEntity, int RoomId);
    }
}