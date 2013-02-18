using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;
using Events;
using NServiceBus;

namespace NSBServer
{
    public class AddBookCommandHandler : IHandleMessages<AddBookCommand>
    {
        public IBus Bus { get; set; }

        public void Handle(AddBookCommand message)
        {
            //  For the minute, write the message to the console
            Console.WriteLine("=============================================");
            Console.WriteLine("AddBookCommand received");
            Console.WriteLine("\tTitle:\t{0}", message.Title);
            Console.WriteLine("\tAuthor:\t{0}", message.Author);
            Console.WriteLine("\tCopies:\t{0}", message.Copies);

            //  Update the data model
            //  1. create instance of Book.
            Book newBook = new Book()
            {
                Title=message.Title,
                Author=message.Author,
                Copies = new List<Copy>()
            };
            //  Ad the number of copies specified
            for (int i = 1; i <= message.Copies; i++)
            {
                newBook.Copies.Add(new Copy());
            }
            //  Add the book to the Fake database
            FakeDb db = FakeDb.GetInstance();
            int newId = db.AddBook(newBook);

            //  Raise the BookAddedEvent
            BookAddedEvent e = new BookAddedEvent()
            {
                BookId = newId,             // the returned Id from the Add to Database method
                Title = message.Title,
                Author = message.Author,
                Copies = message.Copies
            };
            Bus.Publish(e);

            //  Return a success message for the moment.
            CommandSatus response = CommandSatus.Successful;
            Bus.Return<CommandSatus>(response);
        }
    }
}
