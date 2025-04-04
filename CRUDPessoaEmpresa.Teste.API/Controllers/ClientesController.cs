using CRUDPessoaEmpresa.Teste.API.Application.Clients.Commands;
using CRUDPessoaEmpresa.Teste.API.Configurations.Extensions;
using CRUDPessoaEmpresa.Teste.API.Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CRUDPessoaEmpresa.Teste.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<CreateClienteCommand> _validator;

    public ClientesController(IMediator mediator, IValidator<CreateClienteCommand> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCliente([FromBody] CreateClienteRequest request)
    {
        var command = new CreateClienteCommand(
            request.NomeRazaoSocial,
            request.CpfCnpj,
            request.DataNascimento,
            request.Telefone,
            request.Email,
            request.Cep,
            request.Endereco!,
            request.Numero!,
            request.Bairro!,
            request.Cidade!,
            request.Estado!,
            request.PessoaFisica,
            request.InscricaoEstadual!
        );

        var cliente = await _validator.ValidateAsync(command);
        if (!cliente.IsValid)
        {
            cliente.AddToModelState(ModelState);
            return UnprocessableEntity(ModelState);
        }

        try
        {
            var success = await _mediator.Send(command);
            return Ok(new { message = "Cliente criado com sucesso!" });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}
