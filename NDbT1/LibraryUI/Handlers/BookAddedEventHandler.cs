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
            // add the book contained in the event to the ViewModel
            Book book = new Book()
            {
                Id = message.BookId,
                Title = message.Title,
                Author = message.Author,
                Copies=message.Copies,
                OnLoan=0,
                Available = message.Copies
            };


            LibraryBookModel db = LibraryBookModel.GetInstance();
            db.AddBook(book);

        }
    }
}