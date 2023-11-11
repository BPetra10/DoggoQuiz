using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dogs.DB
{
    class Notes
    {
        public string LearningNotes { get; }

        public Notes(MySqlDataReader reader)
        {
            LearningNotes = reader.GetString(0);
        }
    }

    class User
    {
        public string password { get; }
        public string salt { get; }
        public User(MySqlDataReader reader)
        {
            password = reader.GetString(0);
            salt = reader.GetString(1);
        }
    }

    /*Hashing passwords with Pbkdf2 (Password-Based Key Derivation Function)*/
     class PasswordHasher
    {
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        //Generating Hashed password and getting salt. (For storing later in Databse)
        public string Generate(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password), //encodes all the characters in the string into a sequence of bytes
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash); /*Converts an array of 8-bit unsigned integers 
                                               to its equivalent string representation 
                                               that is encoded with uppercase hex characters.*/
        }

        /*gets the plain password, checking if password+salt will give back hash.*/
        public bool IsValid(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
            /*Determines the equality of two byte sequences in an amount of time 
             that depends on the length of the sequences, but not their values.*/
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
     }

    class Question {
        public string question { get; }
        public string correct { get; }
        public string answer1 { get; }
        public string answer2 { get; }
        public string answer3 { get; }
        public Question(MySqlDataReader reader)
        {
            question = reader.GetString(0);
            correct = reader.GetString(1);
            answer1 = reader.GetString(2);
            answer2 = reader.GetString(3);
            answer3 = reader.GetString(4);
        }
    }

    class Points { 
        public int user_id { get; }
        public int points { get; }
        public Points(MySqlDataReader reader) { 
            user_id = reader.GetInt32(0);
            points = reader.GetInt32(1);
        }
    }
}
