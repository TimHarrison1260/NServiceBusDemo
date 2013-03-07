using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NServiceBus;
using Events;
using LibraryUI.Models;

namespace LibraryUI.Handlers
{
    public class BookAddedEventHandler : IHandleMessages<BookAddedEvent>
    {
        public void Handle(BookAddedEvent message)
        {
            // Construct a UI View Model Book from the event details
            Book book = new Book()
            {
                Id = message.BookId,
                Title = message.Title,
                Author = message.Author,
                Copies=message.Copies,
                OnLoan=0,
                Available = message.Copies
            };
            //  Add the book to the UI View Model.
            LibraryBookModel db = LibraryBookModel.GetInstance();
            db.AddBook(book);
        }
    }
}