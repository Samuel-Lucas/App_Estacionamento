using System.ComponentModel.DataAnnotations;

namespace Estacionamento.Domain.Entities;

public class Pessoa
{
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
    [StringLength(12)]
    public string Telefone { get; set; } = null!;
    public IEnumerable<Veiculo> Veiculos { get; set; }

    public Pessoa(string nome, string sobreNome, string email, string telefone)
    {
        IdPessoa = Guid.NewGuid().ToString();
        Nome = nome;
        SobreNome = sobreNome;
        Email = email;
        Telefone = telefone;
    }
}