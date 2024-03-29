using Estacionamento.Domain.Entities;

namespace Estacionamento.Domain.Interfaces;

public interface IVeiculoRepository
{
    Task<IEnumerable<VeiculosResponse>> ObterVeiculos();
    Task<Veiculo?> ObterVeiculo(int idVeiculo);
    Task<Veiculo> AdicionarVeiculo(Veiculo veiculo);
    Task DeletarVeiculo(int idVeiculo);
    Task AtualizarVeiculo(Veiculo veiculo);
}