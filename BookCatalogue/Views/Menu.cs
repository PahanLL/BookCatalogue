using Microsoft.EntityFrameworkCore.Update;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BookCatalogue
{
    class Menu
    {
        const int sleepTime = 1500;



        public void GenreMenu()
        {
            string menu;
            do
            {
                Console.Clear();
                Console.WriteLine("1. View genres");
                Console.WriteLine("2. Add genre");
                Console.WriteLine("3. Delete genre");
                Console.WriteLine("4. Back to menu");
                Console.Write("\nChoose menu item: ");
                menu = Console.ReadLine();
                List<Genre> genres = GenreController.GetGenre();
                if (menu != "1" && menu != "2" && menu != "3" && menu != "4")
                {
                    Console.WriteLine("Enter the correct value!");
                    Thread.Sleep(sleepTime);
                    continue;
                }
                switch (menu)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("ID  |      Name      ");
                        foreach (var genre in genres)
                        {
                            Console.WriteLine($"{genre.Id,-4}| {genre.Name}");
                        }
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                    case "2":
                        string name, tmp;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Enter genre name: ");
                            name = Console.ReadLine();
                            if (string.IsNullOrEmpty(name))
                            {
                                Console.WriteLine("Enter the correct value!");
                                Thread.Sleep(sleepTime);
                            }
                        } while (string.IsNullOrEmpty(name));
                        GenreController.AddGenre(name);
                        Console.Clear();
                        Console.WriteLine("Added!");
                        Thread.Sleep(sleepTime);
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("ID  |      Name      ");
                        foreach (var genre in genres)
                        {
                            Console.WriteLine($"{genre.Id,-4}| {genre.Name}");
                        }
                        Console.WriteLine("Enter book-ID to delete: ");
                        tmp = Console.ReadLine();
                        if (!uint.TryParse(tmp, out uint m) || string.IsNullOrEmpty(tmp))
                        {
                            Console.WriteLine("Enter the correct value!");
                            Thread.Sleep(sleepTime);
                        }
                        else
                        {
                            try
                            {
                                GenreController.DeleteGenre(m);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("\nThis key is used in another table! Exception: " + ex.Message);
                                Console.WriteLine("\nPress any key to continue...");
                                Console.ReadKey();
                                return;
                            }
                            Console.Clear();
                            Console.WriteLine("Deleted!");
                            Thread.Sleep(sleepTime);
                        }
                        break;
                    default:
                        break;
                }
            } while (menu != "4");
        }



        public void AuthorMenu()
        {
            string menu;
            do
            {
                Console.Clear();
                Console.WriteLine("1. View authors");
                Console.WriteLine("2. Add author");
                Console.WriteLine("3. Delete author");
                Console.WriteLine("4. Back to menu");
                Console.Write("\nChoose menu item: ");
                menu = Console.ReadLine();
                List<Author> authors = AuthorController.GetAuthors();
                if (menu != "1" && menu != "2" && menu != "3" && menu != "4")
                {
                    Console.WriteLine("Enter the correct value!");
                    Thread.Sleep(sleepTime);
                    continue;
                }
                switch (menu)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("ID  |     First Name      |       Last Name     |     Email     ");
                        foreach (var author in authors)
                        {
                            Console.WriteLine($"{author.Id,-4}| {author.FirstName,-20}| {author.LastName,-20}| {author.Email}");
                        }
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                    case "2":
                        string fname, lname, email;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Enter author first name: ");
                            fname = Console.ReadLine();
                            if (string.IsNullOrEmpty(fname))
                            {
                                Console.WriteLine("Enter the correct value!");
                                Thread.Sleep(sleepTime);
                            }
                        } while (string.IsNullOrEmpty(fname));
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Enter author last name: ");
                            lname = Console.ReadLine();
                            if (string.IsNullOrEmpty(lname))
                            {
                                Console.WriteLine("Enter the correct value!");
                                Thread.Sleep(sleepTime);
                            }
                        } while (string.IsNullOrEmpty(lname));
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Enter author E-Mail: ");
                            email = Console.ReadLine();
                            if (string.IsNullOrEmpty(email))
                            {
                                Console.WriteLine("Enter the correct value!");
                                Thread.Sleep(sleepTime);
                            }
                        } while (string.IsNullOrEmpty(email));
                        AuthorController.AddAuthor(fname, lname, email);
                        Console.Clear();
                        Console.WriteLine("Added!");
                        Thread.Sleep(sleepTime);
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("ID  |     First Name      |       Last Name     |     Email     ");
                        foreach (var author in authors)
                        {
                            Console.WriteLine($"{author.Id,-4}| {author.FirstName,-20}| {author.LastName,-20}| {author.Email}");
                        }
                        Console.WriteLine("Enter author-ID to delete: ");
                        string tmp = Console.ReadLine();
                        if (!uint.TryParse(tmp, out uint m) || string.IsNullOrEmpty(tmp))
                        {
                            Console.WriteLine("Enter the correct value!");
                            Thread.Sleep(sleepTime);
                        }
                        else
                        {
                            try
                            {
                                AuthorController.DeleteAuthor(m);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("\nThis key is used in another table! Exception: " + ex.Message);
                                Console.WriteLine("\nPress any key to continue...");
                                Console.ReadKey();
                                return;
                            }
                            Console.Clear();
                            Console.WriteLine("Deleted!");
                            Thread.Sleep(sleepTime);
                        }
                        break;
                    default:
                        break;
                }
            } while (menu != "4");
        }



        public void SearchBook()
        {
            string title;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter title of the book: ");
                title = Console.ReadLine();
                if (string.IsNullOrEmpty(title))
                {
                    Console.WriteLine("Enter the correct value!");
                    Thread.Sleep(sleepTime);
                }
            } while (string.IsNullOrEmpty(title));
            List<Book> books = BookController.GetBooks(title);
            Console.Clear();
            Console.WriteLine("ID |     Title     |      Description      | Release Date |       Author      |    Author email    |   Genre");
            List<Genre> genres = GenreController.GetGenre();
            List<Author> authors = AuthorController.GetAuthors();
            foreach (var book in books)
            {
                Console.WriteLine($"{book.Id,-3}| {book.Title,-14}| {book.Description,-22}| {book.Release.ToShortDateString(),-13}| {authors.Where(x => x.Id == book.AuthorId).Single().FirstName + " " + authors.Where(x => x.Id == book.AuthorId).Single().LastName,-18}| {authors.Where(x => x.Id == book.AuthorId).Single().Email,-19}| {genres.Where(x => x.Id == book.GenreId).Single().Name}");
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }


        public void BookMenu()
        {
            string menu;
            do
            {
                Console.Clear();
                Console.WriteLine("1. View books");
                Console.WriteLine("2. Add book");
                Console.WriteLine("3. Delete book");
                Console.WriteLine("4. Edit book");
                Console.WriteLine("5. Back to menu");
                Console.Write("\nChoose menu item: ");
                menu = Console.ReadLine();
                if (menu != "1" && menu != "2" && menu != "3" && menu != "4" && menu != "5")
                {
                    Console.WriteLine("Enter the correct value!");
                    Thread.Sleep(sleepTime);
                    continue;
                }
                switch (menu)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("ID |        Title        |        Description       |   Release Date");
                        List<Book> books = BookController.GetBooks();
                        foreach (var book in books)
                        {
                            Console.WriteLine($"{book.Id,-3}| {book.Title,-20}| {book.Description,-25}| {book.Release.ToShortDateString(),-20}");
                        }
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                    case "2":
                        string title, description, dateString, tmp;
                        int author_id = 0, genre_id = 0;
                        DateTime release;
                        bool passed = false;
                        List<Author> authors = AuthorController.GetAuthors();
                        List<Genre> genres = GenreController.GetGenre();
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Enter title of the book: ");
                            title = Console.ReadLine();
                            if (string.IsNullOrEmpty(title))
                            {
                                Console.WriteLine("Enter the correct value!");
                                Thread.Sleep(sleepTime);
                            }
                        } while (string.IsNullOrEmpty(title));
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Enter description of the book: ");
                            description = Console.ReadLine();
                            if (string.IsNullOrEmpty(description))
                            {
                                Console.WriteLine("Enter the correct value!");
                                Thread.Sleep(sleepTime);
                            }
                        } while (string.IsNullOrEmpty(description));
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Enter release date of the book: ");
                            dateString = Console.ReadLine();
                            if (!DateTime.TryParse(dateString, out release) || string.IsNullOrEmpty(dateString) || release.Year < 1000)
                            {
                                Console.WriteLine("You have entered an incorrect value.");
                                Thread.Sleep(sleepTime);
                            }
                        } while (string.IsNullOrEmpty(dateString) || !DateTime.TryParse(dateString, out _));
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("ID  |     First Name      |       Last Name     |     Email     ");
                            authors = AuthorController.GetAuthors();
                            foreach (var author in authors)
                            {
                                Console.WriteLine($"{author.Id,-4}| {author.FirstName,-20}| {author.LastName,-20}| {author.Email}");
                            }
                            Console.WriteLine("\nEnter author-ID: ");
                            tmp = Console.ReadLine();
                            if (uint.TryParse(tmp, out uint n) && !string.IsNullOrEmpty(tmp))
                            {
                                foreach (var author in authors)
                                {
                                    if (n == author.Id)
                                    {
                                        author_id = author.Id;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Enter the correct value!");
                                Thread.Sleep(sleepTime);
                                continue;
                            }
                            if (author_id == 0)
                            {
                                Console.WriteLine("There is no such id in the table");
                                Thread.Sleep(sleepTime);
                            }
                            else
                            {
                                passed = true;
                            }
                        } while (!passed);
                        passed = false;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("ID  |      Name      ");
                            genres = GenreController.GetGenre();
                            foreach (var genre in genres)
                            {
                                Console.WriteLine($"{genre.Id,-4}| {genre.Name}");
                            }
                            Console.WriteLine("\nEnter genre-ID: ");
                            tmp = Console.ReadLine();
                            if (uint.TryParse(tmp, out uint n) && !string.IsNullOrEmpty(tmp))
                            {
                                foreach (var genre in genres)
                                {
                                    if (n == genre.Id)
                                    {
                                        genre_id = genre.Id;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Enter the correct value!");
                                Thread.Sleep(sleepTime);
                                continue;
                            }
                            if (genre_id == 0)
                            {
                                Console.WriteLine("There is no such id in the table");
                                Thread.Sleep(sleepTime);
                            }
                            else
                            {
                                passed = true;
                            }
                        } while (!passed);

                        BookController.AddBook(title, description, release, author_id, genre_id);
                        Console.Clear();
                        Console.WriteLine("Added!");
                        Thread.Sleep(sleepTime);
                        break;
                    case "3":
                        passed = false;
                        books = BookController.GetBooks();
                        int book_id = 0;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("ID |        Title        |        Description       |   Release Date");
                            foreach (var book in books)
                            {
                                Console.WriteLine($"{book.Id,-3}| {book.Title,-20}| {book.Description,-25}| {book.Release.ToShortDateString(),-20}");
                            }
                            Console.WriteLine("Enter book-ID to delete: ");
                            tmp = Console.ReadLine();
                            if (!uint.TryParse(tmp, out uint m) || string.IsNullOrEmpty(tmp))
                            {
                                Console.WriteLine("Enter the correct value!");
                                Thread.Sleep(sleepTime);
                            }
                            else
                            {
                                foreach (var book in books)
                                {
                                    if (m == book.Id)
                                    {
                                        book_id = book.Id;
                                    }
                                }
                                if (book_id == 0)
                                {
                                    Console.WriteLine("There is no such id in the table");
                                    Thread.Sleep(sleepTime);
                                }
                                else
                                {
                                    passed = true;
                                    BookController.DeleteBook(book_id);
                                    Console.Clear();
                                    Console.WriteLine("Deleted!");
                                    Thread.Sleep(sleepTime);
                                }
                            }
                        } while (!passed);
                        break;
                    case "4":
                        author_id = 0;
                        genre_id = 0;
                        book_id = 0;
                        passed = false;
                        books = BookController.GetBooks();
                        genres = GenreController.GetGenre();
                        authors = AuthorController.GetAuthors();
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("ID |     Title     |      Description      | Release Date |       Author      |    Author email    |   Genre");
                            foreach (var book in books)
                            {
                                Console.WriteLine($"{book.Id,-3}| {book.Title,-14}| {book.Description,-22}| {book.Release.ToShortDateString(),-13}| {authors.Where(x => x.Id == book.AuthorId).Single().FirstName + " " + authors.Where(x => x.Id == book.AuthorId).Single().LastName,-18}| {authors.Where(x => x.Id == book.AuthorId).Single().Email,-19}| {genres.Where(x => x.Id == book.GenreId).Single().Name}");
                            }
                            Console.WriteLine("\nEnter book-ID you want to update: ");
                            tmp = Console.ReadLine();
                            if (uint.TryParse(tmp, out uint n) && !string.IsNullOrEmpty(tmp))
                            {
                                foreach (var book in books)
                                {
                                    if (n == book.Id)
                                    {
                                        book_id = book.Id;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Enter the correct value!");
                                Thread.Sleep(sleepTime);
                                continue;
                            }
                            if (book_id == 0)
                            {
                                Console.WriteLine("There is no such id in the table");
                                Thread.Sleep(sleepTime);
                            }
                            else
                            {
                                passed = true;
                            }
                        } while (!passed);
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Enter new title of the book: ");
                            title = Console.ReadLine();
                            if (string.IsNullOrEmpty(title))
                            {
                                Console.WriteLine("Enter the correct value!");
                                Thread.Sleep(sleepTime);
                            }
                        } while (string.IsNullOrEmpty(title));
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Enter new description of the book: ");
                            description = Console.ReadLine();
                            if (string.IsNullOrEmpty(description))
                            {
                                Console.WriteLine("Enter the correct value!");
                                Thread.Sleep(sleepTime);
                            }
                        } while (string.IsNullOrEmpty(description));
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Enter new release date of the book: ");
                            dateString = Console.ReadLine();
                            if (!DateTime.TryParse(dateString, out release) || string.IsNullOrEmpty(dateString) || release.Year < 1000)
                            {
                                Console.WriteLine("You have entered an incorrect value.");
                                Thread.Sleep(sleepTime);
                            }
                        } while (string.IsNullOrEmpty(dateString) || !DateTime.TryParse(dateString, out _));
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("ID  |     First Name      |       Last Name     |     Email     ");
                            authors = AuthorController.GetAuthors();
                            foreach (var author in authors)
                            {
                                Console.WriteLine($"{author.Id,-4}| {author.FirstName,-20}| {author.LastName,-20}| {author.Email}");
                            }
                            Console.WriteLine("\nEnter new author-ID: ");
                            tmp = Console.ReadLine();
                            if (uint.TryParse(tmp, out uint n) && !string.IsNullOrEmpty(tmp))
                            {
                                foreach (var author in authors)
                                {
                                    if (n == author.Id)
                                    {
                                        author_id = author.Id;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Enter the correct value!");
                                Thread.Sleep(sleepTime);
                                continue;
                            }
                            if (author_id == 0)
                            {
                                Console.WriteLine("There is no such id in the table");
                                Thread.Sleep(sleepTime);
                            }
                            else
                            {
                                passed = true;
                            }
                        } while (!passed);
                        passed = false;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("ID  |      Name      ");
                            genres = GenreController.GetGenre();
                            foreach (var genre in genres)
                            {
                                Console.WriteLine($"{genre.Id,-4}| {genre.Name}");
                            }
                            Console.WriteLine("\nEnter new genre-ID: ");
                            tmp = Console.ReadLine();
                            if (uint.TryParse(tmp, out uint n) && !string.IsNullOrEmpty(tmp))
                            {
                                foreach (var genre in genres)
                                {
                                    if (n == genre.Id)
                                    {
                                        genre_id = genre.Id;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Enter the correct value!");
                                Thread.Sleep(sleepTime);
                                continue;
                            }
                            if (genre_id == 0)
                            {
                                Console.WriteLine("There is no such id in the table");
                                Thread.Sleep(sleepTime);
                            }
                            else
                            {
                                passed = true;
                            }
                        } while (!passed);
                        BookController.UpdateBook(new Book { Id = book_id, AuthorId = author_id, Description = description, GenreId = genre_id, Release = release, Title = title });
                        Console.Clear();
                        Console.WriteLine("Updated!");
                        Thread.Sleep(sleepTime);
                        break;
                    default:
                        break;
                }
            } while (menu != "5");
        }

        public void MainMenu()
        {
            string menu;
            do
            {
                Console.Clear();
                Console.WriteLine("--------Book Catalogue--------");
                Console.WriteLine("1. Show the whole catalog");
                Console.WriteLine("2. Books management");
                Console.WriteLine("3. Authors management");
                Console.WriteLine("4. Genres management");
                Console.WriteLine("5. Search book");
                Console.WriteLine("6. Exit");
                Console.Write("\nChoose menu item: ");
                menu = Console.ReadLine();
                if (menu != "1" && menu != "2" && menu != "3" && menu != "4" && menu != "5")
                {
                    Console.WriteLine("Enter the correct value!");
                    Thread.Sleep(sleepTime);
                    continue;
                }
                switch (menu)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("ID |     Title     |      Description      | Release Date |       Author      |    Author email    |   Genre");
                        List<Book> books = BookController.GetBooks();
                        List<Genre> genres = GenreController.GetGenre();
                        List<Author> authors = AuthorController.GetAuthors();
                        foreach (var book in books)
                        {
                            Console.WriteLine($"{book.Id,-3}| {book.Title,-14}| {book.Description,-22}| {book.Release.ToShortDateString(),-13}| {authors.Where(x => x.Id == book.AuthorId).Single().FirstName + " " + authors.Where(x => x.Id == book.AuthorId).Single().LastName,-18}| {authors.Where(x => x.Id == book.AuthorId).Single().Email,-19}| {genres.Where(x => x.Id == book.GenreId).Single().Name}");
                        }
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                    case "2":
                        BookMenu();
                        break;
                    case "3":
                        AuthorMenu();
                        break;
                    case "4":
                        GenreMenu();
                        break;
                    case "5":
                        SearchBook();
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            } while (menu != "6");
            Console.WriteLine("Program stopps...\nBye!");
        }
    }
}

