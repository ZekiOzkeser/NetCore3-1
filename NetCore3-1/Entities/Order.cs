using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore3_1.Entities
{
    public class Order
    {
        public Order()
        {
            OrderItems=new List<OrderItems>();
        }
        public int Id{ get; set; }
        public IList<OrderItems> OrderItems { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
