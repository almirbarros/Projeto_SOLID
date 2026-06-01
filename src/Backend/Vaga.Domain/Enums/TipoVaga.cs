using System.ComponentModel.DataAnnotations;

namespace Vaga.Domain.Enums;

public enum TipoVaga
{
    [Display(Name = "Remoto")]
    Remoto = 1,
    
    [Display(Name = "Híbrido")]
    Hibrido = 2,
    
    [Display(Name = "Presencial")]
    Presencial = 3
}
