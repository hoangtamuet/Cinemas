using Cinemas.Modules.MUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MCineplex
{
    public interface ICineplexService
    {
        /// <summary>
        /// Đếm tổng số lượng Cineplex
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SearchCineplexEntity"></param>
        /// <returns></returns>
        int Count(UserEntity UserEntity, SearchCineplexEntity SearchCineplexEntity);
        /// <summary>
        /// Lấy các Cineplex theo điều kiện search
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SearchCineplexEntity"></param>
        /// <returns></returns>
        List<CineplexEntity> Gets(UserEntity UserEntity, SearchCineplexEntity SearchCineplexEntity);
        /// <summary>
        /// Lấy các Cineplex theo Id
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="CineplexId"></param>
        /// <returns></returns>
        CineplexEntity GetId(UserEntity UserEntity, int CineplexId);
        /// <summary>
        /// Tạo Cinepplex mới 
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="CineplexEntity"></param>
        /// <returns></returns>
        CineplexEntity Create(UserEntity UserEntity, CineplexEntity CineplexEntity);
        /// <summary>
        /// Cập nhật Cineplex theo Id
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="CinplexId"></param>
        /// <param name="CineplexEntity"></param>
        /// <returns></returns>
        CineplexEntity Update(UserEntity UserEntity, int CinplexId, CineplexEntity CineplexEntity);
        /// <summary>
        /// Xóa Cineplex theo Id
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="CineplexId"></param>
        /// <returns></returns>
        bool Delete(UserEntity UserEntity, int CineplexId);
    }
}