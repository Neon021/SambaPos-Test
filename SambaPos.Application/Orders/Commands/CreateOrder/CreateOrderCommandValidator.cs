using FluentValidation;

namespace SambaPos.Application.Orders.Commands.CreateOrder;
internal class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Contents).NotEmpty();
    }
}
