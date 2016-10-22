using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using HotelData.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelData.ViewModels
{
    public class RoomDateViewModel
    {
        [DisplayName("Номер комнаты")]
        public int RoomID { get; set; }

        [DisplayName("Цена")]
        public int Price { get; set; }


        [DisplayName("Количество человек")]
        public int Persons { get; set; }


        [DisplayName("Класс номера")]
        public string Class { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateIn { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOut { get; set; }


        public virtual ICollection<Record> Records { get; set; }
    }

}
