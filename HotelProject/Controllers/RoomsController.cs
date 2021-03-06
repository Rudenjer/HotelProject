﻿using System;
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

namespace HotelProject.Controllers
{
    public class RoomsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        RoomRepository rr = new RoomRepository();

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
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // GET: Rooms/Create
        [Authorize(Roles ="admin")]
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
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Rooms/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoomID,Price,Persons,Class")] Room room)
        {
            if (ModelState.IsValid)
            {
                rr.ModifyRoom(room);
                return RedirectToAction("Index");
            }
            return View(room);
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
