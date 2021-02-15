using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Models
{
    public class CollectionBase<T>
    {

        public IList<T> Data { get; set; }
    }
}
