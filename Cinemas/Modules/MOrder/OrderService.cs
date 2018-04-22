using Cinemas.Models;
using Cinemas.Modules.MUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Cinemas.Modules.MOrder
{
    public class OrderService : CommonService, IOrderService
    {
        public OrderService() : base() { }
        public int Count(UserEntity UserEntity, SearchOrderEntity SearchOrderEntity)
        {
            if (SearchOrderEntity == null) SearchOrderEntity = new SearchOrderEntity();
            IQueryable<Order> Orders = CinemasEntities.Orders;
            Orders = SearchOrderEntity.ApplyTo(Orders);
            return Orders.Count();
        }

        public List<OrderEntity> Gets(UserEntity UserEntity, SearchOrderEntity SearchOrderEntity)
        {
            if (SearchOrderEntity == null) SearchOrderEntity = new SearchOrderEntity();
            IQueryable<Order> Orders = CinemasEntities.Orders
                .Include(o => o.Seats.Select(s => s.Room))
                .Include(o => o.Showtime.Film)
                .Include(o => o.Showtime.Room);
            Orders = SearchOrderEntity.ApplyTo(Orders);
            Orders = SearchOrderEntity.SkipAndTake(Orders);
            return Orders.ToList().Select(c => new OrderEntity(c, c.Showtime, c.Seats)).ToList();
        }
        public OrderEntity GetId(UserEntity UserEntity, int OrderId)
        {
            Order Order = CinemasEntities.Orders.Where(o => o.Id == OrderId)
                .Include(o => o.Seats.Select(s => s.Room))
                .Include(o => o.Showtime.Film)
                .Include(o => o.Showtime.Room)
                .FirstOrDefault();
            return new OrderEntity(Order, Order.Showtime, Order.Seats);
        }
        public OrderEntity Create(UserEntity UserEntity, OrderEntity OrderEntity)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                try
                {
                    Order Order = new Order();
                    Order = OrderEntity.ToModel(Order);
                    CinemasEntities.Orders.Add(Order);
                    CinemasEntities.SaveChanges();
                    OrderEntity.Id = Order.Id;
                    transaction.Commit();
                    return GetId(UserEntity, OrderEntity.Id);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không tạo được Order mới");
                }
            }
            return null;
        }
        public OrderEntity Update(UserEntity UserEntity, int OrderId, OrderEntity OrderEntity)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                try
                {
                    Order Order = CinemasEntities.Orders.Where(c => c.Id.Equals(OrderId)).FirstOrDefault();
                    if (Order == null)
                        throw new BadRequestException("Không tìm thấy Order có Id là " + OrderId);
                    Order = OrderEntity.ToModel(Order);
                    CinemasEntities.SaveChanges();
                    transaction.Commit();
                    return GetId(UserEntity, OrderEntity.Id);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không cập nhật được Order");
                }
            }
        }

        public bool AddSeat(UserEntity UserEntity, int OrderId, int SeatId)
        {
            Order Order = CinemasEntities.Orders.Where(o => o.Id == OrderId).FirstOrDefault();
            Seat Seat = CinemasEntities.Seats.Where(s => s.Id == SeatId).FirstOrDefault();
            if (Order != null && Seat != null)
            {
                Order.Seats.Add(Seat);
                CinemasEntities.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool DeleteSeat(UserEntity UserEntity, int OrderId, int SeatId)
        {
            Order Order = CinemasEntities.Orders.Where(o => o.Id == OrderId).FirstOrDefault();
            Seat Seat = CinemasEntities.Seats.Where(s => s.Id == SeatId).FirstOrDefault();
            if (Order != null && Seat != null)
            {
                Order.Seats.Remove(Seat);
                CinemasEntities.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool Delete(UserEntity UserEntity, int OrderId)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                try
                {
                    Order Order = CinemasEntities.Orders.Where(c => c.Id.Equals(OrderId)).FirstOrDefault();
                    if (Order == null)
                        throw new BadRequestException("Không tìm thấy Order có Id là " + OrderId);
                    CinemasEntities.Orders.Remove(Order);
                    CinemasEntities.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không xóa được Order");
                }
            }
        }
    }
}