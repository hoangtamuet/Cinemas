using Cinemas.Modules.MUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinemas.Modules.MCity
{
    public interface ICityService
    {
        /// <summary>
        /// Đếm số lượng City
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SearchCityEntity"></param>
        /// <returns></returns>
        int Count(UserEntity UserEntity, SearchCityEntity SearchCityEntity);
        /// <summary>
        /// Lấy các City theo điều kiện search
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SearchCityEntity"></param>
        /// <returns></returns>
        List<CityEntity> Gets(UserEntity UserEntity, SearchCityEntity SearchCityEntity);
        /// <summary>
        /// Lấy các City theo Id
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="CityId"></param>
        /// <returns></returns>
        CityEntity GetId(UserEntity UserEntity, int CityId);
        /// <summary>
        /// Tạo City mới
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="CityEntity"></param>
        /// <returns></returns>
        CityEntity Create(UserEntity UserEntity, CityEntity CityEntity);
        /// <summary>
        /// Cập nhật City theo Id
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="CityId"></param>
        /// <param name="CityEntity"></param>
        /// <returns></returns>
        CityEntity Update(UserEntity UserEntity, int CityId, CityEntity CityEntity);
        /// <summary>
        /// Xóa City theo Id
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="CityId"></param>
        /// <returns></returns>
        bool Delete(UserEntity UserEntity, int CityId);
    }
}
