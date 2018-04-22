using Cinemas.Modules.MUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MCategory
{
    public interface ICategoryService
    {
        /// <summary>
        /// Đếm tổng số lượng category
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SearchCategoryEntity"></param>
        /// <returns></returns>
        int Count(UserEntity UserEntity, SearchCategoryEntity SearchCategoryEntity);
        /// <summary>
        /// Lấy các category theo điều kiện search
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SearchCategoryEntity"></param>
        /// <returns></returns>
        List<CategoryEntity> Gets(UserEntity UserEntity, SearchCategoryEntity SearchCatsegoryEntity);
        /// <summary>
        /// Lấy category theo Id
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        CategoryEntity GetId(UserEntity UserEntity, int CategoryId);
        /// <summary>
        /// Tạo category mới
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="CategoryEntity"></param>
        /// <returns></returns>
        CategoryEntity Create(UserEntity UserEntity, CategoryEntity CategoryEntity);
        /// <summary>
        /// Cập nhật category
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="CategoryId"></param>
        /// <param name="CategoryEntity"></param>
        /// <returns></returns>
        CategoryEntity Update(UserEntity UserEntity, int CategoryId, CategoryEntity CategoryEntity);
        /// <summary>
        /// Xóa category
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        bool Delete(UserEntity UserEntity, int CategoryId);
    }
}