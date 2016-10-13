using System.Data.SqlClient;
using Sale.DataLayer.DbLayer;

namespace Sale.DataLayer.Managers
{
    public class StoredProcManager
    {
        public static void CallStoredProc(ShopContext context, string query, params object[] param)
        {
            context.Database.ExecuteSqlCommand(query, param);
        }

        public static object CallStoredProc(ShopContext context, string query, int index, params object[] param)
        {
            context.Database.ExecuteSqlCommand(query, param);
            return ((SqlParameter)param[index]).Value;
        }
    }
}
