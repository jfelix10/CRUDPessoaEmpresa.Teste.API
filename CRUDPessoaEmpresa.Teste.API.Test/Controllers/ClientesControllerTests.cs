
using CRUDPessoaEmpresa.Teste.API.Application.Clients.Commands;
using CRUDPessoaEmpresa.Teste.API.Controllers;
using CRUDPessoaEmpresa.Teste.API.Domain.Models;
using CRUDPessoaEmpresa.Teste.API.Test.Domain;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CRUDPessoaEmpresa.Teste.API.Test.Controllers;

public class ClientesControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IValidator<CreateClienteCommand>> _validatorMock;

    private readonly ClientesController _controller;

    public ClientesControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _validatorMock = new Mock<IValidator<CreateClienteCommand>>();

        _controller = new ClientesController(_mediatorMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task CreateCliente_WithValidRequest_ReturnsOkResult()
    {
        // Arrange
        var validCommand = new CreateClienteCommand(
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

        // Configuração do ValidationResult (simulando resultado válido)
        var validationResult = new ValidationResult();

        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<CreateClienteCommand>(), default))
                      .ReturnsAsync(validationResult);

        // Configuração do Mediator (simulando envio bem-sucedido)
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateClienteCommand>(), default))
                     .ReturnsAsync(true);

        var request = new CreateClienteRequest
        {
            NomeRazaoSocial = "Razão Social",
            CpfCnpj = "12345678912",
            DataNascimento = DateTime.Now.AddYears(-18),
            Telefone = "Telefone",
            Email = "email@email.com",
            Cep = "12345-678",
            Endereco = "Rua Exemplo",
            Numero = "123",
            Bairro = "Bairro",
            Cidade = "Cidade",
            Estado = "Estado",
            PessoaFisica = true,
            InscricaoEstadual = "isento"
        };

        // Act
        var result = await _controller.CreateCliente(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);

        var json = System.Text.Json.JsonSerializer.Serialize(okResult.Value);
        var responseObject = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(json);

        Assert.NotNull(responseObject);
        Assert.Equal("Cliente criado com sucesso!", responseObject["message"]);
    }

    [Fact]
    public async Task CreateCliente_WithInvalidRequest_ReturnsUnprocessableEntity()
    {
        // Arrange
        var invalidCommand = new CreateClienteCommand(
            "",
            "123",
            DateTime.Now.AddYears(-17),
            "",
            "invalid-email",
            "",
            "",
            "",
            "",
            "",
            "",
            true,
            ""
        );

        // Simulando falhas na validação
        var validationResult = new ValidationResult(
            new[] { new ValidationFailure("CpfCnpj", "CPF/CNPJ inválido.") }
        );

        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<CreateClienteCommand>(), default))
                      .ReturnsAsync(validationResult);

        var request = new CreateClienteRequest
        {
            NomeRazaoSocial = "",
            CpfCnpj = "123",
            DataNascimento = DateTime.Now.AddYears(-17),
            Telefone = "",
            Email = "invalid-email",
            Cep = "",
            Endereco = "",
            Numero = "",
            Bairro = "",
            Cidade = "",
            Estado = "",
            PessoaFisica = true,
            InscricaoEstadual = ""
        };

        // Act
        var result = await _controller.CreateCliente(request);

        // Assert
        var unprocessableResult = Assert.IsType<UnprocessableEntityObjectResult>(result);
        Assert.NotEmpty(((SerializableError)unprocessableResult.Value).Keys);
    }
}
