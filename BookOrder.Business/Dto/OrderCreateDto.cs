using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrder.Business.Dto
{
    public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleFor(x => x.BookId).NotEmpty()
                              .NotNull()
                              .NotEqual(Guid.Empty);

            RuleFor(x => x.CustomerId).NotEmpty()
                              .NotNull()
                              .NotEqual(Guid.Empty);

            RuleFor(x => x.Quantity).NotEmpty()
                              .NotNull()
                              .NotEqual(0);

            RuleFor(x => x.OrderDate).NotEmpty()
                              .NotNull()
                              .NotEqual(DateTime.MinValue)
                              .NotEqual(DateTime.MaxValue);
        }
    }
    public class OrderCreateDto
    {

        [Required(ErrorMessage = "Required field")]
        public Guid BookId { get; set; }

        [Required(ErrorMessage = "Required field")]
        public Guid CustomerId { get; set; }

        [Required(ErrorMessage = "Required field")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Required field")]
        public DateTime OrderDate { get; set; }
    }
}
