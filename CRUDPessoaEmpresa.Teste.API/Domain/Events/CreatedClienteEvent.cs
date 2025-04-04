namespace CRUDPessoaEmpresa.Teste.API.Domain.Events;

public class CreatedClienteEvent : Event
{
    public string? NomeRazaoSocial { get; set; }
    public string? CpfCnpj { get; set; }
    public string? Email { get; set; }
    public DateTime DataNascimento { get; set; }
}
