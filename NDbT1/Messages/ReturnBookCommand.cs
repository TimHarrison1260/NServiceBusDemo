using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NServiceBus;

namespace Messages
{
    class ReturnBookCommand : IMessage
    {
        public int BookId { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
