using Cinemas.Models;
using Cinemas.Modules.MFilm;
using Cinemas.Modules.MOrder;
using Cinemas.Modules.MRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MShowtime
{
    public class ShowtimeEntity
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int RoomId { get; set; }
        public System.DateTime Time { get; set; }
        public FilmEntity FilmEntity { get; set; }
        public RoomEntity RoomEntity { get; set; }
        public List<OrderEntity> OrderEntities { get; set; }

        public ShowtimeEntity()
        { }
        public ShowtimeEntity(Showtime Showtime, params object[] args)
        {
            this.Id = Showtime.Id;
            this.FilmId = Showtime.FilmId;
            this.RoomId = Showtime.RoomId;
            this.Time = Showtime.Time;

            foreach (var arg in args)
            {
                // Showtime tham chiếu tới Film
                if (arg is Film)
                {
                    this.FilmEntity = new FilmEntity(arg as Film);
                }
                // Showtime tham chiếu tới Room
                if (arg is Room)
                {
                    this.RoomEntity = new RoomEntity(arg as Room);
                }
                // Showtime được Order tham chiếu tới
                if (arg is ICollection<Order>)
                {
                    this.OrderEntities = new List<OrderEntity>();
                    ICollection<Order> Orders = arg as ICollection<Order>;
                    foreach (var Order in Orders)
                    {
                        this.OrderEntities.Add(new OrderEntity(Order));

                    }
                }
            }
        }
        public Showtime ToModel(Showtime Showtime)
        {
            if (Showtime == null) return null;
            Showtime.Id = this.Id;
            Showtime.FilmId = this.FilmId;
            Showtime.RoomId = this.RoomId;
            Showtime.Time = this.Time;
            return Showtime;
        }
    }
}