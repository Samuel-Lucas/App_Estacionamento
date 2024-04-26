using System.ComponentModel.DataAnnotations;

namespace Estacionamento.Domain.Entities;

public class Pessoa
{
    [Key]
    public string IdPessoa { get; set; }

    [Required(ErrorMessage = "Informe o nome da pessoa")]
    [StringLength(20)]
    public string Nome { get; set; } = null!;

    [Required(ErrorMessage = "Informe o sobrenome da pessoa")]
    [StringLength(20)]
    public string SobreNome { get; set; } = null!;

    [Required(ErrorMessage = "Informe o email da pessoa")]
    [StringLength(20)]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Informe a senha")]
    [MinLength(6)]
    public string Senha { get; set; } = null!;

    [Required(ErrorMessage = "Informe a confirmação da senha")]
    [MinLength(6)]
    public string ConfirmaSenha { get; set; } = null!;

    [StringLength(12)]
    public string Telefone { get; set; } = null!;

    [MaxLength(20)]
    public string? Role { get; set; }

    public IEnumerable<Veiculo> Veiculos { get; set; }

    public Pessoa(string nome, string sobreNome, string email, string senha, string confirmaSenha, string telefone, string role)
    {
        IdPessoa = Guid.NewGuid().ToString();
        Nome = nome;
        SobreNome = sobreNome;
        Email = email;
        Senha = senha == string.Empty ? string.Empty : HashPassword(senha);
        ConfirmaSenha = confirmaSenha == string.Empty ? string.Empty : HashPassword(confirmaSenha);
        Telefone = telefone;
        Role = role;
    }

    private static string HashPassword(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);

}