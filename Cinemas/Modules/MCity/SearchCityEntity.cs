using Cinemas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MCity
{
    public class SearchCityEntity : FilterEntity
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Lọc City theo điều kiện search
        /// </summary>
        /// <param name="Cities"></param>
        /// <returns></returns>
        public IQueryable<City> ApplyTo(IQueryable<City> Cities)
        {
            // Lọc City theo Id
            if (Id.HasValue)
                Cities = Cities.Where(c => c.Id.Equals(Id.Value));
            // Lọc City theo tên chứa một hay một số kí tự 
            if (!string.IsNullOrEmpty(Name))
                Cities = Cities.Where(c => c.Name.Contains(Name));
            // Lọc City theo thứ tự alphabe
            if (string.IsNullOrEmpty(OrderBy))
                Cities = Cities.OrderBy(c => c.Name);

            return Cities;
        }
    }
}