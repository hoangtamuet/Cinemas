using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Cinemas.Modules.MOrder
{
    [RoutePrefix("api/Orders")]
    public class OrderController : CommonController
    {
        private IOrderService OrderService;
        public OrderController(IOrderService OrderService)
        {
            this.OrderService = OrderService;
        }
        [HttpGet, Route("Count")]
        public int Count([FromUri] SearchOrderEntity SearchOrderEntity)
        {
            return OrderService.Count(UserEntity, SearchOrderEntity);
        }

        [HttpGet, Route("")]
        public List<OrderEntity> Gets([FromUri] SearchOrderEntity SearchOrderEntity)
        {
            return OrderService.Gets(UserEntity, SearchOrderEntity);
        }

        [HttpGet, Route("OrderId")]
        public OrderEntity GetId([FromBody] int OrderId)
        {
            return OrderService.GetId(UserEntity, OrderId);
        }

        [HttpPost, Route("")]
        public OrderEntity Create([FromBody] OrderEntity OrderEntity)
        {
            return OrderService.Create(UserEntity, OrderEntity);
        }

        [HttpPut, Route("{OrderId}")]
        public OrderEntity Update([FromUri] int OrderId, [FromBody] OrderEntity OrderEntity)
        {
            return OrderService.Update(UserEntity, OrderId, OrderEntity);
        }

        [HttpDelete, Route("{OrderId}")]
        public bool Delete([FromUri] int OrderId)
        {
            return OrderService.Delete(UserEntity, OrderId);
        }

        [HttpPut, Route("{OrderId}/Seats/{SeatId}")]
        public bool AddSeat([FromUri] int OrderId, [FromUri] int SeatId)
        {
            return OrderService.AddSeat(UserEntity, OrderId, SeatId);
        }

        [HttpDelete, Route("{OrderId}/Seats/{SeatId}")]
        public bool DeleteSeat([FromUri] int OrderId, [FromUri] int SeatId)
        {
            return OrderService.DeleteSeat(UserEntity, OrderId, SeatId);
        }
    }
}