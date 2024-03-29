namespace Estacionamento.Domain.Entities;

public record VeiculosResponse(int idVeiculo, string marca, string modelo, string cor, string placa, string nome, string sobreNome);