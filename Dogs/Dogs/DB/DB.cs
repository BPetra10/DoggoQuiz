using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dogs.DB
{
    class DB
    {
        public string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=dogapp;";
        MySqlConnection connection;

        public DB()
        {
            connection = new MySqlConnection(connectionString);
            try { 
                connection.Open();
            } 
            catch (Exception e) { 
                MessageBox.Show("Adatbázis hiba: " + e.Message);
            }
        }

        public List<Notes> GetNotes(string Dogname) { 
            string data = $"SELECT notes FROM notes INNER JOIN dogs ON notes.dog_id=dogs.dog_id WHERE dog_name='{Dogname}'";
            MySqlCommand query = new MySqlCommand(data, connection);
            query.CommandTimeout = 60;
            List<Notes> notes = new List<Notes>();
            try
            {
                MySqlDataReader reader = query.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        notes.Add(new Notes(reader));
                    }
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Adatbázis hiba: "+e.Message);
            }
            return notes;
        }

        public void InsertUser(string username, string password) {
            string data = $"INSERT INTO users(username,password) VALUES('{username}','{password}')";
            MySqlCommand query = new MySqlCommand(data, connection);
            query.ExecuteNonQuery();
        }
    }
}
