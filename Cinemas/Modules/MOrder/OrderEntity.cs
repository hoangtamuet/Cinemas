using Cinemas.Models;
using Cinemas.Modules.MFilm;
using Cinemas.Modules.MSeat;
using Cinemas.Modules.MShowtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MOrder
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public int ShowtimeId { get; set; }
        public int Price { get; set; }

        public ShowtimeEntity ShowtimeEntity { get; set; }
        public List<SeatEntity> SeatEntities { get; set; }

        public OrderEntity()
        { }

        public OrderEntity(Order Order, params object[] args)
        {
            this.Id = Order.Id;
            this.Price = Order.Price;

            foreach(var arg in args)
            {
                // Order tham chiếu tới Showtime
                if(arg is Showtime)
                {
                    Showtime showTime = arg as Showtime;
                    this.ShowtimeEntity = new ShowtimeEntity(showTime, showTime.Film, showTime.Room);
                }
                // Order được OrderSeat tham chiếu tới
                if(arg is ICollection<Seat>)
                {
                    this.SeatEntities = new List<SeatEntity>();
                    ICollection<Seat> Seats = arg as ICollection<Seat>;
                    foreach (var Seat in Seats)
                    {
                        this.SeatEntities.Add(new SeatEntity(Seat, Seat.Room));
                    }
                }
            }
        }
        public Order ToModel(Order Order)
        {
            if (Order == null) return null;
            Order.Price = this.Price;
            Order.ShowtimeId = this.ShowtimeId;
            return Order;
        }
    }
}