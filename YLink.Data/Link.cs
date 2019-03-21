using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YLink.Data
{
    public class Link
    {
        public int Id { get; set; }

        [Required]
        public string Key { get; set; }

        [Url]
        public string Url { get; set; }

        public string Description { get; set; }

        public DateTimeOffset Modified { get; set; }
    }
}
