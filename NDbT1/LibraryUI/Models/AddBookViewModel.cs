using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LibraryUI.Models
{
    public class AddBookViewModel
    {
        public AddBookViewModel()
        {
            this.Title = string.Empty;
            this.Author = string.Empty;
            this.Copies = 0;
            this.Message = string.Empty;
        }

        [Required(ErrorMessage="Title cannot be blank")]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required(ErrorMessage="Author cannot be blank")]
        [DisplayName("Author")]
        public string Author { get; set; }
        
        [Required(ErrorMessage="Must specify at least 1 copy")]
        [Range(1,10,ErrorMessage="The number of copies must be between 1 and 10")]
        [DisplayName("Number of copies")]
        public int Copies { get; set; }

        public string Message { get; set; }
    }
}