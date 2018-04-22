using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cinemas.Modules.MUser;
using Cinemas.Models;
using System.Data.Entity;
namespace Cinemas.Modules.MCineplex
{
    public class CineplexService : CommonService, ICineplexService
    {
        public object CineplexId { get; private set; }

        public CineplexService():base() {}
        // Đếm tổng số lượng Cineplex
        public int Count(UserEntity UserEntity, SearchCineplexEntity SearchCineplexEntity)
        {
            if (SearchCineplexEntity == null) SearchCineplexEntity = new SearchCineplexEntity();
            IQueryable<Cineplex> Cineplexes = CinemasEntities.Cineplexes;
            Cineplexes = SearchCineplexEntity.ApplyTo(Cineplexes);
            return Cineplexes.Count();
        }
        // Lấy các Cineplex theo điều kiện search
        public List<CineplexEntity> Gets(UserEntity UserEntity, SearchCineplexEntity SearchCineplexEntity)
        {
            if (SearchCineplexEntity == null) SearchCineplexEntity = new SearchCineplexEntity();
            IQueryable<Cineplex> Cineplexes = CinemasEntities.Cineplexes.Include(c => c.City);
            Cineplexes = SearchCineplexEntity.ApplyTo(Cineplexes);
            Cineplexes = SearchCineplexEntity.SkipAndTake(Cineplexes);
            return Cineplexes.ToList().Select(c => new CineplexEntity(c, c.City)).ToList();

        }
        // Lấy các Cineplex theo Id
        public CineplexEntity GetId(UserEntity UserEntity, int CineplexId)
        {
            Cineplex Cineplex = CinemasEntities.Cineplexes.Where(c => c.Id == CineplexId).Include(c => c.City).FirstOrDefault();
            if (Cineplex == null)
                throw new BadRequestException("Không tồn tại rạp chiếu phim có Id là " + CineplexId);
            return new CineplexEntity(Cineplex, Cineplex.City);
        }
        // Tạo Cineplex mới 
        public CineplexEntity Create(UserEntity UserEntity, CineplexEntity CineplexEntity)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                try
                {
                    Cineplex Cineplex = new Cineplex();
                    Cineplex = CineplexEntity.ToModel(Cineplex);
                    CinemasEntities.Cineplexes.Add(Cineplex);
                    CinemasEntities.SaveChanges();
                    CineplexEntity.Id = Cineplex.Id;
                    transaction.Commit();
                    return GetId(UserEntity, Cineplex.Id);

                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không tạo được Cineplex.");
                }
            }
            return null; 
        }
        public CineplexEntity Update(UserEntity UserEntity, int CinplexId, CineplexEntity CineplexEntity)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                try
                {
                    Cineplex Cineplex = CinemasEntities.Cineplexes.Where(c => c.Id.Equals(CineplexId)).FirstOrDefault();
                    if ( Cineplex == null )
                        throw new BadRequestException("Không tìm thấy Cineplex có Id là " + CineplexId);
                    Cineplex = CineplexEntity.ToModel(Cineplex);
                    CinemasEntities.SaveChanges();
                    transaction.Commit();
                    return GetId(UserEntity, Cineplex.Id); ;
                   
                }
                catch( Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không cập nhật được Cineplex.");
                }
            }
        }
        public bool Delete(UserEntity UserEntity, int CineplexId)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                try
                { 
                    Cineplex Cineplex = CinemasEntities.Cineplexes.Where(c => c.Id.Equals(CineplexId)).FirstOrDefault();
                    if (Cineplex == null)
                        throw new BadRequestException("Không tìm thấy Cineplex có Id là " + CineplexId);
                    CinemasEntities.Cineplexes.Remove(Cineplex);
                    CinemasEntities.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không xóa được Cineplex.");
                }
            }
        }

       
    }
}