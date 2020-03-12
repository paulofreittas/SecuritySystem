using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Systems = SecuritySystem.Domain.Entities.System;

namespace SecuritySystem.Domain.Validators
{
    // Usando o Fluent Validation para realizar as validações, aqui são definidas regras para algumas propriedades do modelo que sempre são validadas ao serem recebidas na requisição
    public class SystemValidator : AbstractValidator<Systems>
    {
        public SystemValidator()
        {                                       // WithMessage("Preencha o campo 'Descrição'")
            RuleFor(c => c.Description).NotEmpty().WithMessage(Messages.InvalidData)
                                       .MaximumLength(100).WithMessage("Número máximo de 100 caracteres");

            RuleFor(c => c.Initials).NotEmpty().WithMessage(Messages.InvalidData)
                                    .MaximumLength(100).WithMessage("Número máximo de 100 caracteres");

            RuleFor(c => c.Email).Matches("^(([^<>()[\\]\\.,;:\\s@\"]+(\\.[^<>()[\\]\\.,;:\\s@\"]+)*)|(\".+\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$").WithMessage(Messages.InvalidEmail);

            RuleFor(c => c.Url).MaximumLength(100).WithMessage("Número máximo de 50 caracteres");

            RuleFor(c => c.Status).NotNull().WithMessage(Messages.InvalidData);
        }
    }
}
