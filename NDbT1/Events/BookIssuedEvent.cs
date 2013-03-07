using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    class BookIssuedEvent
    {
        public int BookId { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
