using Addressbook.Models.Dtos;
using FluentValidation;

namespace Addressbook.Application.Validations
{
    public class AddressBookDtoValidator : AbstractValidator<IpAddressBookDto>
    {
        public AddressBookDtoValidator()
        {
            RuleFor(x => x.IP)
                .NotEmpty()
                .WithMessage("IP cannot be empty");
        }
    }
}
