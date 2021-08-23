using System;
using System.Collections.Generic;
using System.Data.SQLite;
using BerkshireHathawayIO;

namespace BerkshireHathaway
{
    /// <summary>
    /// This class handles the SQLite querying and kicks off the BerkshireFile reader
    /// Builds objects for the Message layer to display
    /// </summary>
    public class BerkshireController
    {
        public static SQLiteConnection CreateConnection()
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

        /// <summary>
        /// Kicks off the Berkshire IO layer for reading the input files
        /// </summary>
        /// <param name="location"> Fully qualified text file location</param>
        /// <param name="username">Username that will be entered into the DB with the contents of the text file/// </param>
        /// <returns> List to display the added reasons</returns>
        public List<Reason> AddFromFile(string location, string username)
        {
            List<Reason> output = new List<Reason>();
            List<string> reasonTexts;
            BerkshireFileReader reader = new BerkshireFileReader(location);
            SQLiteConnection conn = CreateConnection();
            SQLiteCommand sqlCMD;

            if (!CheckUserExists(conn , username))
            {
               
                sqlCMD = conn.CreateCommand();
                sqlCMD.CommandText = "INSERT INTO Users(Name) VALUES(@user);";
                sqlCMD.Parameters.AddWithValue("@user", username);
                sqlCMD.ExecuteNonQuery();
            }

            reasonTexts = reader.Read();
            reasonTexts.ForEach(delegate (string reasonText)
            {
                Reason reason = new Reason(reasonText, username);
                try
                {
                    sqlCMD = conn.CreateCommand();
                    sqlCMD.CommandText = "INSERT INTO Reasons(Reason, Name) VALUES(@reason, @user); ";
                    sqlCMD.Parameters.AddWithValue("@reason", reason.ReasonText);
                    sqlCMD.Parameters.AddWithValue("@user", username);
                    sqlCMD.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                output.Add(reason);
            });
            conn.Close();
            return output;
        }

        /// <summary>
        /// Handles the input of individual Reasons along with the user who input the reason
        /// </summary>
        /// <param name="reasonText"> Reason for working at Berkshire Hathaway</param>
        /// <param name="user">User who input the Reason </param>
        /// <returns> String to display added reason</returns>
        public string Add(string reasonText, string user)
        {
            SQLiteConnection conn = CreateConnection();
            SQLiteCommand sqlCMD;
            Reason reason = new Reason(reasonText, user);
            try
            {
                if (!CheckUserExists(conn, user))
                {                    
                    sqlCMD = conn.CreateCommand(); 
                    sqlCMD.CommandText = "INSERT INTO Users(Name) VALUES(@user);";
                    sqlCMD.Parameters.AddWithValue("@user", user);
                    sqlCMD.ExecuteNonQuery();
                }
                sqlCMD = conn.CreateCommand();
                sqlCMD.CommandText = "INSERT INTO Reasons(Reason, Name) VALUES(@reason, @user); ";
                sqlCMD.Parameters.AddWithValue("@reason", reason.ReasonText);
                sqlCMD.Parameters.AddWithValue("@user", user);
                sqlCMD.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return reason.ReasonText;
        }

        /// <summary>
        /// Outputs the current reasons in the database
        /// </summary>
        /// <returns> List of the contents of the DB</returns>
        public List<Reason> ListReasons()
        {
            List<Reason> output = new List<Reason>();
            SQLiteDataReader sqlReader;
            SQLiteCommand sqlCMD;
            try
            {
                sqlCMD = CreateConnection().CreateCommand();
                sqlCMD.CommandText = "SELECT * FROM Reasons";
                sqlReader = sqlCMD.ExecuteReader();
                while (sqlReader.Read())
                {
                    Reason reason = new Reason(sqlReader.GetString(0), sqlReader.GetString(1));
                    output.Add(reason);
                }
                sqlReader.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return output;
        }
        /// <summary>
        /// Checks to see if the User exists in the Database
        /// </summary>
        /// <param name="conn">SQLite Connection</param>
        /// <param name="username">Username that is being checked against the DB</param>
        /// <returns>True or False</returns>
        public bool CheckUserExists(SQLiteConnection conn, string username)
        {
            bool output = false;
            SQLiteDataReader sqlReader;
            SQLiteCommand sqlCMD;
            try
            {
                sqlCMD = conn.CreateCommand();
                sqlCMD.CommandText = "SELECT * FROM Users where Name = @user";
                sqlCMD.Parameters.AddWithValue("@user", username);
                sqlReader = sqlCMD.ExecuteReader();
                output = sqlReader.HasRows;
                sqlReader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return output;
        }

    }
}
