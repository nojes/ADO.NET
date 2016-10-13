using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sale.DataLayer.DbLayer;
using Sale.DataLayer.Managers;

namespace _01_Sale.Console
{
    class SaleTest
    {
        public int GoodId { get; set; }
        public int GoodCount { get; set; }
        public decimal Summa { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var order = new List<SaleTest>() {
                new SaleTest() {GoodId = 10, GoodCount = 3},
                new SaleTest() {GoodId = 12, GoodCount = 3},
                new SaleTest() {GoodId = 4, GoodCount = 2}
            };

            ShopContext context = new ShopContext();
            var tran = context.Database.BeginTransaction(IsolationLevel.ReadCommitted);

            try {
                object[] paramObjects = new object[] {
                    new SqlParameter("@DATESALE", DateTime.Now) ,
                    new SqlParameter("SaleId", SqlDbType.Int) { Direction = ParameterDirection.Output},
                };

                object saleId = StoredProcManager.CallStoredProc(context, "EXEC dbo.InsertSale @DATESALE, @SaleId output", 1, paramObjects);
//                StoredProcManager.CallStoredProc(context, "EXEC dbo.InsertSale '13.10.2016', @SaleId, 10, 3", Convert.ToInt32(saleId));
//                StoredProcManager.CallStoredProc(context, "EXEC dbo.InsertSale '13.10.2016', @SaleId, 13, 2", Convert.ToInt32(saleId));
//                StoredProcManager.CallStoredProc(context, "EXEC dbo.InsertSale '13.10.2016', @SaleId, 4, 5", Convert.ToInt32(saleId));

                foreach (SaleTest saleTest in order) {
                    object[] p = new object[] {
                        new SqlParameter("@SaleId", Convert.ToInt32(saleId)),
                        new SqlParameter("@GoodId", saleTest.GoodId),
                        new SqlParameter("@CountGood", saleTest.GoodCount)
                    };
                    StoredProcManager.CallStoredProc(context, "EXEC dbo.InsertSalePos @SaleId, @GoodId, @CountGood", p);
                }

                tran.Commit();
                System.Console.WriteLine("OK");
            }
            catch (Exception ex) {
                System.Console.WriteLine(ex.Message);
                tran.Rollback();
            }

            System.Console.Read();
        }
    }
}
