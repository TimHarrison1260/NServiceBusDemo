using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NServiceBus;

namespace Messages
{
    public class BorrowBookCommand : IMessage
    {
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public string Name { get; set; }
    }
}
