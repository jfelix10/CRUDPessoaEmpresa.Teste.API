using CRUDPessoaEmpresa.Teste.API.Application.Clients.Commands;
using CRUDPessoaEmpresa.Teste.API.Domain.Events;
using CRUDPessoaEmpresa.Teste.API.Infra.Context;
using CRUDPessoaEmpresa.Teste.API.Interfaces.Repositories.Events;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CRUDPessoaEmpresa.Teste.API.Test.Application.Clients.Commands;

public class CreateClienteCommandHandlerTests
{
    private readonly Mock<IEventStore> _eventStoreMock;
    private readonly PFPJDBContext _dbContext;

    private readonly CreateClienteCommandHandler _handler;

    public CreateClienteCommandHandlerTests()
    {
        _eventStoreMock = new Mock<IEventStore>();

        var dbContextOptions = new DbContextOptionsBuilder<PFPJDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _dbContext = new PFPJDBContext(dbContextOptions);

        _handler = new CreateClienteCommandHandler(_eventStoreMock.Object, _dbContext);
    }

    [Fact]
    public async Task Handle_WithValidCommand_SavesClienteAndEvent()
    {
        // Arrange
        var command = new CreateClienteCommand(
            "Razão Social",
            "12345678912",
            DateTime.Now.AddYears(-18),
            "Telefone",
            "email@email.com",
            "12345-678",
            "Rua Exemplo",
            "123",
            "Bairro",
            "Cidade",
            "Estado",
            true,
            "isento"
        );

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        Assert.True(result);
        Assert.Equal(1, _dbContext.Clientes.Count());
        _eventStoreMock.Verify(e => e.SaveAsync(It.IsAny<IEnumerable<Event>>()), Times.Once);
    }
}

