//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using Cinemas.Modules.MUser;
//using Cinemas.Models;

//namespace Cinemas.Modules.MOrderSeat
//{
//    public class OrderSeatService : CommonService, IOrderSeatService
//    {
//        public OrderSeatService() : base() { }
//        // Đếm tổng số lượng OrderSeat
//        public int Count(UserEntity UserEntity, SearchOrderSeatEntity SearchOrderSeatEntity)
//        {
//            if (SearchOrderSeatEntity == null) SearchOrderSeatEntity = new SearchOrderSeatEntity();
//            IQueryable<OrderSeat> OrderSeats = CinemasEntities.OrderSeats;
//            OrderSeats = SearchOrderSeatEntity.ApplyTo(OrderSeats);
//            return OrderSeats.Count();
//        }
//        // Lấy các OrderSeat theo điều kiện search
//        public List<OrderSeatEntity> Gets(UserEntity UserEntity, SearchOrderSeatEntity SearchOrderSeatEntity)
//        {
//            if (SearchOrderSeatEntity == null) SearchOrderSeatEntity = new SearchOrderSeatEntity();
//            IQueryable<OrderSeat> OrderSeats = CinemasEntities.OrderSeats;
//            OrderSeats = SearchOrderSeatEntity.ApplyTo(OrderSeats);
//            OrderSeats = SearchOrderSeatEntity.SkipAndTake(OrderSeats);
//            return OrderSeats.ToList().Select(c => new OrderSeatEntity(c)).ToList();
//        }
//        // Lấy OrderSeat theo Id
//        public OrderSeatEntity GetId(UserEntity UserEntity, int OrderId)
//        {
//            IQueryable<OrderSeat> OrderSeats = CinemasEntities.OrderSeats;
//            OrderSeats = OrderSeats.Where(c => c.Id.Equals(OrderId));
//            return new OrderSeatEntity(OrderSeats.FirstOrDefault());
//        }
//        // Tạo OrderSeat mới 
//        public OrderSeatEntity Create(UserEntity UserEntity, OrderSeatEntity OrderSeatEntity)
//        {
//            using (var transaction = CinemasEntities.Database.BeginTransaction())
//            {
//                // Khởi tạo OrderSeat, đưa tên OrderSeat vào database, thực hiện Add OrderSeat, lưu thay đổi và cập nhật Id mới.
//                try
//                {
//                    OrderSeat OrderSeat = new OrderSeat();
//                    OrderSeat = OrderSeatEntity.ToModel(OrderSeat);
//                    CinemasEntities.OrderSeats.Add(OrderSeat);
//                    CinemasEntities.SaveChanges();
//                    CategoryEntity.Id = Order.Id;
//                    transaction.Commit();
//                    return OrderSeatEntity;
//                }
//                // Xử lí ngoại lệ
//                catch (Exception ex)
//                {
//                    transaction.Rollback();
//                    throw new BadRequestException("Không tạo được OrderSeat mới");
//                }
//            }
//        }
//        // Cập nhật OrderSeat theo Id
//        public OrderSeatEntity Update(UserEntity UserEntity, int CategoryId, OrderSeatEntity OrderSeatEntity)
//        {
//            using (var transaction = CinemasEntities.Database.BeginTransaction())
//            {
//                try
//                {
//                    Category Category = CinemasEntities.Categories.Where(c => c.Id.Equals(CategoryId)).FirstOrDefault();
//                    if (Category == null)
//                        throw new BadRequestException("Không tìm thấy OrderSeat có Id là " + CategoryId);
//                    Category = OrderSeatEntity.ToModel(Category);
//                    CinemasEntities.SaveChanges();
//                    transaction.Commit();
//                    return OrderSeatEntity;
//                }
//                catch (Exception ex)
//                {
//                    transaction.Rollback();
//                    throw new BadRequestException("Không cập nhật được OrderSeat");
//                }
//            }
//        }
//        // Xóa OrderSeat theo Id
//        public bool Delete(UserEntity UserEntity, int CategoryId)
//        {
//            using (var transaction = CinemasEntities.Database.BeginTransaction())
//            {
//                try
//                {
//                    Category Category = CinemasEntities.Categories.Where(c => c.Id.Equals(CategoryId)).FirstOrDefault();
//                    if (Category == null)
//                        throw new BadRequestException("Không tìm thấy Category có Id là " + CategoryId);
//                    CinemasEntities.Categories.Remove(Category);
//                    CinemasEntities.SaveChanges();
//                    transaction.Commit();
//                    return true;
//                }
//                catch (Exception ex)
//                {
//                    transaction.Rollback();
//                    throw new BadRequestException("Không xóa được Category");
//                }
//            }
//        }
//    }
//}