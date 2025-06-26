using NUnit.Framework;
using GestorBiblioteca;
using System.Collections.Generic;
using System.Linq;

namespace GestorBibliotecaTests
{
    [TestFixture]
    public class LibraryManagerTests
    {
        private LibraryManager library;

        [SetUp]
        public void Setup()
        {
            library = new LibraryManager();
            library.AddBook("Cien Años de Soledad", "Gabriel García Márquez", "1111", 1967);
            library.AddBook("1984", "George Orwell", "2222", 1949);
        }

        [Test]
        public void AddBook_ShouldAddBook_WhenIsbnIsUnique()
        {
            library.AddBook("Nuevo Libro", "Autor", "3333", 2020);
            Assert.IsTrue(library.IsAvailable("3333"));
        }

        [Test]
        public void AddBook_ShouldNotAddBook_WhenIsbnExists()
        {
            library.AddBook("Otro Libro", "Autor", "1111", 2021);
            var results = library.SearchByTitle("Otro Libro");
            Assert.IsEmpty(results);
        }

        [Test]
        public void RemoveBook_ShouldRemove_WhenBookExists()
        {
            library.RemoveBook("1111");
            Assert.IsFalse(library.IsAvailable("1111"));
        }

        [Test]
        public void RemoveBook_ShouldHandle_WhenBookDoesNotExist()
        {
            Assert.DoesNotThrow(() => library.RemoveBook("0000"));
        }

        [Test]
        public void SearchByTitle_ShouldReturnExactMatch()
        {
            var result = library.SearchByTitle("1984");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("1984", result[0].Title);
        }

        [Test]
        public void SearchByTitle_ShouldReturnPartialMatch()
        {
            var result = library.SearchByTitle("soledad");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Cien Años de Soledad", result[0].Title);
        }

        [Test]
        public void SearchByTitle_ShouldReturnEmpty_WhenNotFound()
        {
            var result = library.SearchByTitle("Harry Potter");
            Assert.IsEmpty(result);
        }

        [Test]
        public void ListBooks_ShouldReturnAllBooks()
        {
            var result = library.SearchByTitle(""); // Devuelve todos
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void IsAvailable_ShouldReturnTrue_WhenBookIsNotBorrowed()
        {
            Assert.IsTrue(library.IsAvailable("2222"));
        }

        [Test]
        public void IsAvailable_ShouldReturnFalse_WhenBookIsBorrowed()
        {
            library.BorrowBook("2222");
            Assert.IsFalse(library.IsAvailable("2222"));
        }

        [Test]
        public void IsAvailable_ShouldReturnFalse_WhenBookDoesNotExist()
        {
            Assert.IsFalse(library.IsAvailable("0000"));
        }

        [Test]
        public void BorrowBook_ShouldSetBookAsBorrowed()
        {
            library.BorrowBook("2222");
            Assert.IsFalse(library.IsAvailable("2222"));
        }

        [Test]
        public void BorrowBook_ShouldNotFail_WhenBookIsAlreadyBorrowed()
        {
            library.BorrowBook("2222");
            Assert.DoesNotThrow(() => library.BorrowBook("2222"));
        }

        [Test]
        public void BorrowBook_ShouldNotFail_WhenBookDoesNotExist()
        {
            Assert.DoesNotThrow(() => library.BorrowBook("0000"));
        }

        [Test]
        public void ReturnBook_ShouldSetBookAsAvailable()
        {
            library.BorrowBook("2222");
            library.ReturnBook("2222");
            Assert.IsTrue(library.IsAvailable("2222"));
        }

        [Test]
        public void ReturnBook_ShouldNotFail_WhenBookIsNotBorrowed()
        {
            Assert.DoesNotThrow(() => library.ReturnBook("2222"));
        }

        [Test]
        public void ReturnBook_ShouldNotFail_WhenBookDoesNotExist()
        {
            Assert.DoesNotThrow(() => library.ReturnBook("0000"));
        }
    }
}