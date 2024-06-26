﻿using MySqlConnector;
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

        public List<Notes> GetNotes(string Dogname)
        {
            string data = "SELECT notes FROM notes INNER JOIN dogs ON notes.dog_id=dogs.dog_id WHERE dog_name=@Dogname";
            using MySqlCommand query = new MySqlCommand(data, connection);
            query.Parameters.AddWithValue("@Dogname", Dogname);
            query.CommandTimeout = 60;
            List<Notes> notes = new List<Notes>();
            try
            {
                using MySqlDataReader reader = query.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        notes.Add(new Notes(reader));
                    }
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
            return notes;
        }

        public void InsertUser(string username, string password, string salt)
        {
            string data = "INSERT INTO users(username,password,salt) VALUES(@username,@password,@salt)";
            using MySqlCommand query = new MySqlCommand(data, connection);
            query.Parameters.AddWithValue("@username", username);
            query.Parameters.AddWithValue("@password", password);
            query.Parameters.AddWithValue("@salt", salt);
            query.CommandTimeout = 60;
            try
            {
                query.ExecuteNonQuery();
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
            string data = "SELECT username FROM users WHERE username=@username";
            using MySqlCommand query = new MySqlCommand(data, connection);
            query.Parameters.AddWithValue("@username", username);
            query.CommandTimeout = 60;
            try
            {
                using MySqlDataReader reader = query.ExecuteReader();
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
            string data = "SELECT password,salt FROM users WHERE username=@username";
            using MySqlCommand query = new MySqlCommand(data, connection);
            query.Parameters.AddWithValue("@username", username);
            query.CommandTimeout = 60;
            try
            {
                using MySqlDataReader reader = query.ExecuteReader();

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
            MySqlCommand query;
            if (dogs == "*")
            {
                data = "SELECT question,correct,answer1,answer2,answer3 FROM questions";
                query = new MySqlCommand(data, connection);
            }
            else
            {
                query = new MySqlCommand();
                /*Why we have to use array?
                    Cause with parameters, the parameters automatically getting "", and in IN()
                    this will not work: IN("dog1,dog2,dog3");
                    Because we want this: IN("dog1","dog2","dog3");
                    So we have to make a parameter for every dog it own.
                 */

                //Adding an array of parameters to MySqlCommand, to use with IN()
                string[] alldogs = dogs.Split(',');//array of strings which we want to add as a parameter
                var parameters = new string[alldogs.Length];
                /* An array cannot be added as a parameter so we need to loop through it.
                 Each item in the array will be a MySqlParameter, and we use the params array 
                to our Commandtext, to add to IN()*/
                for (int i = 0; i < alldogs.Length; i++)
                {
                    parameters[i] = string.Format("@dogs{0}", i);
                    query.Parameters.AddWithValue(parameters[i], alldogs[i]);
                }

                query.CommandText = string.Format("SELECT question,correct,answer1,answer2,answer3 " +
                    "FROM questions INNER JOIN dogs ON questions.dog_id=dogs.dog_id " +
                    "WHERE dogs.dog_name IN ({0})", string.Join(", ", parameters));
                query.Connection = connection;
            }

            query.CommandTimeout = 60;
            List<Question> questions = new List<Question>();
            try
            {
                using MySqlDataReader reader = query.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        questions.Add(new Question(reader));
                    }
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
            return questions;
        }
        public int GetUserId(string username)
        {
            string data = "SELECT user_id FROM users WHERE username=@username";
            using MySqlCommand query = new MySqlCommand(data, connection);
            query.Parameters.AddWithValue("@username", username);
            query.CommandTimeout = 60;
            int userId = 0;//UserID will never become 0, so if something wrong we will know
            try
            {
                using MySqlDataReader reader = query.ExecuteReader();
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
            string data = "SELECT user_id,points FROM points WHERE user_id=@userId";
            using MySqlCommand query = new MySqlCommand(data, connection);
            query.Parameters.AddWithValue("@userId", userId);
            query.CommandTimeout = 60;
            try
            {
                using MySqlDataReader reader = query.ExecuteReader();
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
                string data = "INSERT INTO points(user_id,points) VALUES(@user_id,@points)";
                using MySqlCommand query = new MySqlCommand(data, connection);
                query.Parameters.AddWithValue("@user_id", user_id);
                query.Parameters.AddWithValue("@points", points);
                query.CommandTimeout = 60;
                try
                {
                    query.ExecuteNonQuery();
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
                string data = "UPDATE points SET points = @points WHERE user_id=@user_id";
                using MySqlCommand query = new MySqlCommand(data, connection);
                query.Parameters.AddWithValue("@user_id", user_id);
                query.Parameters.AddWithValue("@points", points);
                query.CommandTimeout = 60;
                try
                {
                    query.ExecuteNonQuery();
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
        * if yes, giving back an Image list. */
        public List<Images> GetUserImages(int userId)
        {
            string data = "SELECT user_id,bought_images FROM images WHERE user_id=@userId";
            using MySqlCommand query = new MySqlCommand(data, connection);
            query.Parameters.AddWithValue("@userId", userId);
            query.CommandTimeout = 60;
            List<Images> images = new List<Images>();
            try
            {
                using MySqlDataReader reader = query.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        images.Add(new Images(reader));
                    }
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
            return images;
        }

        public void InsertImages(int user_id, int bought_image) {
            string data = "INSERT INTO images(user_id,bought_images) VALUES(@user_id,@bought_image)";
            using MySqlCommand query = new MySqlCommand(data, connection);
            query.Parameters.AddWithValue("@user_id", user_id);
            query.Parameters.AddWithValue("@bought_image", bought_image);
            query.CommandTimeout = 60;
                try
                {
                    query.ExecuteNonQuery();
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

        public List<Score> GetScores()
        {
            string data = "SELECT username,COUNT(bought_images) as img, points FROM points " +
                "LEFT JOIN users ON points.user_id = users.user_id LEFT JOIN images " +
                "ON users.user_id = images.user_id " +
                "GROUP BY username ORDER BY img DESC, points DESC LIMIT 5";

            using MySqlCommand query = new MySqlCommand(data, connection);
            query.CommandTimeout = 60;
            List<Score> score = new List<Score>();
            try
            {
                using MySqlDataReader reader = query.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        score.Add(new Score(reader));
                    }
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
            return score;
        }
    }
}
