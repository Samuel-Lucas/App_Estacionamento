namespace Estacionamento.Domain.Entities;

public class Veiculo
{
    public int IdVeiculo { get; set; }
    public string Marca { get; set; } = null!;
    public string Modelo { get; set; } = null!;
    public string Cor { get; set; } = null!;
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