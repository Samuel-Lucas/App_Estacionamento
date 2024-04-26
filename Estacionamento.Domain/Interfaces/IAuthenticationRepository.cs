using System.Security.Claims;
using Estacionamento.Domain.ViewModels;

namespace Estacionamento.Domain.Interfaces;

public interface IAuthenticationRepository
{
    Task<IEnumerable<Claim>> AuthenticateAsync(LoginViewModel model);
    Task<string> GetAuthenticatedIdPessoa();
}