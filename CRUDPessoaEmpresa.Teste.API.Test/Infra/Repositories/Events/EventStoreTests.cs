using CRUDPessoaEmpresa.Teste.API.Domain.Events;
using CRUDPessoaEmpresa.Teste.API.Infra.Context;
using CRUDPessoaEmpresa.Teste.API.Infra.Repositories.Events;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPessoaEmpresa.Teste.API.Test.Infra.Repositories.Events;

public class EFEventStoreTests
{
    private readonly PFPJDBContext _dbContext;
    private readonly EventStore _eventStore;

    public EFEventStoreTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<PFPJDBContext>()
            .UseInMemoryDatabase(databaseName: "EventDb")
            .Options;

        _dbContext = new PFPJDBContext(dbContextOptions);
        _eventStore = new EventStore(_dbContext);
    }

    [Fact]
    public async Task SaveAsync_SavesEventToDatabase()
    {
        // Arrange
        var eventList = new List<Event>
        {
            new CreatedClienteEvent
            {
                Id = Guid.NewGuid(),
                NomeRazaoSocial = "Razão Social",
                CpfCnpj = "12345678912",
                Email = "email@email.com",
                DataNascimento = DateTime.Now.AddYears(-18)
            }
        };

        // Act
        await _eventStore.SaveAsync(eventList);

        // Assert
        Assert.Equal(1, _dbContext.EventStore.Count());
        var storedEvent = _dbContext.EventStore.First();
        Assert.Equal("CreatedClienteEvent", storedEvent.EventType);
    }
}

