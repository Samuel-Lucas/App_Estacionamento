using Estacionamento.Domain.Entities;

namespace Estacionamento.Domain.Interfaces;

public interface IPessoaRepository
{
    Task<IEnumerable<Pessoa>> ObterPessoas();
    Task<Pessoa?> ObterPessoa(string id);
    Task<Pessoa> AdicionarPessoa(Pessoa pessoa);
    Task DeletarPessoa(Pessoa pessoa);
    Task AtualizarPessoa(Pessoa pessoa);
}