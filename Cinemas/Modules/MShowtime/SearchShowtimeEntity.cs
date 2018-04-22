using Cinemas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MShowtime
{
    public class SearchShowtimeEntity : FilterEntity
    {
        public int? Id { get; set; }
        public int? FilmId { get; set; }
        public int? RoomId { get; set; }
        public System.DateTime? Time { get; set; }
        /// <summary>
        /// Lọc thông tin Showtime theo điều kiện search
        /// </summary>
        /// <param name="Showtimes"></param>
        /// <returns></returns>
        public IQueryable<Showtime> ApplyTo(IQueryable<Showtime> Showtimes)
        {
            // Lọc theo ShowtimeId
            if (Id.HasValue)
            {
                Showtimes = Showtimes.Where(c => c.Id.Equals(Id.Value));
            }
            //Lọc theo FilmId
            if (FilmId.HasValue)
            {
                Showtimes = Showtimes.Where(c => c.FilmId.Equals(FilmId.Value));
            }
            //Lọc theo RoomId
            if (RoomId.HasValue)
            {
                Showtimes = Showtimes.Where(c => c.RoomId.Equals(RoomId.Value));
            }
            // Lọc theo Time
            if (Time.HasValue)
            {
                Showtimes = Showtimes.Where(c => c.Time.Equals(Time.Value));
            }
            // Sắp xếp Showtime theo thứ tự alphabe
            Showtimes = Showtimes.OrderBy(c => c.Time);
            return Showtimes;
        }


    }
}