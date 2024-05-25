using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.API
{
    public interface IEvent
    {
        int Id { get; set; }

        int? StateId { get; set; }

        int? UserId { get; set; }

        DateTime? CheckInDate { get; set; }

        DateTime? CheckOutDate { get; set; }
    }
}
