using System.ComponentModel.DataAnnotations;

namespace Estacionamento.Domain.Entities;

public class Veiculo
{
    public int IdVeiculo { get; set; }

    [Required(ErrorMessage = "Informe o nome da marca do carro")]
    public string Marca { get; set; } = null!;

    [Required(ErrorMessage = "Informe o modelo carro")]
    public string Modelo { get; set; } = null!;

    [Required(ErrorMessage = "Informe a cor do carro")]
    public string Cor { get; set; } = null!;

    [Required(ErrorMessage = "Informe a placa carro")]
    public string Placa { get; set; } = null!;
    public string IdPessoa { get; set; } = null!;
    public Pessoa Pessoa { get; set; } = null!;

    public Veiculo(string marca, string modelo, string cor, string placa, string idPessoa)
    {
        Marca = marca;
        Modelo = modelo;
        Cor = cor;
        Placa = placa;
        IdPessoa = idPessoa;
    }
}