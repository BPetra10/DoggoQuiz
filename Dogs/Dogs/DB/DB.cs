using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

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

        public void ReOpenConn() { 
            connection.Open();
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
                MessageBox.Show("Adatbázis hiba: " + e.Message);
            }
            finally 
            { 
                connection.Close();
            }
            return notes;
        }

        public void InsertUser(string username, string password, string salt) {
            string data = $"INSERT INTO users(username,password,salt) VALUES('{username}','{password}','{salt}')";
            MySqlCommand query = new MySqlCommand(data, connection);
            query.CommandTimeout = 60;
            try
            {
                query.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Adatbázis hiba: " + e.Message);
            }
            finally 
            { 
                connection.Close();
            }
        }

        public bool CheckIfUserExist(string username) {
            string data = $"SELECT username FROM users WHERE username='{username}'";
            MySqlCommand query = new MySqlCommand(data , connection);
            query.CommandTimeout = 60;
            try
            {
                MySqlDataReader reader = query.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Adatbázis hiba: " + e.Message);
            }
            finally
            { 
                connection.Close(); 
            }
            return false;
        }

        /*Basically if the database contains the user, this will give back their password and salt,
         but if the user does not exist, this will give back null, so User can be nullable. */
        public User? GetUserSaltAndPwd(string username) {
                string data = $"SELECT password,salt FROM users WHERE username='{username}'";
                MySqlCommand query = new MySqlCommand(data, connection);
                query.CommandTimeout = 60;
                try
                {
                    MySqlDataReader reader = query.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        return new User(reader);
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Adatbázis hiba: " + e.Message);
                }
                finally
                {
                    connection.Close();
                }
            return null;
        }
    }
}
