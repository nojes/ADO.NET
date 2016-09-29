using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_OneToMany.DataLayer
{
    public class Order
    {
        public int OrderId { get; set; }
        [StringLength(32)]
        public string ProductName { get; set; }
        [StringLength(128)]
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime PurchaseDate { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
