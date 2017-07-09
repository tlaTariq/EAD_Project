using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EADProject.Models
{
    public static class DBHelper
    {

        private static String connString = @"Data Source=DESKTOP-ERLET40\SQLEXPRESS2016;Initial Catalog=MusicStore;Integrated Security=True";


        public static UsersDTO Validate(String Login, String Password)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    String sqlQuery = String.Format("Select * from Users where UserName = '{0}' and Password = '{1}'", Login, Password);
                    SqlCommand comm = new SqlCommand(sqlQuery, conn);
                    SqlDataReader reader = comm.ExecuteReader();

                    if (reader.Read() == true)
                    {
                        UsersDTO dto = new UsersDTO();
                        dto.UserID = reader.GetInt32(0);
                        dto.UserName = reader.GetString(1);
                        dto.Password = reader.GetString(2);

                        return dto;
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<SongsDTO> getAllSongs()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    String sqlQuery = String.Format("Select * from Songs");
                    SqlCommand comm = new SqlCommand(sqlQuery, conn);
                    SqlDataReader reader = comm.ExecuteReader();

                    List<SongsDTO> list = new List<SongsDTO>();

                    while (reader.Read() == true)
                    {
                        SongsDTO dto = new SongsDTO();
                        dto.SongID = reader.GetInt32(0);
                        dto.Name = reader.GetString(1);
                        dto.Singer = reader.GetString(2);
                        dto.Link = reader.GetString(3);

                        list.Add(dto);
                    }

                    return list;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static SongsDTO getSongByID(int id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    String sqlQuery = String.Format("Select * from Songs where SongID = {0}", id);
                    SqlCommand comm = new SqlCommand(sqlQuery, conn);
                    SqlDataReader reader = comm.ExecuteReader();

                    SongsDTO dto = new SongsDTO();

                    if (reader.Read() == true)
                    {
                        dto.SongID = reader.GetInt32(0);
                        dto.Name = reader.GetString(1);
                        dto.Singer = reader.GetString(2);
                        dto.Link = reader.GetString(3);                        
                    }

                    return dto;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static void updateSong(int id, string name, string singer)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    String sqlQuery = String.Format("Update Songs set Name = '{0}', Singer = '{1}' where SongID = {2}", name, singer, id);
                    SqlCommand comm = new SqlCommand(sqlQuery, conn);
                    comm.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    
                }
            }
        }

        public static void deleteSong(int id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    String sqlQuery = String.Format("Delete from Songs where SongID = {0}", id);
                    SqlCommand comm = new SqlCommand(sqlQuery, conn);
                    comm.ExecuteNonQuery();

                }
                catch (Exception ex)
                {

                }
            }
        }

        public static void addSong(string name, string singer, string link)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    String sqlQuery = String.Format("Insert into Songs (Name, Singer, Link) values('{0}', '{1}', '{2}')", name, singer, link);
                    SqlCommand comm = new SqlCommand(sqlQuery, conn);
                    comm.ExecuteNonQuery();

                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}