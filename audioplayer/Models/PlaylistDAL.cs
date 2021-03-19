using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace audioplayer.Models
{
    public class PlaylistDAL
    {
        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=audioplayer;Trusted_Connection=True;MultipleActiveResultSets=true";

        public IEnumerable<Playlist> GetPlaylists()
        {
            List<Playlist> playlists = new List<Playlist>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_GetPlaylists", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Playlist playlist = new Playlist();
                    playlist.Id = Convert.ToInt32(dr["Id"].ToString());
                    playlist.Name = dr["Name"].ToString();
                    playlist.Image = dr["Image"].ToString();
                    playlist.Titles = dr["Titles"].ToString();

                    playlists.Add(playlist);
                }
                con.Close();
            }
            return playlists;
        }

        public void AddPlaylist(Playlist playlist)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_AddPlaylist", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@Name", playlist.Name);
                cmd.Parameters.AddWithValue("@Image", playlist.Image);
                cmd.Parameters.AddWithValue("@Titles", playlist.Titles);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdatePlaylist(Playlist playlist)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_UpdatePlaylist", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", playlist.Id);
                cmd.Parameters.AddWithValue("@Name", playlist.Name);
                cmd.Parameters.AddWithValue("@Image", playlist.Image);
                cmd.Parameters.AddWithValue("@Titles", playlist.Titles);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeletePlaylist(int? Id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_DeletePlaylist", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Playlist GetPlaylist(int? Id)
        {
            Playlist playlist = new Playlist();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_GetPlaylist", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    playlist.Id = Convert.ToInt32(dr["Id"].ToString());
                    playlist.Name = dr["Name"].ToString();
                    playlist.Image = dr["Image"].ToString();
                    playlist.Titles = dr["Titles"].ToString();
                }
                con.Close();
            }
            return playlist;
        }
    }
}
