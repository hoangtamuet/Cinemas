using Cinemas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MCineplex
{
    public class SearchCineplexEntity : FilterEntity
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? CityId { get; set; }
        /// <summary>
        /// Lọc Cineplex theo điều kiện search
        /// </summary>
        /// <param name="Cineplexes"></param>
        /// <returns></returns>
        public IQueryable<Cineplex> ApplyTo (IQueryable<Cineplex> Cineplexes)
        {
            // Lọc Cineplex theo Id
            if(Id.HasValue)
            {
                Cineplexes = Cineplexes.Where(c => c.Id.Equals(Id.Value));
            }
            // Lọc Cineplex theo điều kiện tên chứa một hay một số phần tử
            if(!string.IsNullOrEmpty(Name))
            {
                Cineplexes = Cineplexes.Where(c => c.Name.Contains(Name));
            }
            // Lọc Cineplex theo CityId
            if(CityId.HasValue)
            {
                Cineplexes = Cineplexes.Where(c => c.CityId.Equals(CityId.Value));
            }
            // Lọc Cineplex theo thứ tự Alphabe
            Cineplexes = Cineplexes.OrderBy(c => c.Name);
            return Cineplexes;
        }
    }
}