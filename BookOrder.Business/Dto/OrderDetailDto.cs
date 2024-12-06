using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrder.Business.Dto
{
    public class OrderDetailDto
    {
        public Guid Id { get; set; }
        public BookDto BookDetail { get; set; }
        public CustomerDto CustomerDetail { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
