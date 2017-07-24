using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardRibbn.Data
{
    public class Order
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public int customerId { get; set; }
        public string paymentStatus { get; set; }
        public decimal discount { get; set; }
        public decimal total { get; set; }
        public bool fulFillment { get; set; }
        public bool payRequired { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? LastUpdated { get; set; }

    }
}
