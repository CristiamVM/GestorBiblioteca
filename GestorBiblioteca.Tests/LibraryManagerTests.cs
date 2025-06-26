using NUnit.Framework;
using GestorBiblioteca;
using System.Collections.Generic;
using System.IO;

namespace GestorBiblioteca.Tests
{
    [TestFixture]
    public class LibraryManagerTests
    {
        private LibraryManager library;
        private StringWriter consoleOutput;

        [SetUp]
        public void Setup()
        {
            library = new LibraryManager();
            consoleOutput = new StringWriter();
            System.Console.SetOut(consoleOutput);
        }

        [TearDown]
        public void TearDown()
        {
            consoleOutput.Dispose();
            System.Console.SetOut(System.Console.Out);
        }

        // 1. AddBook
        [Test]
        public void TC1_1_AddBook_ValidData_BookAdded()
        {
            // Arrange
            // Act
            library.AddBook("Cien Años de Soledad", "Gabriel García Márquez", "1111", 1967);

            // Assert
            Assert.IsTrue(library.IsAvailable("1111"));
        }

        [Test]
        public void TC1_2_AddBook_DuplicateISBN_ShowsErrorAndDoesNotAdd()
        {
            // Arrange
            library.AddBook("Cien Años de Soledad", "Gabriel García Márquez", "1111", 1967);
            consoleOutput.GetStringBuilder().Clear();

            // Act
            library.AddBook("Otro Libro", "Otro Autor", "1111", 2023);

            // Assert
            string output = consoleOutput.ToString();
            Assert.IsTrue(output.Contains("Ya existe un libro con ese ISBN."));
        }

        [Test]
        public void TC1_3_AddBook_InvalidYear_NoValidationPresent()
        {
            // Arrange
            // Act
            library.AddBook("Libro Prueba", "Autor", "9999", -2020);

            // Assert
            Assert.IsTrue(library.IsAvailable("9999"));
        }

        // 2. RemoveBook
        [Test]
        public void TC2_1_RemoveBook_ExistingBook_RemovesSuccessfully()
        {
            library.AddBook("Libro", "Autor", "1234", 2000);
            consoleOutput.GetStringBuilder().Clear();

            // Act
            library.RemoveBook("1234");

            // Assert
            string output = consoleOutput.ToString();
            Assert.IsTrue(output.Contains("Libro eliminado exitosamente."));
            Assert.IsFalse(library.IsAvailable("1234"));
        }

        [Test]
        public void TC2_2_RemoveBook_NonExistent_ShowsError()
        {
            // Act
            library.RemoveBook("0000");

            // Assert
            string output = consoleOutput.ToString();
            Assert.IsTrue(output.Contains("No se encontró el libro con ese ISBN."));
        }

        // 3. SearchByTitle
        [Test]
        public void TC3_1_SearchByTitle_ExactMatch_FindsBook()
        {
            library.AddBook("1984", "George Orwell", "2222", 1949);

            // Act
            var result = library.SearchByTitle("1984");

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("1984", result[0].Title);
        }

        [Test]
        public void TC3_2_SearchByTitle_PartialMatch_FindsBook()
        {
            library.AddBook("Cien Años de Soledad", "Gabriel García Márquez", "1111", 1967);

            // Act
            var result = library.SearchByTitle("soledad");

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Cien Años de Soledad", result[0].Title);
        }

        [Test]
        public void TC3_3_SearchByTitle_NoMatch_ReturnsEmpty()
        {
            // Act
            var result = library.SearchByTitle("Harry Potter");

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        // 4. ListBooks
        [Test]
        public void TC4_1_ListBooks_WithBooks_DisplaysAll()
        {
            library.AddBook("Libro 1", "Autor", "0001", 2000);
            library.AddBook("Libro 2", "Autor", "0002", 2001);

            consoleOutput.GetStringBuilder().Clear();

            // Act
            library.ListBooks();
            var output = consoleOutput.ToString();

            // Assert
            Assert.IsTrue(output.Contains("Libro 1"));
            Assert.IsTrue(output.Contains("Libro 2"));
        }

        [Test]
        public void TC4_2_ListBooks_EmptyCollection_ShowsMessage()
        {
            // Act
            library.ListBooks();
            var output = consoleOutput.ToString();

            // Assert
            Assert.IsTrue(output.Contains("No hay libros en la biblioteca."));
        }

        // 5. IsAvailable
        [Test]
        public void TC5_1_IsAvailable_BookNotBorrowed_ReturnsTrue()
        {
            library.AddBook("Disponible", "Autor", "8888", 2010);

            // Act & Assert
            Assert.IsTrue(library.IsAvailable("8888"));
        }

        [Test]
        public void TC5_2_IsAvailable_BookBorrowed_ReturnsFalse()
        {
            library.AddBook("Prestado", "Autor", "9999", 2011);
            library.BorrowBook("9999");

            // Act & Assert
            Assert.IsFalse(library.IsAvailable("9999"));
        }

        [Test]
        public void TC5_3_IsAvailable_BookDoesNotExist_ReturnsFalse()
        {
            // Act & Assert
            Assert.IsFalse(library.IsAvailable("0000"));
        }

        // 6. BorrowBook
        [Test]
        public void TC6_1_BorrowBook_AvailableBook_Success()
        {
            library.AddBook("1984", "George Orwell", "2222", 1949);
            consoleOutput.GetStringBuilder().Clear();

            // Act
            library.BorrowBook("2222");

            // Assert
            Assert.IsFalse(library.IsAvailable("2222"));
            Assert.IsTrue(consoleOutput.ToString().Contains("Libro prestado correctamente."));
        }

        [Test]
        public void TC6_2_BorrowBook_AlreadyBorrowed_ShowsMessage()
        {
            library.AddBook("1984", "George Orwell", "2222", 1949);
            library.BorrowBook("2222");
            consoleOutput.GetStringBuilder().Clear();

            // Act
            library.BorrowBook("2222");

            // Assert
            Assert.IsTrue(consoleOutput.ToString().Contains("El libro ya está prestado."));
        }

        [Test]
        public void TC6_3_BorrowBook_NonExistentBook_ShowsMessage()
        {
            // Act
            library.BorrowBook("9999");

            // Assert
            Assert.IsTrue(consoleOutput.ToString().Contains("Libro no encontrado."));
        }

        // 7. ReturnBook
        [Test]
        public void TC7_1_ReturnBook_BookWasBorrowed_Success()
        {
            library.AddBook("1984", "George Orwell", "2222", 1949);
            library.BorrowBook("2222");
            consoleOutput.GetStringBuilder().Clear();

            // Act
            library.ReturnBook("2222");

            // Assert
            Assert.IsTrue(library.IsAvailable("2222"));
            Assert.IsTrue(consoleOutput.ToString().Contains("Libro devuelto correctamente."));
        }

        [Test]
        public void TC7_2_ReturnBook_BookWasNotBorrowed_ShowsMessage()
        {
            library.AddBook("1984", "George Orwell", "2222", 1949);
            consoleOutput.GetStringBuilder().Clear();

            // Act
            library.ReturnBook("2222");

            // Assert
            Assert.IsTrue(consoleOutput.ToString().Contains("El libro ya estaba disponible."));
        }

        [Test]
        public void TC7_3_ReturnBook_BookDoesNotExist_ShowsMessage()
        {
            // Act
            library.ReturnBook("0000");

            // Assert
            Assert.IsTrue(consoleOutput.ToString().Contains("Libro no encontrado."));
        }
    }
}
