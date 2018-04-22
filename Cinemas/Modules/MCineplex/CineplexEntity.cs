using Cinemas.Models;
using Cinemas.Modules.MCity;
using Cinemas.Modules.MRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MCineplex
{
    public class CineplexEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }

        public CityEntity CityEntity { get; set; }
        public List<RoomEntity> RoomEntities { get; set; }

        public CineplexEntity()
        { }

        public CineplexEntity(Cineplex Cineplex, params object[] args)
        {
            this.Id = Cineplex.Id;
            this.Name = Cineplex.Name;
            this.CityId = Cineplex.CityId;
            
            foreach(var arg in args)
            {
                // Cineplex tham chiếu tới City
                if (arg is City)
                {
                    this.CityEntity = new CityEntity(arg as City);
                }
                // Cineplex được Room tham chiếu tới
                if (arg is ICollection<Room>)
                {
                    this.RoomEntities = new List<RoomEntity>();
                    ICollection<Room> Rooms = arg as ICollection<Room>;
                    foreach(var Room in Rooms)
                    {
                        this.RoomEntities.Add(new RoomEntity(Room));
                    }
                }
            }
        }

        public Cineplex ToModel(Cineplex Cineplex)
        {
            if (Cineplex == null) return null;
            Cineplex.Name = this.Name;
            Cineplex.CityId = this.CityId;
            return Cineplex;
        }
    }
}