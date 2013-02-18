using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NServiceBus;

namespace Events
{
    public class BookReturnedEvent : IEvent
    {
        public int BookId{ get; set; }
    }
}
