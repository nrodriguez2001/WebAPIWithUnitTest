using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using VevoAPI.Models;

namespace VevoAPI.Helpers
{
    using System;

    internal class StoredProc
    {
        public static IEnumerable<T> GetList<T>(string procedure, string connectionString) where T : new()
        {
            var data = new List<T>();

            using (var conn = new SqlConnection(connectionString))
            {
                var com = new SqlCommand
                              {
                                  Connection = conn,
                                  CommandType = CommandType.StoredProcedure,
                                  CommandText = procedure
                              };

                var adapt = new SqlDataAdapter { SelectCommand = com };
                var dataset = new DataSet();
                adapt.Fill(dataset);

                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    var newT = new T();

                    foreach (DataColumn col in dataset.Tables[0].Columns)
                    {
                        var property = newT.GetType().GetProperty(col.ColumnName);
                        property.SetValue(newT, row[col.ColumnName]);
                    }

                    data.Add(newT);
                }

                return data;
            }
        }

        public static IEnumerable<T> GetListVideosByArtist<T>(string procedure, string idParam, int artistId, string connectionString) where T : new()
        {
            var data = new List<T>();

            using (var conn = new SqlConnection(connectionString))
            {
                var com = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = procedure
                };
                com.Parameters.AddWithValue(idParam, artistId);
                var adapt = new SqlDataAdapter { SelectCommand = com };
                var dataset = new DataSet();
                adapt.Fill(dataset);

                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    var newT = new T();

                    foreach (DataColumn col in dataset.Tables[0].Columns)
                    {
                        var property = newT.GetType().GetProperty(col.ColumnName);
                        property.SetValue(newT, row[col.ColumnName]);
                    }

                    data.Add(newT);
                }

                return data;
            }
        }

        public static T GetSingleRecord<T>(string procedure, string idParam, int id, string connectionString) where T : new()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var com = new SqlCommand();
                com.Parameters.AddWithValue(idParam, id);
                com.Connection = conn;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = procedure;

                var adapt = new SqlDataAdapter { SelectCommand = com };
                var dataset = new DataSet();
                adapt.Fill(dataset);

                var newT = new T();
                if (dataset.Tables[0].Rows.Count <= 0)
                {
                    return newT;
                }

                var row = dataset.Tables[0].Rows[0];
                foreach (DataColumn col in dataset.Tables[0].Columns)
                {
                    var property = newT.GetType().GetProperty(col.ColumnName);
                    property.SetValue(newT, row[col.ColumnName]);
                }
                return newT;
            }
        }

        public static Artist AddOrUpdateArtists(string procedure, Artist record, string connectionString)
        {
            int id;
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (var cmd = new SqlCommand(procedure, conn))
                    {
                        cmd.Parameters.Add("@artist_id", SqlDbType.Int).Value = record.artist_id;
                        cmd.Parameters.Add("@urlSafeName", SqlDbType.VarChar).Value = record.urlSafeName;
                        cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = record.name;
                        cmd.Parameters.Add("@id", SqlDbType.Int, 0, "id");
                        cmd.Parameters["@id"].Direction = ParameterDirection.Output;
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        id = (int)cmd.Parameters["@id"].Value;
                    }

                    conn.Close();
                }
                record.artist_id = id;
                return record;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Video AddOrUpdateVideos(string procedure, Video record, string connectionString)
        {
            try
            {
                int id;

                using (var conn = new SqlConnection(connectionString))
                {
                    using (var cmd = new SqlCommand(procedure, conn))
                    {
                        cmd.Parameters.Add("@video_id", SqlDbType.Int).Value = record.video_id;
                        cmd.Parameters.Add("@artist_id", SqlDbType.Int).Value = record.artist_id;
                        cmd.Parameters.Add("@isrc", SqlDbType.VarChar).Value = record.isrc;
                        cmd.Parameters.Add("@urlSafeTitle", SqlDbType.VarChar).Value = record.urlSafeTitle;
                        cmd.Parameters.Add("@VideoTitle", SqlDbType.VarChar).Value = record.VideoTitle;
                        cmd.Parameters.Add("@id", SqlDbType.Int, 0, "id");
                        cmd.Parameters["@id"].Direction = ParameterDirection.Output;
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        id = (int)cmd.Parameters["@id"].Value;
                    }

                    conn.Close();
                }
                record.video_id = id;
                return record;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public static int DeleteRecord(string procedure, string idParam, int id, string connectionString)
        {
            int response;
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(procedure, conn))
                {
                    conn.Open();
                    var transaction = conn.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.Parameters.Add(idParam, SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@result", SqlDbType.Int, 0, "result");
                    cmd.Parameters["@result"].Direction = ParameterDirection.Output;
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                        transaction.Commit();
                        response = (int)cmd.Parameters["@result"].Value;

                        conn.Close();

                    }
                    catch
                    {
                        transaction.Rollback();
                        response = 1;
                    }
                }
            }
            return response;
        }
    }
}
