using Xunit;
using BerkshireHathaway;
using System.Data.SQLite;
using System;
using System.IO;

namespace BerkshireHathawayTest
{
    public class BerskshireControllerTest
    {
        [Fact]
        public void InitializeTests()
        {
           var conn = CreateConnection();
           CreateTable(conn);
        }

        static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlConn;
            sqlConn = new SQLiteConnection("DataSource = BerkshireDB.db;Version=3;New=True;Compress=True");
            try
            {
                sqlConn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return sqlConn;
        }
        
        static void CreateTable(SQLiteConnection conn)
        {
            try
            {
                SQLiteCommand sqlCMD;
                string CreateSQL = "CREATE TABLE Users(Name VARCHAR(15) NOT NULL);";
                string CreateSQL1 = "CREATE TABLE Reasons(Reason VARCHAR(100) NOT NULL, Name VARCHAR(15) NOT NULL);";
                sqlCMD = conn.CreateCommand();
                sqlCMD.CommandText = CreateSQL;
                sqlCMD.ExecuteNonQuery();
                sqlCMD.CommandText = CreateSQL1;
                sqlCMD.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }              

        [Fact]
        public void Add_InsertTest()
        {
            var controller = new BerkshireController();
            var output = controller.Add("TestReasonAdd","ChrisTest");

            Assert.Equal("TestReasonAdd", output);

        }

        [Fact]
        public void Add_NameExists()
        {
            var controller = new BerkshireController();
            var output = controller.Add("TestReasonAdd1", "ChrisTest");

            Assert.Equal("TestReasonAdd1", output);
        }

        [Fact]
        public void Add_NameDoesNotExist()
        {
            var controller = new BerkshireController();
            var output = controller.Add("TestReasonAdd2","ChrisTestNew1");

            Assert.Equal("TestReasonAdd2", output);
        }

        [Fact]
        public void ListReasons()
        {
            var controller = new BerkshireController();
            var output = controller.ListReasons();

            Assert.NotEmpty(output);

        }

        [Fact]       
        public void CheckUserExists_True()
        {
            var sqlConn = new SQLiteConnection("DataSource = BerkshireDB.db;Version=3;New=True;Compress=True");
            sqlConn.Open();
            var controller = new BerkshireController();
            var output = controller.CheckUserExists(sqlConn,"ChrisTest");
            sqlConn.Close();

            Assert.True(output);
        }

        [Fact]
        public void CheckUserExists_False()
        {
            var sqlConn = new SQLiteConnection("DataSource = BerkshireDB.db;Version=3;New=True;Compress=True");
            sqlConn.Open();
            var controller = new BerkshireController();
            var output = controller.CheckUserExists(sqlConn, "ChrisTestNew2");
            sqlConn.Close();

            Assert.False(output);
        }
    }
}
