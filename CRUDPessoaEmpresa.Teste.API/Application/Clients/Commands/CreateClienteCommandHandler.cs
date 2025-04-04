using CRUDPessoaEmpresa.Teste.API.Domain.Entities;
using CRUDPessoaEmpresa.Teste.API.Infra.Context;
using CRUDPessoaEmpresa.Teste.API.Interfaces.Repositories.Events;
using MediatR;

namespace CRUDPessoaEmpresa.Teste.API.Application.Clients.Commands;

public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, bool>
{
    private readonly IEventStore _eventStore;
    private readonly PFPJDBContext _dbContext;

    public CreateClienteCommandHandler(IEventStore eventStore, PFPJDBContext dbContext)
    {
        _eventStore = eventStore;
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
    {

        var novoCliente = new Cliente (
            request.NomeRazaoSocial,
            request.CpfCnpj,
            request.DataNascimento,
            request.Email
        )
        {
            NomeRazaoSocial = request.NomeRazaoSocial,
            CpfCnpj = request.CpfCnpj,
            DataNascimento = request.DataNascimento,
            Telefone = request.Telefone,
            Email = request.Email,
            Cep = request.Cep,
            Endereco = request.Endereco,
            Numero = request.Numero,
            Bairro = request.Bairro,
            Cidade = request.Cidade,
            Estado = request.Estado,
            PessoaFisica = request.PessoaFisica,
            InscricaoEstadual = request.InscricaoEstadual
        };

        // Salva os eventos no EventEntity Store
        await _eventStore.SaveAsync(novoCliente.GetUncommittedChanges());

        _dbContext.Clientes.Add(novoCliente);
        var rowsAffected = await _dbContext.SaveChangesAsync(cancellationToken);

        return rowsAffected > 0;
    }
}
