using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelData.Context;
using HotelData.Entities;
using HotelData.Repositories;
using HotelData.ViewModels;
using Microsoft.AspNet.Identity;

namespace HotelProject.Controllers
{
    //Человек заходит, вводит диапозон дат и смотрит какие номера доступны. После этого производит бронь

    public class RecordsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        RoomRepository rr = new RoomRepository();


        // GET: Records
        public async Task<ActionResult> Index()
        {
            
            SelectList roomSelectList = new SelectList(rr.GetRooms(), "RoomID", "RoomID");
            
            RecordRoomViewModels rrvm = new RecordRoomViewModels { Rooms = roomSelectList, Records = db.Records.ToList() };


            return View(rrvm);
        }

        // GET: Records/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = await db.Records.FindAsync(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        // GET: Records/Create
        [Authorize(Roles = "user")]
        public ActionResult Create(int id, DateTime DateIn, DateTime DateOut)
        {
            Session["DateIn"] = DateIn.Date;
            Session["DateOut"] = DateIn.Date;
            Session["RoomId"] = id;

            ViewBag.Id = id;
            ViewBag.DateIn = DateIn.Date.ToShortDateString();
            ViewBag.DateOut = DateOut.Date.ToShortDateString();
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create()
        {
            string a = User.Identity.GetUserId();
            var rec = new Record { ClientID=a,DateIn=(DateTime)Session["DateIn"], /*DateIn=DateInM,*/ DateOut= (DateTime)Session["DateOut"], Room=db.Rooms.Find(Convert.ToInt32(Session["RoomId"]))};

            db.Records.Add(rec);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //TODO: запилить метод

        public ActionResult GetRecords(string id)
        {
            try
            {
                if (id == "" || id == null)
                {
                    return PartialView("NullRecords", db.Records);
                }

                Room r = rr.FindRoom(Convert.ToInt32(id));


                return PartialView(r.Records);

            }
            catch (NullReferenceException ex)
            {

                return HttpNotFound();

            }
        }




        //public async Task<ActionResult> Create([Bind(Include = "RecordID,ClientID,DateIn,DateOut")] Record record)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Records.Add(record);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(record);
        //}

        // GET: Records/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = await db.Records.FindAsync(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        // POST: Records/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RecordID,ClientID,DateIn,DateOut")] Record record)
        {
            if (ModelState.IsValid)
            {
                db.Entry(record).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(record);
        }

        // GET: Records/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = await db.Records.FindAsync(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        // POST: Records/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Record record = await db.Records.FindAsync(id);
            db.Records.Remove(record);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult ForAvailableRooms()
        {
            RecordDateVievModels rd = new RecordDateVievModels();
            

            return View(rd);
        }

        public ActionResult GetAvailableRooms(RecordDateVievModels rd)
        {
            List<Record> rec = new List<Record>();

            IQueryable<Record> rec2 = db.Records.Where(a => !(a.DateIn.CompareTo(rd.DateOut) > 0 || a.DateOut.CompareTo(rd.DateIn) < 0));

            //db.Records.Select();
            //var a = from item in db.Records 
            //        where item.RoomID 

            //foreach (var item in db.Records.Where(a => (a.DateIn.CompareTo(rd.DateOut) > 0 || a.DateOut.CompareTo(rd.DateIn) < 0)))
            //{
            //    if (rec.Where(a => (a.RoomID != item.RoomID)))
            //    {
            //        rec.Add(item);
            //    }
            //}
            IEnumerable<int?> ids = (IEnumerable<int?>)from item in db.Records
                      where !(item.DateIn.CompareTo(rd.DateOut) > 0 || item.DateOut.CompareTo(rd.DateIn) < 0)
                      select item.RoomID;
            var rooms = from item in db.Rooms 
                        where !ids.Contains(item.RoomID)
                        select new RoomDateViewModel {RoomID=item.RoomID, Persons=item.Persons, Price=item.Price, Records=item.Records,Class=item.Class, DateOut=rd.DateOut ,DateIn=rd.DateIn };
            //var roomsDate = 
            ViewBag.DateIn = rd.DateIn;
            ViewBag.DateOut = rd.DateOut;
            //return PartialView("GetAvailableRooms", db.Records.Where(a=>!(a.DateIn.CompareTo(rd.DateOut)>0||a.DateOut.CompareTo(rd.DateIn)<0)));
            return PartialView("GetAvailableRooms", rooms);
        }

        public ActionResult Book(int id, DateTime DateIn, DateTime DateOut)
        {
            

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public async Task<ActionResult> Confirm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = await db.Records.FindAsync(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }
    }
}
