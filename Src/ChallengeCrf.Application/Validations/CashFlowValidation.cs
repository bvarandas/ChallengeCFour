using ChallengeCrf.Application.Commands;
using FluentValidation;

namespace ChallengeCrf.Application.Validations;

public abstract class CashFlowValidation<T> : AbstractValidator<T> where T : CashFlowCommand
{
    protected void ValidateCashFlowId()
    {
        RuleFor(c => c.CashFlowId)
            .NotEqual("");
    }

    protected void ValidateDescription()
    {
        RuleFor(c => c.Description)
            .NotEmpty().WithMessage("É necessário inserir a Descrição!")
            .Length(2, 150).WithMessage("É necessário inserir ao menos 2 caracteres na Descrição!");
    }

    protected void ValidateCashValue()
    {
        RuleFor(c => c.Amount)
            .NotEqual(0)
            .WithMessage("É necessário inserir um valor válido!");
   
    }

    protected void ValidateCashDirection()
    {
        RuleFor(c => c.Entry)
            .NotEmpty().WithMessage("É necessário inserir o Lançamento!")
            .WithMessage("É necessário inserir Débito ou Crédito!");
    }

    protected void ValidateDate()
    {
        RuleFor(c => c.Date)
            .NotEmpty()
            .WithMessage("É necessário inserir a Data !")
            //.Length(1, 150)
            .WithMessage("É necessário inserir ao menos 1 caracter na Data!");
    }
}
