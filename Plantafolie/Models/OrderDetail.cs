using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plantafolie.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int AlbumId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        //public virtual Produit Produit { get; set; }
        public virtual Order Order { get; set; }
    }
}
