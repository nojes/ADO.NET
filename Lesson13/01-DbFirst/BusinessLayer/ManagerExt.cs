using _01_DbFirst.DbLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_DbFirst.BusinessLayer
{
    public class ManagerExt
    {
        public static List<GoodExt> GetGoods(ShopAdoEntities context)
        {
            var query = from goods in context.Goods
                        select new GoodExt() {
                            GoodId = goods.GoodId,
                            GoodName = goods.GoodName,
                            ManufacturerName = goods.Manufacturer.ManufacturerName,
                            CategoryName = goods.Category.CategoryName,
                            GoodCount = goods.GoodCount,
                            Price = goods.Price
                        };
            return query.ToList();
        }
    }
}
