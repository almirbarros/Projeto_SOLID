using System;
using Vaga.Domain.Enums; 

namespace Vaga.Blazor.Models;

public class VagaFormModel
{
    public string Titulo { get; set; } = string.Empty;
    
    public string Descricao { get; set; } = string.Empty;
    
    public TipoVaga TipoVaga { get; set; } = TipoVaga.Remoto;
    
    public bool EhVagaAfirmativa { get; set; }
    
    public DateTime DataInicio { get; set; } = DateTime.Today;
    
    public DateTime DataFim { get; set; } = DateTime.Today.AddDays(30);
}
