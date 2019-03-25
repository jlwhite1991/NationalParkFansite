using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using System.Data.SqlClient;
using Capstone.Web;
using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.Tests.DAL
{
    [TestClass()]
    public class WeatherSqlDALTest
    {
        private TransactionScope tran;

        private string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=NPGeek;Integrated Security=True";

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                cmd = new SqlCommand("INSERT INTO park VALUES ('JNP', 'JellyStone National Park', 'Ohio', 666420, 6969, 300, 42, 'Mushroom', 3000, 1000000, 'There is actually no difference between good things and bad things', 'dril', 'Hellworld: You will die.', 0, 0);", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("INSERT INTO weather VALUES ('JNP', 1, 100, 200, 'meteors');", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("INSERT INTO weather VALUES ('JNP', 2, 200, 300, 'apocalyptic');", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("INSERT INTO weather VALUES ('JNP', 3, 300, 400, 'death comes');", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("INSERT INTO weather VALUES ('JNP', 4, 400, 500, 'dear god why');", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("INSERT INTO weather VALUES ('JNP', 5, 500, 600, '...');", conn);
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod()]
        public void GetWeatherAtParkTest()
        {
            WeatherSqlDAL weatherSqlDAL = new WeatherSqlDAL(connectionString);
            List<Weather> weathers = weatherSqlDAL.GetWeatherAtPark("JNP");

            Assert.IsNotNull(weathers);
            Assert.AreEqual("meteors", weathers[0].Forecast);
        }

    }
}


