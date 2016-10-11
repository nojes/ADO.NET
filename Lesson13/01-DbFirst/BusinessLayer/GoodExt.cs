using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_DbFirst.BusinessLayer
{
    public class GoodExt
    {
        public int GoodId { get; set; }
        public string GoodName { get; set; }
        public string ManufacturerName { get; set; }
        public decimal Price { get; set; }
        public decimal GoodCount { get; set; }
        public string CategoryName { get; internal set; }
    }
}