using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelData.Repositories;
using HotelData.Entities;
using HotelData.Context;

namespace HotelProject.Controllers
{
    public class HomeController : Controller
    {

        RoomRepository rr = new RoomRepository();

        public ActionResult Index()
        {
            return View(rr.GetRooms());
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Room r)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                //rr.CreateRoom(r);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}