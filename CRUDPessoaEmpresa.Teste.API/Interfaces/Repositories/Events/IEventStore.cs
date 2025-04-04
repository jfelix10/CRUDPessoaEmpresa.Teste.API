using CRUDPessoaEmpresa.Teste.API.Domain.Entities;
using CRUDPessoaEmpresa.Teste.API.Domain.Events;

namespace CRUDPessoaEmpresa.Teste.API.Interfaces.Repositories.Events;

public interface IEventStore
{
    Task SaveAsync(IEnumerable<Event> events);
    Task<IEnumerable<EventEntity>> GetEventsAsync(string aggregateId);
}
