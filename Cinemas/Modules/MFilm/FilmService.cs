using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cinemas.Modules.MUser;
using Cinemas.Models;
using Cinemas.Modules.MFilm;
using System.Data.Entity;
namespace Cinemas.Modules.MFilm
{
    public class FilmService : CommonService, IFilmService
    {
        public FilmService() : base() { }
        // Đếm tổng số lượng Film
        public int Count(UserEntity UserEntity, SearchFilmEntity SearchFilmEntity)
        {
            if (SearchFilmEntity == null) SearchFilmEntity = new SearchFilmEntity();
            IQueryable<Film> Films = CinemasEntities.Films;
            Films = SearchFilmEntity.ApplyTo(Films);
            return Films.Count();
        }
        // Lọc Film theo điều kiện search
        public List<FilmEntity> Gets(UserEntity UserEntity, SearchFilmEntity SearchFilmEntity)
        {
            if (SearchFilmEntity == null) SearchFilmEntity = new SearchFilmEntity();
            IQueryable<Film> Films = CinemasEntities.Films.Include(f => f.Category);
            Films = SearchFilmEntity.ApplyTo(Films);
            Films = SearchFilmEntity.SkipAndTake(Films);
            return Films.ToList().Select(f => new FilmEntity(f, f.Category)).ToList();
        }
        // Lọc Film theo Id
        public FilmEntity GetId(UserEntity UserEntity, int FilmId)
        {
            Film Film = CinemasEntities.Films.Include(f => f.Category).Where(f => f.Id == FilmId).FirstOrDefault();
            if (Film == null)
                throw new BadRequestException("Không tồn tại Film có Id là " + FilmId);
            return new FilmEntity(Film, Film.Category);
        }
        // Tạo Film mới 
        public FilmEntity Create(UserEntity UserEntity, FilmEntity FilmEntity)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                try
                {
                    Film Film = new Film();
                    Film = FilmEntity.ToModel(Film);
                    CinemasEntities.Films.Add(Film);
                    CinemasEntities.SaveChanges();
                    FilmEntity.Id = Film.Id;
                    transaction.Commit();
                    return GetId(UserEntity,FilmEntity.Id);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không tạo được Film mới");
                }
            }
        }
        // Cập nhật Film theo Id
        public FilmEntity Update(UserEntity UserEntity, int FilmId, FilmEntity FilmEntity)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                try
                {
                    Film Film = CinemasEntities.Films.Where(c => c.Id.Equals(FilmId)).FirstOrDefault();
                    if (Film == null)
                        throw new BadRequestException("Không tìm thấy Film có Id là " + FilmId);
                    Film = FilmEntity.ToModel(Film);
                    CinemasEntities.SaveChanges();
                    transaction.Commit();
                    return GetId(UserEntity, FilmEntity.Id);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không cập nhật được Film");
                }
            }
        }
        // Xóa Film theo Id
        public bool Delete(UserEntity UserEntity, int FilmId)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                try
                {
                    Film Film = CinemasEntities.Films.Where(c => c.Id.Equals(FilmId)).FirstOrDefault();
                    if (Film == null)
                        throw new BadRequestException("Không tìm thấy Film có Id là " + Film);
                    CinemasEntities.Films.Remove(Film);
                    CinemasEntities.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không xóa được Film");
                }
            }
        }
    }
}
