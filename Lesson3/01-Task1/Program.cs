using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            string conString = ConfigurationManager.ConnectionStrings["RegionCity"].ConnectionString;
            SqlConnection connection = new SqlConnection(conString);

            SqlCommand qRegion = new SqlCommand();
            SqlCommand qCityCount = new SqlCommand();
            qRegion.Connection = connection;
            qCityCount.Connection = connection;


            qRegion.CommandText = @"SELECT RegionId, RegionName, UserCreateId, DateCreate, UserModifyId, DateModify
                                    FROM Region";
            qCityCount.CommandText = @"SELECT COUNT(*) FROM City;";

            try
            {
                connection.Open();

                SqlDataReader rRegion = qRegion.ExecuteReader();

                if (rRegion.HasRows)
                {
                    while (rRegion.Read())
                    {
                        Console.WriteLine(
                            " {0}\t{1}\t{2}\t{3}\t{4}\t{5}",
                            rRegion[0], rRegion[1], rRegion[2], rRegion[3], rRegion[4], rRegion[5]
                        );
                    }
                }

                rRegion.Close();
                object cityCount = qCityCount.ExecuteScalar();

                Console.WriteLine("\n Count of City: {0}", Convert.ToInt32(cityCount));

            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]: {0}", ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open) {
                    connection.Close();
                }
            }
        }
    }
}
