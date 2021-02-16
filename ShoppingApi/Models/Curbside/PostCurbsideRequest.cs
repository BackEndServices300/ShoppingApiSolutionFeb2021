using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Models.Curbside
{
    public class PostCurbsideRequest
    {
  
        public string PickupPerson { get; set; }
        public string Items { get; set; }
    }

}
