using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookOrder.Business.Dto
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {

            RuleFor(x => x.Email).NotEmpty()
                              .NotNull();


            RuleFor(x => x.Password).NotEmpty()
                              .NotNull();
        }
    }
    public class LoginDto
    {
        [Required(ErrorMessage = "Required field")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Required field")]
        public string Password { get; set; }
    }
}
