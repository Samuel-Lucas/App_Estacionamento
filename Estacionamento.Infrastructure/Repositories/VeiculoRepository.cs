using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Interfaces;
using Estacionamento.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Infrastructure.Repositories;

public class VeiculoRepository : IVeiculoRepository
{
    private readonly AppDbContext _context;

    public VeiculoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Veiculo> AdicionarVeiculo(Veiculo veiculo)
    {
        try
        {            
            await _context.Veiculos!.AddAsync(veiculo);
            await _context.SaveChangesAsync();

            return veiculo;
        } catch (Exception e)
        {
            throw new Exception($"Ocorreu um erro ao cadastrar o veiculo: {e.Message}");
        }
    }

    public async Task AtualizarVeiculo(Veiculo veiculo)
    {
        try
        {
            if (veiculo is not null)
            {
                _context.Entry(veiculo).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentNullException("Dados inválidos para alteração do veiculo...");
            }
        } catch (Exception e)
        {
            throw new Exception($"Ocorre um erro ao buscar veiculo de id {veiculo.IdVeiculo}: {e.Message}");
        }
    }

    public async Task DeletarVeiculo(int idVeiculo)
    {
        try 
        {
            var veiculo = await ObterVeiculo(idVeiculo);
            if (veiculo is not null)
            {
                _context.Veiculos!.Remove(veiculo);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Dados inválidos para exclusão do veiculo");
            }
        } catch (Exception e)
        {
            throw new Exception($"Ocorre um erro ao excluir veiculo de id = {idVeiculo}: {e.Message}");
        }
    }

    public async Task<Veiculo?> ObterVeiculo(int idVeiculo)
    {
        try
        {
            var veiculo = await _context.Veiculos!
                                        .Where(v => v.IdVeiculo == idVeiculo)
                                        .Include(p => p.Pessoa)
                                        .FirstOrDefaultAsync();

            return veiculo!;
        } catch (Exception e)
        {
            throw new Exception($"Ocorre um erro ao buscar veiculo de id {idVeiculo}: {e.Message}");
        }
    }

    public async Task<IEnumerable<VeiculosResponse>> ObterVeiculos()
    {
        try
        {
            var veiculos = await _context.Veiculos!
                                    .Include(p => p.Pessoa)
                                    .ToListAsync();

            var veiculosResponse = veiculos
                                   .Select(p => new VeiculosResponse
                                        (
                                            p.IdVeiculo,
                                            p.Marca,
                                            p.Modelo,
                                            p.Cor,
                                            p.Placa,
                                            p.Pessoa.Nome,
                                            p.Pessoa.SobreNome
                                        ));
          
            return veiculosResponse;
        } catch (Exception e)
        {
            throw new Exception($"Ocorre um erro ao buscar veiculos cadastrados: {e.Message}");
        }
    }
}