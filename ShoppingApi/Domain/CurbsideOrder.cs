using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Domain
{
    public class CurbsideOrder
    {
        public int Id { get; set; }
        public string PickupPerson { get;  set; }
        public string Items { get; set; }

        public DateTime? PickupTimeAssigned { get; set; }
    }
}
