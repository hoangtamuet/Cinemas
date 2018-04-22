using Cinemas.Modules.MFilm;
using Cinemas.Modules.MUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MFilm
{
    public interface IFilmService
    {
        /// <summary>
        /// Đếm số lượng Film
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SearchFilmEntity"></param>
        /// <returns></returns>
        int Count(UserEntity UserEntity, SearchFilmEntity SearchFilmEntity);
        /// <summary>
        /// Lọc Film dựa trên điều kiện search
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SearchFilmEntity"></param>
        /// <returns></returns>
        List<FilmEntity> Gets(UserEntity UserEntity, SearchFilmEntity SearchFilmEntity);
        /// <summary>
        /// Lọc Film theo FilmId
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="FilmId"></param>
        /// <returns></returns>
        FilmEntity GetId(UserEntity UserEntity, int FilmId);
        /// <summary>
        /// Tạo Film mới 
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="FilmEntity"></param>
        /// <returns></returns>
        FilmEntity Create(UserEntity UserEntity, FilmEntity FilmEntity);
        /// <summary>
        /// Cập nhật Film theo Id
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="FilmId"></param>
        /// <param name="FilmEntity"></param>
        /// <returns></returns>
        FilmEntity Update(UserEntity UserEntity, int FilmId, FilmEntity FilmEntity);
        /// <summary>
        /// Xóa Film theo Id
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="FilmId"></param>
        /// <returns></returns>
        bool Delete(UserEntity UserEntity, int FilmId);
    }
}