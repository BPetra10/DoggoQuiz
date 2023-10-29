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

}
