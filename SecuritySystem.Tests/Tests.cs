using SecuritySystem.Domain.Enums;
using SecuritySystem.Domain.Validators;
using System;
using Xunit;
using Systems = SecuritySystem.Domain.Entities.System;

namespace SecuritySystem.Tests
{
    public class Tests
    {
        private SystemValidator validator;
        public Tests()
        {
            validator = new SystemValidator();
        }

        // Teste para garantir a regra de negócio de validação de email, específicada no caso de uso.
        [Fact]
        public void System_validEmail_ShouldValidateEmail()
        {
            // Arrange
            var validSystem = new Systems
            {
                Id = 5,
                Description = "One Description",
                Initials = "AABC",
                Email = "x@x.xx",
                Status = Status.ATIVO,
            };

            // Assert
            Assert.True(validator.Validate(validSystem).IsValid);
        }
    }
}
