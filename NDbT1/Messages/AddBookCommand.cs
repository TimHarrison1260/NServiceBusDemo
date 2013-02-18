using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NServiceBus;

namespace Messages
{
    [Serializable]
    public class AddBookCommand : IMessage
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Copies { get; set; }
    }
}
