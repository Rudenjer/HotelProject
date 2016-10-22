using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelData.Entities
{
    public class Record
    {

        public int RecordID { get; set; }

        [ForeignKey("Room")]
        public int? RoomID { get; set; }

        public virtual Room Room { get; set; }


        public string ClientID { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateIn { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOut { get; set; }

        

    }
}
