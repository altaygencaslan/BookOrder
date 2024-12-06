using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrder.Data.Domain
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Stock { get; set; }
        public decimal Amount { get; set; }
    }
}
