using System.ComponentModel.DataAnnotations;

namespace Estacionamento.Domain.Entities;

public class Pessoa
{
    [Key]
    public string IdPessoa { get; set; }

    [Required(ErrorMessage = "Informe o seu nome")]
    [StringLength(20)]
    public string Nome { get; set; } = null!;

    [Required(ErrorMessage = "Informe o seu sobrenome")]
    [StringLength(20)]
    public string SobreNome { get; set; } = null!;

    [Required(ErrorMessage = "Informe o seu email")]
    [StringLength(80)]
    [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de Email válido")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Informe a senha")]
    // [StringLength(12, MinimumLength = 6, ErrorMessage = "A Senha precisa ter entre 6 e 12 caracteres")]
    public string Senha { get; set; } = null!;

    [Required(ErrorMessage = "Informe a confirmação da senha")]
    // [StringLength(12, MinimumLength = 6, ErrorMessage = "A confirmação da senha precisa ter entre 6 e 12 caracteres")]
    // [Compare(nameof(Senha), ErrorMessage = "Senha informada na confirmação não coincide com o campo Senha")]
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