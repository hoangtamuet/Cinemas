using Cinemas.Models;
using Cinemas.Modules.MCineplex;
using Cinemas.Modules.MSeat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MRoom
{
    public class RoomEntity
    {
        public int Id { get; set; }
        public int CineplexId { get; set; }
        public string Name { get; set; }
        public string Rank { get; set; }

        public CineplexEntity CineplexEntity { get; set; }

        public List<SeatEntity> SeatEntities { get; set; }

        public RoomEntity()
        { }

        public RoomEntity(Room Room, params object[] args)
        {
            this.Id = Room.Id;
            this.Name = Room.Name;
            this.Rank = Room.Rank;

            foreach(var arg in args)
            {
                // Room tham chiếu tới Cineplex
                if(arg is Cineplex)
                {
                    this.CineplexEntity = new CineplexEntity(arg as Cineplex);
                }
                // Room được tham chiếu bởi Seat 
                if(arg is ICollection<Seat>)
                {
                    this.SeatEntities = new List<SeatEntity>();
                    ICollection<Seat> Seats = arg as ICollection<Seat>;
                    foreach (var Seat in Seats)
                    {
                        this.SeatEntities.Add(new SeatEntity(Seat));
                    }
                }
            }
        }

        public Room ToModel(Room Room)
        {
            if (Room == null) return null;
            Room.Name = this.Name;
            Room.CineplexId = this.CineplexId;
            Room.Rank = this.Rank;
            return Room;
        }
    }
}