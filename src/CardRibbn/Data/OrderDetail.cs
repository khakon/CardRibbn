using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardRibbn.Data
{
    public class OrderDetail
    {
        public int id { get; set; }
        public int orderId { get; set; }
        public int productId { get; set; }
        public int delivery { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
