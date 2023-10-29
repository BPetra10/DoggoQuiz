using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dogs.DB
{
    class Notes
    {
        public string LearningNotes { get; set; }

        public Notes(MySqlDataReader reader)
        {
            LearningNotes = reader.GetString(0);
        }

    }

    class Register { 
        public string username { get; set; }

        public string password { get; set; }
    
        public Register(MySqlDataReader reader) 
        {
            username = reader.GetString(0);
            password = reader.GetString(1);
        }

        public Register(string _username, string _password) { 
            username = _username;
            password = _password;
        }
    }
}
