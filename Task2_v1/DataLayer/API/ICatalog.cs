using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.API
{
    public interface ICatalog
    {
        int Id { get; set; }
        int? RoomNumber { get; set; }
        string RoomType { get; set; }
        bool? isBooked { get; set; }
    }
}
