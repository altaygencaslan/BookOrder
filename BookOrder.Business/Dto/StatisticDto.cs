using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrder.Business.Dto
{
    public class StatisticDto
    {
        public string NameOfMonth { get; set; }
        public int TotalOrderCount { get; set; }
        public int TotalBookCount { get; set; }
        public decimal TotalPurchasedAmount { get; set; }
    }
}
