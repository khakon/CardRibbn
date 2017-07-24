using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardRibbn.Data
{
    public class Product
    {
        public int id { get; set; }
        public int typeid { get; set; }
        public int vendorid { get; set; }
        public string title { get; set; }
        public virtual ProductType type { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public bool taxes { get; set; }
        public virtual Vendor vendor { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
