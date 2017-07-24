using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CardRibbn.Data
{
    public class ProductCollection
    {
        public int id { get; set; }
        public int collectionId { get; set; }
        public int productId { get; set; }
        [ForeignKey("collectionId")]
        public virtual Collection collection { get; set; }
        [ForeignKey("productId")]
        public virtual Product product { get; set; }
    }
}
