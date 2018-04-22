using Cinemas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MSeat
{
    public class SearchSeatEntity : FilterEntity
    {
        public int? Id { get; set; }
        public int? RoomId { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Lọc thông tin Seat từ điều kiện search
        /// </summary>
        /// <param name="Seats"></param>
        /// <returns></returns>
        public IQueryable<Seat> ApplyTo(IQueryable<Seat> Seats)
        {
            // Lọc Seat có cho SeatId
            if (Id.HasValue)
            {
                Seats = Seats.Where(c => c.Id.Equals(Id.Value));
            }
            // Lọc Seat mà Name có chứa kí tự đã cho
            if (!string.IsNullOrEmpty(Name))
            {
                Seats = Seats.Where(c => c.Name.Contains(Name));
            }
            // Lọc Seat mà cho RoomId
            if (RoomId.HasValue)
            {
                Seats = Seats.Where(c => c.RoomId.Equals(RoomId.Value));
            }
            // Lọc Seat có Name theo thứ tự alphabe
            Seats = Seats.OrderBy(s => s.Name);
            return Seats;
        }
    }
}