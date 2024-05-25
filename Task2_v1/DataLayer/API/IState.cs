using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.API
{
    public interface IState
    {
        int Id { get; set; }
        int RoomCatalogId { get; set; }
        int Price { get; set; }
    }
}
