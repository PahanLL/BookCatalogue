namespace BookCatalogue
{
    class Program
    {
        static void Main(string[] args)
        {
            SQL.InitialCreate();
            Menu menu = new Menu();
            menu.MainMenu();
        }
    }
}
