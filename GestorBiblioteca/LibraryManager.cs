using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBiblioteca
{
    public class LibraryManager
    {
        private List<Book> books = new List<Book>();

        // 1. Agregar un libro
        public void AddBook(string title, string author, string isbn, int year)
        {
            if (books.Any(b => b.ISBN == isbn))
            {
                Console.WriteLine("Ya existe un libro con ese ISBN.");
                return;
            }
            books.Add(new Book { Title = title, Author = author, ISBN = isbn, Year = year });
        }

        // 2. Eliminar un libro
        public void RemoveBook(string isbn)
        {
            var book = books.FirstOrDefault(b => b.ISBN == isbn);
            if (book != null)
            {
                books.Remove(book);
                Console.WriteLine("Libro eliminado exitosamente.");
            }
            else
            {
                Console.WriteLine("No se encontró el libro con ese ISBN.");
            }
        }

        // 3. Buscar un libro por título
        public List<Book> SearchByTitle(string title)
        {
            return books.Where(b => b.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        // 4. Listar todos los libros
        public void ListBooks()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("No hay libros en la biblioteca.");
                return;
            }

            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }

        // 5. Verificar disponibilidad
        public bool IsAvailable(string isbn)
        {
            var book = books.FirstOrDefault(b => b.ISBN == isbn);
            return book != null && !book.IsBorrowed;
        }

        // 6. Prestar un libro
        public void BorrowBook(string isbn)
        {
            var book = books.FirstOrDefault(b => b.ISBN == isbn);
            if (book == null)
            {
                Console.WriteLine("Libro no encontrado.");
            }
            else if (book.IsBorrowed)
            {
                Console.WriteLine("El libro ya está prestado.");
            }
            else
            {
                book.IsBorrowed = true;
                Console.WriteLine("Libro prestado correctamente.");
            }
        }

        // 7. Devolver un libro
        public void ReturnBook(string isbn)
        {
            var book = books.FirstOrDefault(b => b.ISBN == isbn);
            if (book == null)
            {
                Console.WriteLine("Libro no encontrado.");
            }
            else if (!book.IsBorrowed)
            {
                Console.WriteLine("El libro ya estaba disponible.");
            }
            else
            {
                book.IsBorrowed = false;
                Console.WriteLine("Libro devuelto correctamente.");
            }
        }
    }
}
