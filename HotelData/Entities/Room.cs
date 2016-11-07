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

        [Range(0,100000,ErrorMessage ="Недопустимая цена")]
        [DisplayName("Цена")]
        public int Price { get; set; }


        [DisplayName("Количество человек")]
        [Range(1, 6, ErrorMessage = "Недопустимая количество человек")]
        public int Persons { get; set; }


        [DisplayName("Класс номера")]
        [Range(1, 3, ErrorMessage = "Недопустимый класс")]
        public string Class { get; set; }

       
        public virtual ICollection<Record> Records { get; set; }

        //public List<Record> Records { get; set; }
    }
}
