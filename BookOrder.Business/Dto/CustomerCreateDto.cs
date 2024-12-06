using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrder.Business.Dto
{
    public class CustomerCreateDtoValidator : AbstractValidator<CustomerCreateDto>
    {
        public CustomerCreateDtoValidator()
        {

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

    public class CustomerCreateDto
    {
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
