using Cinemas.Models;
using Cinemas.Modules.MUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinemas.Modules.MRoom
{
    public class RoomService : CommonService, IRoomService
    {
         public RoomService():base() { }
        // Đếm số lượng Room
        public int Count(UserEntity UserEntity, SearchRoomEntity SearchRoomEntity)
        {
            if (SearchRoomEntity == null) SearchRoomEntity = new SearchRoomEntity();
            IQueryable<Room> Rooms = CinemasEntities.Rooms;
            Rooms = SearchRoomEntity.ApplyTo(Rooms);
            return Rooms.Count();
        }
        // Lọc thông tin Room dựa vào điều kiện search
        public List<RoomEntity> Gets(UserEntity UserEntity, SearchRoomEntity SearchRoomEntity)
        {
            if (SearchRoomEntity == null) SearchRoomEntity = new SearchRoomEntity();
            IQueryable<Room> Rooms = CinemasEntities.Rooms;
            Rooms = SearchRoomEntity.ApplyTo(Rooms);
            Rooms = SearchRoomEntity.SkipAndTake(Rooms);
            return Rooms.ToList().Select(c => new RoomEntity(c)).ToList();
        }
        // Lọc thông tin Room dựa vào điều kiện Id
        public RoomEntity GetId(UserEntity UserEntity, int RoomId)
        {
            IQueryable<Room> Rooms = CinemasEntities.Rooms;
            Rooms = Rooms.Where(c => c.Id.Equals(RoomId));
            return new RoomEntity(Rooms.FirstOrDefault());
        }
        // Tạo Room mới 
        public RoomEntity Create(UserEntity UserEntity, RoomEntity RoomEntity)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                try
                {
                    Room Room = new Room();
                    Room = RoomEntity.ToModel(Room);
                    CinemasEntities.Rooms.Add(Room);
                    CinemasEntities.SaveChanges();
                    RoomEntity.Id = Room.Id;
                    transaction.Commit();
                    return RoomEntity;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không tạo được Room mới");
                }
            }
        }
        // Cập nhật Room theo Id
        public RoomEntity Update(UserEntity UserEntity, int RoomId, RoomEntity RoomEntity)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                try
                {
                    Room Room = CinemasEntities.Rooms.Where(c => c.Id.Equals(RoomId)).FirstOrDefault();
                    if (Room == null)
                        throw new BadRequestException("Không tìm thấy Room có Id là " + RoomId);
                    Room = RoomEntity.ToModel(Room);
                    CinemasEntities.SaveChanges();
                    transaction.Commit();
                    return RoomEntity;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không cập nhập được Room");
                }
            }
        }
        // Xóa Room theo Id
        public bool Delete(UserEntity UserEntity, int RoomId)
        {
            using (var transaction = CinemasEntities.Database.BeginTransaction())
            {
                try
                {
                    Room Room = CinemasEntities.Rooms.Where(c => c.Id.Equals(RoomId)).FirstOrDefault();
                    if (Room == null)
                        throw new BadRequestException("Không tìm thấy Room có Id là " + RoomId);
                    CinemasEntities.Rooms.Remove(Room);
                    CinemasEntities.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BadRequestException("Không xóa được Room");
                }
            }
        }
    }
}