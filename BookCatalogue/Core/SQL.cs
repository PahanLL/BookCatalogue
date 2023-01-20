using Npgsql;

namespace BookCatalogue
{
    public class SQL
    {
        public static string conString = "Server=localhost;Database=BookCatalogue;User ID=postgres;Password=123456;";
        public static void InitialCreate()
        {
            using (NpgsqlConnection con = new NpgsqlConnection(conString))
            {
                con.Open();
                string genreCreate = @"CREATE TABLE IF NOT EXISTS genre (
                                                id SERIAL PRIMARY KEY,
                                                name varchar(50));";
                string authorCreate = @"CREATE TABLE IF NOT EXISTS author (
                                            id SERIAL PRIMARY KEY,
                                            first_name varchar(50),
                                            last_name varchar(50),
                                            email varchar(100));";
                string bookCreate = @"CREATE TABLE IF NOT EXISTS book (
                                            id SERIAL PRIMARY KEY,
                                            author_id int,
                                            genre_id int,
                                            title varchar(50),
                                            description varchar(250),
                                            release timestamp,
                                            CONSTRAINT auth_
                                            FOREIGN KEY (author_id) 
                                                REFERENCES author(id),
                                            CONSTRAINT genre_
                                            FOREIGN KEY (genre_id) 
                                                REFERENCES genre(id));";
                using (NpgsqlCommand cmd = new NpgsqlCommand("ALTER DATABASE \"BookCatalogue\" SET datestyle TO \"ISO, DMY\";", con))
                {
                    cmd.ExecuteNonQuery();
                }
                using (NpgsqlCommand cmd = new NpgsqlCommand(genreCreate, con))
                {
                    cmd.ExecuteNonQuery();
                }
                using (NpgsqlCommand cmd = new NpgsqlCommand(authorCreate, con))
                {
                    cmd.ExecuteNonQuery();
                }
                using (NpgsqlCommand cmd = new NpgsqlCommand(bookCreate, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}



