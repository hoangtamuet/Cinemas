using Cinemas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MOrder
{
    public class SearchOrderEntity : FilterEntity
    {
        public int? Id { get; set; }
        public int? ShowtimeId { get; set; }
        public int? Price { get; set; }
        /// <summary>
        /// Lọc Order theo điều kiện search
        /// </summary>
        /// <param name="Orders"></param>
        /// <returns></returns>
        public IQueryable<Order> ApplyTo(IQueryable<Order> Orders)
        {
            if(Id.HasValue)
            {
                Orders = Orders.Where(c => c.Id.Equals(Id.Value));
            }
            if (ShowtimeId.HasValue)
            {
                Orders = Orders.Where(c => c.ShowtimeId.Equals(ShowtimeId.Value));
            }
            if (Price.HasValue)
            {
                Orders = Orders.Where(c => c.Price.Equals(Price.Value));
            }
            Orders = Orders.OrderBy(c => c.Price);
            return Orders;
        }

    }
}