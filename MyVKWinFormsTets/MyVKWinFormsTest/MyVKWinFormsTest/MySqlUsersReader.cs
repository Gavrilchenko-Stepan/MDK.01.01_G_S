using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace MyVKWinFormsTest
{
    class MySqlUsersReader
    {
        public List<User> ReadUsers()
        {
            List<User> result = new List<User>();

            MySqlConnection conn;
            string myConnectionString = "server=127.0.0.1;unid=root;database=myvknetwork;";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();

                const string query = "SELECT login, password, name, surname, data_birth FROM users";
                MySqlCommand command = new MySqlCommand(query, conn);
                using(MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("User Login: " + reader["login"] + " | Password: " + reader["password"] + " | Name: " + reader["name"] + " | Surname: " + reader["surname"]);
                    }
                }
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return result;
            }
        }
        
    }
}
