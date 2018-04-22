using Cinemas.Modules.MUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MOrder
{
    public interface IOrderService
    {
        /// <summary>
        /// Đếm số lượng Order 
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SearchOrderEntity"></param>
        /// <returns></returns>
        int Count(UserEntity UserEntity, SearchOrderEntity SearchOrderEntity);
        /// <summary>
        /// Lọc Order dựa trên điều kiện search
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SearchOrderEntity"></param>
        /// <returns></returns>
        List<OrderEntity> Gets(UserEntity UserEntity, SearchOrderEntity SearchOrderEntity);
        /// <summary>
        /// Lọc Order dựa trên Id
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        OrderEntity GetId(UserEntity UserEntity, int OrderId);
        /// <summary>
        /// Tạo Order mới 
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="OrderEntity"></param>
        /// <returns></returns>
        OrderEntity Create(UserEntity UserEntity, OrderEntity OrderEntity);
        /// <summary>
        /// Cập nhật Order theo Id
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="CategoryId"></param>
        /// <param name="OrderEntity"></param>
        /// <returns></returns>
        OrderEntity Update(UserEntity UserEntity, int CategoryId, OrderEntity OrderEntity);
        /// <summary>
        /// Xóa Order theo Id
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        bool Delete(UserEntity UserEntity, int OrderId);

        bool AddSeat(UserEntity UserEntity, int OrderId, int SeatId);
        bool DeleteSeat(UserEntity UserEntity, int OrderId, int SeatId);
    }
}