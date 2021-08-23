using System;
using System.Data.SQLite;

namespace BerkshireHathaway
{
    /// <summary>
    /// Main function kicks off the menu system and handles initiation of DB structure and initial data input.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "init")
            {
                Console.WriteLine("Initializing Database Creation");
                SQLiteConnection sqlConn;
                sqlConn = CreateConnection();
                CreateTable(sqlConn);
                InsertData(sqlConn);
                Console.WriteLine("Database Creation Completed Successfully");
                ReadData(sqlConn);
            }

            Message message = new Message();
            message.Intro();

            

        }    
        
        static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlConn;
            sqlConn = new SQLiteConnection("DataSource = BerkshireDB.db;Version=3;New=True;Compress=True");
            try
            {
                sqlConn.Open();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return sqlConn;
        }
        /// <summary>
        /// Creates Tables for the application
        /// </summary>
        /// <param name="conn"></param>
        static void CreateTable(SQLiteConnection conn)
        {
            try
            {
                SQLiteCommand sqlCMD;
                string CreateSQL = "CREATE TABLE Users(Name VARCHAR(15) NOT NULL);";
                string CreateSQL1 = "CREATE TABLE Reasons(Reason VARCHAR(200) NOT NULL, Name VARCHAR(15) NOT NULL);";            
                sqlCMD = conn.CreateCommand();
                sqlCMD.CommandText = CreateSQL;
                sqlCMD.ExecuteNonQuery();
                sqlCMD.CommandText = CreateSQL1;
                sqlCMD.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Initialization Failed at Table Creation");
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        /// <summary>
        /// Inserts initial data into the DB
        /// </summary>
        /// <param name="conn"></param>
        static void InsertData(SQLiteConnection conn)
        {
            try
            {
                SQLiteCommand sqlCMD;
                sqlCMD = conn.CreateCommand();
                sqlCMD.CommandText = "INSERT INTO Users(Name) VALUES('Chris'); ";
                sqlCMD.ExecuteNonQuery();
                sqlCMD.CommandText = "INSERT INTO Reasons (Reason, Name) VALUES('The 3 month in office onboarding shows that Berkshire-Hathaway values its new hires and is commited to supporting them.', 'Chris'); ";
                sqlCMD.ExecuteNonQuery();
                sqlCMD.CommandText = "INSERT INTO Reasons (Reason, Name) VALUES('The office has a good location, in an area that is familiar to me, and suits my personal goal of buying a house in that area in the next few years.','Chris'); ";
                sqlCMD.ExecuteNonQuery();
                sqlCMD.CommandText = "INSERT INTO Reasons (Reason, Name) VALUES('The emphasis on placing devs in roles that match their existing skillset shows that Berkshire-Hathaway values its employees as individuals and has given consideration to maximizing both an employees contribution to their team, and their personal ability to succeed.', 'Chris'); ";
                sqlCMD.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        /// <summary>
        /// Prints the Current Data after Initialization
        /// </summary>
        /// <param name="conn"></param>
        static void ReadData(SQLiteConnection conn)
        {
            SQLiteDataReader sqlReader;
            SQLiteCommand sqlCMD;
            try
            {
                sqlCMD = conn.CreateCommand();
                sqlCMD.CommandText = "SELECT * FROM Users";

                sqlReader = sqlCMD.ExecuteReader();
                while (sqlReader.Read())
                {
                    string myreader = sqlReader.GetString(0);
                    Console.WriteLine(myreader);
                }
                sqlReader.Close();
                sqlCMD.CommandText = "SELECT * FROM Reasons";

                sqlReader = sqlCMD.ExecuteReader();
                while (sqlReader.Read())
                {
                    string myreader = sqlReader.GetString(0);
                    Console.WriteLine(myreader);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            conn.Close();
        }
    }
}
