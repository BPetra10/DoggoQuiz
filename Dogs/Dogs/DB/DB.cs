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
            try
            {
                connection.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show("Adatbázis hiba: " + e.Message);
            }
        }

        public void ReOpenConn()
        {
            connection.Open();
        }

        //Fact: ${} string interpolation
        public List<Notes> GetNotes(string Dogname)
        {
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

        public void InsertUser(string username, string password, string salt)
        {
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

        public bool CheckIfUserExist(string username)
        {
            string data = $"SELECT username FROM users WHERE username='{username}'";
            MySqlCommand query = new MySqlCommand(data, connection);
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
        public User? GetUserSaltAndPwd(string username)
        {
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

        public List<Question> GetQuestions(string dogs)
        {
            string data;
            if (dogs == "*")
            {
                data = "SELECT question,correct,answer1,answer2,answer3 FROM questions";
            }
            else
            {
                data = $"SELECT question,correct,answer1,answer2,answer3 FROM questions INNER JOIN dogs ON questions.dog_id=dogs.dog_id WHERE dogs.dog_name IN({dogs})";
            }
            MySqlCommand query = new MySqlCommand(data, connection);
            query.CommandTimeout = 60;
            List<Question> questions = new List<Question>();
            try
            {
                MySqlDataReader reader = query.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        questions.Add(new Question(reader));
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
            return questions;
        }
        public int GetUserId(string username)
        {
            string data = $"SELECT user_id FROM users WHERE username='{username}'";
            MySqlCommand query = new MySqlCommand(data, connection);
            query.CommandTimeout = 60;
            int userId = 0;//UserID will never become 0, so if something wrong we will know
            try
            {
                MySqlDataReader reader = query.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    userId = reader.GetInt32(0);
                    reader.Close();
                    return userId;
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
            return userId;
        }

        /*Similarly to GetUserSaltAndPwd we check if the DB points table has the userId given in the parameter, 
         * if yes, giving back a Points class instance, else returning null. */
        public Points? GetUserPoint(int userId)
        {
            string data = $"SELECT user_id,points FROM points WHERE user_id='{userId}'";
            MySqlCommand query = new MySqlCommand(data, connection);
            query.CommandTimeout = 60;
            try
            {
                MySqlDataReader reader = query.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    return new Points(reader);
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

        /*If GetUserPoint gives back a Points instance, then we have to update the users points.
        Else, we will insert their points to DB.*/
        public void InsertOrUpdatePoints(int user_id, int points, bool isInsert) {
            if (isInsert) {
                string data = $"INSERT INTO points(user_id,points) VALUES('{user_id}','{points}')";
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
            else {
                string data = $"UPDATE points SET points = {points} WHERE user_id={user_id}";
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
        }

        /*Similarly to GetUserPoints we check if the DB images table has the userId given in the parameter, 
        * if yes, giving back an Image class instance, else returning null. */
        public Images? GetUserImages(int userId)
        {
            string data = $"SELECT user_id,bought_images FROM images WHERE user_id='{userId}'";
            MySqlCommand query = new MySqlCommand(data, connection);
            query.CommandTimeout = 60;
            try
            {
                MySqlDataReader reader = query.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    return new Images(reader);
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

        public void InsertOrUpdateImages(int user_id, string bought_images, bool isInsert) {
            if (isInsert)
            {
                string data = $"INSERT INTO images(user_id,bought_images) VALUES('{user_id}','{bought_images}')";
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
            else
            {
                string data = $"UPDATE images SET bought_images = '{bought_images}' WHERE user_id={user_id}";
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
        }
    }
}
