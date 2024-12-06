using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrder.Business.Dto
{
    public class BookUpdateDtoValidator : AbstractValidator<BookUpdateDto>
    {
        public BookUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty()
                              .NotNull()
                              .NotEqual(Guid.Empty);

            RuleFor(x => x.Name).NotEmpty()
                              .NotNull()
                              .MinimumLength(2);

            RuleFor(x => x.Author).NotEmpty()
                              .NotNull()
                              .MinimumLength(2);

            RuleFor(x => x.Amount).NotEmpty()
                              .NotNull()
                              .NotEqual(0);
        }
    }

    public class BookUpdateDto
    {
        [Required(ErrorMessage = "Required field")]
        public Guid Id { get; set; }

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
