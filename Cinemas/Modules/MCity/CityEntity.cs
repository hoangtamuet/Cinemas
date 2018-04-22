using Cinemas.Models;
using Cinemas.Modules.MCineplex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MCity
{
    public class CityEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CineplexEntity> CineplexEntities { get; set; }

        public CityEntity()
        {

        }

        public CityEntity(City City, params object[] args)
        {
            this.Id = City.Id;
            this.Name = City.Name;

            foreach(var arg in args)
            {
                // Cineplex được City tham chiếu tới
                if (arg is ICollection<Cineplex>)
                {
                    ICollection<Cineplex> Cineplexes = arg as ICollection<Cineplex>;
                    CineplexEntities = new List<CineplexEntity>();
                    foreach(var Cineplex in Cineplexes)
                    {
                        CineplexEntities.Add(new CineplexEntity(Cineplex));
                    }
                }
            }
        }

        public City ToModel(City City)
        {
            if (City == null)
                return null;
            City.Name = this.Name;
            return City;
        }
    }
}