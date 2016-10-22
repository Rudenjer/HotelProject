using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using HotelData.Entities;
using System.Web.Mvc;
using System;

namespace HotelData.ViewModels
{
    public class RecordManageViewModels
    {

        //public IEnumerable<Record> Records { get; set; }
        [Key]
        public int? ClientID { get; set; }

        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "01-01-1900", "01-01-2132", ErrorMessage = "Введите допустимую дату")]
        public DateTime DateIn { get; set; }

        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "01-01-1900", "01-01-2136", ErrorMessage = "Введите допустимую дату")]
        public DateTime DateOut { get; set; }

        public string Room { get; set; }

        //public int RoomID { get; set; }


    }



    public class RecordRoomViewModels
    {
        public List<Record> Records { get; set; }

        public SelectList Rooms { get; set; }
    }

    public class RecordDateVievModels
    {
        [DataType(DataType.Date)]
        public DateTime DateIn { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOut { get; set; }
    }
        
}
