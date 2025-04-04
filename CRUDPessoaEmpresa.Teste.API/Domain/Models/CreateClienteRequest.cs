namespace CRUDPessoaEmpresa.Teste.API.Domain.Models;

public class CreateClienteRequest
{
    public required string NomeRazaoSocial { get; set; }
    public required string CpfCnpj { get; set; }
    public required DateTime DataNascimento { get; set; }
    public required string Telefone { get; set; }
    public required string Email { get; set; }
    public required string Cep { get; set; }
    public string? Endereco { get; set; }
    public string? Numero { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public bool PessoaFisica { get; set; }
    public string? InscricaoEstadual { get; set; }
}
