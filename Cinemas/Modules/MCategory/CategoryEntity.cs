using Cinemas.Models;
using Cinemas.Modules.MFilm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MCategory
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<FilmEntity> FilmEntities { get; set; }

        public CategoryEntity()
        { }

        public CategoryEntity(Category Category, params object[] args)
        {
            this.Id = Category.Id;
            this.Name = Category.Name;

            foreach(var arg in args)
            {
                // Category được Film tham chiếu tới 
                if(arg is ICollection<Category>)
                {
                    this.FilmEntities = new List<FilmEntity>();
                    ICollection<Film> Films = arg as ICollection<Film>;
                    foreach(var Film in Films)
                    {
                        this.FilmEntities.Add(new FilmEntity(Film));
                    }
                }
            }
        }

        public Category ToModel(Category Category)
        {
            if (Category == null)
                return null;
            Category.Name = this.Name;
            return Category;
        }
    }
}