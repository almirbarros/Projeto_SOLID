using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> CriarVaga([FromBody] VagaClasse vaga)
    {
        var validationResult = await _validator.ValidateAsync(vaga);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

        await _repository.AdicionarAsync(vaga);
        return CreatedAtAction(nameof(ObterTodas), new { id = vaga.Id }, vaga);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarVaga(Guid id, [FromBody] VagaClasse vagaAtualizada)
    {
        if (id != vagaAtualizada.Id) return BadRequest("ID da vaga não corresponde.");

        var validationResult = await _validator.ValidateAsync(vagaAtualizada);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

        // Busca a vaga original persistida no banco
        var vagaExiste = await _repository.ObterPorIdAsync(id);
        if (vagaExiste == null) return NotFound("Vaga não encontrada.");

        vagaExiste.Titulo = vagaAtualizada.Titulo;
        vagaExiste.Descricao = vagaAtualizada.Descricao;
        vagaExiste.TipoVaga = vagaAtualizada.TipoVaga;
        vagaExiste.EhVagaAfirmativa = vagaAtualizada.EhVagaAfirmativa;
        vagaExiste.DataInicio = vagaAtualizada.DataInicio;
        vagaExiste.DataFim = vagaAtualizada.DataFim;

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
