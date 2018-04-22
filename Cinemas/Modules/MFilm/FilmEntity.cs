using Cinemas.Models;
using Cinemas.Modules.MCategory;
using Cinemas.Modules.MFilm;
using Cinemas.Modules.MShowtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MFilm
{
    public class FilmEntity
    {
        private Category category;

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }

        public CategoryEntity CategoryEntity { get; set; }

        public List<ShowtimeEntity> ShowtimeEntities { get; set; }
        public FilmEntity()
        { }

        public FilmEntity(Film Film, params object[] args)
        {
            this.Id = Film.Id;
            this.Name = Film.Name;
            this.StartDate = Film.StartDate;
            this.EndDate = Film.EndDate;
            this.Duration = Film.Duration;
            this.Description = Film.Description;

            foreach (var arg in args)
            {
                // Film tham chiếu tới Category
                if (arg is Category)
                {
                    this.CategoryEntity = new CategoryEntity(arg as Category);
                }
                // Film được tham chiếu bởi Showtime
                if (arg is ICollection<Showtime>)
                {
                    this.ShowtimeEntities = new List<ShowtimeEntity>();
                    ICollection<Showtime> Showtimes = arg as ICollection<Showtime>;
                    foreach (var Showtime in Showtimes)
                    {
                        this.ShowtimeEntities.Add(new ShowtimeEntity(Showtime));

                    }
                }
            }
        }

        public Film ToModel(Film Film)
        {
            if (Film == null) return null;
            Film.Name = this.Name;
            Film.CategoryId = this.CategoryId;
            Film.StartDate = this.StartDate;
            Film.EndDate = this.EndDate;
            Film.Duration = this.Duration;
            Film.Description = this.Description;
            return Film;
        }
    }
}