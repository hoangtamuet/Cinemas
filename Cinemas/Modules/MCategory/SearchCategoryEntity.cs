using Cinemas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MCategory
{
    public class SearchCategoryEntity : FilterEntity
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public SearchCategoryEntity()
        {
            Skip = 0;
            Take = 10;
        }
        /// <summary>
        /// Lọc Categories theo điều kiện search
        /// </summary>
        /// <param name="Categories"></param>
        /// <returns></returns>
        public IQueryable<Category> ApplyTo(IQueryable<Category> Categories)
        {
            // Lọc Categories có Id cho sẵn
            if (Id.HasValue)
            {
                Categories = Categories.Where(c => c.Id.Equals(Id.Value));
            }
            // Lọc Categories theo điều kiện search tên có chứa 1 hay nhiều phần tử
            if (!string.IsNullOrEmpty(Name))
            {
                Categories = Categories.Where(c => c.Name.Contains(Name));
            }
            // Sắp xếp Categories theo thứ tự alphabe
            Categories = Categories.OrderBy(c => c.Name);
            return Categories;
        }
    }
}