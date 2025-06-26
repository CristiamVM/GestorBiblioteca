using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBiblioteca
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int Year { get; set; }
        public bool IsBorrowed { get; set; } = false;

        public override string ToString()
        {
            return $"{Title} escrito por {Author} (ISBN: {ISBN}, Año: {Year}) - {(IsBorrowed ? "Prestado" : "Disponible")}";
        }
    }
}
