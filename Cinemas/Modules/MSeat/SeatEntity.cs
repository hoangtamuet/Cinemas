using Cinemas.Models;
using Cinemas.Modules.MOrder;
using Cinemas.Modules.MRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MSeat
{
    public class SeatEntity
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string Name { get; set; }
        public RoomEntity RoomEntity { get; private set; }
        public List<OrderEntity> OrderEntities { get; private set; }

        public SeatEntity()
        {}
        
        public SeatEntity(Seat Seat, params object[] args)
        {
            this.Id = Seat.Id;
            this.Name = Seat.Name;

            foreach(var arg in args)
            {
                // Seat tham chiếu tới Room
                if(arg is Room)
                {
                    this.RoomEntity = new RoomEntity(arg as Room);
                }
               
            }
        }
        public Seat ToModel(Seat Seat)
        {
            if (Seat == null) return null;
            Seat.Name = this.Name;
            Seat.RoomId = this.RoomId;
            return Seat;
        }
    }
}