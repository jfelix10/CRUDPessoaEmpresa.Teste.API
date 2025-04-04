using CRUDPessoaEmpresa.Teste.API.Domain.Entities;
using CRUDPessoaEmpresa.Teste.API.Domain.Events;
using CRUDPessoaEmpresa.Teste.API.Infra.Context;
using CRUDPessoaEmpresa.Teste.API.Interfaces.Repositories.Events;
using Microsoft.EntityFrameworkCore;

namespace CRUDPessoaEmpresa.Teste.API.Infra.Repositories.Events;

public class EventStore : IEventStore
{
    private readonly PFPJDBContext _dbContext;

    public EventStore(PFPJDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveAsync(IEnumerable<Event> events)
    {
        foreach (var evt in events)
        {
            // Converte de Event para EventEntity
            var eventEntity = new EventEntity
            {
                Id = evt.Id,
                AggregateId = evt.Id.ToString(),
                EventType = evt.GetType().Name,
                Data = System.Text.Json.JsonSerializer.Serialize(evt), // Serializa o evento para JSON
                Timestamp = evt.Timestamp
            };

            await _dbContext.EventStore.AddAsync(eventEntity);
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<EventEntity>> GetEventsAsync(string aggregateId)
    {
        return await _dbContext.EventStore
            .Where(e => e.AggregateId == aggregateId)
            .OrderBy(e => e.Timestamp)
            .ToListAsync();
    }
}
