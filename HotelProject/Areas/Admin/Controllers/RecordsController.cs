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

namespace HotelProject.Areas.Admin.Controllers
{
    public class RecordsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        RoomRepository rr = new RoomRepository();

        // GET: Admin/Records
        public async Task<ActionResult> Index()
        {

            SelectList roomSelectList = new SelectList(rr.GetRooms(), "RoomID", "RoomID");

            RecordRoomViewModels rrvm = new RecordRoomViewModels { Rooms = roomSelectList, Records = db.Records.ToList() };


            return View(rrvm);
        }

        public ActionResult GetRecords(string id)
        {
            try
            {
                if (id == "" || id == null)
                {
                    return PartialView("NullRecords", db.Records.OrderBy(item=>item.DateIn));
                }

                Room r = rr.FindRoom(Convert.ToInt32(id));


                return PartialView(r.Records.OrderBy(item => item.DateIn));

            }
            catch (NullReferenceException ex)
            {

                return HttpNotFound();

            }
        }
    }
}