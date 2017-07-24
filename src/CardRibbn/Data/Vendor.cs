using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CardRibbn.Data
{
    public class Vendor
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}
