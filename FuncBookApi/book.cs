﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncBookApi
{
    public class Book
    {
        [Key]
        [Required]
        public int BookId { get; set; }
        public string BookTitle { get; set; }

        public string BookLocationName { get; set; }
        public string BookLocationPath { get; set; }
        public string BookAuthor { get; set; }
        public string BookGenre { get; set; }
        public DateTime? DatePublished { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
