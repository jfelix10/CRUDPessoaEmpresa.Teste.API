using CRUDPessoaEmpresa.Teste.API.Application.Clients.Commands;
using CRUDPessoaEmpresa.Teste.API.Infra.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CRUDPessoaEmpresa.Teste.API.Application.Clients.Validators;

public class CreateClienteCommandValidator : AbstractValidator<CreateClienteCommand>
{
    private readonly PFPJDBContext _dbContext;

    public CreateClienteCommandValidator(PFPJDBContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(p => p.NomeRazaoSocial)
            .NotEmpty().WithMessage("O Nome/Razão Social é obrigatório.")
            .MaximumLength(200).WithMessage("O Nome/Razão Social não pode exceder 200 caracteres.");

        RuleFor(p => p.CpfCnpj)
            .NotEmpty().WithMessage("O CPF/CNPJ é obrigatório.")
            .MaximumLength(14).WithMessage("O CPF/CNPJ não pode exceder 14 caracteres.")
            .MustAsync(async (cpfCnpj, cancellationToken) =>
            {
                return !await _dbContext.Clientes.AnyAsync(c => c.CpfCnpj == cpfCnpj, cancellationToken);
            }).WithMessage("O CPF informado já está cadastrado.");

        RuleFor(p => p.DataNascimento)
            .NotEmpty().WithMessage("A Data de Nascimento é obrigatória.")
            .LessThan(DateTime.Now.AddYears(-18)).When(p => p.PessoaFisica)
            .WithMessage("A idade mínima para pessoa física é de 18 anos.");

        RuleFor(p => p.Telefone)
            .MaximumLength(20).WithMessage("O Telefone não pode exceder 20 caracteres.")
            .MinimumLength(4).WithMessage("teste menor que 3");

        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("O E-mail é obrigatório.")
            .EmailAddress().WithMessage("O E-mail fornecido não é válido.")
            .MaximumLength(100).WithMessage("O E-mail não pode exceder 100 caracteres.")
            .MustAsync(async (email, cancellationToken) =>
            {
                return !await _dbContext.Clientes.AnyAsync(c => c.Email == email, cancellationToken);
            }).WithMessage("O E-mail informado já está cadastrado.");

        RuleFor(p => p.Cep)
            .MaximumLength(10).WithMessage("O CEP não pode exceder 10 caracteres.");

        RuleFor(p => p.Endereco)
            .MaximumLength(200).WithMessage("O Endereço não pode exceder 200 caracteres.");

        RuleFor(p => p.Numero)
            .MaximumLength(10).WithMessage("O Número não pode exceder 10 caracteres.");

        RuleFor(p => p.Bairro)
            .MaximumLength(100).WithMessage("O Bairro não pode exceder 100 caracteres.");

        RuleFor(p => p.Cidade)
            .MaximumLength(100).WithMessage("A Cidade não pode exceder 100 caracteres.");

        RuleFor(p => p.Estado)
            .MaximumLength(2).WithMessage("O Estado deve ter 2 caracteres.");

        RuleFor(p => p.InscricaoEstadual)
            .NotEmpty().WithMessage("A Inscrição Estadual é obrigatória para Pessoa Jurídica.")
            .When(p => !p.PessoaFisica && string.IsNullOrWhiteSpace(p.InscricaoEstadual) && !string.Equals(p.InscricaoEstadual, "isento", StringComparison.OrdinalIgnoreCase))
            .WithMessage("A Inscrição Estadual é obrigatória para Pessoa Jurídica (a menos que informada como 'isento').")
            .MaximumLength(20).WithMessage("A Inscrição Estadual não pode exceder 20 caracteres.");
    }
}
