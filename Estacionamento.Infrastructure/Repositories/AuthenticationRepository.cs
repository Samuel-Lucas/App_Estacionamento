using System.Security.Claims;
using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Interfaces;
using Estacionamento.Domain.ViewModels;
using Estacionamento.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;

namespace Estacionamento.Infrastructure.Repositories;

public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthenticationRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor, AuthenticationStateProvider authenticationStateProvider)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<IEnumerable<Claim>> AuthenticateAsync(LoginViewModel model)
    {
        var userAccount = await _context.Pessoas!.Where(x => x.Email == model.Email).FirstOrDefaultAsync();

        if (userAccount is null || VerifyPassword(model.Senha!, userAccount.Senha)!)
        {
            return null!;
        }

        return new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userAccount.IdPessoa),
            new Claim(ClaimTypes.Name, model.Email!),
            new Claim(ClaimTypes.Role, userAccount.Role!)
        };
    }

    public async Task<string> GetAuthenticatedIdPessoa()
    {
        var pessoaLogadaId = await Task.FromResult(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        if (pessoaLogadaId is null)
            return null!;
        
        return pessoaLogadaId;
    }

    public async Task<bool> IsCurrentUserOwner(Pessoa pessoa)
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        return user.Identity!.IsAuthenticated && user.FindFirst(ClaimTypes.NameIdentifier)?.Value == pessoa.IdPessoa;
    }

    public async Task<bool> IsCurrentCarOwner(VeiculosResponse donoVeiculo)
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        return user.Identity!.IsAuthenticated && user.FindFirst(ClaimTypes.NameIdentifier)?.Value == donoVeiculo.IdPessoa;
    }

    private static bool VerifyPassword(string password, string hashedPassword)
        => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}