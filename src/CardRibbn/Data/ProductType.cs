using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CardRibbn.Data
{
    public class ProductType
    {
        [Key]
        [ForeignKey("Product")]
        public int id { get; set; }
        public string title { get; set; }
    }
}
