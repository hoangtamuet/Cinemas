using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cinemas.Modules.MUser;
using Cinemas.Models;

namespace Cinemas.Modules.MCity
{
    public class CityService : CommonService, ICityService
    {
        public CityService():base()
        { }

        public int Count(UserEntity UserEntity, SearchCityEntity SearchCityEntity)
        {
            if (SearchCityEntity == null) SearchCityEntity = new SearchCityEntity();
            IQueryable<City> Cities = CinemasEntities.Cities;
            Cities = SearchCityEntity.ApplyTo(Cities);
            return Cities.Count();
        }

        public List<CityEntity> Gets(UserEntity UserEntity, SearchCityEntity SearchCityEntity)
        {
            if (SearchCityEntity == null) SearchCityEntity = new SearchCityEntity();
            IQueryable<City> Cities = CinemasEntities.Cities;
            Cities = SearchCityEntity.ApplyTo(Cities);
            Cities = SearchCityEntity.SkipAndTake(Cities);
            return Cities.ToList().Select(c => new CityEntity(c)).ToList();
        }

        public CityEntity GetId(UserEntity UserEntity, int CityId)
        {
            IQueryable<City> Cities = CinemasEntities.Cities;
            Cities = Cities.Where(c => c.Id.Equals(CityId));
            return new CityEntity(Cities.FirstOrDefault());
        }

        public CityEntity Create(UserEntity UserEntity, CityEntity CityEntity)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                try
                {
                    City City = new City();
                    City = CityEntity.ToModel(City);
                    CinemasEntities.Cities.Add(City);
                    CinemasEntities.SaveChanges();
                    CityEntity.Id = City.Id;
                    transaction.Commit();
                    return CityEntity;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không tạo được City mới");
                }
            }
            return null;
        }

        public CityEntity Update(UserEntity UserEntity, int CityId, CityEntity CityEntity)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                try
                {
                    City City = CinemasEntities.Cities.Where(c => c.Id.Equals(CityId)).FirstOrDefault();
                    if (City == null)
                        throw new BadRequestException("Không tìm thấy City có Id là " + CityId);
                    City = CityEntity.ToModel(City);
                    CinemasEntities.SaveChanges();
                    transaction.Commit();
                    return CityEntity;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không cập nhật được City");
                }
            }
        }

        public bool Delete(UserEntity UserEntity, int CityId)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                try
                {
                    City City = CinemasEntities.Cities.Where(c => c.Id.Equals(CityId)).FirstOrDefault();
                    if (City == null)
                        throw new BadRequestException("Không tìm thấy Citycó Id là " + CityId);
                    CinemasEntities.Cities.Remove(City);
                    CinemasEntities.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không xóa được City");
                }
            }
        }

       
    }
}