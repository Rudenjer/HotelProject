using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelData.Entities
{
    public class Room
    {
        [DisplayName("Номер комнаты")]
        public int RoomID { get; set; }

        [DisplayName("Цена")]
        public int Price { get; set; }


        [DisplayName("Количество человек")]
        public int Persons { get; set; }


        [DisplayName("Класс номера")]
        public string Class { get; set; }

       
        public virtual ICollection<Record> Records { get; set; }

        //public List<Record> Records { get; set; }
    }
}
