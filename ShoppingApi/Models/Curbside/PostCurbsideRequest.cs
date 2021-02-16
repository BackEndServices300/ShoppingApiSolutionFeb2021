using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Models.Curbside
{
    public class PostCurbsideRequest
    {
        [Required]
        public string PickupPerson { get; set; }
        [Required]
        public string Items { get; set; }
    }

}
