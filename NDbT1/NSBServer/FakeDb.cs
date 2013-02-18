using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSBServer
{
    /// <summary>
    /// Singleton class that mimics a repository and the 
    /// database behind it. 
    /// </summary>
    public class FakeDb
    {
        //  The fake database
        private static List<Book> _library = new List<Book>();

        public static FakeDb _instance;
        public static object _syncLock = new object();

        private FakeDb() {
            LoadDatabase();
        }

        /// <summary>
        /// Returns the singleton instance of the FakeDatabase.
        /// </summary>
        /// <returns></returns>
        public static FakeDb GetInstance()
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new FakeDb();
                    }
                }
            }
            return _instance;
        }

        /// <summary>
        /// Add a book to the fake database
        /// </summary>
        /// <param name="book">The book to be added</param>
        /// <returns>The Id of the book added.</returns>
        /// <remarks>
        /// If the book alread exists, comparison by Title, 
        /// then the copies are added to it.
        /// </remarks>
        public int AddBook(Book book)
        {
            int rc = 0;
            //  Look for the book by Title.
            var idx = _library.FindIndex(b => b.Title == book.Title);
            if (idx >= 0)
            {
                //  Update existing by adding the number of copies
                foreach (var c in book.Copies)
                    _library[idx].Copies.Add(c);
                rc = _library[idx].Id;
            }
            else
            {
                //  Assign the next available id.
                book.Id = GetNextId();
                //  Add a new book to the library
                _library.Add(book);
                rc = book.Id;
            }
            return rc;
        }

        /// <summary>
        /// Get the next available Id from the library
        /// </summary>
        /// <returns></returns>
        private int GetNextId()
        {
            int newId = _library.Max(b => b.Id);
            return newId + 1;
        }

        /// <summary>
        /// Initialise the data.
        /// </summary>
        private void LoadDatabase()
        {
            Book bk1 = new Book()
            {
                Id = 1,
                Title = "The furst book",
                Author = "Someone",
                Copies = new List<Copy>()
            };
            bk1.Copies.Add(new Copy() { IsAvailable = false });
            bk1.Copies.Add(new Copy() { IsAvailable = true });

            Book bk2 = new Book()
            {
                Id = 2,
                Title = "The Second book",
                Author = "Someone Else",
                Copies = new List<Copy>()
            };
            bk2.Copies.Add(new Copy() { IsAvailable = false });

            Book bk3 = new Book()
            {
                Id = 3,
                Title = "The Third book",
                Author = "Someone",
                Copies = new List<Copy>()
            };
            bk3.Copies.Add(new Copy() { IsAvailable = false });
            bk3.Copies.Add(new Copy() { IsAvailable = true });
            bk3.Copies.Add(new Copy() { IsAvailable = true });

            _library.Add(bk1);
            _library.Add(bk2);
            _library.Add(bk3);
        }



    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set;}
        public string Author { get; set; }
        public List<Copy> Copies { get; set; }
    }

    public class Copy
    {
        public bool IsAvailable { get; set; }

        public Copy()
        {
            this.IsAvailable = true;
        }
    }
}
