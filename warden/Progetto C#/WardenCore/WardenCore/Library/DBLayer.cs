using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using WardenCore.Models;

namespace WardenCore.Library
{
    public class DBLayer
    {
        string cs = "";
        public DBLayer()
        {
            cs = ConfigurationManager.ConnectionStrings["wardenLocal"].ConnectionString;
        }

        public SqlConnection connect()
        {
            SqlConnection cnn = new SqlConnection(cs);
            cnn.Open();
            return cnn;
        }
          

        public IEnumerable<Dictionary<string, object>> select(string sql, SqlConnection cnn)
        {
     
        SqlCommand cmd = new SqlCommand(sql, cnn);
        SqlDataReader dataReader = cmd.ExecuteReader();

        var results = new List<Dictionary<string, object>>();
            var cols = new List<string>();
            for (var i = 0; i < dataReader.FieldCount; i++)
                cols.Add(dataReader.GetName(i));

            while (dataReader.Read())
                results.Add(SerializeRow(cols, dataReader));

            return results;


           

    }
        public bool insertTournament(Tournament t, SqlConnection cnn)
        {

            string sql = string.Format("INSERT INTO[dbo].[Event] ([EventName],[EventDate],[GameModeId]) VALUES ({0}, {1}, {2})", t.name, t.date, t.gmid);

            SqlCommand cmd = new SqlCommand(sql, cnn);
            int toReturn = cmd.ExecuteNonQuery();
            return toReturn > 0;
            

        }

        public bool insertScore(Score s, SqlConnection cnn)
        {

            //string sql = string.Format("INSERT INTO[dbo].[Event] ([EventName],[EventDate],[GameModeId]) VALUES ({0}, {1}, {2})"
            return false;
        }

        public IEnumerable<Dictionary<string, object>> Serialize(SqlDataReader reader)
        {
            var results = new List<Dictionary<string, object>>();
            var cols = new List<string>();
            for (var i = 0; i < reader.FieldCount; i++)
                cols.Add(reader.GetName(i));

            while (reader.Read())
                results.Add(SerializeRow(cols, reader));

            return results;
        }
        private Dictionary<string, object> SerializeRow(IEnumerable<string> cols,
                                                        SqlDataReader reader)
        {
            var result = new Dictionary<string, object>();
            foreach (var col in cols)
                result.Add(col, reader[col]);
            return result;
        }
    }
}