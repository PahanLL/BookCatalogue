using Npgsql;

namespace BookCatalogue
{
    public class AuthorController
    {
        const string conString = "Server=localhost;Database=BookCatalogue;User ID=postgres;Password=123456;";
        public static void AddAuthor(string fname, string email, string lname)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(conString))
            {
                con.Open();
                string authorInsert = "INSERT INTO author (first_name, last_name, email) VALUES (@fname, @lname, @email)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(authorInsert, con))
                {
                    cmd.Parameters.AddWithValue("@fname", fname);
                    cmd.Parameters.AddWithValue("@lname", lname);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.ExecuteNonQuery();
                }
            }
        }



        public static List<Author> GetAuthors()
        {
            string conString = "Server=localhost;Database=BookCatalogue;User ID=postgres;Password=123456;";
            using (NpgsqlConnection con = new NpgsqlConnection(conString))
            {
                con.Open();
                string authorGet = "SELECT id, first_name, last_name, email FROM author";
                using (NpgsqlCommand cmd = new NpgsqlCommand(authorGet, con))
                {
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    List<Author> authors = new List<Author>();
                    while (reader.Read())
                    {
                        authors.Add(new Author
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Email = reader.GetString(3)
                        });
                    }
                    return authors;
                }
            }
        }


        public static void DeleteAuthor(uint author_id)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(conString))
            {
                con.Open();
                string authorDelete = $"DELETE FROM author WHERE id = {author_id}";
                using (NpgsqlCommand cmd = new NpgsqlCommand(authorDelete, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
