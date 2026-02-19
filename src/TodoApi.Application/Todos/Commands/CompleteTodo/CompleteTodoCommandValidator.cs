using FluentValidation;

namespace TodoApi.Application.Todos.Commands.CompleteTodo;

public class CompleteTodoCommandValidator : AbstractValidator<CompleteTodoCommand>
{
    public CompleteTodoCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Todo Id is required.");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
    }
}
