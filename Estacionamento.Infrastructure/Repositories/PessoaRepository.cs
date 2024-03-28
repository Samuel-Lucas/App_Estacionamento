using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Interfaces;
using Estacionamento.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Infrastructure.Repositories;

public class PessoaRepository : IPessoaRepository
{
    private readonly AppDbContext _context;

    public PessoaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Pessoa> AdicionarPessoa(Pessoa pessoa)
    {
        try
        {
            pessoa.IdPessoa = Guid.NewGuid().ToString();
            
            await _context.Pessoas!.AddAsync(pessoa);
            await _context.SaveChangesAsync();

            return pessoa;
        } catch (Exception e)
        {
            throw new Exception($"Ocorre um erro ao cadastrar a pessoa: {e.Message}");
        }
    }

    public async Task AtualizarPessoa(Pessoa pessoa)
    {
        try
        {
            if (pessoa is not null)
            {
                _context.Entry(pessoa).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentNullException("Dados inválidos para alteração...");
            }
        } catch (Exception e)
        {
            throw new Exception($"Ocorre um erro ao buscar pessoa de id {pessoa.IdPessoa}: {e.Message}");
        }
    }

    public async Task DeletarPessoa(string idPessoa)
    {
        var pessoa = await ObterPessoa(idPessoa);
        if (pessoa is not null)
        {
            _context.Pessoas!.Remove(pessoa);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException("Dados inválidos para exclusão");
        }
    }

    public async Task<Pessoa?> ObterPessoa(string id)
    {
        try
        {
            var pessoa = await _context.Pessoas!.FirstOrDefaultAsync(s => s.IdPessoa == id);

            return pessoa!;
        } catch (Exception e)
        {
            throw new Exception($"Ocorre um erro ao buscar pessoa de id {id}: {e.Message}");
        }
    }

    public async Task<IEnumerable<Pessoa>> ObterPessoas()
    {
        try
        {
            var pessoas = await _context.Pessoas!.ToListAsync();

            return pessoas;
        } catch (Exception e)
        {
            throw new Exception($"Ocorre um erro ao buscar pessoas cadastradas: {e.Message}");
        }
    }
}