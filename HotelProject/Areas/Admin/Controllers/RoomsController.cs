using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelData.Context;
using HotelData.Entities;
using HotelData.Repositories;
using HotelData.ViewModels;

namespace HotelProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoomsController : Controller
    {
        RoomRepository rr = new RoomRepository();
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rooms
        public ActionResult Index()
        {
            return View(rr.GetRooms());
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = rr.FindRoom(id);
            RoomInfo roomInfo = db.RoomInfos.Find(Convert.ToInt32(room.Class));

            var rrim = new RoomRoomInfoViewModel();
            rrim.RoomID = room.RoomID;
            rrim.Price = room.Price;
            rrim.Persons = room.Persons;
            rrim.ClassName = roomInfo.ClassName;
            rrim.Info = roomInfo.Info;
            rrim.Photo = roomInfo.Photo;
            //var rrim = new RoomRoomInfoViewModel(room.RoomID, room.Price, room.Persons, roomInfo.ClassName, roomInfo.Info, roomInfo.Photo);
            

            if (room == null)
            {
                return HttpNotFound();
            }
            return View(rrim);
        }

        // GET: Rooms/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoomID,Price,Persons,Class")] Room room)
        {
            if (ModelState.IsValid)
            {
                rr.CreateRoom(room);
                return RedirectToAction("Index");
            }

            return View(room);
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = rr.FindRoom(id);

            SelectList roomInfoSelectList = new SelectList(db.RoomInfos.ToList<RoomInfo>(), "RoomInfoID", "ClassName");

            RoomInfoListViewModel rilvm = new RoomInfoListViewModel();
            rilvm.RoomID = room.RoomID;
            rilvm.Persons = room.Persons;
            rilvm.Price = room.Price;
            rilvm.Class = room.Class;
            rilvm.RoomInfos = roomInfoSelectList;

            //RoomInfo roomInfo = db.RoomInfos.Find(Convert.ToInt32(room.Class));

            //var rrim = new RoomRoomInfoViewModel();
            //rrim.RoomID = room.RoomID;
            //rrim.Price = room.Price;
            //rrim.Persons = room.Persons;
            //rrim.ClassName = roomInfo.ClassName;
            //rrim.Info = roomInfo.Info;
            //rrim.Photo = roomInfo.Photo;
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(rilvm);
        }

        // POST: Rooms/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "RoomID,Price,Persons,Class")] Room room)
        public ActionResult Edit(RoomInfoListViewModel roomLVM)
        {
            if (ModelState.IsValid)
            {
                //Room room = new Room();

                Room room = rr.FindRoom(roomLVM.RoomID);
                room.Price = roomLVM.Price;
                room.Persons = roomLVM.Persons;
                room.Class = roomLVM.Class;
                rr.ModifyRoom(room);
                return RedirectToAction("Index");
            }
            return View(roomLVM);
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = rr.FindRoom(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Room room = rr.FindRoom(id);
            rr.DeleteRoom(room);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                rr.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}