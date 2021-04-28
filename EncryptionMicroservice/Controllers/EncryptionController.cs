using EncryptionMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EncryptionMicroservice.Controllers
{
    public class EncryptionController : ApiController
    {
        private const string _databasePath = @"E:\EncryptionMicroservice\database.db";
        private const string _tableName = @"encryptedentry";
        static EncryptionController()
        {
            CreateTablesIfNotExist();
        }

        // GET: api/Encryption
        public IEnumerable<EncryptedEntry> Get()
        {
            List<EncryptedEntry> entries = new List<EncryptedEntry>();  

            SQLiteDataReader reader = DBQuery($"select * from {_tableName}", out SQLiteConnection connection);
            if (reader == null || connection == null)
            {
                return entries;
            }
            while (reader.Read())
            {
                EncryptedEntry entry = new EncryptedEntry();
                entry.Id = (long)reader["id"];
                string byteString = reader["bytes"].ToString();
                entry.Bytes = byteString.Split(' ').Select(_ => byte.Parse(_)).ToArray();
                entries.Add(entry);
            } 

            reader.Close();
            connection.Close();
            return entries;
        }

        // GET: api/Encryption/5
        public EncryptedEntry Get(long id)
        {
            EncryptedEntry entry = null;

            SQLiteDataReader reader = DBQuery($"select * from {_tableName} where id='{id}';", out SQLiteConnection connection);
            if (reader == null || connection == null)
            {
                return entry;
            }
            if (reader.Read())
            {
                entry = new EncryptedEntry();
                entry.Id = (long)reader["id"];
                string byteString = reader["bytes"].ToString();
                entry.Bytes = byteString.Split(' ').Select(_ => byte.Parse(_)).ToArray();
            }

            reader.Close();
            connection.Close();
            return entry;
        }

        // POST: api/Encryption
        public EncryptedEntry Post([FromBody] EncryptedEntry entry)
        {
            var postStatement = $"insert into {_tableName}(bytes) values ('{string.Join(" ", entry.Bytes)}');";
            _ = DBNonQuery(postStatement, out SQLiteConnection connection);
            if (connection != null)
            {
                entry.Id = connection.LastInsertRowId;
                connection.Close();
            }
            else
            {
                entry = null;
            }
            return entry;
        }

        // PUT: api/Encryption/5
        public void Put(int id, [FromBody] EncryptedEntry entry)
        {
            var postStatement = $"update {_tableName} set bytes='{string.Join(" ", entry.Bytes)}' where id='{id}';";
            _ = DBNonQuery(postStatement, out SQLiteConnection connection);
            connection?.Close();
        }

        // DELETE: api/Encryption/5
        public void Delete(int id)
        {
            string deleteStatement = $"delete from {_tableName} where id='{id}';";
            _ = DBNonQuery(deleteStatement, out SQLiteConnection connection);
            connection?.Close();
        }


        private static void CreateTablesIfNotExist()
        {
            int foo = DBNonQuery($"create table if not exists {_tableName}(id integer not null primary key autoincrement, bytes text not null);", out SQLiteConnection connection);
            connection?.Close();
        }
        private static SQLiteDataReader DBQuery(string query, out SQLiteConnection connection)
        {
            SQLiteDataReader reader = null;
            connection = null;
            try
            {
                connection = ConnectToDB();
                if (connection == null)
                {
                    return reader;
                }

                SQLiteCommand command = new SQLiteCommand(query, connection);
                reader = command.ExecuteReader();
            }
            catch (Exception e)
            {
                reader = null;
                connection = null;
            }
            return reader;
        }
        private static int DBNonQuery(string query, out SQLiteConnection connection)
        {
            int affectedRows = -1;
            connection = null;
            try
            {
                connection = ConnectToDB();
                if (connection == null)
                {
                    return affectedRows;
                }

                SQLiteCommand command = new SQLiteCommand(query, connection);
                affectedRows = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                affectedRows = -1;
                connection = null;
            }
            return affectedRows;
        }
        private static SQLiteConnection ConnectToDB()
        {
            SQLiteConnection connection = null;
            try
            {
                connection = new SQLiteConnection($"Data Source={_databasePath}");
                var currDir = Directory.GetCurrentDirectory();
                if (!File.Exists(_databasePath))
                {
                    SQLiteConnection.CreateFile(_databasePath);
                }
                connection.Open();
            }
            catch (Exception e)
            {

            }
            return connection;
        }
    }
}
