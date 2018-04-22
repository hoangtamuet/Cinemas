using Cinemas.Modules.MUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MSeat
{
    public interface ISeatService
    {
        /// <summary>
        /// Đếm số Seat
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SearchSeatEntity"></param>
        /// <returns></returns>
        int Count(UserEntity UserEntity, SearchSeatEntity SearchSeatEntity);
        /// <summary>
        /// Lọc thông tin Seat từ điều kiện search
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SearchSeatEntity"></param>
        /// <returns></returns>
        List<SeatEntity> Gets(UserEntity UserEntity, SearchSeatEntity SearchSeatEntity);
        /// <summary>
        /// Lọc thông tin Seat từ SeatId
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SeatId"></param>
        /// <returns></returns>
        SeatEntity GetId(UserEntity UserEntity, int SeatId);
        /// <summary>
        /// Tạo Seat mới
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SeatEntity"></param>
        /// <returns></returns>
        SeatEntity Create(UserEntity UserEntity, SeatEntity SeatEntity);
        /// <summary>
        /// Cập nhật Seat dựa trên Id
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SeatId"></param>
        /// <param name="SeatEntity"></param>
        /// <returns></returns>
        SeatEntity Update(UserEntity UserEntity, int SeatId, SeatEntity SeatEntity);
        /// <summary>
        /// Xóa Seat dựa trên Id
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SeatId"></param>
        /// <returns></returns>
        bool Delete(UserEntity UserEntity, int SeatId);
    }
}