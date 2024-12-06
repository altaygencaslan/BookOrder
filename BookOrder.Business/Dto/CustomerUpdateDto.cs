using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrder.Business.Dto
{
    public class CustomerUpdateDtoValidator : AbstractValidator<CustomerUpdateDto>
    {
        public CustomerUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty()
                              .NotNull()
                              .NotEqual(Guid.Empty);

            RuleFor(x => x.Name).NotEmpty()
                              .NotNull()
                              .MinimumLength(2);

            RuleFor(x => x.LastName).NotEmpty()
                              .NotNull()
                              .MinimumLength(2);

            RuleFor(x => x.Email).NotEmpty()
                              .NotNull()
                              .MinimumLength(6);

            RuleFor(x => x.Password).NotEmpty()
                              .NotNull()
                              .MinimumLength(6);
        }
    }

    public class CustomerUpdateDto
    {
        [Required(ErrorMessage = "Required field")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Required field")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required field")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required field")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required field")]
        public string Password { get; set; }
    }
}
