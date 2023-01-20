using Npgsql;

namespace BookCatalogue
{
    public class BookController
    {
        const string conString = "Server=localhost;Database=BookCatalogue;User ID=postgres;Password=123456;";
        public static void AddBook(string title, string description, DateTime release, int author_id, int genre_id)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(conString))
            {
                con.Open();
                string bookInsert = @"INSERT INTO book (title, description, release, genre_id, author_id) 
                                    VALUES (@title, @description, @release, @genre_id, @author_id)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(bookInsert, con))
                {
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@release", release);
                    cmd.Parameters.AddWithValue("@genre_id", genre_id);
                    cmd.Parameters.AddWithValue("@author_id", author_id);
                    cmd.ExecuteNonQuery();
                }
            }
        }



        public static List<Book> GetBooks(string search = "")
        {
            List<Book> books = new List<Book>();
            using (NpgsqlConnection con = new NpgsqlConnection(conString))
            {
                con.Open();
                string bookGet = @"SELECT
                                book.id, 
                                book.title,
                                book.description,
                                book.release,
                                book.author_id,
                                book.genre_id
                            FROM book";
                if (!string.IsNullOrEmpty(search))
                {
                    bookGet += " WHERE title ILIKE @search";
                }
                using (NpgsqlCommand cmd = new NpgsqlCommand(bookGet, con))
                {
                    if (!string.IsNullOrEmpty(search))
                    {
                        cmd.Parameters.AddWithValue("@search", search);
                    }
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            books.Add(new Book
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                AuthorId = reader.GetInt32(4),
                                GenreId = reader.GetInt32(5),
                                Description = reader.GetString(2),
                                Release = reader.GetDateTime(3)
                            });
                        }
                    }
                }
            }
            return books;
        }



        public static void DeleteBook(int book_id)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(conString))
            {
                con.Open();
                string bookDelete = $"DELETE FROM book WHERE id = {book_id}";
                using (NpgsqlCommand cmd = new NpgsqlCommand(bookDelete, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public static void UpdateBook(Book book)
        {
            string conString = "Server=localhost;Database=BookCatalogue;User ID=postgres;Password=123456;";
            using (NpgsqlConnection con = new NpgsqlConnection(conString))
            {
                con.Open();
                string bookUpdate = "UPDATE book SET title = @title, description = @description, release = @release, author_id = @author_id, genre_id = @genre_id WHERE id = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(bookUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@title", book.Title);
                    cmd.Parameters.AddWithValue("@description", book.Description);
                    cmd.Parameters.AddWithValue("@release", book.Release);
                    cmd.Parameters.AddWithValue("@author_id", book.AuthorId);
                    cmd.Parameters.AddWithValue("@genre_id", book.GenreId);
                    cmd.Parameters.AddWithValue("@id", book.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
