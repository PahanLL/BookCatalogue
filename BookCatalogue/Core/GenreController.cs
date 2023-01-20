using Npgsql;
using System.Data;

namespace BookCatalogue
{
    public class GenreController
    {
        public static void AddGenre(string name)
        {
            string conString = "Server=localhost;Database=BookCatalogue;User ID=postgres;Password=123456;";
            using (NpgsqlConnection con = new NpgsqlConnection(conString))
            {
                con.Open();
                string genreInsert = "INSERT INTO genre (name) VALUES (@name)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(genreInsert, con))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.ExecuteNonQuery();
                }
            }
        }



        public static List<Genre> GetGenre()
        {
            string conString = "Server=localhost;Database=BookCatalogue;User ID=postgres;Password=123456;";
            using (NpgsqlConnection con = new NpgsqlConnection(conString))
            {
                con.Open();
                string genreGet = "SELECT id, name FROM genre";
                using (NpgsqlCommand cmd = new NpgsqlCommand(genreGet, con))
                {
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    List<Genre> genres = new List<Genre>();
                    while (reader.Read())
                    {
                        genres.Add(new Genre
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                    }
                    return genres;
                }
            }
        }



        public static void DeleteGenre(uint genre_id)
        {
            string conString = "Server=localhost;Database=BookCatalogue;User ID=postgres;Password=123456;";
            using (NpgsqlConnection con = new NpgsqlConnection(conString))
            {
                con.Open();
                string genreDelete = $"DELETE FROM genre WHERE id = {genre_id}";
                using (NpgsqlCommand cmd = new NpgsqlCommand(genreDelete, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
