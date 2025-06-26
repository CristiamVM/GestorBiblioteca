using GestorBiblioteca;
using System;

class Program
{
    static void Main()
    {

        LibraryManager library = new LibraryManager();

        library.AddBook("Cien Años de Soledad", "Gabriel García Márquez", "1", 1967);
        library.AddBook("1984", "George Orwell", "2", 1949);
        library.AddBook("Don Quijote de la Mancha", "Miguel de Cervantes", "3", 1605);
        library.AddBook("El Principito", "Antoine de Saint-Exupéry", "4", 1943);
        library.AddBook("Crónica de una Muerte Anunciada", "Gabriel García Márquez", "5", 1981);

        bool continuar = true;
        while (continuar)
        {
            Console.WriteLine("\n--- Biblioteca Digital ---");
            Console.WriteLine("1. Agregar libro");
            Console.WriteLine("2. Eliminar libro");
            Console.WriteLine("3. Buscar libro por título");
            Console.WriteLine("4. Listar todos los libros");
            Console.WriteLine("5. Verificar disponibilidad");
            Console.WriteLine("6. Prestar libro");
            Console.WriteLine("7. Devolver libro");
            Console.WriteLine("8. Salir");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Título: ");
                    string titulo = Console.ReadLine();
                    Console.Write("Autor: ");
                    string autor = Console.ReadLine();
                    Console.Write("ISBN: ");
                    string isbn = Console.ReadLine();
                    Console.Write("Año: ");
                    int anio = int.Parse(Console.ReadLine());
                    library.AddBook(titulo, autor, isbn, anio);
                    break;

                case "2":
                    Console.Write("ISBN del libro a eliminar: ");
                    library.RemoveBook(Console.ReadLine());
                    break;

                case "3":
                    Console.Write("Ingrese parte del título a buscar: ");
                    var resultados = library.SearchByTitle(Console.ReadLine());
                    if (resultados.Count == 0)
                        Console.WriteLine("No se encontraron libros.");
                    else
                        resultados.ForEach(Console.WriteLine);
                    break;

                case "4":
                    library.ListBooks();
                    break;

                case "5":
                    Console.Write("Ingrese ISBN del libro: ");
                    bool disponible = library.IsAvailable(Console.ReadLine());
                    Console.WriteLine(disponible ? "El libro está disponible." : "El libro está prestado o no existe.");
                    break;

                case "6":
                    Console.Write("Ingrese ISBN del libro a prestar: ");
                    library.BorrowBook(Console.ReadLine());
                    break;

                case "7":
                    Console.Write("Ingrese ISBN del libro a devolver: ");
                    library.ReturnBook(Console.ReadLine());
                    break;

                case "8":
                    continuar = false;
                    break;

                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    }
}
