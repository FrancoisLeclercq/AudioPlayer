using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace audioplayer.Models
{
    public class MusicDAL
    {
        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=audioplayer;Trusted_Connection=True;MultipleActiveResultSets=true";

        public List<Music> GetAllMusic()
        {
            List<Music> allMusic = new List<Music>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_GetAllMusic", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Music music = new Music();
                    music.Id = Convert.ToInt32(dr["Id"].ToString());
                    music.Name = dr["Name"].ToString();
                    music.FileData = (byte[])dr["FileData"];
                    music.Date = dr["Date"].ToString();

                    allMusic.Add(music);
                }
                con.Close();
            }
            return allMusic;
        }

        public void AddMusic(Music music)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_AddMusic", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@Name", music.Name);
                cmd.Parameters.AddWithValue("@FileData", music.FileData);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteMusic(int? Id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_DeleteMusic", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Music GetMusic(int? Id)
        {
            Music music = new Music();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_GetMusic", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    music.Id = Convert.ToInt32(dr["Id"].ToString());
                    music.Name = dr["Name"].ToString();
                    music.FileData = (byte[])dr["FileData"];
                    music.Date= dr["Date"].ToString();
                }
                con.Close();
            }
            return music;
        }
    }
}
