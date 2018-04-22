using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Cinemas.Modules.MCategory
{
    [RoutePrefix("api/Categories")]
    public class CategoryController : CommonController
    {
        private ICategoryService CategoryService;
        public CategoryController(ICategoryService CategoryService)
        {
            this.CategoryService = CategoryService;
        }

        [HttpGet, Route("Count")]
        public int Count([FromUri] SearchCategoryEntity SearchCategoryEntity)
        {
            return CategoryService.Count(UserEntity, SearchCategoryEntity);
        }

        [HttpGet, Route("")]
        public List<CategoryEntity> Gets([FromUri] SearchCategoryEntity SearchCategoryEntity)
        {
            return CategoryService.Gets(UserEntity, SearchCategoryEntity);
        }

        [HttpGet, Route("{CategoryId}")]
        public CategoryEntity GetId([FromUri] int CategoryId)
        {
            return CategoryService.GetId(UserEntity, CategoryId);
        }


        [HttpPost, Route("")]
        public CategoryEntity Create([FromBody] CategoryEntity CategoryEntity)
        {
            return CategoryService.Create(UserEntity, CategoryEntity);
        }

        [HttpPut, Route("{CategoryId}")]
        public CategoryEntity Update([FromUri] int CategoryId, [FromBody]CategoryEntity CategoryEntity)
        {
            return CategoryService.Update(UserEntity, CategoryId, CategoryEntity);
        }

        [HttpDelete, Route("{CategoryId}")]
        public bool Delete([FromUri] int CategoryId)
        {
            return CategoryService.Delete(UserEntity, CategoryId);
        }
    }
}