using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrder.Business.Dto
{
    public class BookCreateDtoValidator : AbstractValidator<BookCreateDto>
    {
        public BookCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                              .NotNull()
                              .MinimumLength(2);

            RuleFor(x => x.Author).NotEmpty()
                              .NotNull()
                              .MinimumLength(2);

            RuleFor(x => x.Stock).NotEmpty()
                              .NotNull()
                              .NotEqual(0);

            RuleFor(x => x.Amount).NotEmpty()
                              .NotNull()
                              .NotEqual(0);
        }
    }

    public class BookCreateDto
    {
        [Required(ErrorMessage = "Required field")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required field")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Required field")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Required field")]
        public decimal Amount { get; set; }
    }
}
