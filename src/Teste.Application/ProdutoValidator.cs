using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Teste.Domain;
using Teste.Domain.Interfaces;

namespace Teste.Application;

public class ProdutoValidator : AbstractValidator<Produto>
{
    public ProdutoValidator()
    {
        
        RuleFor(p => p.Nome)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.")
            .MaximumLength(100).WithMessage("O nome do produto pode ter no máximo 100 caracteres.");

        RuleFor(p => p.Preco)
            .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");

        RuleFor(p => p.Descricao)
            .MaximumLength(255).WithMessage("A descrição pode ter no máximo 255 caracteres.");

        RuleFor(p => p.DataCadastro)
            .NotNull().WithMessage("A data de cadastro não pode ser nula.");
    }
}