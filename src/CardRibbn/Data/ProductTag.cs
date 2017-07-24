using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CardRibbn.Data
{
    public class ProductTag
    {
        public int id { get; set; }
        public int tagId { get; set; }
        public int productId { get; set; }
        [ForeignKey("tagId")]
        public virtual Collection tag { get; set; }
        [ForeignKey("productId")]
        public virtual Product product { get; set; }
    }
}
