using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cinemas.Modules.MUser;
using Cinemas.Models;

namespace Cinemas.Modules.MCategory
{
    public class CategoryService : CommonService, ICategoryService
    {
        public CategoryService():base() { }
        // Đếm tổng số lượng Category
        public int Count(UserEntity UserEntity, SearchCategoryEntity SearchCategoryEntity)
        {
            if (SearchCategoryEntity == null) SearchCategoryEntity = new SearchCategoryEntity();
            IQueryable<Category> Categories = CinemasEntities.Categories;
            Categories = SearchCategoryEntity.ApplyTo(Categories);
            return Categories.Count();
        }
        // Lấy các Category theo điều kiện search
        public List<CategoryEntity> Gets(UserEntity UserEntity, SearchCategoryEntity SearchCategoryEntity)
        {
            if (SearchCategoryEntity == null) SearchCategoryEntity = new SearchCategoryEntity();
            IQueryable<Category> Categories = CinemasEntities.Categories;
            Categories = SearchCategoryEntity.ApplyTo(Categories);
            Categories = SearchCategoryEntity.SkipAndTake(Categories);
            return Categories.ToList().Select(c => new CategoryEntity(c)).ToList();
        }
        // Lấy Category theo Id
        public CategoryEntity GetId(UserEntity UserEntity, int CategoryId)
        {
            IQueryable<Category> Categories = CinemasEntities.Categories;
            Categories = Categories.Where(c => c.Id.Equals(CategoryId));
            return new CategoryEntity(Categories.FirstOrDefault());
        }
        // Tạo Category mới 
        public CategoryEntity Create(UserEntity UserEntity, CategoryEntity CategoryEntity)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                // Khởi tạo Category, đưa tên Category vào database, thực hiện Add Category, lưu thay đổi và cập nhật CategoryId mới.
                try
                {
                    Category Category = new Category();
                    Category = CategoryEntity.ToModel(Category);
                    CinemasEntities.Categories.Add(Category);
                    CinemasEntities.SaveChanges();
                    CategoryEntity.Id = Category.Id;
                    transaction.Commit();
                    return CategoryEntity;
                }
                // Xử lí ngoại lệ
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không tạo được Category mới");
                }
            }
        }
        // Cập nhật Category theo Id
        public CategoryEntity Update(UserEntity UserEntity, int CategoryId, CategoryEntity CategoryEntity)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                try
                {
                    Category Category = CinemasEntities.Categories.Where(c => c.Id.Equals(CategoryId)).FirstOrDefault();
                    if (Category == null)
                        throw new BadRequestException("Không tìm thấy Category có Id là " + CategoryId);
                    Category = CategoryEntity.ToModel(Category);
                    CinemasEntities.SaveChanges();
                    transaction.Commit();
                    return CategoryEntity;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không cập nhật được Category");
                }
            }
        }
        // Xóa Category theo Id
        public bool Delete(UserEntity UserEntity,int  CategoryId)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                try
                {
                    Category Category = CinemasEntities.Categories.Where(c => c.Id.Equals(CategoryId)).FirstOrDefault();
                    if (Category == null)
                        throw new BadRequestException("Không tìm thấy Category có Id là " + CategoryId);
                    CinemasEntities.Categories.Remove(Category);
                    CinemasEntities.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không xóa được Category");
                }
            }
        }
    }
}