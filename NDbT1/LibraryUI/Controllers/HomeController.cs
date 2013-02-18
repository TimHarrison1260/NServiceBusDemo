using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

using LibraryUI.Models;
using Messages;
using NServiceBus;

namespace LibraryUI.Controllers
{
    public class HomeController : AsyncController
    {
        //  Instance of the message bus, (property injection)
        public IBus Bus {get; set;}

        public ActionResult Index()
        {
            ViewBag.Message = "Displays a list of Books in the Library";

            //  Get the books from the cached data model.
            LibraryBookModel db = LibraryBookModel.GetInstance();
            var books = db.GetAllBooks();

            return View(books);
        }

        public ActionResult AddBook()
        {
            var newBook = new AddBookViewModel();
            return View(newBook);
        }

        [HttpPost]
        [AsyncTimeout(50000)]
        public ActionResult AddBookAsync(AddBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                //  Create message from the data entered.
                Messages.AddBookCommand msg = new AddBookCommand()
                {
                    Title = model.Title,
                    Author = model.Author,
                    Copies = model.Copies
                };

                //  Send the message through the bus to the endpoint (defined in Web.Config)
                //  This is an Asynchronous call.
                Bus.Send(msg)
                    .Register<CommandSatus>(result =>
                    {
                        //  NServiceBus Callback function: store the result in the AsyncManager.
                        AsyncManager.Parameters["result"] = result;
                    });
            }
            return RedirectToAction("Index");
        }

        public ActionResult AddBookCompleted(CommandSatus result)
        {
            TempData.Add("Return", Enum.GetName(typeof(CommandSatus), result));
            return RedirectToAction("Index");
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    
    }
}
