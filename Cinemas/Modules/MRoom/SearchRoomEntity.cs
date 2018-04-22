using Cinemas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MRoom
{
    public class SearchRoomEntity : FilterEntity
    {
        public int? Id { get; set; }
        public int? CineplexId { get; set; }
        public string Name { get; set; }
        public string Rank { get; set; }
        /// <summary>
        /// Lọc thông tin Room theo điều kiện search 
        /// </summary>
        /// <param name="Rooms"></param>
        /// <returns></returns>
        public IQueryable<Room> ApplyTo(IQueryable<Room> Rooms)
        {
            // Lọc Room theo RoomId 
            if (Id.HasValue)
            {
                Rooms = Rooms.Where(c => c.Id.Equals(Id.Value));
            }
            // Lọc Room theo việc search 1 hay một số kí tự trong Name
            if (!string.IsNullOrEmpty(Name))
            {
                Rooms = Rooms.Where(c => c.Name.Contains(Name));
            }
            // Lọc Room theo CineplexId
            if (CineplexId.HasValue)
            {
                Rooms = Rooms.Where(c => c.CineplexId.Equals(CineplexId.Value));
            }
            // Lọc Room theo việc search 1 hay một số kí tự trong rank
            if (!string.IsNullOrEmpty(Rank))
            {
                Rooms = Rooms.Where(c => c.Rank.Contains(Rank));
            }
            // Sắp xếp Room có Name thứ tự alphabe
            Rooms = Rooms.OrderBy(r => r.Name);
            return Rooms;
        }
    }
}