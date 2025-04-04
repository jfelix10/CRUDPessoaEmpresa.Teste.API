namespace CRUDPessoaEmpresa.Teste.API.Domain.Entities;
public class EventEntity
{
    public Guid Id { get; set; }
    public string? AggregateId { get; set; } // Relaciona ao cliente (ou outro domínio)
    public string? EventType { get; set; } // Tipo do evento (ex.: CreatedCliente)
    public string? Data { get; set; } // Dados do evento (em formato JSON)
    public DateTime Timestamp { get; set; } = DateTime.UtcNow; // Momento do evento
}
