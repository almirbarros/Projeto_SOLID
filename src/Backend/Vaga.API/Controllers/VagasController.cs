using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Vaga.API.DTOs;
using Vaga.Infra.Interfaces;
using VagaClasse = Vaga.Domain.Entities.Vaga;

namespace Vaga.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VagasController : ControllerBase
{
    private readonly IVagaRepository _repository;
    private readonly IValidator<VagaClasse> _validator;

    public VagasController(IVagaRepository repository, IValidator<VagaClasse> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodas() => Ok(await _repository.ListarTodasAsync());

    [HttpPost]
    public async Task<IActionResult> CriarVaga([FromBody] VagaRequestDto dto)
    {
        try
        {
            // 1. Cria a entidade com o DTO
            var novaVaga = new VagaClasse(
                dto.Titulo,
                dto.Descricao,
                dto.TipoVaga,
                dto.EhVagaAfirmativa,
                dto.DataInicio,
                dto.DataFim
            );

            // 2. Valida com o FluentValidation
            var validationResult = await _validator.ValidateAsync(novaVaga);
            if (!validationResult.IsValid)
            {
                // Imprime no console se o FluentValidation barrou o salvamento
                Console.WriteLine($"[Validação Falhou]: {string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))}");
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            // 3. Salva no banco
            await _repository.AdicionarAsync(novaVaga);
            Console.WriteLine($"[Sucesso]: Vaga {novaVaga.Titulo} salva com ID {novaVaga.Id}");
            
            return CreatedAtAction(nameof(ObterTodas), new { id = novaVaga.Id }, novaVaga);
        }
        catch (Exception ex)
        {
            // Se estourar qualquer erro de infraestrutura ou conversão, aparecerá aqui no terminal
            Console.WriteLine($"[Erro Crítico no Post]: {ex.Message} -> {ex.InnerException?.Message}");
            return StatusCode(500, "Erro interno ao salvar a vaga.");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarVaga(Guid id, [FromBody] VagaRequestDto dto)
    {
        // 1. Busca a vaga original persistida no banco
        var vagaExiste = await _repository.ObterPorIdAsync(id);
        if (vagaExiste == null) return NotFound("Vaga não encontrada.");

        // 2. Atualiza o estado interno usando os métodos de negócio da entidade rica
        vagaExiste.AtualizarDados(dto.Titulo, dto.Descricao, dto.TipoVaga, dto.EhVagaAfirmativa);
        vagaExiste.AlterarPeriodo(dto.DataInicio, dto.DataFim);

        // 3. Valida se o novo estado da entidade continua respeitando as regras de negócio
        var validationResult = await _validator.ValidateAsync(vagaExiste);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

        await _repository.AtualizarAsync(vagaExiste);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoverVaga(Guid id)
    {
        var vaga = await _repository.ObterPorIdAsync(id);
        if (vaga == null) return NotFound("Vaga não encontrada.");

        await _repository.RemoverAsync(vaga);
        return NoContent();
    }
}
