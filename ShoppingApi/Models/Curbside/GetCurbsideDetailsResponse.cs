using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Models.Curbside
{
    public class GetCurbsideDetailsResponse
    {
        public int Id { get; set; }
        public string PickupPerson { get; set; }
        public string Items { get; set; }
        public DateTime PickupTimeAssigned { get; set; }
    }

}
