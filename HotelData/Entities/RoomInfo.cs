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
    public class RoomInfo
    {
        [DisplayName("Номер")]
        public int? RoomInfoID { get; set;}

        [DisplayName("Название класса")]
        public string ClassName { get; set; }

        [DisplayName("Информация о комнате")]
        public string Info { get; set; }

        [DisplayName("Фотография")]
        public string Photo { get; set; }
    }
}
