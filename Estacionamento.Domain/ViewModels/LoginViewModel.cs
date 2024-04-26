using System.ComponentModel.DataAnnotations;

namespace Estacionamento.Domain.ViewModels;

public class LoginViewModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor informe seu E-mail")]
    public string? Email { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor informe sua Senha")]
    public string? Senha { get; set; }
}