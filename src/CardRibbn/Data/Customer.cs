using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardRibbn.Data
{
    public class Customer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string mail { get; set; }
        public string adress { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
