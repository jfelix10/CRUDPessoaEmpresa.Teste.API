using CRUDPessoaEmpresa.Teste.API.Domain.Events;
using System.ComponentModel.DataAnnotations;

namespace CRUDPessoaEmpresa.Teste.API.Domain.Entities;

public class Cliente
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O Nome/Razão Social é obrigatório.")]
    [MaxLength(200)]
    public required string NomeRazaoSocial { get; set; }

    [Required(ErrorMessage = "O CPF/CNPJ é obrigatório.")]
    [MaxLength(14)]
    public required string CpfCnpj { get; set; }

    [Required(ErrorMessage = "O E-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O E-mail fornecido não é válido.")]
    [MaxLength(100)]
    public required string Email { get; set; }

    public DateTime DataNascimento { get; set; }
    public string? Telefone { get; set; }
    public string? Cep { get; set; }
    public string? Endereco { get; set; }
    public string? Numero { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public bool PessoaFisica { get; set; }
    public string? InscricaoEstadual { get; set; }

    // A lista deve ser de eventos de domínio
    private readonly List<Event> _changes = new List<Event>();

    public IEnumerable<Event> GetUncommittedChanges() => _changes;

    private Cliente() { }

    public Cliente(
        string nomeRazaoSocial,
        string cpfCnpj,
        DateTime dataNascimento,
        string email
    )
    {
        Apply(new CreatedClienteEvent
        {
            NomeRazaoSocial = nomeRazaoSocial,
            CpfCnpj = cpfCnpj,
            DataNascimento = dataNascimento,
            Email = email
        });
    }

    public Cliente(
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
    ) : this(nomeRazaoSocial, cpfCnpj, dataNascimento, email)
    {
        Apply(new UpdatedClienteEvent
        {
            NomeRazaoSocial = nomeRazaoSocial,
            CpfCnpj = cpfCnpj,
            DataNascimento = dataNascimento,
            Telefone = telefone,
            Email = email,
            Cep = cep,
            Endereco = endereco,
            Numero = numero,
            Bairro = bairro,
            Cidade = cidade,
            Estado = estado,
            PessoaFisica = pessoaFisica,
            InscricaoEstadual = inscricaoEstadual
        });
    }

    private void Apply(Event evt)
    {
        switch (evt)
        {
            case CreatedClienteEvent clienteCriado:
                NomeRazaoSocial = clienteCriado.NomeRazaoSocial;
                CpfCnpj = clienteCriado.CpfCnpj;
                Email = clienteCriado.Email;
                DataNascimento = clienteCriado.DataNascimento;
                break;

            case UpdatedClienteEvent clienteAtualizado:
                NomeRazaoSocial = clienteAtualizado.NomeRazaoSocial;
                CpfCnpj = clienteAtualizado.CpfCnpj;
                Email = clienteAtualizado.Email;
                DataNascimento = clienteAtualizado.DataNascimento;
                Telefone = clienteAtualizado.Telefone;
                Cep = clienteAtualizado.Cep;
                Endereco = clienteAtualizado.Endereco;
                Numero = clienteAtualizado.Numero;
                Bairro = clienteAtualizado.Bairro;
                Cidade = clienteAtualizado.Cidade;
                Estado = clienteAtualizado.Estado;
                PessoaFisica = clienteAtualizado.PessoaFisica;
                InscricaoEstadual = clienteAtualizado.InscricaoEstadual;
                break;
        }

        _changes.Add(evt); // Adiciona eventos de domínio
    }
}


