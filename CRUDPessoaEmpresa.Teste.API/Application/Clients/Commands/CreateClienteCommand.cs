using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CRUDPessoaEmpresa.Teste.API.Application.Clients.Commands;

public class CreateClienteCommand : IRequest<bool>
{
    public string NomeRazaoSocial { get; set; } = string.Empty;
    public string CpfCnpj { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Cep { get; set; }
    public string Endereco { get; set; }
    public string Numero { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public bool PessoaFisica { get; set; }
    public string InscricaoEstadual { get; set; }

    public CreateClienteCommand(
        string nomeRazaoSocial, 
        string cpfCnpj, 
        DateTime dataNascimento, 
        string telefone, 
        string email, 
        string cep, 
        string endereco, 
        string numero, 
        string bairro,
        string cidade, 
        string estado, 
        bool pessoaFisica, 
        string inscricaoEstadual
    )
    {
        NomeRazaoSocial = nomeRazaoSocial;
        CpfCnpj = cpfCnpj;
        DataNascimento = dataNascimento;
        Telefone = telefone;
        Email = email;
        Cep = cep;
        Endereco = endereco;
        Numero = numero;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
        PessoaFisica = pessoaFisica;
        InscricaoEstadual = inscricaoEstadual;
    }
}
