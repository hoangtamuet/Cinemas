using Cinemas.Modules.MUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MShowtime
{
    public interface IShowtimeService
    {
        /// <summary>
        /// Đếm số Showtime
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SearchShowtimeEntity"></param>
        /// <returns></returns>
        int Count(UserEntity UserEntity, SearchShowtimeEntity SearchShowtimeEntity);
        /// <summary>
        /// Lọc Showtime theo điều kiện search
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SearchShowtimeEntity"></param>
        /// <returns></returns>
        List<ShowtimeEntity> Gets(UserEntity UserEntity, SearchShowtimeEntity SearchShowtimeEntity);
        /// <summary>
        /// Lọc Showtime theo ShowtimeId
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="ShowtimeId"></param>
        /// <returns></returns>
        ShowtimeEntity GetId(UserEntity UserEntity, int ShowtimeId);
        /// <summary>
        /// Tạo Showtime mới
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="ShowtimeEntity"></param>
        /// <returns></returns>
        ShowtimeEntity Create(UserEntity UserEntity, ShowtimeEntity ShowtimeEntity);
        /// <summary>
        /// Cập nhật Showtime theo ShowtimeId
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="ShowtimeId"></param>
        /// <param name="howtimeEntity"></param>
        /// <returns></returns>
        ShowtimeEntity Update(UserEntity UserEntity, int ShowtimeId, ShowtimeEntity ShowtimeEntity);
        /// <summary>
        /// Xóa Showtime theo Id
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="ShowtimeId"></param>
        /// <returns></returns>
        bool Delete(UserEntity UserEntity, int ShowtimeId);
    }
}