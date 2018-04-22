using Cinemas.Models;
using Cinemas.Modules.MUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace Cinemas.Modules.MShowtime
{
    public class ShowtimeService : CommonService, IShowtimeService
    {
        /// <summary>
        /// Đếm số lượng Showtime
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SearchShowtimeEntity"></param>
        /// <returns></returns>
        public int Count(UserEntity UserEntity, SearchShowtimeEntity SearchShowtimeEntity)
        {
            if (SearchShowtimeEntity == null) SearchShowtimeEntity = new SearchShowtimeEntity();
            IQueryable<Showtime> Showtimes = CinemasEntities.Showtimes;
            Showtimes = SearchShowtimeEntity.ApplyTo(Showtimes);
            return Showtimes.Count();
        }
        /// <summary>
        /// Lọc thông tin Showtime theo điều kiện search
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="SearchShowtimeEntity"></param>
        /// <returns></returns>
        public List<ShowtimeEntity> Gets(UserEntity UserEntity, SearchShowtimeEntity SearchShowtimeEntity)
        {
            if (SearchShowtimeEntity == null) SearchShowtimeEntity = new SearchShowtimeEntity();
            IQueryable<Showtime> Showtimes = CinemasEntities.Showtimes.Include(st => st.Room).Include(st => st.Film);
            Showtimes = SearchShowtimeEntity.ApplyTo(Showtimes);
            Showtimes = SearchShowtimeEntity.SkipAndTake(Showtimes);
            return Showtimes.ToList().Select(st => new ShowtimeEntity(st, st.Film, st.Room)).ToList();
        }
        /// <summary>
        /// Lọc thông tin Showtime theo ShowtimeId
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="ShowtimeId"></param>
        /// <returns></returns>
        public ShowtimeEntity GetId(UserEntity UserEntity, int ShowtimeId)
        {
            IQueryable<Showtime> Showtimes = CinemasEntities.Showtimes;
            Showtimes = Showtimes.Where(c => c.Id.Equals(ShowtimeId));
            return new ShowtimeEntity(Showtimes.FirstOrDefault());
        }
        /// <summary>
        /// Tạo Showtime mới 
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="ShowtimeEntity"></param>
        /// <returns></returns>
        public ShowtimeEntity Create(UserEntity UserEntity, ShowtimeEntity ShowtimeEntity)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                try
                {
                    Showtime Showtime = new Showtime();
                    Showtime = ShowtimeEntity.ToModel(Showtime);
                    CinemasEntities.Showtimes.Add(Showtime);
                    CinemasEntities.SaveChanges();
                    ShowtimeEntity.Id = Showtime.Id;
                    transaction.Commit();
                    return ShowtimeEntity;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không tạo được Showtime mới");
                }
            }
            return null;
        }
        /// <summary>
        /// Cập nhật Showtime theo Id
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="ShowtimeId"></param>
        /// <param name="ShowtimeEntity"></param>
        /// <returns></returns>
        public ShowtimeEntity Update(UserEntity UserEntity, int ShowtimeId, ShowtimeEntity ShowtimeEntity)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                try
                {
                    Showtime Showtime = CinemasEntities.Showtimes.Where(c => c.Id.Equals(ShowtimeId)).FirstOrDefault();
                    if (Showtime == null)
                        throw new BadRequestException("Không tìm thấy Showtime có Id là " + ShowtimeId);
                    Showtime = ShowtimeEntity.ToModel(Showtime);
                    CinemasEntities.SaveChanges();
                    transaction.Commit();
                    return ShowtimeEntity;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không cập nhập được  Showtime");
                }
            }
        }
        /// <summary>
        /// Xóa Showtime theo Id
        /// </summary>
        /// <param name="UserEntity"></param>
        /// <param name="ShowtimeId"></param>
        /// <returns></returns>
        public bool Delete(UserEntity UserEntity, int ShowtimeId)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                try
                {
                    Showtime Showtime = CinemasEntities.Showtimes.Where(c => c.Id.Equals(ShowtimeId)).FirstOrDefault();
                    if (Showtime == null)
                        throw new BadRequestException("Không tìm thấy Showtime có Id là " + ShowtimeId);
                    CinemasEntities.Showtimes.Remove(Showtime);
                    CinemasEntities.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không xóa được Showtime");
                }
            }
        }
    }
}