using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelData.Context;
using HotelData.Entities;
using System.Data.Entity;

namespace HotelData.Repositories
{
    public class RoomRepository
    {
        ApplicationDbContext db;


        public RoomRepository()
        {
            db = new ApplicationDbContext();

        }


        public List<Room> GetRooms()
        {
            return db.Rooms.ToList<Room>();
        }



        public Room FindRoom(int? id)
        {
            return db.Rooms.Find(id);
        }

        public void DeleteRoom(Room r)
        {
            db.Rooms.Remove(r);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void ModifyRoom(Room r)
        {
            db.Entry(r).State = EntityState.Modified;
            db.SaveChanges();

        }


        public void CreateRoom(Room r)
        {
            db.Rooms.Add(r);
            db.SaveChanges();

        }

    }
}
