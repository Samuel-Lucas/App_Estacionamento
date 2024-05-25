using System.Security.Claims;
using Estacionamento.Domain.Entities;
using Estacionamento.Domain.ViewModels;

namespace Estacionamento.Domain.Interfaces;

public interface IAuthenticationRepository
{
    Task<IEnumerable<Claim>> AuthenticateAsync(LoginViewModel model);
    Task<string> GetAuthenticatedIdPessoa();
    Task<bool> IsCurrentUserOwner(Pessoa pessoa);
    Task<bool> IsCurrentCarOwner(VeiculosResponse donoVeiculo);
}