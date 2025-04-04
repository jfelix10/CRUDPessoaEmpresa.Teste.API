namespace CRUDPessoaEmpresa.Teste.API.Domain.Events;

public abstract class Event
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
