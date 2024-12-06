using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrder.Business.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public int BookId { get; set; }

        public int CustomerId { get; set; }

        public int Quantity { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
