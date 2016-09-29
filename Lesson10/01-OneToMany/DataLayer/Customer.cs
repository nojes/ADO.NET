using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_OneToMany.DataLayer
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [StringLength(32)]
        public string FirstName { get; set; }
        [StringLength(32)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public byte[] Photo { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public Customer()
        {
            this.Orders = new List<Order>();
        }
    }
}
