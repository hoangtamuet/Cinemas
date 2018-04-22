//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Http;

//namespace Cinemas.Modules.MOrderSeat
//{
//    [RoutePrefix("api/OrderSeats")]
//    public class OrderSeatController : CommonController
//    {
//        private IOrderSeatService OrderSeatService;
//        public OrderSeatController(IOrderSeatService OrderSeatService)
//        {
//            this.OrderSeatService = OrderSeatService;
//        }

//        [HttpGet, Route("Count")]
//        public int Count([FromUri] SearchOrderSeatEntity SearchOrderSeatEntity)
//        {
//            return OrderSeatService.Count(UserEntity, SearchOrderSeatEntity);
//        }

//        [HttpGet, Route("")]
//        public List<OrderSeatEntity> Gets([FromUri] SearchOrderSeatEntity SearchOrderSeatEntity)
//        {
//            return OrderSeatService.Gets(UserEntity, SearchOrderSeatEntity);
//        }

//        [HttpGet, Route("{OrderId}")]
//        public OrderSeatEntity GetId([FromUri] int OrderId)
//        {
//            return OrderSeatService.GetId(UserEntity, OrderId);
//        }

//        [HttpPost, Route("")]
//        public OrderSeatEntity Create([FromBody] OrderSeatEntity OrderSeatEntity)
//        {
//            return OrderSeatService.Create(UserEntity, OrderSeatEntity);
//        }

//        [HttpPut, Route("{OrderId}")]
//        public OrderSeatEntity Update([FromUri] int OrderId, [FromBody] OrderSeatEntity OrderSeatEntity)
//        {
//            return OrderSeatService.Update(UserEntity, OrderId, OrderSeatEntity);
//        }

//        [HttpDelete, Route("{OrderId}")]
//        public bool Delete([FromUri] int OrderId)
//        {
//            return OrderSeatService.Delete(UserEntity, OrderId);
//        }
//    }
//}