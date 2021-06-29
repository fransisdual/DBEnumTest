using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DBEnumTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            DBEnum guids = new DBEnum();

            var ids = guids.GetIDs();

            foreach(var id in ids)
            {
                Console.WriteLine(id);
            }

        }
    }



    class DBEnum : IEnumerable<Guid>
    {
        SqlConnection connection;
        string sqlConnectionString = "Data Source=localhost;Initial Catalog=BillionFileDownloader;Integrated Security=True";
        IDataReader reader;
        SqlCommand command;

        public DBEnum()
        {
            connection = new SqlConnection(sqlConnectionString);
            command = new SqlCommand($"Select * from files");
            command.Connection = connection;
        }


        public IEnumerable GetIDs()
        {
            connection.Open();
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                yield return reader.GetGuid(1);
            }

            reader.Close();
            connection.Close();
            yield break;
        }

        public IEnumerator<Guid> GetEnumerator()
        {
            connection.Open();
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                yield return reader.GetGuid(1); 
            }

            reader.Close();
            connection.Close();
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
