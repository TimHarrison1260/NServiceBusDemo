using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryUI.Models
{
    public class LibraryBookModel
    {
        private static List<Book> _libraryBooks = new List<Book>();

        private static LibraryBookModel _instance = null;

        private static object _syncLock = new object();

        private LibraryBookModel()
        {
            LoadData();
        }

        public static LibraryBookModel GetInstance()
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new LibraryBookModel();
                    }
                }
            }
            return _instance;
        }


        public IEnumerable<Book> GetAllBooks()
        {
            return _libraryBooks.AsEnumerable<Book>();
        }

        public void AddBook(Book book)
        {
            //  Find book in library
            int idx = _libraryBooks.FindIndex(b => b.Id == book.Id);
            //  If found, add to counters
            if (idx >= 0)
            {
                _libraryBooks[idx].Copies += book.Copies;
                _libraryBooks[idx].Available += book.Copies;
            }
            else
            {
                //  Add the copies to the library
                book.OnLoan = 0;
                book.Available = book.Copies;
                _libraryBooks.Add(book);
            }
        }

        public void BorrowBook(Book book)
        {
            throw new NotImplementedException();
        }

        public void ReturnBook(Book book)
        {
            throw new NotImplementedException();
        }


        public void LoadData()
        {
            Book bk1 = new Book()
            {
                Id = 1,
                Title = "The furst book",
                Author="Someone",
                Copies=2,
                Available=1,
                OnLoan=1            };
            Book bk2 = new Book()
            {
                Id = 2,
                Title = "The Second book",
                Author = "Someone Else",
                Copies = 1,
                Available = 1,
                OnLoan = 0
            };
            Book bk3 = new Book()
            {
                Id = 3,
                Title = "The Third book",
                Author = "Someone",
                Copies = 3,
                Available = 2,
                OnLoan = 1
            };

            _libraryBooks.Add(bk1);
            _libraryBooks.Add(bk2);
            _libraryBooks.Add(bk3);

        }
    }

    public class Book
    {
        /// <summary>
        /// The Id of the Book
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Title of the book
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The Author of the book
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// The total number of copies of the book, 
        /// available in the library
        /// </summary>
        public int Copies { get; set; }
        /// <summary>
        /// The total number of copies on loan
        /// </summary>
        public int OnLoan { get; set; }
        /// <summary>
        /// The number of books available to be borrowed
        /// </summary>
        public int Available { get; set; }
    }
}