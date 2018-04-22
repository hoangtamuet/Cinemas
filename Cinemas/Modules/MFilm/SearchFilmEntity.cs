using Cinemas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MFilm
{
    public class SearchFilmEntity : FilterEntity
    {
        public int? Id { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public int? Duration { get; set; }
        public string Description { get; set; }
        public System.DateTime? StartDate { get; set; }
        public System.DateTime? EndDate { get; set; }

        public IQueryable<Film> ApplyTo(IQueryable<Film> Films)
        {
            if(Id.HasValue)
            {
                Films = Films.Where(c => c.Id.Equals(Id.Value));
            }
            if(CategoryId.HasValue)
            {
                Films = Films.Where(C => C.CategoryId.Equals(CategoryId.Value));
            
            }
            if(!string.IsNullOrEmpty(Name))
            {
                Films = Films.Where(c => c.Name.Contains(Name));
            }
            if(Duration.HasValue)
            {
                Films = Films.Where(c => c.Duration.Equals(Duration.Value));
            }
            if(!string.IsNullOrEmpty(Description))
            {
                Films = Films.Where(c => c.Description.Contains(Description));
            }
            if(StartDate.HasValue)
            {
                Films = Films.Where(C => C.StartDate.Equals(StartDate.Value));
            }
            if (EndDate.HasValue)
            {
                Films = Films.Where(C => C.EndDate.Equals(EndDate.Value));
            }

            Films = Films.OrderBy(c => c.Name);
            return Films;

        }
    }
}