using Cinemas.Models;
using Cinemas.Modules.MUser;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MSeat
{
    public class SeatService : CommonService, ISeatService
    {

        public SeatService():base() { }
        /// <summary>
        /// Đếm số lượng Seat
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SearchSeatEntity"></param>
        /// <returns></returns>
        public int Count(UserEntity UserEntity, SearchSeatEntity SearchSeatEntity)
        {
            if (SearchSeatEntity == null) SearchSeatEntity = new SearchSeatEntity();
            IQueryable<Seat> Seats = CinemasEntities.Seats;
            Seats = SearchSeatEntity.ApplyTo(Seats);
            return Seats.Count();
        }
        /// <summary>
        /// Lọc thông tin Seat dựa trên điều kiện search
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SearchSeatEntity"></param>
        /// <returns></returns>
        public List<SeatEntity> Gets(UserEntity UserEntity, SearchSeatEntity SearchSeatEntity)
        {
            if (SearchSeatEntity == null) SearchSeatEntity = new SearchSeatEntity();
            IQueryable<Seat> Seats = CinemasEntities.Seats.Include(s => s.Room);
            Seats = SearchSeatEntity.ApplyTo(Seats);
            Seats = SearchSeatEntity.SkipAndTake(Seats);
            return Seats.ToList().Select(c => new SeatEntity(c, c.Room)).ToList();
        }
        /// <summary>
        /// Lấy thông tin Seat dựa trên Id
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SeatId"></param>
        /// <returns></returns>
        public SeatEntity GetId(UserEntity UserEntity, int SeatId)
        {
            Seat Seat = CinemasEntities.Seats.Where(s => s.Id == SeatId).FirstOrDefault();
            return new SeatEntity(Seat);
        }
        /// <summary>
        /// Tạo Seat mới
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SeatEntity"></param>
        /// <returns></returns>
        public SeatEntity Create(UserEntity UserEntity, SeatEntity SeatEntity)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                try
                {
                    Seat Seat = new Seat();
                    Seat = SeatEntity.ToModel(Seat);
                    CinemasEntities.Seats.Add(Seat);
                    CinemasEntities.SaveChanges();
                    SeatEntity.Id = Seat.Id;
                    transaction.Commit();
                    return SeatEntity;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không tạo được Seat mới");
                }
            }
            return null;
        }
        /// <summary>
        /// Cập nhật Seat theo Id
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SeatId"></param>
        /// <param name="SeatEntity"></param>
        /// <returns></returns>
        public SeatEntity Update(UserEntity UserEntity, int SeatId, SeatEntity SeatEntity)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                try
                {
                    Seat Seat = CinemasEntities.Seats.Where(c => c.Id.Equals(SeatId)).FirstOrDefault();
                    if (Seat == null)
                        throw new BadRequestException("Không tìm thấy Seat có Id là " + SeatId);
                    Seat = SeatEntity.ToModel(Seat);
                    CinemasEntities.SaveChanges();
                    transaction.Commit();
                    return SeatEntity;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không cập nhập được Seat");
                }
            }
        }
        /// <summary>
        /// Xóa Seat theo Id
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SeatId"></param>
        /// <returns></returns>
        public bool Delete(UserEntity UserEntity, int SeatId)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                try
                {
                    Seat Seat = CinemasEntities.Seats.Where(c => c.Id.Equals(SeatId)).FirstOrDefault();
                    if (Seat == null)
                        throw new BadRequestException("Không tìm thấy Seat có Id là " + SeatId);
                    CinemasEntities.Seats.Remove(Seat);
                    CinemasEntities.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không xóa được Seat");
                }
            }
        }
    }
}