using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardRibbn.Data
{
    public class ImageProduct
    {
        public int id { get; set; }
        public int idProduct { get; set; }
        public byte[] image { get; set; }
    }
}
